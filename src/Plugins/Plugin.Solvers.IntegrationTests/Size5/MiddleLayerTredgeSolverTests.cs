using System;
using System.Threading.Tasks;
using NCubeSolver.Plugins.Solvers.Size5;
using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.IntegrationTests.Size5
{
    [TestFixture]
    [Category("FullSolve")]
    public class MiddleLayerTredgeSolverTests
    {
        [Test]
        public async Task Solve_GivenARandomConfiguration_ProducesSolvedTredge()
        {
            await TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, () => Solve(AssertOneTredgeIsComplete)).ConfigureAwait(false);
        }

        [Test]
        public async Task Solve_GivenARandomConfiguration_ProducesTwoSolvedTredges()
        {
            await TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, () => Solve(AssertTwoTredgesAreComplete)).ConfigureAwait(false);
        }

        [Test]
        public async Task Solve_GivenARandomConfiguration_ProducesThreeSolvedTredges()
        {
            await TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, () => Solve(AssertThreeTredgesAreComplete)).ConfigureAwait(false);
        }

        [Test]
        public async Task Solve_GivenARandomConfiguration_ProducesFourSolvedTredges()
        {
            await TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, () => Solve(AssertAllTredgesAreComplete)).ConfigureAwait(false);
        }

        private static async Task Solve(Action<CubeConfiguration<FaceColour>> assertFunc)
        {
            var configuration = ConfigurationGenerator.GenerateRandomConfiguration(5, 100);
            new AllInnerCrossesSolver().Solve(configuration).Wait(TestRunner.Timeout);
            new InnerSquareSolver().Solve(configuration).Wait(TestRunner.Timeout);
            new UpperAndDownFaceTredgesSolver().Solve(configuration).Wait(TestRunner.Timeout);
            var solver = new MiddleLayerTredgeSolver();

            await solver.Solve(configuration).ConfigureAwait(false);

            assertFunc(configuration);
        }

        private static void AssertOneTredgeIsComplete(CubeConfiguration<FaceColour> configuration)
        {
            try
            {
                Assert.IsTrue(Utilities.IsInnerEdgeComplete(FaceType.Front, FaceType.Left, configuration));
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

        private static void AssertTwoTredgesAreComplete(CubeConfiguration<FaceColour> configuration)
        {
            try
            {
                Assert.IsTrue(Utilities.IsInnerEdgeComplete(FaceType.Front, FaceType.Left, configuration));
                Assert.IsTrue(Utilities.IsInnerEdgeComplete(FaceType.Back, FaceType.Left, configuration));
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

        private static void AssertThreeTredgesAreComplete(CubeConfiguration<FaceColour> configuration)
        {
            try
            {
                Assert.IsTrue(Utilities.IsInnerEdgeComplete(FaceType.Front, FaceType.Left, configuration));
                Assert.IsTrue(Utilities.IsInnerEdgeComplete(FaceType.Back, FaceType.Left, configuration));
                Assert.IsTrue(Utilities.IsInnerEdgeComplete(FaceType.Back, FaceType.Right, configuration));
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

        private static void AssertAllTredgesAreComplete(CubeConfiguration<FaceColour> configuration)
        {
            try
            {
                Assert.IsTrue(Utilities.IsInnerEdgeComplete(FaceType.Front, FaceType.Left, configuration));
                Assert.IsTrue(Utilities.IsInnerEdgeComplete(FaceType.Back, FaceType.Left, configuration));
                Assert.IsTrue(Utilities.IsInnerEdgeComplete(FaceType.Back, FaceType.Right, configuration));
                Assert.IsTrue(Utilities.IsInnerEdgeComplete(FaceType.Right, FaceType.Front, configuration));
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
