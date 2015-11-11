using System.Collections.Generic;
using System.Threading.Tasks;
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
            await base.Solve(configuration);
            m_configuration = configuration;

            var solution = new List<IRotation>();

            var cubeRotation = await CommonActions.PositionOnBottom(m_configuration, FaceColour.White);
            if (cubeRotation != null)
                solution.Add(cubeRotation);

            var innerCrossesSolver = new AllInnerCrossesSolver();
            var stepsToSolveCrosses = await innerCrossesSolver.Solve(m_configuration);
            solution.AddRange(stepsToSolveCrosses);

            var innerSquareSolver = new InnerSquareSolver();
            var stepsToSolveSquares = await innerSquareSolver.Solve(m_configuration);
            solution.AddRange(stepsToSolveSquares);

            var tredgeSolver = new AllTredgesSolver();
            var stepsToSolveTredges = await tredgeSolver.Solve(m_configuration);
            solution.AddRange(stepsToSolveTredges);

            return solution.Condense();
        }

        public override string PluginName
        {
            get { return "Simple 5x5x5 Solver"; }
        }
    }
}
