using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Core.UnitTests
{
    [TestFixture]
    public class UtilitiesTests
    {
        [Test]
        public void IsInnerEdgeComplete_OnAConfigurationOfSizeFiveGivenFrontEdgeAndUpperEdgeWhereBothContainTheSameValueForEach_ReturnsTrue()
        {
            var configuration = CreateConfiguration();
            configuration.Faces[FaceType.Front].SetEdge(Edge.Top, new[] { 1, 2, 2, 2, 1 });
            configuration.Faces[FaceType.Upper].SetEdge(Edge.Bottom, new[] { 1, 3, 3, 3, 1 });

            var isInnerEdgeComplete = Utilities.IsInnerEdgeComplete(FaceType.Front, FaceType.Upper, configuration);

            Assert.IsTrue(isInnerEdgeComplete);
        }

        [Test]
        public void IsInnerEdgeComplete_OnAConfigurationOfSizeFiveGivenFrontEdgeAndUpperEdgeWhereUpperEdgeDoesNotContainAllTheSmaeValues_ReturnsFalse()
        {
            var configuration = CreateConfiguration();
            configuration.Faces[FaceType.Front].SetEdge(Edge.Top, new[] { 1, 2, 2, 2, 1 });
            configuration.Faces[FaceType.Upper].SetEdge(Edge.Bottom, new[] { 1, 3, 4, 3, 1 });

            var isInnerEdgeComplete = Utilities.IsInnerEdgeComplete(FaceType.Front, FaceType.Upper, configuration);

            Assert.IsFalse(isInnerEdgeComplete);
        }

        [Test]
        public void IsInnerEdgeComplete_OnAConfigurationOfSizeFiveGivenLeftEdgeAndUpperEdgeWhereBothContainTheSameValueForEach_ReturnsTrue()
        {
            var configuration = CreateConfiguration();
            configuration.Faces[FaceType.Left].SetEdge(Edge.Top, new[] { 1, 2, 2, 2, 1 });
            configuration.Faces[FaceType.Upper].SetEdge(Edge.Left, new[] { 1, 3, 3, 3, 1 });

            var isInnerEdgeComplete = Utilities.IsInnerEdgeComplete(FaceType.Left, FaceType.Upper, configuration);

            Assert.IsTrue(isInnerEdgeComplete);
        }

        private static CubeConfiguration<int> CreateConfiguration()
        {
            var intArray = new [,]
            {
                {1, 2, 3, 4, 5},
                {1, 2, 3, 4, 5},
                {1, 2, 3, 4, 5},
                {1, 2, 3, 4, 5},
                {1, 2, 3, 4, 5},
            };

            var configuration = new CubeConfiguration<int>(
                intArray,
                intArray,
                intArray,
                intArray,
                intArray,
                intArray
            );

            return configuration;
        }
    }
}
