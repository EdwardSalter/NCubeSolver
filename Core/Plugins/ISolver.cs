using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace Core.Plugins
{
    [InheritedExport(typeof(ISolver))]
    public interface ISolver : IPlugin
    {
        Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration);
    }
}
