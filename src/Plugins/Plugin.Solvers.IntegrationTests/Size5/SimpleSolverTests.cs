using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
                var rotations = ConfigurationGenerator.GenerateRandomRotations(100).ToList();
                var initialConfiguration = CubeConfiguration<FaceColour>.CreateStandardCubeConfiguration(5);
                CommonActions.ApplyRotations(rotations, initialConfiguration);
                var solver = new SimpleSolver();

                var solution = await solver.SolveAsync(initialConfiguration, CancellationToken.None).ConfigureAwait(true);

                var configurationToTest = CubeConfiguration<FaceColour>.CreateStandardCubeConfiguration(5);
                CommonActions.ApplyRotations(rotations, initialConfiguration);
                CommonActions.ApplyRotations(solution, configurationToTest);

                assert.Invoke(configurationToTest);
            });
        }
    }
}
