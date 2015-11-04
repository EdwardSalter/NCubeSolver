// 

using System.Collections.Generic;
using System.Linq;
using NCubeSolver.Core;
using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.UnitTests
{
    [TestFixture]
    public class RotationListExTests
    {
        [Test]
        public void Condense_GivenAListThatCannotBeCondensed_ReturnsTheSameList()
        {
            var list = new List<IRotation> { Rotations.LeftClockwise, Rotations.RightClockwise };

            var condensed = list.Condense();

            Assert.AreEqual(list, condensed);
        }

        [Test]
        public void Condense_GivenTwoLeftClockwiseRotations_ReturnsASingleLeft2Rotation()
        {
            var list = new List<IRotation> { Rotations.LeftClockwise, Rotations.LeftClockwise, Rotations.RightClockwise };

            var condensed = list.Condense();

            CollectionAssert.AreEqual(new[] { Rotations.Left2, Rotations.RightClockwise }, condensed);
        }

        [Test]
        public void Condense_GivenTwoLeftAntiClockwiseRotations_ReturnsASingleLeft2Rotation()
        {
            var list = new List<IRotation> { Rotations.LeftAntiClockwise, Rotations.LeftAntiClockwise, Rotations.RightClockwise };

            var condensed = list.Condense();

            CollectionAssert.AreEqual(new[] { Rotations.Left2, Rotations.RightClockwise }, condensed);
        }

        [Test]
        public void Condense_GivenTwoLeft2Rotations_RemovesBothRotations()
        {
            var list = new List<IRotation> { Rotations.Left2, Rotations.Left2, Rotations.RightClockwise };

            var condensed = list.Condense();

            CollectionAssert.AreEqual(new[] { Rotations.RightClockwise }, condensed);
        }

        [Test]
        public void Condense_GivenOneLeftClockwiseAndOneLeftAnitClockwiseRotations_RemovesBothRotations()
        {
            var list = new List<IRotation> { Rotations.LeftClockwise, Rotations.LeftAntiClockwise, Rotations.RightClockwise };

            var condensed = list.Condense();

            CollectionAssert.AreEqual(new[] { Rotations.RightClockwise }, condensed);
        }

        [Test]
        public void Condense_GivenOneLeft2AndOneLeftAntiClockwise_ReturnsLeftClockwise()
        {
            var list = new List<IRotation> { Rotations.Left2, Rotations.LeftAntiClockwise, Rotations.RightClockwise };

            var condensed = list.Condense();

            CollectionAssert.AreEqual(new[] { Rotations.LeftClockwise, Rotations.RightClockwise }, condensed);
        }

        [Test]
        public void Condense_GivenOneLeft2AndOneLeftClockwise_ReturnsLeftAntiClockwise()
        {
            var list = new List<IRotation> { Rotations.Left2, Rotations.LeftClockwise, Rotations.RightClockwise };

            var condensed = list.Condense();

            CollectionAssert.AreEqual(new[] { Rotations.LeftAntiClockwise, Rotations.RightClockwise }, condensed);
        }

        [Test]
        public void Condense_GivenThreeLeftClockwise_ReturnsSingleLeftAntiClockwise()
        {
            var list = new List<IRotation> { Rotations.LeftClockwise, Rotations.LeftClockwise, Rotations.LeftClockwise, Rotations.RightClockwise };

            var condensed = list.Condense();

            CollectionAssert.AreEqual(new[] { Rotations.LeftAntiClockwise, Rotations.RightClockwise }, condensed);
        }

        [Test]
        public void Condense_GivenFourLeftClockwise_RemovesAllRotations()
        {
            var list = new List<IRotation> { Rotations.LeftClockwise, Rotations.LeftClockwise, Rotations.LeftClockwise, Rotations.LeftClockwise, Rotations.RightClockwise };

            var condensed = list.Condense();

            CollectionAssert.AreEqual(new[] { Rotations.RightClockwise }, condensed);
        }

        [Test]
        public void Condense_GivenTwoLeftClockwiseRotationsOnTheSameLayer_RemovesBothRotationsAndAddsADoubleRotationForThatLayer()
        {
            var list = new List<IRotation> { Rotations.SecondLayerLeftClockwise, Rotations.SecondLayerLeftClockwise };

            var condensed = list.Condense();

            CollectionAssert.AreEqual(new [] { Rotations.SecondLayerLeft2 }, condensed);
        }

        [Test]
        public void Condense_GivenTwoLeftClockWiseRotationsOnDifferentLayers_RemovesNoRotations()
        {
            var list = new List<IRotation> { Rotations.SecondLayerLeftClockwise, Rotations.LeftClockwise };

            var condensed = list.Condense();

            CollectionAssert.AreEqual(list, condensed);
        }

        [Test]
        public void Condense_GivenOneLeftClockwiseAndOneLeftAnitClockwiseRotationsOnTheSameLayer_RemovesBothRotations()
        {
            var list = new List<IRotation> { Rotations.SecondLayerLeftClockwise, Rotations.SecondLayerLeftAntiClockwise };

            var condensed = list.Condense();

            CollectionAssert.IsEmpty(condensed);
        }

        [Test]
        public void Condense_GivenOneLeftClockwiseAndOneLeftAnitClockwiseRotationsOnDifferentLayers_RemovesNoRotations()
        {
            var list = new List<IRotation> { Rotations.SecondLayerLeftClockwise, Rotations.LeftAntiClockwise };

            var condensed = list.Condense();

            CollectionAssert.AreEqual(list, condensed);
        }

        #region CubeRotations

        [Test]
        public void Condense_GivenAListOfCubeRotationsThatCannotBeCondensed_ReturnsTheSameList()
        {
            var list = new List<IRotation> { CubeRotations.YClockwise, CubeRotations.ZClockwise };

            var condensed = list.Condense();

            Assert.AreEqual(list, condensed);
        }

        [Test]
        public void Condense_GivenTwoYClockwiseCubeRotations_ReturnsASingleY2Rotation()
        {
            var list = new List<IRotation> { CubeRotations.YClockwise, CubeRotations.YClockwise, CubeRotations.ZClockwise };

            var condensed = list.Condense();

            CollectionAssert.AreEqual(new[] { CubeRotations.Y2, CubeRotations.ZClockwise }, condensed);
        }

        [Test]
        public void Condense_GivenTwoYAntiClockwiseCubeRotations_ReturnsASingleY2Rotation()
        {
            var list = new List<IRotation> { CubeRotations.YAntiClockwise, CubeRotations.YAntiClockwise, CubeRotations.ZClockwise };

            var condensed = list.Condense();

            CollectionAssert.AreEqual(new[] { CubeRotations.Y2, CubeRotations.ZClockwise }, condensed);
        }

        [Test]
        public void Condense_GivenTwoY2CubeRotations_RemovesBothCubeRotations()
        {
            var list = new List<IRotation> { CubeRotations.Y2, CubeRotations.Y2, CubeRotations.ZClockwise };

            var condensed = list.Condense();

            CollectionAssert.AreEqual(new[] { CubeRotations.ZClockwise }, condensed);
        }

        [Test]
        public void Condense_GivenOneYClockwiseAndOneYAnitClockwiseCubeRotations_RemovesBothCubeRotations()
        {
            var list = new List<IRotation> { CubeRotations.YClockwise, CubeRotations.YAntiClockwise, CubeRotations.ZClockwise };

            var condensed = list.Condense();

            CollectionAssert.AreEqual(new[] { CubeRotations.ZClockwise }, condensed);
        }

        [Test]
        public void Condense_GivenOneY2AndOneYAntiClockwise_ReturnsYClockwise()
        {
            var list = new List<IRotation> { CubeRotations.Y2, CubeRotations.YAntiClockwise, CubeRotations.ZClockwise };

            var condensed = list.Condense();

            CollectionAssert.AreEqual(new[] { CubeRotations.YClockwise, CubeRotations.ZClockwise }, condensed);
        }

        [Test]
        public void Condense_GivenOneY2AndOneYClockwise_ReturnsYAntiClockwise()
        {
            var list = new List<IRotation> { CubeRotations.Y2, CubeRotations.YClockwise, CubeRotations.ZClockwise };

            var condensed = list.Condense();

            CollectionAssert.AreEqual(new[] { CubeRotations.YAntiClockwise, CubeRotations.ZClockwise }, condensed);
        }

        [Test]
        public void Condense_GivenThreeYClockwise_ReturnsSingleYAntiClockwise()
        {
            var list = new List<IRotation> { CubeRotations.YClockwise, CubeRotations.YClockwise, CubeRotations.YClockwise, CubeRotations.ZClockwise };

            var condensed = list.Condense();

            CollectionAssert.AreEqual(new[] { CubeRotations.YAntiClockwise, CubeRotations.ZClockwise }, condensed);
        }

        [Test]
        public void Condense_GivenFourYClockwise_RemovesAllCubeRotations()
        {
            var list = new List<IRotation> { CubeRotations.YClockwise, CubeRotations.YClockwise, CubeRotations.YClockwise, CubeRotations.YClockwise, CubeRotations.ZClockwise };

            var condensed = list.Condense();

            CollectionAssert.AreEqual(new[] { CubeRotations.ZClockwise }, condensed);
        }
        // TODO: CUBE ROTATIONS

        [Test]
        public void Condense_CubeRotationsRegressionTest1()
        {
            var list = new List<IRotation> { CubeRotations.YClockwise, CubeRotations.XClockwise, CubeRotations.XClockwise, CubeRotations.XAntiClockwise, CubeRotations.XAntiClockwise, CubeRotations.XClockwise, CubeRotations.YClockwise };

            var condensed = list.Condense();

            CollectionAssert.AreEqual(new[] { CubeRotations.YClockwise, CubeRotations.XClockwise, CubeRotations.YClockwise }, condensed);
        }

        #endregion

        #region Regression Tests

        [Test]
        public void Condense_FaceRotations_RegressionTest1()
        {
            var list = new List<IRotation> { Rotations.LeftClockwise, Rotations.UpperClockwise, Rotations.UpperClockwise, Rotations.UpperAntiClockwise, Rotations.UpperAntiClockwise, Rotations.UpperClockwise, Rotations.LeftClockwise };

            var condensed = list.Condense();

            CollectionAssert.AreEqual(new[] { Rotations.LeftClockwise, Rotations.UpperClockwise, Rotations.LeftClockwise }, condensed);
        }

        [Test]
        public void Condense_MixedRotations_RegressionTest1()
        {
            var list = RotationListEx.ParseInstructionList("l2 y2 y2 y2 D' D' D' D' D D' U' U' l l' U' R R' D D D' D' D' x' x x' x' y2 b2 b2 z U U' U U' U U' f' f D' D' D y' y z2 z2 y' y D2 D2 D2");

            var condensed = list.Condense();

            CollectionAssert.AreEqual(RotationListEx.ParseInstructionList("l2 y2 U D' x2 y2 z D").ToList(), condensed);
        }

        #endregion
    }
}