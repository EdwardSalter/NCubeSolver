using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace Core.Plugins
{
    [InheritedExport(typeof(ICelebrator))]
    public interface ICelebrator : IPlugin
    {
        Task Celebrate();
    }
}
