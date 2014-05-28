using System;
using System.Linq;
using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Core.UnitTests
{
    [TestFixture]
    public class ArrayExtensionsTests
    {
        [Test]
        public void ToList_GivenAMultiDimensionalArray_ReturnsAnArrayWithAllItemsInIt()
        {
            var array = new[,] { { 1, 2 }, { 3, 4 } };

            var list = array.AsEnumerable();

            CollectionAssert.AreEquivalent(list, array);
        }

        [Test]
        public void ToList_GivenAMultiDimensionalArray_ReturnsAnArrayWithAllItemsInTheCorrectOrder()
        {
            var array = new[,] { { 1, 2 }, { 3, 4 } };

            var list = array.AsEnumerable().ToList();

            Assert.AreEqual(list[0], array[0, 0]);
            Assert.AreEqual(list[1], array[1, 0]);
            Assert.AreEqual(list[2], array[0, 1]);
            Assert.AreEqual(list[3], array[1, 1]);
        }

        [TestCase(0)]
        [TestCase(2)]
        [TestCase(4)]
        public void Centre_GivenAnArrayWithAnEvenNumberOfElements_Throws(int arraySize)
        {
            var array = new int[arraySize];
            for (int i = 0; i < arraySize; i++)
            {
                array[i] = i + 1;
            }

            Assert.Throws<ArgumentException>(() => array.Centre());
        }

        [TestCase(1, 1)]
        [TestCase(3, 2)]
        [TestCase(5, 3)]
        public void Centre_GivenAnArrayWithAnOddNumberOfElements_ReturnsTheCentreElement(int arraySize, int expected)
        {
            var array = new int[arraySize];
            for (int i = 0; i < arraySize; i++)
            {
                array[i] = i + 1;
            }

            var centre = array.Centre();

            Assert.AreEqual(expected, centre);
        }
    }
}