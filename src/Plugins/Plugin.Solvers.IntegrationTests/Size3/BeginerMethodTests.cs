using System;
using System.Linq;
using System.Threading.Tasks;
using NCubeSolver.Core.UnitTestHelpers;
using NCubeSolver.Plugins.Solvers.Size3;
using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.IntegrationTests.Size3
{
    [TestFixture]
    [Category("FullSolve")]
    public class BeginerMethodTests
    {
        [Test]
        public async Task Solve_GivenARandomConfiguration_ProducesSolvedCross()
        {
            await Test(configurationToTest =>
                CubeConfigurationAssert.FaceCentreColourMatchesCentresOfLayerNumber(configurationToTest, FaceType.Down, FaceColour.White)).ConfigureAwait(false);
        }

        [Test]
        public async Task Solve_GivenARandomConfiguration_ProducesSolvedCorners()
        {
            await Test(CubeConfigurationAssert.BottomLayerCornersAreCorrect).ConfigureAwait(false);
        }

        [Test]
        public async Task Solve_GivenARandomConfiguration_ProducesSolvedBottomLayer()
        {
            await Test(configurationToTest =>
                CubeConfigurationAssert.FaceIsColour(configurationToTest, FaceType.Down, FaceColour.White)).ConfigureAwait(false);
        }

        [Test]
        public async Task Solve_GivenARandomConfiguration_ProducesSolvedMiddleLayer()
        {
            await Test(CubeConfigurationAssert.MiddleLayerIsCorrect).ConfigureAwait(false);
        }

        [Test]
        public async Task Solve_GivenARandomConfiguration_ProducesSolvedTopCross()
        {
            await Test(configurationToTest =>
                CubeConfigurationAssert.FaceCentreColourMatchesCentresOfLayerNumber(configurationToTest, FaceType.Upper, FaceColour.Yellow)).ConfigureAwait(false);
        }

        [Test]
        public async Task Solve_GivenARandomConfiguration_ProducesSolvedTopFace()
        {
            await Test(configurationToTest =>
                CubeConfigurationAssert.FaceIsColour(configurationToTest, FaceType.Upper, FaceColour.Yellow)).ConfigureAwait(false);
        }

        [Test]
        public async Task Solve_GivenARandomConfiguration_ProducesCorrectlyPermutedTopCross()
        {
            await Test(CubeConfigurationAssert.TopLayerCrossIsCorrect).ConfigureAwait(false);
        }

        [Test]
        public async Task Solve_GivenARandomConfiguration_ProducesACompleteCube()
        {
            await Test(CubeConfigurationAssert.CubeIsCorrect).ConfigureAwait(false);
        }

        private static async Task Test(Action<CubeConfiguration<FaceColour>> assert)
        {
            await TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, async () =>
            {
                var configuration = ConfigurationGenerator.GenerateRandomConfiguration(3, 50);
                var solver = new BeginerMethod();

                await solver.Solve(configuration).ConfigureAwait(false);

                assert.Invoke(configuration);
            }).ConfigureAwait(false);
        }
    }
}
