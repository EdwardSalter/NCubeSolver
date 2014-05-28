using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Solvers.IntegrationTests
{
    static class TestRunner
    {
        public const int MultipleTimesToRun = 100;

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
                    timesFailed ++;
                }
            }

            var percent = ((double)timesToRun - timesFailed) / timesToRun;

            Debug.WriteLine("Ran test {0} times with a success rate of {1:0%} ({2} failures)", timesToRun, percent, timesFailed);
            Assert.AreEqual(1, percent);
        }
    }
}
