using System;
using System.Collections.Generic;
using Core;
using NCubeSolver.Core;
using NCubeSolver.Core.UnitTestHelpers;
using NCubeSolver.Plugins.Solvers.Size3;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.IntegrationTests.Size3
{
    [TestFixture]
    [Category("FullSolve")]
    public class TopCrossSolverTests
    {
        [Test]
        public void FindStuff_Action_Expected()
        {
            CubeConfiguration<FaceColour> configuration;
            List<FaceRotation> rotations;
            do
            {
                rotations = new List<FaceRotation>();
                for (int i = 0; i < 2; i++)
                {
                    rotations.Add(Rotations.Random());
                }

                configuration = CubeConfiguration<FaceColour>.CreateStandardCubeConfiguration(3);
                CommonActions.ApplyRotations(rotations, configuration);
                new BottomCrossSolver().Solve(configuration).Wait();
                new BottomLayerSolver().Solve(configuration).Wait();
                new MiddleLayerSolver().Solve(configuration).Wait();
                new TopCrossSolver().SolveCross(configuration, new List<IRotation>()).Wait();
                CommonActions.ResetToDefaultPosition(configuration).Wait();
            } while (!((configuration.Faces[FaceType.Front].TopCentre() == FaceColour.Red &&
                     configuration.Faces[FaceType.Left].TopCentre() == FaceColour.Orange) &&
                     configuration.Faces[FaceType.Right].TopCentre() == FaceColour.Blue &&
                     configuration.Faces[FaceType.Back].TopCentre() == FaceColour.Green));

            Console.WriteLine(string.Join(" ", rotations));
        }



        [Test]
        public void Solve_GivenARandomConfiguration_ProducesASolvedCross()
        {
            TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, () =>
            {
                var configuration = CreateSolvedMiddleLayerConfiguration(50);
                var solver = new TopCrossSolver();

                solver.Solve(configuration).Wait();

                CubeConfigurationAssert.FaceHasCrossOfColour(configuration, FaceType.Upper, FaceColour.Yellow);
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
