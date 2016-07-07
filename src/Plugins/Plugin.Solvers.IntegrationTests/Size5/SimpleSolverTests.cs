using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCubeSolver.Core.UnitTestHelpers;
using NCubeSolver.Plugins.Solvers.Size5;
using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.IntegrationTests.Size5
{
    [TestFixture]
    [Category("FullSolve")]
    public class SimpleSolverTests
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
                var configuration = ConfigurationGenerator.GenerateRandomConfiguration(5, 500);
                var solver = new SimpleSolver();

                solver.Solve(configuration).Wait(TestRunner.Timeout);

                assert.Invoke(configuration);
            });
        }
    }
}
