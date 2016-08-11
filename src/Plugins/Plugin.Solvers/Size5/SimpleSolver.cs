using System.Collections.Generic;
using System.Threading.Tasks;
using NCubeSolver.Plugins.Solvers.Common;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size5
{
    public class SimpleSolver : SolverBase
    {
        private CubeConfiguration<FaceColour> m_configuration;

        public override IEnumerable<int> ForCubeSizes
        {
            get { return new[] { 5 }; }
        }

        // TODO: MAYBE RETURN THINGS IN SECTIONS?
        public override async Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration)
        {
            await base.Solve(configuration).ConfigureAwait(false);

            m_configuration = configuration;

            var solution = new List<IRotation>();

            var cubeRotation = await CommonActions.PositionOnBottom(m_configuration, FaceColour.White).ConfigureAwait(false);

            if (cubeRotation != null)
                solution.Add(cubeRotation);

            var innerCrossesSolver = new AllInnerCrossesSolver();
            var stepsToSolveCrosses = await innerCrossesSolver.Solve(m_configuration).ConfigureAwait(false);

            solution.AddRange(stepsToSolveCrosses);

            var innerSquareSolver = new InnerSquareSolver(m_configuration.MinInnerLayerIndex(), m_configuration.MaxInnerLayerIndex());
            var stepsToSolveSquares = await innerSquareSolver.Solve(m_configuration).ConfigureAwait(false);

            solution.AddRange(stepsToSolveSquares);

            var tredgeSolver = new UpperAndDownFaceTredgesSolver();
            var stepsToSolveTredges = await tredgeSolver.Solve(m_configuration).ConfigureAwait(false);

            solution.AddRange(stepsToSolveTredges);

            var middleTredgeSolver = new MiddleLayerTredgeSolver();
            var stepsToSolveMiddleLayerTredges = await middleTredgeSolver.Solve(configuration).ConfigureAwait(false);

            solution.AddRange(stepsToSolveMiddleLayerTredges);

            // TODO: INJECT A 3x3x3 solver in here so different ones can be used
            var threeByThreeByThreeSolver = new Size3.BeginerMethod
            {
                SkipChecks = true
            };
            var stepsToSolveReduced3X3X3 = await threeByThreeByThreeSolver.Solve(configuration).ConfigureAwait(false);

            solution.AddRange(stepsToSolveReduced3X3X3);

            return solution.Condense();
        }

        public override string PluginName
        {
            get { return "Simple 5x5x5 Solver"; }
        }
    }
}
