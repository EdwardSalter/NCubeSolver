using System.Threading.Tasks;
using NCubeSolver.Plugins.Solvers.Size7;
using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.IntegrationTests.Size7
{
    [TestFixture]
    public class Layer1EdgeSolverTests
    {
        [Test]
        public async Task Solve_Given_ARandomConfiguration_ProducesASolvedLayer1InnerEdgeOnTheTopFace()
        {
            await TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, async () =>
            {
                var configuration = ConfigurationGenerator.GenerateRandomConfiguration(7, 200);
                var solver = new Layer1EdgeSolver();

                await solver.Solve(configuration).ConfigureAwait(false);

                var upperFace = configuration.Faces[FaceType.Upper];
                Assert.AreEqual(upperFace.Centre, upperFace.GetEdge(2, Edge.Top));
                Assert.AreEqual(upperFace.Centre, upperFace.GetEdge(4, Edge.Top));
                Assert.AreEqual(upperFace.Centre, upperFace.GetEdge(2, Edge.Bottom));
                Assert.AreEqual(upperFace.Centre, upperFace.GetEdge(4, Edge.Bottom));
                Assert.AreEqual(upperFace.Centre, upperFace.GetEdge(2, Edge.Left));
                Assert.AreEqual(upperFace.Centre, upperFace.GetEdge(4, Edge.Left));
                Assert.AreEqual(upperFace.Centre, upperFace.GetEdge(2, Edge.Right));
                Assert.AreEqual(upperFace.Centre, upperFace.GetEdge(4, Edge.Right));
            }).ConfigureAwait(false);
        }
    }
}
