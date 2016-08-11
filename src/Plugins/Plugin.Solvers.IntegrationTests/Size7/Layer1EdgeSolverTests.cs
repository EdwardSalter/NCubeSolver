using System.Threading.Tasks;
using NCubeSolver.Plugins.Solvers.Size7;
using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.IntegrationTests.Size7
{
    [TestFixture]
    public class Layer1EdgeSolverTests
    {

        private static void AssertFaceDoesNotContainColourInLayers2And4(FaceColour wantedColour, Face<FaceColour> face)
        {
            Assert.AreNotEqual(wantedColour, face.GetEdge(1, Edge.Top)[2]);
            Assert.AreNotEqual(wantedColour, face.GetEdge(1, Edge.Top)[4]);
            Assert.AreNotEqual(wantedColour, face.GetEdge(1, Edge.Bottom)[2]);
            Assert.AreNotEqual(wantedColour, face.GetEdge(1, Edge.Bottom)[4]);
            Assert.AreNotEqual(wantedColour, face.GetEdge(1, Edge.Left)[2]);
            Assert.AreNotEqual(wantedColour, face.GetEdge(1, Edge.Left)[4]);
            Assert.AreNotEqual(wantedColour, face.GetEdge(1, Edge.Right)[2]);
            Assert.AreNotEqual(wantedColour, face.GetEdge(1, Edge.Right)[4]);
        }

        [Test]
        public async Task Solve_Given_ARandomConfiguration_ProducesASolvedLayer1InnerEdgeOnTheTopFace()
        {
            await TestRunner.RunTestMultipleTimes(
                TestRunner.MultipleTimesToRun, 
                async () =>
            {
                var configuration = ConfigurationGenerator.GenerateRandomConfiguration(7, 200);
                configuration.Debug();
                var solver = new Layer1EdgeSolver();

                await solver.Solve(configuration).ConfigureAwait(false);
                configuration.Debug();

                var upperFace = configuration.Faces[FaceType.Upper];
                AssertFaceDoesNotContainColourInLayers2And4(upperFace.Centre, configuration.Faces[FaceType.Front]);
                AssertFaceDoesNotContainColourInLayers2And4(upperFace.Centre, configuration.Faces[FaceType.Left]);
                AssertFaceDoesNotContainColourInLayers2And4(upperFace.Centre, configuration.Faces[FaceType.Right]);
                AssertFaceDoesNotContainColourInLayers2And4(upperFace.Centre, configuration.Faces[FaceType.Back]);

                Assert.AreEqual(upperFace.Centre, upperFace.GetEdge(1, Edge.Top)[2]);
                Assert.AreEqual(upperFace.Centre, upperFace.GetEdge(1, Edge.Top)[4]);
                Assert.AreEqual(upperFace.Centre, upperFace.GetEdge(1, Edge.Bottom)[2]);
                Assert.AreEqual(upperFace.Centre, upperFace.GetEdge(1, Edge.Bottom)[4]);
                Assert.AreEqual(upperFace.Centre, upperFace.GetEdge(1, Edge.Left)[2]);
                Assert.AreEqual(upperFace.Centre, upperFace.GetEdge(1, Edge.Left)[4]);
                Assert.AreEqual(upperFace.Centre, upperFace.GetEdge(1, Edge.Right)[2]);
                Assert.AreEqual(upperFace.Centre, upperFace.GetEdge(1, Edge.Right)[4]);
            }).ConfigureAwait(false);
        }
    }
}
