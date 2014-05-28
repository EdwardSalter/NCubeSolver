using System.Collections.Generic;
using Core;
using NCubeSolver.Core;
using NCubeSolver.Plugins.Solvers.Size3;

namespace NCubeSolver.Plugins.Solvers.UnitTests.Size3
{
    public class Helpers
    {
        public static CubeConfiguration<FaceColour> CreateConfiguration(IEnumerable<FaceRotation> moves)
        {
            var configuration = CubeConfiguration<FaceColour>.CreateStandardCubeConfiguration(3);
            foreach (var rotation in moves)
            {
                configuration.Rotate(rotation).Wait();
            }
            return configuration;
        }

        public static CubeConfiguration<FaceColour> CreateSolvedMiddleLayerConfiguration(IEnumerable<FaceRotation> initialRotations)
        {
            var configuration = CreateConfiguration(initialRotations);
            new BottomCrossSolver().Solve(configuration).Wait();
            new BottomLayerSolver().Solve(configuration).Wait();
            new MiddleLayerSolver().Solve(configuration).Wait();
            return configuration;
        }
    }
}