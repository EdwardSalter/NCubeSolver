using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size2
{
    internal class BottomLayerSolver : IEvenSizedPartialSolver
    {
        public async Task<IEnumerable<IRotation>> Solve(FaceColour bottomFaceColour, CubeConfiguration<FaceColour> configuration)
        {
            var solution = new List<IRotation>();

            return solution;
        }
    }
}