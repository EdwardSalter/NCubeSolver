﻿using System;
using System.Threading.Tasks;
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
        public async Task Solve_GivenARandomConfiguration_ProducesSolvedLayer2Cross()
        {
            await Test(configurationToTest =>
                CubeConfigurationAssert.FaceCentreColourMatchesCentresOfLayerNumber(configurationToTest, FaceType.Down, FaceColour.White)).ConfigureAwait(false);
        }

        [Test]
        public async Task Solve_GivenARandomConfiguration_ProducesSolvedLayer1Cross()
        {
            await Test(configurationToTest =>
                CubeConfigurationAssert.FaceCentreColourMatchesCentresOfLayerNumber(configurationToTest, FaceType.Down, FaceColour.White, 1)).ConfigureAwait(false);
        }

        //[Test]
        //public async Task Solve_GivenARandomConfiguration_ProducesSolvedBottomLayer()
        //{
        //    await Test(configurationToTest =>
        //        CubeConfigurationAssert.FaceIsColour(configurationToTest, FaceType.Down, FaceColour.White)).ConfigureAwait(false);
        //}

        //[Test]
        //public async Task Solve_GivenARandomConfiguration_ProducesSolvedMiddleLayer()
        //{
        //    await Test(CubeConfigurationAssert.MiddleLayerIsCorrect).ConfigureAwait(false);
        //}

        //[Test]
        //public async Task Solve_GivenARandomConfiguration_ProducesSolvedTopCross()
        //{
        //    await Test(configurationToTest =>
        //        CubeConfigurationAssert.FaceCentreColourMatchesCentresOfLayerNumber(configurationToTest, FaceType.Upper, FaceColour.Yellow)).ConfigureAwait(false);
        //}

        //[Test]
        //public async Task Solve_GivenARandomConfiguration_ProducesSolvedTopFace()
        //{
        //    await Test(configurationToTest =>
        //        CubeConfigurationAssert.FaceIsColour(configurationToTest, FaceType.Upper, FaceColour.Yellow)).ConfigureAwait(false);
        //}

        //[Test]
        //public async Task Solve_GivenARandomConfiguration_ProducesCorrectlyPermutedTopCross()
        //{
        //    await Test(CubeConfigurationAssert.TopLayerCrossIsCorrect).ConfigureAwait(false);
        //}

        //[Test]
        //public async Task Solve_GivenARandomConfiguration_ProducesACompleteCube()
        //{
        //    await Test(CubeConfigurationAssert.CubeIsCorrect).ConfigureAwait(false);
        //}

        private static async Task Test(Action<CubeConfiguration<FaceColour>> assert)
        {
            await TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, async () =>
            {
                var configuration = ConfigurationGenerator.GenerateRandomConfiguration(7, 200);
                var solver = new SimpleSolver();

                await solver.Solve(configuration).ConfigureAwait(false);

                assert.Invoke(configuration);
            }).ConfigureAwait(false);
        }
    }
}