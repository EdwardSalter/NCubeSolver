using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace NCubeSolvers.Core.Plugins
{
    [InheritedExport]
    public interface IDisplay : IRotatable, IPlugin
    {
        event EventHandler Closed;
        Task Initialise();
        Task SetCubeConfiguration(CubeConfiguration<FaceColour> configuration, IEnumerable<IRotation> initialRotations);
    }
}