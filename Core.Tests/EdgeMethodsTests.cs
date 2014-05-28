using System;
using NUnit.Framework;

namespace Core.UnitTests
{
    [TestFixture]
    public class EdgeMethodsTests
    {
        [TestCase(Edge.Top, Edge.Top)]
        [TestCase(Edge.Bottom, Edge.Bottom)]
        [TestCase(Edge.Left, Edge.Right)]
        [TestCase(Edge.Right, Edge.Left)]
        public void GetReverseEdge_GivenAnEdge_ReturnsTheCorrectReverseEdge(Edge testEdge, Edge expectedReverseEdge)
        {
            var reverseEdge = EdgeMethods.GetReverseEdge(testEdge);

            Assert.AreEqual(expectedReverseEdge, reverseEdge);
        }

        [Test]
        public void GetReverseEdge_WhenGivenAnInvalidEdge_Throws()
        {
            Assert.Throws<ArgumentException>(() => EdgeMethods.GetReverseEdge((Edge)99));
        }
    }
}
