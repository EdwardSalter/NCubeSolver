using System.Threading.Tasks;
using NCubeSolver.Core.UnitTestHelpers;
using NCubeSolver.Plugins.Solvers.Size3;
using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.IntegrationTests.Size3
{
    [TestFixture]
    [Category("FullSolve")]
    public class TopCrossSolverTests
    {
        [Test]
        public async Task Solve_GivenARandomConfiguration_ProducesASolvedCross()
        {
            await TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, async () =>
            {
                var configuration = CreateSolvedMiddleLayerConfiguration(50);
                var solver = new TopCrossSolver();

                await solver.Solve(configuration).ConfigureAwait(false);

                CubeConfigurationAssert.FaceCentreColourMatchesCentresOfLayerNumber(configuration, FaceType.Upper, FaceColour.Yellow);
            }).ConfigureAwait(false);
        }

        [Test]
        public async Task Solve_GivenARandomConfiguration_ProducesACorrectlyPermutedCross()
        {
            await TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, async () =>
            {
                var configuration = CreateSolvedMiddleLayerConfiguration(50);
                var solver = new TopCrossSolver();

                await solver.Solve(configuration).ConfigureAwait(false);

                CubeConfigurationAssert.TopLayerCrossIsCorrect(configuration);
            }).ConfigureAwait(false);
        }

        private static CubeConfiguration<FaceColour> CreateSolvedMiddleLayerConfiguration(int numberOfRotations)
        {
            var configuration = ConfigurationGenerator.GenerateRandomConfiguration(3, numberOfRotations);
            new BottomCrossSolver().Solve(configuration).Wait();
            new BottomLayerSolver().Solve(configuration).Wait();
            new MiddleLayerSolver().Solve(configuration).Wait();
            return configuration;
        }
    }
}
