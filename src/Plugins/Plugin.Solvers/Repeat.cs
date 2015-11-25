using NCubeSolvers.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NCubeSolver.Plugins.Solvers
{
    internal static class Repeat
    {
        private const int DefaultMaxTries = 50;

        public static async Task SolvingUntilNoMovesCanBeMade(IEnumerable<IRotation> solution, Func<Task> action, int maxTries = DefaultMaxTries)
        {
            int prevSolutionSize = 0;
            int count = 0;
            do
            {
                prevSolutionSize = solution.Count();

                await action.Invoke().ConfigureAwait(false);


                count++;
            } while (prevSolutionSize != solution.Count() && count < maxTries);

            if (count >= maxTries)
            {
                throw new SolveFailureException(string.Format("Failed to solve after {0} tries", maxTries));
            }
        }
    }
}
