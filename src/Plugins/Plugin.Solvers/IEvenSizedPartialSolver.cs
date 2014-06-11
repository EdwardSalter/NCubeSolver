using System.Collections.Generic;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers
{
    internal interface IEvenSizedPartialSolver
    {
        Task<IEnumerable<IRotation>> Solve(FaceColour bottomFaceColour, CubeConfiguration<FaceColour> configuration);
    }
}