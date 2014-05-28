using System.Collections.Generic;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers
{
    public interface IPartialSolver
    {
        Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration);
    }
}