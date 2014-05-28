using System.ComponentModel.Composition;

namespace Core.Plugins
{
    [InheritedExport]
    public interface ICubeConfigurationGenerator : IPlugin
    {
        CubeConfiguration<FaceColour> GenerateConfiguration(int size, int numberOfRotations);
    }
}
