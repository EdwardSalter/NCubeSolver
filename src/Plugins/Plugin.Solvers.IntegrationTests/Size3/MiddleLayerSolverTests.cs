using System.Threading.Tasks;
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
        public async Task Solve_GivenARandomConfiguration_ProducesSolvedCorners()
        {
            await TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, Solve).ConfigureAwait(false);
        }

        private static async Task Solve()
        {
            var configuration = ConfigurationGenerator.GenerateRandomConfiguration(3, 50);
            new BottomCrossSolver().Solve(configuration).Wait();
            new BottomLayerSolver().Solve(configuration).Wait();
            var solver = new MiddleLayerSolver();

            await solver.Solve(configuration).ConfigureAwait(false);

            CubeConfigurationAssert.MiddleLayerIsCorrect(configuration);
        }
    }
}
