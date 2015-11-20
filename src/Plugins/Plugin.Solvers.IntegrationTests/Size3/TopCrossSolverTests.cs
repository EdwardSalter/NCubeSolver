using System;
using System.Collections.Generic;
using NCubeSolver.Core;
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
        public void Solve_GivenARandomConfiguration_ProducesASolvedCross()
        {
            TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, () =>
            {
                var configuration = CreateSolvedMiddleLayerConfiguration(50);
                var solver = new TopCrossSolver();

                solver.Solve(configuration).Wait();

                CubeConfigurationAssert.FaceCentreColourMatchesCentresOfLayerNumber(configuration, FaceType.Upper, FaceColour.Yellow);
            });
        }

        [Test]
        public void Solve_GivenARandomConfiguration_ProducesACorrectlyPermutedCross()
        {
            TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, () =>
            {
                var configuration = CreateSolvedMiddleLayerConfiguration(50);
                var solver = new TopCrossSolver();

                solver.Solve(configuration).Wait();

                CubeConfigurationAssert.TopLayerCrossIsCorrect(configuration);
            });
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
