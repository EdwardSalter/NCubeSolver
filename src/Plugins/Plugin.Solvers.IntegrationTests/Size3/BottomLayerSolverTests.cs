using System.Threading.Tasks;
using NCubeSolver.Core.UnitTestHelpers;
using NCubeSolver.Plugins.Solvers.Size3;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.IntegrationTests.Size3
{
    [TestFixture]
    [Category("FullSolve")]
    public class BottomLayerSolverTests
    {
        [Test]
        public async Task Solve_GivenARandomConfiguration_ProducesSolvedCorners()
        {
            await TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, Solve).ConfigureAwait(false);
        }

        private static async Task Solve()
        {
            var configuration = ConfigurationGenerator.GenerateRandomConfiguration(3, 50);
            var solver = new BottomLayerSolver();

            await solver.Solve(configuration).ConfigureAwait(false);

            CubeConfigurationAssert.BottomLayerCornersAreCorrect(configuration);
        }
    }
}
