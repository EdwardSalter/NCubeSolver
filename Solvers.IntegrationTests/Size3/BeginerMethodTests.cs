using System;
using System.Linq;
using Core;
using Core.UnitTestHelpers;
using NUnit.Framework;
using Solvers.Size3;

namespace Solvers.IntegrationTests.Size3
{
    [TestFixture]
    [Category("FullSolve")]
    public class BeginerMethodTests
    {
        [Test]
        public void Solve_GivenARandomConfiguration_ProducesSolvedCross()
        {
            Test(configurationToTest =>
                CubeConfigurationAssert.FaceHasCrossOfColour(configurationToTest, FaceType.Down, FaceColour.White));
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
                CubeConfigurationAssert.FaceHasCrossOfColour(configurationToTest, FaceType.Upper, FaceColour.Yellow));
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
            TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, async () =>
            {
                var rotations = ConfigurationGenerator.GenerateRandomRotations(50).ToList();
                var initialConfiguration = CubeConfiguration<FaceColour>.CreateStandardCubeConfiguration(3);
                CommonActions.ApplyRotations(rotations, initialConfiguration);
                var solver = new BeginerMethod();

                var solution = await solver.Solve(initialConfiguration);
                var configurationToTest = CubeConfiguration<FaceColour>.CreateStandardCubeConfiguration(3);
                CommonActions.ApplyRotations(rotations, initialConfiguration);
                CommonActions.ApplyRotations(solution, configurationToTest);

                assert.Invoke(configurationToTest);
            });
        }
    }
}
