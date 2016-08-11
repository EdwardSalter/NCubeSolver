using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NCubeSolver.Plugins.Solvers.Common;
using NCubeSolver.Plugins.Solvers.Size5;
using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.IntegrationTests.Size5
{
    [TestFixture]
    [Category("FullSolve")]
    public class UpperAndDownFaceTredgesSolverTests
    {
        [Test]
        public async Task Solve_GivenARandomConfiguration_AllTredgesInTheTopAndBottomRowsAreSolved()
        {
            await TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, () => Solve(AssertTredgesInUpperAndDownLayersAreSolved)).ConfigureAwait(false);
        }

        [Test]
        public async Task Solve_GivenARandomConfiguration_TheInnerSquareIsIntactForAllFaces()
        {
            await TestRunner.RunTestMultipleTimes(TestRunner.MultipleTimesToRun, () => Solve(AssertInnerSquareIsCorrectOnAllFaces, true)).ConfigureAwait(false);
        }

        private static async Task Solve(Action<CubeConfiguration<FaceColour>> assertFunc, bool runAllStepsUpToThis = false)
        {
            var configuration = ConfigurationGenerator.GenerateRandomConfiguration(5, 100);
            var solver = new UpperAndDownFaceTredgesSolver();

            try
            {
                if (runAllStepsUpToThis)
                {
                    await new AllInnerCrossesSolver().Solve(configuration).ConfigureAwait(false);
                    await new InnerSquareSolver(configuration.MinInnerLayerIndex(), configuration.MaxInnerLayerIndex()).Solve(configuration).ConfigureAwait(false);
                }

                await solver.Solve(configuration).ConfigureAwait(false);
                assertFunc(configuration);
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

        private static void AssertTredgesInUpperAndDownLayersAreSolved(CubeConfiguration<FaceColour> configuration)
        {
            Assert.IsTrue(Utilities.IsInnerEdgeComplete(FaceType.Front, FaceType.Upper, configuration));
            Assert.IsTrue(Utilities.IsInnerEdgeComplete(FaceType.Back, FaceType.Upper, configuration));
            Assert.IsTrue(Utilities.IsInnerEdgeComplete(FaceType.Left, FaceType.Upper, configuration));
            Assert.IsTrue(Utilities.IsInnerEdgeComplete(FaceType.Right, FaceType.Upper, configuration));
            Assert.IsTrue(Utilities.IsInnerEdgeComplete(FaceType.Front, FaceType.Down, configuration));
            Assert.IsTrue(Utilities.IsInnerEdgeComplete(FaceType.Back, FaceType.Down, configuration));
            Assert.IsTrue(Utilities.IsInnerEdgeComplete(FaceType.Left, FaceType.Down, configuration));
            Assert.IsTrue(Utilities.IsInnerEdgeComplete(FaceType.Right, FaceType.Down, configuration));
        }

        private static void AssertInnerSquareIsCorrectOnAllFaces(CubeConfiguration<FaceColour> configuration)
        {
            Assert.IsTrue(InnerSquareIsComplete(FaceType.Front, configuration));
            Assert.IsTrue(InnerSquareIsComplete(FaceType.Back, configuration));
            Assert.IsTrue(InnerSquareIsComplete(FaceType.Left, configuration));
            Assert.IsTrue(InnerSquareIsComplete(FaceType.Right, configuration));
            Assert.IsTrue(InnerSquareIsComplete(FaceType.Upper, configuration));
            Assert.IsTrue(InnerSquareIsComplete(FaceType.Down, configuration));
        }

        private static bool InnerSquareIsComplete(FaceType face, CubeConfiguration<FaceColour> configuration)
        {
            var allItems = GetInnerSquareForFace(configuration, FaceType.Front);
            return allItems.Distinct().Count() == 1;
        }

        private static IEnumerable<FaceColour> GetInnerSquareForFace(CubeConfiguration<FaceColour> configuration, FaceType faceType)
        {
            var upperEdge = configuration.Faces[faceType].GetEdge(1, Edge.Top).Skip(1).Take(3).ToArray();
            var middleEdge = configuration.Faces[faceType].GetEdge(2, Edge.Top).Skip(1).Take(3).ToArray();
            var lowerEdge = configuration.Faces[faceType].GetEdge(3, Edge.Top).Skip(1).Take(3).ToArray();

            var allItems = upperEdge.Concat(middleEdge).Concat(lowerEdge);
            return allItems;
        }
    }
}
