using System.Threading.Tasks;
using NCubeSolver.Plugins.Solvers.Size7;
using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.IntegrationTests.Size7
{
    [TestFixture]
    public class AllLayer1EdgeSolverTests
    {
        [Test]
        public async Task Solve_Given_ARandomConfiguration_ProducesASolvedLayer1InnerEdgeOnTheTopFace()
        {
            await TestRunner.RunTestMultipleTimes(
                TestRunner.MultipleTimesToRun,
                async () =>
                {
                    var configuration = ConfigurationGenerator.GenerateRandomConfiguration(7, 200);
                    configuration.Debug();
                    var solver = new AllLayer1EdgeSolver();

                    await solver.Solve(configuration).ConfigureAwait(false);
                    configuration.Debug();

                    AssertFaceIsCorrect(configuration, FaceType.Upper);
                    AssertFaceIsCorrect(configuration, FaceType.Down);
                    AssertFaceIsCorrect(configuration, FaceType.Left);
                    AssertFaceIsCorrect(configuration, FaceType.Right);
                    AssertFaceIsCorrect(configuration, FaceType.Front);
                    AssertFaceIsCorrect(configuration, FaceType.Back);
                }).ConfigureAwait(false);
        }

        private static void AssertFaceIsCorrect(CubeConfiguration<FaceColour> configuration, FaceType faceType)
        {
            var face = configuration.Faces[faceType];

            Assert.AreEqual(face.Centre, face.GetEdge(1, Edge.Top)[2]);
            Assert.AreEqual(face.Centre, face.GetEdge(1, Edge.Top)[4]);
            Assert.AreEqual(face.Centre, face.GetEdge(1, Edge.Bottom)[2]);
            Assert.AreEqual(face.Centre, face.GetEdge(1, Edge.Bottom)[4]);
            Assert.AreEqual(face.Centre, face.GetEdge(1, Edge.Left)[2]);
            Assert.AreEqual(face.Centre, face.GetEdge(1, Edge.Left)[4]);
            Assert.AreEqual(face.Centre, face.GetEdge(1, Edge.Right)[2]);
            Assert.AreEqual(face.Centre, face.GetEdge(1, Edge.Right)[4]);
        }
    }
}
