using System.Collections.Generic;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size5
{
    internal class AllInnerCrossesSolver : IPartialSolver
    {
        public async Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration)
        {
            var solution = new List<IRotation>();

            var stepsToSolveCross = await new SingleInnerCrossSolver(FaceColour.White).Solve(configuration);
            solution.AddRange(stepsToSolveCross);

            stepsToSolveCross = await new SingleInnerCrossSolver(FaceColour.Red).Solve(configuration);
            solution.AddRange(stepsToSolveCross);

            stepsToSolveCross = await new SingleInnerCrossSolver(FaceColour.Blue).Solve(configuration);
            solution.AddRange(stepsToSolveCross);

            stepsToSolveCross = await new SingleInnerCrossSolver(FaceColour.Orange).Solve(configuration);
            solution.AddRange(stepsToSolveCross);

            stepsToSolveCross = await new SingleInnerCrossSolver(FaceColour.Green).Solve(configuration);
            solution.AddRange(stepsToSolveCross);

            stepsToSolveCross = await new SingleInnerCrossSolver(FaceColour.Yellow).Solve(configuration);
            solution.AddRange(stepsToSolveCross);

            return solution;
        }
    }
}
