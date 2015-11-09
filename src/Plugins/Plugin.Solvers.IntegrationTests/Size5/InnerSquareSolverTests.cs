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
        public void Solve_GivenARandomConfiguration_ProducesSolvedInnerCorners()
        {
            TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, Solve);
        }

        private static void Solve()
        {
            var configuration = ConfigurationGenerator.GenerateRandomConfiguration(5, 100);
            var solver = new InnerSquareSolver();

            solver.Solve(configuration).Wait(TestRunner.Timeout);

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
