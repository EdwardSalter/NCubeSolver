﻿using Core;
using Core.UnitTestHelpers;
using NUnit.Framework;
using Solvers.Size3;

namespace Solvers.IntegrationTests.Size3
{
    [TestFixture]
    public class TopLayerSolverTests
    {
        [Test]
        public void Solve_GivenARandomConfiguration_ProducesSolvedCube()
        {
            TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, () =>
            {
                var configuration = CreateSolvedTopFaceConfiguration(50);
                var solver = new TopLayerSolver();

                solver.Solve(configuration).Wait();

                CubeConfigurationAssert.CubeIsCorrect(configuration);
            });
        }

        private static CubeConfiguration<FaceColour> CreateSolvedTopFaceConfiguration(int numberOfRotations)
        {
            var configuration = ConfigurationGenerator.GenerateRandomConfiguration(3, numberOfRotations);
            new BottomCrossSolver().Solve(configuration).Wait();
            new BottomLayerSolver().Solve(configuration).Wait();
            new MiddleLayerSolver().Solve(configuration).Wait();
            new TopCrossSolver().Solve(configuration).Wait();
            new TopFaceSolver().Solve(configuration).Wait();
            return configuration;
        }
    }
}