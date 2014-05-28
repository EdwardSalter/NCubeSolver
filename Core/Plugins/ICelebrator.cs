using System.ComponentModel.Composition;
using System.Threading.Tasks;

namespace Core.Plugins
{
    [InheritedExport]
    public interface ICelebrator : IPlugin
    {
        Task Celebrate();
    }
}
