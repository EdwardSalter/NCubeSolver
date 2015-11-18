using NCubeSolver.Core.UnitTestHelpers;
using NCubeSolver.Plugins.Solvers.Size5;
using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.IntegrationTests.Size5
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
        public void Solve_GivenARandomConfiguration_ProducesASolvedCross(FaceColour crossColour)
        {
            TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, () => Solve(crossColour));
        }

        private static void Solve(FaceColour crossColour)
        {
            var configuration = ConfigurationGenerator.GenerateRandomConfiguration(5, 100);
            var solver = new SingleFaceCrossSolver(crossColour);

            solver.Solve(configuration).Wait(TestRunner.Timeout);

            CubeConfigurationAssert.FaceHasCrossOfColour(configuration, FaceType.Front, crossColour);
        }
    }
}
