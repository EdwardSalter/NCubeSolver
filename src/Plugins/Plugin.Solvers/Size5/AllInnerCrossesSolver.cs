using System.Collections.Generic;
using System.Threading.Tasks;
using NCubeSolver.Plugins.Solvers.Common;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size5
{
    internal class AllInnerCrossesSolver : IPartialSolver
    {
        public async Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration)
        {
            var solution = new List<IRotation>();

            var stepsToSolveCross = await new SingleFaceCrossSolver(FaceColour.White).Solve(configuration);
            solution.AddRange(stepsToSolveCross);

            stepsToSolveCross = await new SingleFaceCrossSolver(FaceColour.Red).Solve(configuration);
            solution.AddRange(stepsToSolveCross);

            stepsToSolveCross = await new SingleFaceCrossSolver(FaceColour.Blue).Solve(configuration);
            solution.AddRange(stepsToSolveCross);

            stepsToSolveCross = await new SingleFaceCrossSolver(FaceColour.Orange).Solve(configuration);
            solution.AddRange(stepsToSolveCross);

            stepsToSolveCross = await new SingleFaceCrossSolver(FaceColour.Green).Solve(configuration);
            solution.AddRange(stepsToSolveCross);

            stepsToSolveCross = await new SingleFaceCrossSolver(FaceColour.Yellow).Solve(configuration);
            solution.AddRange(stepsToSolveCross);

            return solution;
        }
    }
}
