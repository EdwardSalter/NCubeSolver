using System.Threading.Tasks;

namespace NCubeSolvers.Core.Extensions
{
    public static class TaskEx
    {
        public static readonly Task Completed = Task.FromResult((object)null);
    }
}
