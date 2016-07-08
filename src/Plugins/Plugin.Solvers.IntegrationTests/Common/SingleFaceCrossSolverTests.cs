using System.Threading.Tasks;
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
        public async Task Solve_GivenARandomConfigurationOfSize5_ProducesASolvedCross(FaceColour crossColour)
        {
            await TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, () => Solve(crossColour, 5)).ConfigureAwait(false);
        }

        [TestCase(FaceColour.White)]
        [TestCase(FaceColour.Red)]
        [TestCase(FaceColour.Blue)]
        [TestCase(FaceColour.Green)]
        [TestCase(FaceColour.Orange)]
        [TestCase(FaceColour.Yellow)]
        public async Task Solve_GivenARandomConfigurationOfSize7_ProducesASolvedCrossInLayerNextToCentre(FaceColour crossColour)
        {
            await TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, () => Solve(crossColour, 7, 2)).ConfigureAwait(false);
        }

        [TestCase(FaceColour.White)]
        [TestCase(FaceColour.Red)]
        [TestCase(FaceColour.Blue)]
        [TestCase(FaceColour.Green)]
        [TestCase(FaceColour.Orange)]
        [TestCase(FaceColour.Yellow)]
        public async Task Solve_GivenARandomConfigurationOfSize7_ProducesASolvedCrossInLayerTwoFromCentre(FaceColour crossColour)
        {
            await TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, () => Solve(crossColour, 7, 1)).ConfigureAwait(false);
        }

        private static async Task Solve(FaceColour crossColour, int cubeSize, int? layerToCheck = null)
        {
            var configuration = ConfigurationGenerator.GenerateRandomConfiguration(cubeSize, 200);
            var solver = new SingleFaceCrossSolver(crossColour, layerToCheck);

            await solver.Solve(configuration).ConfigureAwait(false);

            CubeConfigurationAssert.FaceCentreColourMatchesCentresOfLayerNumber(configuration, FaceType.Front, crossColour, layerToCheck);
        }
    }
}
