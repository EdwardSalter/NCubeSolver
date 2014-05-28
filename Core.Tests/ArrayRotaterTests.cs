using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Core.UnitTests
{
    [TestFixture]
    public class ArrayRotaterTests
    {
        [Test]
        public void RotateClockwise_GivenAn2x2ArrayOfInt_RotatesBy90Degrees()
        {
            var array = new[,]
            {
                {0, 1},
                {2, 3}
            };
            var expected = new[,]
            {
                {2, 0},
                {3, 1}
            };

            var actual = ArrayRotater.RotateClockwise(array);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void RotateClockwise_GivenAn3x3ArrayOfInt_RotatesBy90Degrees()
        {
            var array = new[,]
            {
                {0, 1, 2},
                {3, 4, 5},
                {6, 7, 8}
            };
            var expected = new[,]
            {
                {6, 3, 0},
                {7, 4, 1},
                {8, 5, 2}
            };

            var actual = ArrayRotater.RotateClockwise(array);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void RotateClockwise_GivenAn4x4ArrayOfInt_RotatesBy90Degrees()
        {
            var array = new[,]
            {
                { 0,  1,  2,  3},
                { 4,  5,  6,  7},
                { 8,  9, 10, 11},
                {12, 13, 14, 15}
            };
            var expected = new[,]
            {
                {12,  8,  4,  0},
                {13,  9,  5,  1},
                {14,  10, 6,  2},
                {15, 11,  7,  3}
            };

            var actual = ArrayRotater.RotateClockwise(array);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void RotateAntiClockwise_GivenAn2x2ArrayOfInt_RotatesBy90Degrees()
        {
            var array = new[,]
            {
                {0, 1},
                {2, 3}
            };
            var expected = new[,]
            {
                {1, 3},
                {0, 2}
            };

            var actual = ArrayRotater.RotateAntiClockwise(array);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void RotateAntiClockwise_GivenAn3x3ArrayOfInt_RotatesBy90Degrees()
        {
            var array = new[,]
            {
                {0, 1, 2},
                {3, 4, 5},
                {6, 7, 8}
            };
            var expected = new[,]
            {
                {2, 5, 8},
                {1, 4, 7},
                {0, 3, 6}
            };

            var actual = ArrayRotater.RotateAntiClockwise(array);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void RotateAntiClockwise_GivenAn4x4ArrayOfInt_RotatesBy90Degrees()
        {
            var array = new[,]
            {
                { 0,  1,  2,  3},
                { 4,  5,  6,  7},
                { 8,  9, 10, 11},
                {12, 13, 14, 15}
            };
            var expected = new[,]
            {
                { 3,  7, 11, 15},
                { 2,  6, 10, 14},
                { 1,  5,  9, 13},
                { 0,  4,  8, 12}
            };

            var actual = ArrayRotater.RotateAntiClockwise(array);

            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public void Flip_GivenA3x3NumberedList_CorrectlyFlipsList()
        {
            var array = new[,]
            {
                {0, 1, 2},
                {3, 4, 5},
                {6, 7, 8}
            };
            var expected = new[,]
            {
                {8, 7, 6},
                {5, 4, 3},
                {2, 1, 0}
            };

            var actual = ArrayRotater.Flip(array);

            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
