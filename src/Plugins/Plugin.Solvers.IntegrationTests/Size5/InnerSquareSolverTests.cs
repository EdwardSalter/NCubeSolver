using System.Threading.Tasks;
using NCubeSolver.Plugins.Solvers.Size5;
using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.IntegrationTests.Size5
{
    [TestFixture]
    [Category("FullSolve")]
    public class InnerSquareSolverTests
    {
        [Test]
        public async Task Solve_GivenARandomConfiguration_ProducesSolvedInnerCorners()
        {
            await TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, Solve).ConfigureAwait(false);
        }

        private static async Task Solve()
        {
            var configuration = ConfigurationGenerator.GenerateRandomConfiguration(5, 100);
            var solver = new InnerSquareSolver();

            await solver.Solve(configuration).ConfigureAwait(false);

            AssertCornersAreCorrect(configuration, FaceType.Front);
            AssertCornersAreCorrect(configuration, FaceType.Back);
            AssertCornersAreCorrect(configuration, FaceType.Upper);
            AssertCornersAreCorrect(configuration, FaceType.Down);
            AssertCornersAreCorrect(configuration, FaceType.Left);
            AssertCornersAreCorrect(configuration, FaceType.Right);
        }

        private static void AssertCornersAreCorrect(CubeConfiguration<FaceColour> configuration, FaceType faceType)
        {
            var colour = configuration.Faces[faceType].Centre;
            var topEdge = configuration.Faces[faceType].GetEdge(1, Edge.Top);
            var bottomEdge = configuration.Faces[faceType].GetEdge(1, Edge.Bottom);
            Assert.AreEqual(colour, topEdge[1]);
            Assert.AreEqual(colour, topEdge[3]);
            Assert.AreEqual(colour, bottomEdge[1]);
            Assert.AreEqual(colour, bottomEdge[3]);
        }
    }
}
