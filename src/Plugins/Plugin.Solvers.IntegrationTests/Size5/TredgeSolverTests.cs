using NCubeSolver.Plugins.Solvers.Size5;
using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.IntegrationTests.Size5
{
    [TestFixture]
    [Category("FullSolve")]
    public class TredgeSolverTests
    {
        [Test]
        public void Solve_GivenARandomConfiguration_ProducesASolvedTredge()
        {
            TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, Solve);
        }

        private static void Solve()
        {
            var configuration = ConfigurationGenerator.GenerateRandomConfiguration(5, 100);
            var solver = new TredgeSolver();

            solver.Solve(configuration).Wait(TestRunner.Timeout);

            var frontColour = configuration.Faces[FaceType.Front].GetEdge(Edge.Left).Centre();
            var leftColour = configuration.Faces[FaceType.Left].GetEdge(Edge.Right).Centre();
            Assert.AreEqual(frontColour, configuration.Faces[FaceType.Front].GetEdge(Edge.Left)[1]);
            Assert.AreEqual(frontColour, configuration.Faces[FaceType.Front].GetEdge(Edge.Left)[3]);
            Assert.AreEqual(leftColour, configuration.Faces[FaceType.Left].GetEdge(Edge.Right)[1]);
            Assert.AreEqual(leftColour, configuration.Faces[FaceType.Left].GetEdge(Edge.Right)[3]);
        }
    }
}
