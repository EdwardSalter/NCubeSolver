using Core.UnitTestHelpers;
using NUnit.Framework;
using Solvers.Size3;

namespace Solvers.IntegrationTests.Size3
{
    [TestFixture]
    [Category("FullSolve")]
    public class MiddleLayerSolverTests
    {
        [Test]
        public void Solve_GivenARandomConfiguration_ProducesSolvedCorners()
        {
            TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, Solve);
        }

        private static void Solve()
        {
            var configuration = ConfigurationGenerator.GenerateRandomConfiguration(3, 50);
            new BottomCrossSolver().Solve(configuration).Wait();
            new BottomLayerSolver().Solve(configuration).Wait();
            var solver = new MiddleLayerSolver();

            solver.Solve(configuration).Wait();

            CubeConfigurationAssert.MiddleLayerIsCorrect(configuration);
        }
    }
}
