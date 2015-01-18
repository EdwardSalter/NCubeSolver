using NCubeSolver.Core.UnitTestHelpers;
using NCubeSolver.Plugins.Solvers.Size3;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.IntegrationTests.Size3
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
