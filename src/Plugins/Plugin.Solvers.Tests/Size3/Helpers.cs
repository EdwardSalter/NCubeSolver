using System.Collections.Generic;
using NCubeSolver.Plugins.Solvers.Size3;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.UnitTests.Size3
{
    public class Helpers
    {
        public static CubeConfiguration<FaceColour> CreateSolvedMiddleLayerConfiguration(IEnumerable<FaceRotation> initialRotations)
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(initialRotations, 3);
            new BottomCrossSolver().Solve(configuration).Wait();
            new BottomLayerSolver().Solve(configuration).Wait();
            new MiddleLayerSolver().Solve(configuration).Wait();
            return configuration;
        }
    }
}