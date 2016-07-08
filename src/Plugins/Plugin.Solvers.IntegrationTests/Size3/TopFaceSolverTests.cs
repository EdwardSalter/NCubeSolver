using System.Threading.Tasks;
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
        public async Task Solve_GivenARandomConfiguration_ProducesACorrectlySolvedTopFace()
        {
            await TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, async () =>
            {
                var configuration = CreateSolvedTopCrossConfiguration(50);
                var solver = new TopFaceSolver();

                await solver.Solve(configuration).ConfigureAwait(false);

                CubeConfigurationAssert.FaceIsColour(configuration, FaceType.Upper, FaceColour.Yellow);
                CubeConfigurationAssert.TopLayerCrossIsCorrect(configuration);
            }).ConfigureAwait(false);
        }

        private static CubeConfiguration<FaceColour> CreateSolvedTopCrossConfiguration(int numberOfRotations)
        {
            var configuration = ConfigurationGenerator.GenerateRandomConfiguration(3, numberOfRotations);
            new BottomCrossSolver().Solve(configuration).Wait(TestRunner.Timeout);
            new BottomLayerSolver().Solve(configuration).Wait(TestRunner.Timeout);
            new MiddleLayerSolver().Solve(configuration).Wait(TestRunner.Timeout);
            new TopCrossSolver().Solve(configuration).Wait(TestRunner.Timeout);
            return configuration;
        }
    }
}