using System;
using System.Linq;
using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Core.UnitTestHelpers
{
    public static class CubeConfigurationAssert
    {
        public static void BottomLayerCornersAreCorrect(CubeConfiguration<FaceColour> configuration)
        {
            CornerAssert.BottomBackLeftIsCorrect(configuration);
            CornerAssert.BottomBackRightIsCorrect(configuration);
            CornerAssert.BottomFrontLeftIsCorrect(configuration);
            CornerAssert.BottomFrontRightIsCorrect(configuration);
        }

        public static void TopLayerCrossIsCorrect(CubeConfiguration<FaceColour> configuration)
        {
            CommonActions.ResetToDefaultPosition(configuration).Wait();
            FaceCentreColourMatchesCentresOfLayerNumber(configuration, FaceType.Upper, FaceColour.Yellow);
            Assert.AreEqual(FaceColour.Red, configuration.Faces[FaceType.Front].TopCentre());
            Assert.AreEqual(FaceColour.Blue, configuration.Faces[FaceType.Left].TopCentre());
            Assert.AreEqual(FaceColour.Orange, configuration.Faces[FaceType.Back].TopCentre());
            Assert.AreEqual(FaceColour.Green, configuration.Faces[FaceType.Right].TopCentre());
        }

        public static void FaceCentreColourMatchesCentresOfLayerNumber(CubeConfiguration<FaceColour> configuration, FaceType faceType, FaceColour faceColour, int? layer = null)
        {
            layer = layer ?? configuration.GetCentreLayer() - 1;
            var face = configuration.Faces[faceType];
            Assert.AreEqual(faceColour, face.Centre);
            Assert.AreEqual(faceColour, face.GetEdge(layer.Value, Edge.Right).Centre());
            Assert.AreEqual(faceColour, face.GetEdge(layer.Value, Edge.Top).Centre());
            Assert.AreEqual(faceColour, face.GetEdge(layer.Value, Edge.Bottom).Centre());
            Assert.AreEqual(faceColour, face.GetEdge(layer.Value, Edge.Bottom).Centre());
        }

        public static void FaceHasSolvedInnerSquare(CubeConfiguration<FaceColour> configuration, FaceType faceType, int? sizeOfSquare)
        {
            sizeOfSquare = sizeOfSquare ?? configuration.Size - 2;
            var startIndex = configuration.GetCentreLayer() - (sizeOfSquare.Value + 1) / 2 + 1;
            var endIndex = startIndex + sizeOfSquare.Value - 1;

            var face = configuration.Faces[faceType];
            var centre = face.Centre;
            for (int i = startIndex; i < endIndex; i++)
            for (int j = startIndex; j < endIndex; j++)
            {
                Assert.AreEqual(centre, face.Items[j,i]);
            }
        }

        public static void FaceIsColour(CubeConfiguration<FaceColour> configuration, FaceType face, FaceColour colour)
        {
            Assert.IsTrue(configuration.Faces[face].Items.AsEnumerable().All(c => c == colour));
        }

        public static void MiddleBackRightIsCorrect(CubeConfiguration<FaceColour> configuration)
        {
            CommonActions.ResetToDefaultPosition(configuration).Wait();
            Assert.AreEqual(FaceColour.Orange, configuration.Faces[FaceType.Back].LeftCentre());
            Assert.AreEqual(FaceColour.Green, configuration.Faces[FaceType.Right].RightCentre());
        }

        public static void MiddleBackLeftIsCorrect(CubeConfiguration<FaceColour> configuration)
        {
            CommonActions.ResetToDefaultPosition(configuration).Wait();
            Assert.AreEqual(FaceColour.Orange, configuration.Faces[FaceType.Back].RightCentre());
            Assert.AreEqual(FaceColour.Blue, configuration.Faces[FaceType.Left].LeftCentre());
        }

        public static void MiddleFrontRightIsCorrect(CubeConfiguration<FaceColour> configuration)
        {
            CommonActions.ResetToDefaultPosition(configuration).Wait();
            Assert.AreEqual(FaceColour.Red, configuration.Faces[FaceType.Front].RightCentre());
            Assert.AreEqual(FaceColour.Green, configuration.Faces[FaceType.Right].LeftCentre());
        }

        public static void MiddleFrontLeftIsCorrect(CubeConfiguration<FaceColour> configuration)
        {
            CommonActions.ResetToDefaultPosition(configuration).Wait();
            Assert.AreEqual(FaceColour.Red, configuration.Faces[FaceType.Front].LeftCentre());
            Assert.AreEqual(FaceColour.Blue, configuration.Faces[FaceType.Left].RightCentre());
        }

        public static void BottomFrontCentreIsCorrect(CubeConfiguration<FaceColour> configuration)
        {
            CommonActions.ResetToDefaultPosition(configuration).Wait();
            Assert.AreEqual(FaceColour.White, configuration.Faces[FaceType.Down].TopCentre());
            Assert.AreEqual(FaceColour.Red, configuration.Faces[FaceType.Front].BottomCentre());
        }

        public static void BottomBackCentreIsCorrect(CubeConfiguration<FaceColour> configuration)
        {
            CommonActions.ResetToDefaultPosition(configuration).Wait();
            Assert.AreEqual(FaceColour.White, configuration.Faces[FaceType.Down].BottomCentre());
            Assert.AreEqual(FaceColour.Orange, configuration.Faces[FaceType.Back].BottomCentre());
        }

        public static void BottomLeftCentreIsCorrect(CubeConfiguration<FaceColour> configuration)
        {
            CommonActions.ResetToDefaultPosition(configuration).Wait();
            Assert.AreEqual(FaceColour.White, configuration.Faces[FaceType.Down].LeftCentre());
            Assert.AreEqual(FaceColour.Blue, configuration.Faces[FaceType.Left].BottomCentre());
        }

        public static void BottomRightCentreIsCorrect(CubeConfiguration<FaceColour> configuration)
        {
            CommonActions.ResetToDefaultPosition(configuration).Wait();
            Assert.AreEqual(FaceColour.White, configuration.Faces[FaceType.Down].RightCentre());
            Assert.AreEqual(FaceColour.Green, configuration.Faces[FaceType.Right].BottomCentre());
        }

        public static void MiddleLayerIsCorrect(CubeConfiguration<FaceColour> configuration)
        {
            MiddleFrontLeftIsCorrect(configuration);
            MiddleFrontRightIsCorrect(configuration);
            MiddleBackLeftIsCorrect(configuration);
            MiddleBackRightIsCorrect(configuration);
        }

        public static void CubeIsCorrect(CubeConfiguration<FaceColour> configuration)
        {
            CommonActions.ResetToDefaultPosition(configuration).Wait();
            FaceIsColour(configuration, FaceType.Front, FaceColour.Red);
            FaceIsColour(configuration, FaceType.Back, FaceColour.Orange);
            FaceIsColour(configuration, FaceType.Upper, FaceColour.Yellow);
            FaceIsColour(configuration, FaceType.Down, FaceColour.White);
            FaceIsColour(configuration, FaceType.Left, FaceColour.Blue);
            FaceIsColour(configuration, FaceType.Right, FaceColour.Green);
        }
    }
}
