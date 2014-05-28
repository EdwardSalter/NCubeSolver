using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class TaskEx
    {
        public static readonly Task Completed = Task.FromResult((object)null);
    }
}
