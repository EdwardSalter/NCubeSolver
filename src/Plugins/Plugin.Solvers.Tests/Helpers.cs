using System.Collections.Generic;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.UnitTests
{
    class Helpers
    {
        public static CubeConfiguration<FaceColour> CreateConfiguration(IEnumerable<FaceRotation> moves, int cubeSize)
        {
            var configuration = CubeConfiguration<FaceColour>.CreateStandardCubeConfiguration(cubeSize);
            foreach (var rotation in moves)
            {
                configuration.Rotate(rotation).Wait();
            }
            return configuration;
        }
    }
}
