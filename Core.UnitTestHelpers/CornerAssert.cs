using NUnit.Framework;

namespace Core.UnitTestHelpers
{
    public static class CornerAssert
    {
        public static void BottomFrontRightIsCorrect(CubeConfiguration<FaceColour> configuration)
        {
            CommonActions.ResetToDefaultPosition(configuration).Wait();
            Assert.AreEqual(FaceColour.White, configuration.Faces[FaceType.Down].TopRight());
            Assert.AreEqual(FaceColour.Red, configuration.Faces[FaceType.Front].BottomRight());
            Assert.AreEqual(FaceColour.Green, configuration.Faces[FaceType.Right].BottomLeft());
        }

        public static void BottomFrontLeftIsCorrect(CubeConfiguration<FaceColour> configuration)
        {
            CommonActions.ResetToDefaultPosition(configuration).Wait();
            Assert.AreEqual(FaceColour.White, configuration.Faces[FaceType.Down].TopLeft());
            Assert.AreEqual(FaceColour.Red, configuration.Faces[FaceType.Front].BottomLeft());
            Assert.AreEqual(FaceColour.Blue, configuration.Faces[FaceType.Left].BottomRight());
        }

        public static void BottomBackRightIsCorrect(CubeConfiguration<FaceColour> configuration)
        {
            CommonActions.ResetToDefaultPosition(configuration).Wait();
            Assert.AreEqual(FaceColour.White, configuration.Faces[FaceType.Down].BottomRight());
            Assert.AreEqual(FaceColour.Orange, configuration.Faces[FaceType.Back].BottomLeft());
            Assert.AreEqual(FaceColour.Green, configuration.Faces[FaceType.Right].BottomRight());
        }

        public static void BottomBackLeftIsCorrect(CubeConfiguration<FaceColour> configuration)
        {
            CommonActions.ResetToDefaultPosition(configuration).Wait();
            Assert.AreEqual(FaceColour.White, configuration.Faces[FaceType.Down].BottomLeft());
            Assert.AreEqual(FaceColour.Orange, configuration.Faces[FaceType.Back].BottomRight());
            Assert.AreEqual(FaceColour.Blue, configuration.Faces[FaceType.Left].BottomLeft());
        }
    }
}