using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading;
using System.Threading.Tasks;

namespace NCubeSolvers.Core.Plugins
{
    [InheritedExport]
    public interface ISolver : IPlugin
    {
        Task<IEnumerable<IRotation>> SolveAsync(CubeConfiguration<FaceColour> configuration, CancellationToken cancel);

        IEnumerable<int> ForCubeSizes { get; }
    }
}
