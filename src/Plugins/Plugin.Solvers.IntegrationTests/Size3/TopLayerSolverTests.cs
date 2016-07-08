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
                var configuration = CreateSolvedTopFaceConfiguration(50);
                var solver = new TopLayerSolver();

                await solver.Solve(configuration).ConfigureAwait(false);

                CubeConfigurationAssert.CubeIsCorrect(configuration);
            }).ConfigureAwait(false);
        }

        private static CubeConfiguration<FaceColour> CreateSolvedTopFaceConfiguration(int numberOfRotations)
        {
            var configuration = ConfigurationGenerator.GenerateRandomConfiguration(3, numberOfRotations);
            new BottomCrossSolver().Solve(configuration).Wait(TestRunner.Timeout);
            new BottomLayerSolver().Solve(configuration).Wait(TestRunner.Timeout);
            new MiddleLayerSolver().Solve(configuration).Wait(TestRunner.Timeout);
            new TopCrossSolver().Solve(configuration).Wait(TestRunner.Timeout);
            new TopFaceSolver().Solve(configuration).Wait(TestRunner.Timeout);
            return configuration;
        }
    }
}