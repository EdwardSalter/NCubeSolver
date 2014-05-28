using System;
using NUnit.Framework;

namespace Core.UnitTests
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
