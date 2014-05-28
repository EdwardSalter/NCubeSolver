using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace Core.Plugins
{
    [InheritedExport(typeof(IDisplay))]
    public interface IDisplay : IRotatable, IPlugin
    {
        event EventHandler Closed;
        Task Initialise();
        Task SetCubeConfiguration(CubeConfiguration<FaceColour> configuration);
    }
}