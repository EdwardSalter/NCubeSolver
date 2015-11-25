using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Core.UnitTests
{
    [TestFixture]
    public class RotationsTests
    {
        [TestCase(FaceType.Front, RotationDirection.Clockwise, 0)]
        [TestCase(FaceType.Front, RotationDirection.AntiClockwise, 0)]
        [TestCase(FaceType.Front, RotationDirection.Clockwise, 1)]
        [TestCase(FaceType.Front, RotationDirection.AntiClockwise, 1)]
        [TestCase(FaceType.Front, RotationDirection.Clockwise, 2)]
        [TestCase(FaceType.Front, RotationDirection.AntiClockwise, 2)]
        [TestCase(FaceType.Back, RotationDirection.Clockwise, 0)]
        [TestCase(FaceType.Back, RotationDirection.AntiClockwise, 0)]
        [TestCase(FaceType.Back, RotationDirection.Clockwise, 1)]
        [TestCase(FaceType.Back, RotationDirection.AntiClockwise, 1)]
        [TestCase(FaceType.Back, RotationDirection.Clockwise, 2)]
        [TestCase(FaceType.Back, RotationDirection.AntiClockwise, 2)]
        [TestCase(FaceType.Right, RotationDirection.Clockwise, 0)]
        [TestCase(FaceType.Right, RotationDirection.AntiClockwise, 0)]
        [TestCase(FaceType.Right, RotationDirection.Clockwise, 1)]
        [TestCase(FaceType.Right, RotationDirection.AntiClockwise, 1)]
        [TestCase(FaceType.Right, RotationDirection.Clockwise, 2)]
        [TestCase(FaceType.Right, RotationDirection.AntiClockwise, 2)]
        [TestCase(FaceType.Left, RotationDirection.Clockwise, 0)]
        [TestCase(FaceType.Left, RotationDirection.AntiClockwise, 0)]
        [TestCase(FaceType.Left, RotationDirection.Clockwise, 1)]
        [TestCase(FaceType.Left, RotationDirection.AntiClockwise, 1)]
        [TestCase(FaceType.Left, RotationDirection.Clockwise, 2)]
        [TestCase(FaceType.Left, RotationDirection.AntiClockwise, 2)]
        [TestCase(FaceType.Upper, RotationDirection.Clockwise, 0)]
        [TestCase(FaceType.Upper, RotationDirection.AntiClockwise, 0)]
        [TestCase(FaceType.Upper, RotationDirection.Clockwise, 1)]
        [TestCase(FaceType.Upper, RotationDirection.AntiClockwise, 1)]
        [TestCase(FaceType.Upper, RotationDirection.Clockwise, 2)]
        [TestCase(FaceType.Upper, RotationDirection.AntiClockwise, 2)]
        [TestCase(FaceType.Down, RotationDirection.Clockwise, 0)]
        [TestCase(FaceType.Down, RotationDirection.AntiClockwise, 0)]
        [TestCase(FaceType.Down, RotationDirection.Clockwise, 1)]
        [TestCase(FaceType.Down, RotationDirection.AntiClockwise, 1)]
        [TestCase(FaceType.Down, RotationDirection.Clockwise, 2)]
        [TestCase(FaceType.Down, RotationDirection.AntiClockwise, 2)]
        public void ByFace_GivenData_ReturnsAFaceRotationWithGivenValues(FaceType face, RotationDirection direction, int layerNumber)
        {
            var faceRotation = Rotations.ByFace(face, direction, layerNumber);

            Assert.AreEqual(face, faceRotation.Face);
            Assert.AreEqual(direction, faceRotation.Direction);
            Assert.AreEqual(layerNumber, faceRotation.LayerNumberFromFace);
            Assert.AreEqual(1, faceRotation.Count);
        }

        [TestCase(FaceType.Front, 0)]
        [TestCase(FaceType.Front, 1)]
        [TestCase(FaceType.Front, 2)]
        [TestCase(FaceType.Back, 0)]
        [TestCase(FaceType.Back, 1)]
        [TestCase(FaceType.Back, 2)]
        [TestCase(FaceType.Right, 0)]
        [TestCase(FaceType.Right, 1)]
        [TestCase(FaceType.Right, 2)]
        [TestCase(FaceType.Left, 0)]
        [TestCase(FaceType.Left, 1)]
        [TestCase(FaceType.Left, 2)]
        [TestCase(FaceType.Upper, 0)]
        [TestCase(FaceType.Upper, 1)]
        [TestCase(FaceType.Upper, 2)]
        [TestCase(FaceType.Down, 0)]
        [TestCase(FaceType.Down, 1)]
        [TestCase(FaceType.Down, 2)]
        public void ByFaceTwice_GivenData_ReturnsAFaceRotationWithGivenValues(FaceType face, int layerNumber)
        {
            var faceRotation = Rotations.ByFaceTwice(face, layerNumber);

            Assert.AreEqual(face, faceRotation.Face);
            Assert.AreEqual(RotationDirection.Clockwise, faceRotation.Direction);
            Assert.AreEqual(layerNumber, faceRotation.LayerNumberFromFace);
            Assert.AreEqual(2, faceRotation.Count);
        }
    }
}
