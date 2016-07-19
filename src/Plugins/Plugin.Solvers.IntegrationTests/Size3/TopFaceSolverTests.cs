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
                var configuration = await CreateSolvedTopCrossConfiguration(50).ConfigureAwait(false);
                var solver = new TopFaceSolver();

                await solver.Solve(configuration).ConfigureAwait(false);

                CubeConfigurationAssert.FaceIsColour(configuration, FaceType.Upper, FaceColour.Yellow);
                CubeConfigurationAssert.TopLayerCrossIsCorrect(configuration);
            }).ConfigureAwait(false);
        }

        private static async Task<CubeConfiguration<FaceColour>> CreateSolvedTopCrossConfiguration(int numberOfRotations)
        {
            var configuration = ConfigurationGenerator.GenerateRandomConfiguration(3, numberOfRotations);
            await new BottomCrossSolver().Solve(configuration).ConfigureAwait(false);
            await new BottomLayerSolver().Solve(configuration).ConfigureAwait(false);
            await new MiddleLayerSolver().Solve(configuration).ConfigureAwait(false);
            await new TopCrossSolver().Solve(configuration).ConfigureAwait(false);
            return configuration;
        }
    }
}