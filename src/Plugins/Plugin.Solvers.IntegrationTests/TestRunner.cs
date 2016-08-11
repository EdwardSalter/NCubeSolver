using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.IntegrationTests
{
    internal static class TestRunner
    {
        public static readonly int MultipleTimesToRun = 100;
        private const int Timeout = 5000;

        static TestRunner()
        {
            var envVar = Environment.GetEnvironmentVariable("NCubeSolver_TestRunCount");
            if (string.IsNullOrEmpty(envVar)) return;

            int i;
            if (int.TryParse(envVar, out i))
            {
                MultipleTimesToRun = i;
                Debug.WriteLine("Found a test count override setting in environment variables. Mutliple test runs will be run {0} times.", MultipleTimesToRun);
            }
        }

        public static async Task RunTestMultipleTimes(int timesToRun, Func<Task> test)
        {
            var tasks = new List<Task>();
            Exception exception = null;
            for (int i = 0; i < timesToRun; i++)
            {
                tasks.Add(Task.Run(test));
            }

            try
            {
                var allTasks = Task.WhenAll(tasks);
                var timeoutTask = Task.Delay(Timeout);
                var completed = await Task.WhenAny(allTasks, timeoutTask).ConfigureAwait(false);
                if (completed == timeoutTask)
                {
                    throw new TimeoutException("Timed out running test");
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            var timesFailed = tasks.Count(t => t.IsFaulted);

            var percent = ((double)timesToRun - timesFailed) / timesToRun;

            Debug.WriteLine("Ran test {0} times with a success rate of {1:0.00%} ({2} failures)", timesToRun, percent, timesFailed);

            try
            {
                Assert.AreEqual(1, percent);
            }
            finally
            {
                var execptions = tasks.Where(t => t.Exception != null).SelectMany(t => t.Exception.InnerExceptions).ToList();
                if (execptions.Any())
                {
                    throw new AggregateException(execptions);
                }
            }
        }
    }
}
