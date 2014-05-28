using Core;
using Core.UnitTestHelpers;
using NUnit.Framework;
using Solvers.Size3;

namespace Solvers.IntegrationTests.Size3
{
    [TestFixture]
    [Category("FullSolve")]
    public class BottomCrossSolverTests
    {
        [Test]
        public void Solve_GivenARandomConfiguration_ProducesASolvedCross()
        {
            TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, Solve);
        }

        private static void Solve()
        {
            var configuration = ConfigurationGenerator.GenerateRandomConfiguration(3, 50);
            var solver = new BottomCrossSolver();

            solver.Solve(configuration).Wait();

            CubeConfigurationAssert.FaceHasCrossOfColour(configuration, FaceType.Down, FaceColour.White);
        }
    }
}
