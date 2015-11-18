using NCubeSolver.Core.UnitTestHelpers;
using NCubeSolver.Plugins.Solvers.Common;
using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.IntegrationTests.Common
{
    [TestFixture]
    [Category("FullSolve")]
    public class SingleFaceCrossSolverTests
    {
        [TestCase(FaceColour.White)]
        [TestCase(FaceColour.Red)]
        [TestCase(FaceColour.Blue)]
        [TestCase(FaceColour.Green)]
        [TestCase(FaceColour.Orange)]
        [TestCase(FaceColour.Yellow)]
        public void Solve_GivenARandomConfigurationOfSize5_ProducesASolvedCross(FaceColour crossColour)
        {
            TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, () => Solve(crossColour, 5));
        }

        [TestCase(FaceColour.White)]
        [TestCase(FaceColour.Red)]
        [TestCase(FaceColour.Blue)]
        [TestCase(FaceColour.Green)]
        [TestCase(FaceColour.Orange)]
        [TestCase(FaceColour.Yellow)]
        public void Solve_GivenARandomConfigurationOfSize7_ProducesASolvedCrossInLayerNextToCentre(FaceColour crossColour)
        {
            TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, () => Solve(crossColour, 7, 2));
        }

        [TestCase(FaceColour.White)]
        [TestCase(FaceColour.Red)]
        [TestCase(FaceColour.Blue)]
        [TestCase(FaceColour.Green)]
        [TestCase(FaceColour.Orange)]
        [TestCase(FaceColour.Yellow)]
        public void Solve_GivenARandomConfigurationOfSize7_ProducesASolvedCrossInLayerTwoFromCentre(FaceColour crossColour)
        {
            TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, () => Solve(crossColour, 7, 1));
        }

        private static void Solve(FaceColour crossColour, int cubeSize, int? layerToCheck = null)
        {
            var configuration = ConfigurationGenerator.GenerateRandomConfiguration(cubeSize, 200);
            var solver = new SingleFaceCrossSolver(crossColour, layerToCheck);

            solver.Solve(configuration).Wait(
                //TestRunner.Timeout
                );

            CubeConfigurationAssert.FaceCentreColourMatchesCentresOfLayerNumber(configuration, FaceType.Front, crossColour, layerToCheck);
        }
    }
}
