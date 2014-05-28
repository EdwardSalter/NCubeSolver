using System.Collections.Generic;
using System.Threading.Tasks;
using Core;

namespace Solvers
{
    public interface IPartialSolver
    {
        Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration);
    }
}