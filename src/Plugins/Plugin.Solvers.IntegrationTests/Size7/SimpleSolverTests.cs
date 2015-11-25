using System;
using System.Linq;
using NCubeSolver.Core.UnitTestHelpers;
using NCubeSolver.Plugins.Solvers.Size5;
using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.IntegrationTests.Size7
{
    [TestFixture]
    [Category("FullSolve")]
    public class SimpleSolverTests
    {
        [Test]
        public void Solve_GivenARandomConfiguration_ProducesSolvedLayer2Cross()
        {
            Test(configurationToTest =>
                CubeConfigurationAssert.FaceCentreColourMatchesCentresOfLayerNumber(configurationToTest, FaceType.Down, FaceColour.White));
        }

        [Test]
        public void Solve_GivenARandomConfiguration_ProducesSolvedLayer1Cross()
        {
            Test(configurationToTest =>
                CubeConfigurationAssert.FaceCentreColourMatchesCentresOfLayerNumber(configurationToTest, FaceType.Down, FaceColour.White, 1));
        }

        //[Test]
        //public void Solve_GivenARandomConfiguration_ProducesSolvedBottomLayer()
        //{
        //    Test(configurationToTest =>
        //        CubeConfigurationAssert.FaceIsColour(configurationToTest, FaceType.Down, FaceColour.White));
        //}

        //[Test]
        //public void Solve_GivenARandomConfiguration_ProducesSolvedMiddleLayer()
        //{
        //    Test(CubeConfigurationAssert.MiddleLayerIsCorrect);
        //}

        //[Test]
        //public void Solve_GivenARandomConfiguration_ProducesSolvedTopCross()
        //{
        //    Test(configurationToTest =>
        //        CubeConfigurationAssert.FaceCentreColourMatchesCentresOfLayerNumber(configurationToTest, FaceType.Upper, FaceColour.Yellow));
        //}

        //[Test]
        //public void Solve_GivenARandomConfiguration_ProducesSolvedTopFace()
        //{
        //    Test(configurationToTest =>
        //        CubeConfigurationAssert.FaceIsColour(configurationToTest, FaceType.Upper, FaceColour.Yellow));
        //}

        //[Test]
        //public void Solve_GivenARandomConfiguration_ProducesCorrectlyPermutedTopCross()
        //{
        //    Test(CubeConfigurationAssert.TopLayerCrossIsCorrect);
        //}

        //[Test]
        //public void Solve_GivenARandomConfiguration_ProducesACompleteCube()
        //{
        //    Test(CubeConfigurationAssert.CubeIsCorrect);
        //}

        private static void Test(Action<CubeConfiguration<FaceColour>> assert)
        {
            TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, async () =>
            {
                var rotations = ConfigurationGenerator.GenerateRandomRotations(200).ToList();
                var initialConfiguration = CubeConfiguration<FaceColour>.CreateStandardCubeConfiguration(7);
                CommonActions.ApplyRotations(rotations, initialConfiguration);
                var solver = new SimpleSolver();

                var solution = await solver.Solve(initialConfiguration);
                var configurationToTest = CubeConfiguration<FaceColour>.CreateStandardCubeConfiguration(7);
                CommonActions.ApplyRotations(rotations, initialConfiguration);
                CommonActions.ApplyRotations(solution, configurationToTest);

                assert.Invoke(configurationToTest);
            });
        }
    }
}
