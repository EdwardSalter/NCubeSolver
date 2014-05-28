﻿using NCubeSolver.Core;
using NCubeSolver.Core.UnitTestHelpers;
using NCubeSolver.Plugins.Solvers.Size3;
using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.IntegrationTests.Size3
{
    [TestFixture]
    public class TopFaceSolverTests
    {
        [Test]
        public void Solve_GivenARandomConfiguration_ProducesACorrectlySolvedTopFace()
        {
            TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, () =>
            {
                var configuration = CreateSolvedTopCrossConfiguration(50);
                var solver = new TopFaceSolver();

                solver.Solve(configuration).Wait();

                CubeConfigurationAssert.FaceIsColour(configuration, FaceType.Upper, FaceColour.Yellow);
                CubeConfigurationAssert.TopLayerCrossIsCorrect(configuration);
            });
        }

        private static CubeConfiguration<FaceColour> CreateSolvedTopCrossConfiguration(int numberOfRotations)
        {
            var configuration = ConfigurationGenerator.GenerateRandomConfiguration(3, numberOfRotations);
            new BottomCrossSolver().Solve(configuration).Wait();
            new BottomLayerSolver().Solve(configuration).Wait();
            new MiddleLayerSolver().Solve(configuration).Wait();
            new TopCrossSolver().Solve(configuration).Wait();
            return configuration;
        }
    }
}