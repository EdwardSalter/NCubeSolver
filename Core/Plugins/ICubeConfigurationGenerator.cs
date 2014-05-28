using System.ComponentModel.Composition;

namespace Core.Plugins
{
    [InheritedExport(typeof(ICubeConfigurationGenerator))]
    public interface ICubeConfigurationGenerator : IPlugin
    {
        CubeConfiguration<FaceColour> GenerateConfiguration(int size, int numberOfRotations);
    }
}
