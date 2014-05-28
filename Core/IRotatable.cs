using System.Threading.Tasks;

namespace Core
{
    public interface IRotatable
    {
        Task Rotate(FaceRotation rotation);
        Task RotateCube(CubeRotation rotation);
    }
}
