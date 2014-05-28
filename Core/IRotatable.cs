using System.Threading.Tasks;

namespace NCubeSolvers.Core
{
    public interface IRotatable
    {
        Task Rotate(FaceRotation rotation);
        Task RotateCube(CubeRotation rotation);
    }
}
