using System.Threading.Tasks;
using NCubeSolver.Plugins.Solvers.Size5;
using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.IntegrationTests.Size5
{
    [TestFixture]
    [Category("FullSolve")]
    public class SingleTredgeSolverTests
    {
        [Test]
        public async Task Solve_GivenARandomConfiguration_ProducesASolvedTredge()
        {
            await TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, Solve).ConfigureAwait(false);
        }

        private static async Task Solve()
        {
            var configuration = ConfigurationGenerator.GenerateRandomConfiguration(5, 100);
            var solver = new SingleTredgeSolver();

            await solver.Solve(configuration).ConfigureAwait(false);

            try
            {
                var frontColour = configuration.Faces[FaceType.Front].LeftCentre();
                var leftColour = configuration.Faces[FaceType.Left].RightCentre();
                Assert.AreEqual(frontColour, configuration.Faces[FaceType.Front].GetEdge(Edge.Left)[1]);
                Assert.AreEqual(frontColour, configuration.Faces[FaceType.Front].GetEdge(Edge.Left)[3]);
                Assert.AreEqual(leftColour, configuration.Faces[FaceType.Left].GetEdge(Edge.Right)[1]);
                Assert.AreEqual(leftColour, configuration.Faces[FaceType.Left].GetEdge(Edge.Right)[3]);
            }
            catch
            {
                var f = configuration.Faces[FaceType.Front].Items;
                var b = configuration.Faces[FaceType.Back].Items;
                var l = configuration.Faces[FaceType.Left].Items;
                var r = configuration.Faces[FaceType.Right].Items;
                var u = configuration.Faces[FaceType.Upper].Items;
                var d = configuration.Faces[FaceType.Down].Items;
                throw;
            }
        }
    }
}
