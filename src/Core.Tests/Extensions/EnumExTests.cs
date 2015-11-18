using NCubeSolvers.Core.Extensions;
using NUnit.Framework;

namespace NCubeSolver.Core.UnitTests.Extensions
{
    [TestFixture]
    public class EnumExTests
    {
        private enum TestEnum
        {
            MinValue = -1,
            MiddleValue = 0,
            MaxValue = 1
        }

        [Test]
        public void GetMaxValue_GivenATestEnumWithMaxValueOfOne_ReturnsOne()
        {
            var maxValue = EnumEx.GetMaxValue<TestEnum>();

            Assert.AreEqual(1, (int)maxValue);
        }

        [Test]
        public void GetMaxValue_GivenATestEnumWithMinValueOfMinusOne_ReturnsMinusOne()
        {
            var minValue = EnumEx.GetMinValue<TestEnum>();

            Assert.AreEqual(-1, (int)minValue);
        }
    }
}
