using System;
using NCubeSolver.Core.UnitTestHelpers;
using NCubeSolver.Plugins.Solvers.Size7;
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
            TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, () =>
            {
                var configuration = ConfigurationGenerator.GenerateRandomConfiguration(7, 200);
                var solver = new SimpleSolver();

                solver.Solve(configuration).Wait(TestRunner.Timeout);

                assert.Invoke(configuration);
            });
        }
    }
}
