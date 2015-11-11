using System;
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
        public void Solve_GivenARandomConfiguration_ProducesSolvedTredge()
        {
            TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, () => Solve(AssertOneTredgeIsComplete));
        }

        [Test]
        public void Solve_GivenARandomConfiguration_ProducesTwoSolvedTredges()
        {
            TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, () => Solve(AssertTwoTredgesAreComplete));
        }

        [Test]
        public void Solve_GivenARandomConfiguration_ProducesThreeSolvedTredges()
        {
            TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, () => Solve(AssertThreeTredgesAreComplete));
        }

        [Test]
        public void Solve_GivenARandomConfiguration_ProducesFourSolvedTredges()
        {
            TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, () => Solve(AssertAllTredgesAreComplete));
        }

        private static void Solve(Action<CubeConfiguration<FaceColour>> assertFunc)
        {
            var configuration = ConfigurationGenerator.GenerateRandomConfiguration(5, 100);
            new AllInnerCrossesSolver().Solve(configuration).Wait(TestRunner.Timeout);
            new InnerSquareSolver().Solve(configuration).Wait(TestRunner.Timeout);
            new UpperAndDownFaceTredgesSolver().Solve(configuration).Wait(TestRunner.Timeout);
            var solver = new MiddleLayerTredgeSolver();

            solver.Solve(configuration).Wait(TestRunner.Timeout);

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
