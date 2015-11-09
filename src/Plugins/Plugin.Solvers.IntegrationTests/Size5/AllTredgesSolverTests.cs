using NCubeSolver.Plugins.Solvers.Size5;
using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.IntegrationTests.Size5
{
    [TestFixture]
    [Category("FullSolve")]
    public class AllTredgesSolverTests
    {
        [Test]
        public void Solve_GivenARandomConfiguration_AllTredgesInTheTopAndBottomRowsAreSolved()
        {
            TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, Solve);
        }

        private static void Solve()
        {
            var configuration = ConfigurationGenerator.GenerateRandomConfiguration(5, 100);
            var solver = new AllTredgesSolver();

            solver.Solve(configuration).Wait(TestRunner.Timeout);

            Assert.IsTrue(Utilities.IsInnerEdgeComplete(FaceType.Front, FaceType.Upper, configuration));
            Assert.IsTrue(Utilities.IsInnerEdgeComplete(FaceType.Back, FaceType.Upper, configuration));
            Assert.IsTrue(Utilities.IsInnerEdgeComplete(FaceType.Left, FaceType.Upper, configuration));
            Assert.IsTrue(Utilities.IsInnerEdgeComplete(FaceType.Right, FaceType.Upper, configuration));
            Assert.IsTrue(Utilities.IsInnerEdgeComplete(FaceType.Front, FaceType.Down, configuration));
            Assert.IsTrue(Utilities.IsInnerEdgeComplete(FaceType.Back, FaceType.Down, configuration));
            Assert.IsTrue(Utilities.IsInnerEdgeComplete(FaceType.Left, FaceType.Down, configuration));
            Assert.IsTrue(Utilities.IsInnerEdgeComplete(FaceType.Right, FaceType.Down, configuration));
        }
    }
}
