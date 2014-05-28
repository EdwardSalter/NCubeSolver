using System.Collections.Generic;
using System.Threading.Tasks;
using Core;

namespace NCubeSolver.Plugins.Solvers
{
    public interface IPartialSolver
    {
        Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration);
    }
}