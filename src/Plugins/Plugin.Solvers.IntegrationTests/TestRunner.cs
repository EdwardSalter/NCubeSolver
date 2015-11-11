using System;
using System.Diagnostics;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.IntegrationTests
{
    internal static class TestRunner
    {
        public static readonly int MultipleTimesToRun = 100;
        public const int Timeout = 50;

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

        public static void RunTestMultipleTimes(int timesToRun, Action test)
        {
            int timesFailed = 0;
            for (int i = 0; i < timesToRun; i++)
            {
                try
                {
                    test();
                }
                catch
                {
                    //Debug.WriteLine("Test Failed");
                    timesFailed ++;
                }
                Debug.WriteLine("");
            }

            var percent = ((double)timesToRun - timesFailed) / timesToRun;

            Debug.WriteLine("Ran test {0} times with a success rate of {1:0%} ({2} failures)", timesToRun, percent, timesFailed);
            Assert.AreEqual(1, percent);
        }
    }
}
