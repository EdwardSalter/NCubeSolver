using System.Threading.Tasks;
using NCubeSolver.Core.UnitTestHelpers;
using NCubeSolver.Plugins.Solvers.Size3;
using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.IntegrationTests.Size3
{
    [TestFixture]
    public class TopLayerSolverTests
    {
        [Test]
        public async Task Solve_GivenARandomConfiguration_ProducesSolvedCube()
        {
            await TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, async () =>
            {
                var configuration = await CreateSolvedTopFaceConfiguration(50).ConfigureAwait(false);
                var solver = new TopLayerSolver();

                await solver.Solve(configuration).ConfigureAwait(false);

                CubeConfigurationAssert.CubeIsCorrect(configuration);
            }).ConfigureAwait(false);
        }

        private static async Task<CubeConfiguration<FaceColour>> CreateSolvedTopFaceConfiguration(int numberOfRotations)
        {
            var configuration = ConfigurationGenerator.GenerateRandomConfiguration(3, numberOfRotations);
            await new BottomCrossSolver().Solve(configuration).ConfigureAwait(false);
            await new BottomLayerSolver().Solve(configuration).ConfigureAwait(false);
            await new MiddleLayerSolver().Solve(configuration).ConfigureAwait(false);
            await new TopCrossSolver().Solve(configuration).ConfigureAwait(false);
            await new TopFaceSolver().Solve(configuration).ConfigureAwait(false);
            return configuration;
        }
    }
}