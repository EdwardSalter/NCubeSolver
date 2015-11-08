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

            await SolveCrosses(solution);

            var innerSquareSolver = new InnerSquareSolver();
            var stepsToSolveSquares = await innerSquareSolver.Solve(m_configuration);
            solution.AddRange(stepsToSolveSquares);


            return solution.Condense();
        }

        private async Task SolveCrosses(List<IRotation> solution)
        {
            var stepsToSolveCross = await new InnerCrossSolver(FaceColour.White).Solve(m_configuration);
            solution.AddRange(stepsToSolveCross);

            stepsToSolveCross = await new InnerCrossSolver(FaceColour.Red).Solve(m_configuration);
            solution.AddRange(stepsToSolveCross);

            stepsToSolveCross = await new InnerCrossSolver(FaceColour.Blue).Solve(m_configuration);
            solution.AddRange(stepsToSolveCross);

            stepsToSolveCross = await new InnerCrossSolver(FaceColour.Orange).Solve(m_configuration);
            solution.AddRange(stepsToSolveCross);

            stepsToSolveCross = await new InnerCrossSolver(FaceColour.Green).Solve(m_configuration);
            solution.AddRange(stepsToSolveCross);

            stepsToSolveCross = await new InnerCrossSolver(FaceColour.Yellow).Solve(m_configuration);
            solution.AddRange(stepsToSolveCross);
        }

        public override string PluginName
        {
            get { return "Simple 5x5x5 Solver"; }
        }
    }
}
