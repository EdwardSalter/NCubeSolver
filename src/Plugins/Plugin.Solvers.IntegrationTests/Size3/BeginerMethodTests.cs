using System;
using System.Linq;
using NCubeSolver.Core;
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
        public void Solve_GivenARandomConfiguration_ProducesSolvedCross()
        {
            Test(configurationToTest =>
                CubeConfigurationAssert.FaceCentreColourMatchesCentresOfLayerNumber(configurationToTest, FaceType.Down, FaceColour.White));
        }

        [Test]
        public void Solve_GivenARandomConfiguration_ProducesSolvedCorners()
        {
            Test(CubeConfigurationAssert.BottomLayerCornersAreCorrect);
        }

        [Test]
        public void Solve_GivenARandomConfiguration_ProducesSolvedBottomLayer()
        {
            Test(configurationToTest =>
                CubeConfigurationAssert.FaceIsColour(configurationToTest, FaceType.Down, FaceColour.White));
        }

        [Test]
        public void Solve_GivenARandomConfiguration_ProducesSolvedMiddleLayer()
        {
            Test(CubeConfigurationAssert.MiddleLayerIsCorrect);
        }

        [Test]
        public void Solve_GivenARandomConfiguration_ProducesSolvedTopCross()
        {
            Test(configurationToTest =>
                CubeConfigurationAssert.FaceCentreColourMatchesCentresOfLayerNumber(configurationToTest, FaceType.Upper, FaceColour.Yellow));
        }

        [Test]
        public void Solve_GivenARandomConfiguration_ProducesSolvedTopFace()
        {
            Test(configurationToTest =>
                CubeConfigurationAssert.FaceIsColour(configurationToTest, FaceType.Upper, FaceColour.Yellow));
        }

        [Test]
        public void Solve_GivenARandomConfiguration_ProducesCorrectlyPermutedTopCross()
        {
            Test(CubeConfigurationAssert.TopLayerCrossIsCorrect);
        }

        [Test]
        public void Solve_GivenARandomConfiguration_ProducesACompleteCube()
        {
            Test(CubeConfigurationAssert.CubeIsCorrect);
        }

        private static void Test(Action<CubeConfiguration<FaceColour>> assert)
        {
            TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, () =>
            {
                var configuration = ConfigurationGenerator.GenerateRandomConfiguration(3, 50);
                var solver = new BeginerMethod();

                solver.Solve(configuration).Wait(TestRunner.Timeout);

                assert.Invoke(configuration);
            });
        }
    }
}
