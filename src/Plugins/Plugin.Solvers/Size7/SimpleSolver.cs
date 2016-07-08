using System.Collections.Generic;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size7
{
    public class SimpleSolver : SolverBase
    {
        private CubeConfiguration<FaceColour> m_configuration;

        public override IEnumerable<int> ForCubeSizes
        {
            get { return new[] { 7 }; }
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

            var innerCrossesSolver = new Size5.AllInnerCrossesSolver();
            var stepsToSolveCrosses = await innerCrossesSolver.Solve(m_configuration).ConfigureAwait(false);
            solution.AddRange(stepsToSolveCrosses);

            var innerSquareSolver = new Size5.InnerSquareSolver();
            var stepsToSolveSquares = await innerSquareSolver.Solve(m_configuration).ConfigureAwait(false);
            solution.AddRange(stepsToSolveSquares);

            var layer1CenterSolver = new AllLayer1CrossesSolver();
            var stepsToSolveLayer1Centers = await layer1CenterSolver.Solve(m_configuration).ConfigureAwait(false);
            solution.AddRange(stepsToSolveLayer1Centers);

            //// TODO: INJECT A 3x3x3 solver in here so different ones can be used
            //var threeByThreeByThreeSolver = new Size3.BeginerMethod
            //{
            //    SkipChecks = true
            //};
            //var stepsToSolveReduced3X3X3 = await threeByThreeByThreeSolver.Solve(configuration);
            //solution.AddRange(stepsToSolveReduced3X3X3);

            // TODO: REMOVE
            await CommonActions.ResetToDefaultPosition(configuration).ConfigureAwait(false);

            return solution.Condense();
        }

        public override string PluginName
        {
            get { return "Simple 7x7x7 Solver"; }
        }
    }
}
