using System;
using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Core.UnitTests
{
    [TestFixture]
    public class FaceTests
    {
        [Test]
        public void Constructor_GivenAnInitialConfiguration_SetsTheItemProperty()
        {
            var array = new[,] { { 1, 2 }, { 3, 4 } };

            var face = new Face<int>(null, array);

            CollectionAssert.AreEqual(array, face.Items);
        }

        #region Exceptions

        [Test]
        public void GetEdge_GivenANonEdgeValue_Throws()
        {
            var face = new Face<int>(null, 2);

            Assert.Throws<ArgumentException>(() => face.GetEdge((Edge)99));
        }

        [Test]
        public void SetEdge_GivenANonEdgeValue_Throws()
        {
            var face = new Face<int>(null, 2);

            Assert.Throws<ArgumentException>(() => face.SetEdge((Edge)99, new[] { 1, 2 }));
        }

        #endregion

        #region Right

        [Test]
        public void GetRight_GivenA2x2Array_ReturnsTheValuesOnTheRight()
        {
            var face = CreatedNumberedFace(2);
            var expected = new[] { 1, 3 };

            var actual = face.GetEdge(Edge.Right);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetRight_GivenA3x3Array_ReturnsTheValuesOnTheRight()
        {
            var face = CreatedNumberedFace(3);
            var expected = new[] { 2, 5, 8 };

            var actual = face.GetEdge(Edge.Right);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetRight_GivenA4x4Array_ReturnsTheValuesOnTheRight()
        {
            var face = CreatedNumberedFace(4);
            var expected = new[] { 3, 7, 11, 15 };

            var actual = face.GetEdge(Edge.Right);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetRight_GivenA2x2ArrayAndAnIndexFromValueOf1_ReturnsTheLeftMostColumnOfValues()
        {
            var face = CreatedNumberedFace(2);
            var expected = new[] { 0, 2 };

            var actual = face.GetEdge(1, Edge.Right);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetRight_GivenA2x2ArrayAndAnIndexFromValueOf2_ThrowsOutOfRangeException()
        {
            var face = CreatedNumberedFace(2);

            TestDelegate test = () => face.GetEdge(2, Edge.Right);

            Assert.Throws<ArgumentOutOfRangeException>(test);
        }

        [Test]
        public void GetRight_GivenA3x3ArrayAndAnIndexFromValueOf1_ReturnsTheMiddleColumnOfValues()
        {
            var face = CreatedNumberedFace(3);
            var expected = new[] { 1, 4, 7 };

            var actual = face.GetEdge(1, Edge.Right);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetRight_GivenA3x3ArrayAndAnIndexFromValueOf2_ReturnsTheLeftMostColumnOfValues()
        {
            var face = CreatedNumberedFace(3);
            var expected = new[] { 0, 3, 6 };

            var actual = face.GetEdge(2, Edge.Right);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetRight_GivenA4x4ArrayAndAnIndexFromValueOf1_ReturnsTheThirdColumnOfValues()
        {
            var face = CreatedNumberedFace(4);
            var expected = new[] { 2, 6, 10, 14 };

            var actual = face.GetEdge(1, Edge.Right);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetRight_GivenA4x4ArrayAndAnIndexFromValueOf2_ReturnsTheSecondColumnOfValues()
        {
            var face = CreatedNumberedFace(4);
            var expected = new[] { 1, 5, 9, 13 };

            var actual = face.GetEdge(2, Edge.Right);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void SetRight_GivenAnArrayThatIsTheWrongSize_Throws()
        {
            var face = CreatedNumberedFace(3);

            Assert.Throws<ArgumentException>(() => face.SetEdge(Edge.Right, new[] { 1, 2 }));
        }

        [Test]
        public void SetRight_UsedOnA2x2Array_SetsTheElementsOnTheRight()
        {
            var face = CreatedNumberedFace(2);
            var array = new[] { 1, 2 };

            face.SetEdge(Edge.Right, array);

            CollectionAssert.AreEqual(array, face.GetEdge(Edge.Right));
        }

        [Test]
        public void SetRight_UsedOnA3x3Array_SetsTheElementsOnTheRight()
        {
            var face = CreatedNumberedFace(3);
            var array = new[] { 1, 2, 3 };

            face.SetEdge(Edge.Right, array);

            CollectionAssert.AreEqual(array, face.GetEdge(Edge.Right));
        }

        [Test]
        public void SetRight_UsedOnA4x4Array_SetsTheElementsOnTheRight()
        {
            var face = CreatedNumberedFace(4);
            var array = new[] { 1, 2, 3, 4 };

            face.SetEdge(Edge.Right, array);

            CollectionAssert.AreEqual(array, face.GetEdge(Edge.Right));
        }

        [Test]
        public void SetRight_UsedOn2x2ArrayWithIndexFromEdgeValueOf1_SetsTheFirstColumnOfValues()
        {
            var face = CreatedNumberedFace(2);
            var array = new[] { 10, 11 };

            face.SetEdge(1, Edge.Right, array);

            CollectionAssert.AreEqual(array, face.GetEdge(1, Edge.Right));
        }

        [Test]
        public void SetRight_UsedOn2x2ArrayWithIndexFromEdgeValueOf2_ThrowsArugementOutOfRangeException()
        {
            var face = CreatedNumberedFace(2);
            var array = new[] { 100, 101 };

            TestDelegate test = () => face.SetEdge(2, Edge.Right, array);

            Assert.Throws<ArgumentOutOfRangeException>(test);
        }

        [Test]
        public void SetRight_UsedOn3x3ArrayWithIndexFromEdgeValueOf1_SetsTheSecondColumnOfValues()
        {
            var face = CreatedNumberedFace(3);
            var array = new[] { 100, 101, 102 };

            face.SetEdge(1, Edge.Right, array);

            CollectionAssert.AreEqual(array, face.GetEdge(1, Edge.Right));
        }

        [Test]
        public void SetRight_UsedOn3x3ArrayWithIndexFromEdgeValueOf2_SetsTheFirstColumnOfValues()
        {
            var face = CreatedNumberedFace(3);
            var array = new[] { 100, 101, 102 };

            face.SetEdge(2, Edge.Right, array);

            CollectionAssert.AreEqual(array, face.GetEdge(2, Edge.Right));
        }

        [Test]
        public void SetRight_UsedOn4x4ArrayWithIndexFromEdgeValueOf1_SetsTheThirdColumnOfValues()
        {
            var face = CreatedNumberedFace(4);
            var array = new[] { 100, 101, 102, 103 };

            face.SetEdge(1, Edge.Right, array);

            CollectionAssert.AreEqual(array, face.GetEdge(1, Edge.Right));
        }

        [Test]
        public void SetRight_UsedOn4x4ArrayWithIndexFromEdgeValueOf2_SetsTheSecondColumnOfValues()
        {
            var face = CreatedNumberedFace(4);
            var array = new[] { 100, 101, 102, 103 };

            face.SetEdge(2, Edge.Right, array);

            CollectionAssert.AreEqual(array, face.GetEdge(2, Edge.Right));
        }

        #endregion

        #region Left

        [Test]
        public void GetLeft_GivenA2x2Array_ReturnsTheValuesOnTheLeft()
        {
            var face = CreatedNumberedFace(2);
            var expected = new[] { 0, 2 };

            var actual = face.GetEdge(Edge.Left);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetLeft_GivenA3x3Array_ReturnsTheValuesOnTheLeft()
        {
            var face = CreatedNumberedFace(3);
            var expected = new[] { 0, 3, 6 };

            var actual = face.GetEdge(Edge.Left);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetLeft_GivenA4x4Array_ReturnsTheValuesOnTheLeft()
        {
            var face = CreatedNumberedFace(4);
            var expected = new[] { 0, 4, 8, 12 };

            var actual = face.GetEdge(Edge.Left);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetLeft_GivenA2x2ArrayAndAnIndexFromValueOf1_ReturnsTheRightMostColumnOfValues()
        {
            var face = CreatedNumberedFace(2);
            var expected = new[] { 1, 3 };

            var actual = face.GetEdge(1, Edge.Left);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetLeft_GivenA2x2ArrayAndAnIndexFromValueOf2_ThrowsOutOfRangeException()
        {
            var face = CreatedNumberedFace(2);

            TestDelegate test = () => face.GetEdge(2, Edge.Left);

            Assert.Throws<ArgumentOutOfRangeException>(test);
        }

        [Test]
        public void GetLeft_GivenA3x3ArrayAndAnIndexFromValueOf1_ReturnsTheMiddleColumnOfValues()
        {
            var face = CreatedNumberedFace(3);
            var expected = new[] { 1, 4, 7 };

            var actual = face.GetEdge(1, Edge.Left);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetLeft_GivenA3x3ArrayAndAnIndexFromValueOf2_ReturnsTheRightMostColumnOfValues()
        {
            var face = CreatedNumberedFace(3);
            var expected = new[] { 2, 5, 8 };

            var actual = face.GetEdge(2, Edge.Left);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetLeft_GivenA4x4ArrayAndAnIndexFromValueOf1_ReturnsTheSecondColumnOfValues()
        {
            var face = CreatedNumberedFace(4);
            var expected = new[] { 1, 5, 9, 13 };

            var actual = face.GetEdge(1, Edge.Left);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetLeft_GivenA4x4ArrayAndAnIndexFromValueOf2_ReturnsTheThirdColumnOfValues()
        {
            var face = CreatedNumberedFace(4);
            var expected = new[] { 2, 6, 10, 14 };

            var actual = face.GetEdge(2, Edge.Left);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void SetLeft_GivenAnArrayThatIsTheWrongSize_Throws()
        {
            var face = CreatedNumberedFace(3);

            Assert.Throws<ArgumentException>(() => face.SetEdge(Edge.Left, new[] { 1, 2 }));
        }

        [Test]
        public void SetLeft_UsedOnA2x2Array_SetsTheElementsOnTheLeft()
        {
            var face = CreatedNumberedFace(2);
            var array = new[] { 1, 2 };

            face.SetEdge(Edge.Left, array);

            CollectionAssert.AreEqual(array, face.GetEdge(Edge.Left));
        }

        [Test]
        public void SetLeft_UsedOnA3x3Array_SetsTheElementsOnTheLeft()
        {
            var face = CreatedNumberedFace(3);
            var array = new[] { 1, 2, 3 };

            face.SetEdge(Edge.Left, array);

            CollectionAssert.AreEqual(array, face.GetEdge(Edge.Left));
        }

        [Test]
        public void SetLeft_UsedOnA4x4Array_SetsTheElementsOnTheLeft()
        {
            var face = CreatedNumberedFace(4);
            var array = new[] { 1, 2, 3, 4 };

            face.SetEdge(Edge.Left, array);

            CollectionAssert.AreEqual(array, face.GetEdge(Edge.Left));
        }

        [Test]
        public void SetLeft_UsedOn2x2ArrayWithIndexFromEdgeValueOf1_SetsTheSecondColumnOfValues()
        {
            var face = CreatedNumberedFace(2);
            var array = new[] { 10, 11 };

            face.SetEdge(1, Edge.Left, array);

            CollectionAssert.AreEqual(array, face.GetEdge(1, Edge.Left));
        }

        [Test]
        public void SetLeft_UsedOn2x2ArrayWithIndexFromEdgeValueOf2_ThrowsArugementOutOfRangeException()
        {
            var face = CreatedNumberedFace(2);
            var array = new[] { 100, 101 };

            TestDelegate test = () => face.SetEdge(2, Edge.Left, array);

            Assert.Throws<ArgumentOutOfRangeException>(test);
        }

        [Test]
        public void SetLeft_UsedOn3x3ArrayWithIndexFromEdgeValueOf1_SetsTheSecondColumnOfValues()
        {
            var face = CreatedNumberedFace(3);
            var array = new[] { 100, 101, 102 };

            face.SetEdge(1, Edge.Left, array);

            CollectionAssert.AreEqual(array, face.GetEdge(1, Edge.Left));
        }

        [Test]
        public void SetLeft_UsedOn3x3ArrayWithIndexFromEdgeValueOf2_SetsTheThirdColumnOfValues()
        {
            var face = CreatedNumberedFace(3);
            var array = new[] { 100, 101, 102 };

            face.SetEdge(2, Edge.Left, array);

            CollectionAssert.AreEqual(array, face.GetEdge(2, Edge.Left));
        }

        [Test]
        public void SetLeft_UsedOn4x4ArrayWithIndexFromEdgeValueOf1_SetsTheSecondColumnOfValues()
        {
            var face = CreatedNumberedFace(4);
            var array = new[] { 100, 101, 102, 103 };

            face.SetEdge(1, Edge.Left, array);

            CollectionAssert.AreEqual(array, face.GetEdge(1, Edge.Left));
        }

        [Test]
        public void SetLeft_UsedOn4x4ArrayWithIndexFromEdgeValueOf2_SetsTheThirdColumnOfValues()
        {
            var face = CreatedNumberedFace(4);
            var array = new[] { 100, 101, 102, 103 };

            face.SetEdge(2, Edge.Left, array);

            CollectionAssert.AreEqual(array, face.GetEdge(2, Edge.Left));
        }

        #endregion

        #region Top

        [Test]
        public void GetTop_GivenA2x2Array_ReturnsTheValuesOnTheTop()
        {
            var face = CreatedNumberedFace(2);
            var expected = new[] { 0, 1 };

            var actual = face.GetEdge(Edge.Top);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetTop_GivenA3x3Array_ReturnsTheValuesOnTheTop()
        {
            var face = CreatedNumberedFace(3);
            var expected = new[] { 0, 1, 2 };

            var actual = face.GetEdge(Edge.Top);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetTop_GivenA4x4Array_ReturnsTheValuesOnTheTop()
        {
            var face = CreatedNumberedFace(4);
            var expected = new[] { 0, 1, 2, 3 };

            var actual = face.GetEdge(Edge.Top);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetTop_GivenA2x2ArrayAndAnIndexFromValueOf1_ReturnsTheBottomRowOfValues()
        {
            var face = CreatedNumberedFace(2);
            var expected = new[] { 2, 3 };

            var actual = face.GetEdge(1, Edge.Top);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetTop_GivenA2x2ArrayAndAnIndexFromValueOf2_ThrowsOutOfRangeException()
        {
            var face = CreatedNumberedFace(2);

            TestDelegate test = () => face.GetEdge(2, Edge.Top);

            Assert.Throws<ArgumentOutOfRangeException>(test);
        }

        [Test]
        public void GetTop_GivenA3x3ArrayAndAnIndexFromValueOf1_ReturnsTheMiddleRowOfValues()
        {
            var face = CreatedNumberedFace(3);
            var expected = new[] { 3, 4, 5 };

            var actual = face.GetEdge(1, Edge.Top);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetTop_GivenA3x3ArrayAndAnIndexFromValueOf2_ReturnsTheBottomRowOfValues()
        {
            var face = CreatedNumberedFace(3);
            var expected = new[] { 6, 7, 8 };

            var actual = face.GetEdge(2, Edge.Top);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetTop_GivenA4x4ArrayAndAnIndexFromValueOf1_ReturnsTheSecondRowOfValues()
        {
            var face = CreatedNumberedFace(4);
            var expected = new[] { 4, 5, 6, 7 };

            var actual = face.GetEdge(1, Edge.Top);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetTop_GivenA4x4ArrayAndAnIndexFromValueOf2_ReturnsTheThirdRowOfValues()
        {
            var face = CreatedNumberedFace(4);
            var expected = new[] { 8, 9, 10, 11 };

            var actual = face.GetEdge(2, Edge.Top);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void SetTop_GivenAnArrayThatIsTheWrongSize_Throws()
        {
            var face = CreatedNumberedFace(3);

            Assert.Throws<ArgumentException>(() => face.SetEdge(Edge.Top, new[] { 1, 2 }));
        }

        [Test]
        public void SetTop_UsedOnA2x2Array_SetsTheElementsOnTheTop()
        {
            var face = CreatedNumberedFace(2);
            var array = new[] { 1, 2 };

            face.SetEdge(Edge.Top, array);

            CollectionAssert.AreEqual(array, face.GetEdge(Edge.Top));
        }

        [Test]
        public void SetTop_UsedOnA3x3Array_SetsTheElementsOnTheTop()
        {
            var face = CreatedNumberedFace(3);
            var array = new[] { 1, 2, 3 };

            face.SetEdge(Edge.Top, array);

            CollectionAssert.AreEqual(array, face.GetEdge(Edge.Top));
        }

        [Test]
        public void SetTop_UsedOnA4x4Array_SetsTheElementsOnTheTop()
        {
            var face = CreatedNumberedFace(4);
            var array = new[] { 1, 2, 3, 4 };

            face.SetEdge(Edge.Top, array);

            CollectionAssert.AreEqual(array, face.GetEdge(Edge.Top));
        }

        [Test]
        public void SetTop_UsedOn2x2ArrayWithIndexFromEdgeValueOf1_SetsTheSecondRowOfValues()
        {
            var face = CreatedNumberedFace(2);
            var array = new[] { 10, 11 };

            face.SetEdge(1, Edge.Top, array);

            CollectionAssert.AreEqual(array, face.GetEdge(1, Edge.Top));
        }

        [Test]
        public void SetTop_UsedOn2x2ArrayWithIndexFromEdgeValueOf2_ThrowsArugementOutOfRangeException()
        {
            var face = CreatedNumberedFace(2);
            var array = new[] { 100, 101 };

            TestDelegate test = () => face.SetEdge(2, Edge.Top, array);

            Assert.Throws<ArgumentOutOfRangeException>(test);
        }

        [Test]
        public void SetTop_UsedOn3x3ArrayWithIndexFromEdgeValueOf1_SetsTheSecondRowOfValues()
        {
            var face = CreatedNumberedFace(3);
            var array = new[] { 100, 101, 102 };

            face.SetEdge(1, Edge.Top, array);

            CollectionAssert.AreEqual(array, face.GetEdge(1, Edge.Top));
        }

        [Test]
        public void SetTop_UsedOn3x3ArrayWithIndexFromEdgeValueOf2_SetsTheThirdRowOfValues()
        {
            var face = CreatedNumberedFace(3);
            var array = new[] { 100, 101, 102 };

            face.SetEdge(2, Edge.Top, array);

            CollectionAssert.AreEqual(array, face.GetEdge(2, Edge.Top));
        }

        [Test]
        public void SetTop_UsedOn4x4ArrayWithIndexFromEdgeValueOf1_SetsTheSecondRowOfValues()
        {
            var face = CreatedNumberedFace(4);
            var array = new[] { 100, 101, 102, 103 };

            face.SetEdge(1, Edge.Top, array);

            CollectionAssert.AreEqual(array, face.GetEdge(1, Edge.Top));
        }

        [Test]
        public void SetTop_UsedOn4x4ArrayWithIndexFromEdgeValueOf2_SetsTheThrirdRowOfValues()
        {
            var face = CreatedNumberedFace(4);
            var array = new[] { 100, 101, 102, 103 };

            face.SetEdge(2, Edge.Top, array);

            CollectionAssert.AreEqual(array, face.GetEdge(2, Edge.Top));
        }

        #endregion

        #region Bottom

        [Test]
        public void GetBottom_GivenA2x2Array_ReturnsTheValuesOnTheBottom()
        {
            var face = CreatedNumberedFace(2);
            var expected = new[] { 2, 3 };

            var actual = face.GetEdge(Edge.Bottom);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBottom_GivenA3x3Array_ReturnsTheValuesOnTheBottom()
        {
            var face = CreatedNumberedFace(3);
            var expected = new[] { 6, 7, 8 };

            var actual = face.GetEdge(Edge.Bottom);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBottom_GivenA4x4Array_ReturnsTheValuesOnTheBottom()
        {
            var face = CreatedNumberedFace(4);
            var expected = new[] { 12, 13, 14, 15 };

            var actual = face.GetEdge(Edge.Bottom);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBottom_GivenA2x2ArrayAndAnIndexFromValueOf1_ReturnsTheTopRowOfValues()
        {
            var face = CreatedNumberedFace(2);
            var expected = new[] { 0, 1 };

            var actual = face.GetEdge(1, Edge.Bottom);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBottom_GivenA2x2ArrayAndAnIndexFromValueOf2_ThrowsOutOfRangeException()
        {
            var face = CreatedNumberedFace(2);

            TestDelegate test = () => face.GetEdge(2, Edge.Bottom);

            Assert.Throws<ArgumentOutOfRangeException>(test);
        }

        [Test]
        public void GetBottom_GivenA3x3ArrayAndAnIndexFromValueOf1_ReturnsTheMiddleRowOfValues()
        {
            var face = CreatedNumberedFace(3);
            var expected = new[] { 3, 4, 5 };

            var actual = face.GetEdge(1, Edge.Bottom);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBottom_GivenA3x3ArrayAndAnIndexFromValueOf2_ReturnsTheTopRowOfValues()
        {
            var face = CreatedNumberedFace(3);
            var expected = new[] { 0, 1, 2 };

            var actual = face.GetEdge(2, Edge.Bottom);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBottom_GivenA4x4ArrayAndAnIndexFromValueOf1_ReturnsTheThirdRowOfValues()
        {
            var face = CreatedNumberedFace(4);
            var expected = new[] { 8, 9, 10, 11 };

            var actual = face.GetEdge(1, Edge.Bottom);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void GetBottom_GivenA4x4ArrayAndAnIndexFromValueOf2_ReturnsTheSecondRowOfValues()
        {
            var face = CreatedNumberedFace(4);
            var expected = new[] { 4, 5, 6, 7 };

            var actual = face.GetEdge(2, Edge.Bottom);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void SetBottom_GivenAnArrayThatIsTheWrongSize_Throws()
        {
            var face = CreatedNumberedFace(3);

            Assert.Throws<ArgumentException>(() => face.SetEdge(Edge.Bottom, new[] { 1, 2 }));
        }

        [Test]
        public void SetBottom_UsedOnA2x2Array_SetsTheElementsOnTheBottom()
        {
            var face = CreatedNumberedFace(2);
            var array = new[] { 1, 2 };

            face.SetEdge(Edge.Bottom, array);

            CollectionAssert.AreEqual(array, face.GetEdge(Edge.Bottom));
        }

        [Test]
        public void SetBottom_UsedOnA3x3Array_SetsTheElementsOnTheBottom()
        {
            var face = CreatedNumberedFace(3);
            var array = new[] { 1, 2, 3 };

            face.SetEdge(Edge.Bottom, array);

            CollectionAssert.AreEqual(array, face.GetEdge(Edge.Bottom));
        }

        [Test]
        public void SetBottom_UsedOnA4x4Array_SetsTheElementsOnTheBottom()
        {
            var face = CreatedNumberedFace(4);
            var array = new[] { 1, 2, 3, 4 };

            face.SetEdge(Edge.Bottom, array);

            CollectionAssert.AreEqual(array, face.GetEdge(Edge.Bottom));
        }

        [Test]
        public void SetBottom_UsedOn2x2ArrayWithIndexFromEdgeValueOf1_SetsTheFirstRowOfValues()
        {
            var face = CreatedNumberedFace(2);
            var array = new[] { 10, 11 };

            face.SetEdge(1, Edge.Bottom, array);

            CollectionAssert.AreEqual(array, face.GetEdge(1, Edge.Bottom));
        }

        [Test]
        public void SetBottom_UsedOn2x2ArrayWithIndexFromEdgeValueOf2_ThrowsArugementOutOfRangeException()
        {
            var face = CreatedNumberedFace(2);
            var array = new[] { 100, 101 };

            TestDelegate test = () => face.SetEdge(2, Edge.Bottom, array);

            Assert.Throws<ArgumentOutOfRangeException>(test);
        }

        [Test]
        public void SetBottom_UsedOn3x3ArrayWithIndexFromEdgeValueOf1_SetsTheSecondRowOfValues()
        {
            var face = CreatedNumberedFace(3);
            var array = new[] { 100, 101, 102 };

            face.SetEdge(1, Edge.Bottom, array);

            CollectionAssert.AreEqual(array, face.GetEdge(1, Edge.Bottom));
        }

        [Test]
        public void SetBottom_UsedOn3x3ArrayWithIndexFromEdgeValueOf2_SetsTheFirstRowOfValues()
        {
            var face = CreatedNumberedFace(3);
            var array = new[] { 100, 101, 102 };

            face.SetEdge(2, Edge.Bottom, array);

            CollectionAssert.AreEqual(array, face.GetEdge(2, Edge.Bottom));
        }

        [Test]
        public void SetBottom_UsedOn4x4ArrayWithIndexFromEdgeValueOf1_SetsTheThirdRowOfValues()
        {
            var face = CreatedNumberedFace(4);
            var array = new[] { 100, 101, 102, 103 };

            face.SetEdge(1, Edge.Bottom, array);

            CollectionAssert.AreEqual(array, face.GetEdge(1, Edge.Bottom));
        }

        [Test]
        public void SetBottom_UsedOn4x4ArrayWithIndexFromEdgeValueOf2_SetsTheSecondRowOfValues()
        {
            var face = CreatedNumberedFace(4);
            var array = new[] { 100, 101, 102, 103 };

            face.SetEdge(2, Edge.Bottom, array);

            CollectionAssert.AreEqual(array, face.GetEdge(2, Edge.Bottom));
        }

        #endregion

        #region Helpers

        public static Face<int> CreatedNumberedFace(int size)
        {
            int num = 0;

            var face = new Face<int>(null, 0, size);
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    face.Items[i, j] = num++;
                }
            }
            return face;
        }

        #endregion
    }
}
