using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace NCubeSolvers.Core.Plugins
{
    [InheritedExport]
    public interface ICubeConfigurationGenerator : IPlugin
    {
        CubeConfiguration<FaceColour> GenerateConfiguration(int size, int numberOfRotations);
        IEnumerable<IRotation> GenerateRandomRotationList(int moves);
    }
}
