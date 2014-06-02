using System;
using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Core.UnitTests
{
    [TestFixture]
    public class CubeConfigurationTests
    {
        #region Shared Object

        [Test]
        public void Rotate_UpperClockwise()
        {
            var brd = new T { Id = "brd" };
            var bld = new T { Id = "bld" };
            var bru = new T { Id = "bru" };
            var blu = new T { Id = "blu" };
            var frd = new T { Id = "frd" };
            var fld = new T { Id = "fld" };
            var fru = new T { Id = "fru" };
            var flu = new T { Id = "flu" };

            var frontFace = new[,] { { flu, fru }, { fld, frd } };
            var backFace = new[,] { { bru, blu }, { brd, bld } };
            var faceTop = new[,] { { flu, fru }, { blu, bru } };
            var faceBottom = new[,] { { bld, brd }, { fld, frd } };
            var faceLeft = new[,] { { blu, flu }, { bld, fld } };
            var faceRight = new[,] { { fru, bru }, { frd, brd } };
            var config = new CubeConfiguration<T>(faceTop, faceBottom, faceLeft, faceRight, frontFace, backFace);

            config.Rotate(Rotations.UpperClockwise);

            CollectionAssert.AreEqual(new[,] { { fru, bru }, { fld, frd } }, config.Faces[FaceType.Front].Items);
            CollectionAssert.AreEqual(new[,] { { flu, fru }, { bld, fld } }, config.Faces[FaceType.Left].Items);
            CollectionAssert.AreEqual(new[,] { { blu, flu }, { brd, bld } }, config.Faces[FaceType.Back].Items);
            CollectionAssert.AreEqual(new[,] { { bru, blu }, { frd, brd } }, config.Faces[FaceType.Right].Items);
        }

        #region 3D

        [Test]
        public void ThreeDimensional()
        {
            var ldb = new Object();
            var cdb = new Object();
            var rdb = new Object();
            var lcb = new Object();
            var ccb = new Object();
            var rcb = new Object();
            var lub = new Object();
            var cub = new Object();
            var rub = new Object();

            var ldc = new Object();
            var cdc = new Object();
            var rdc = new Object();
            var lcc = new Object();
            var rcc = new Object();
            var luc = new Object();
            var cuc = new Object();
            var ruc = new Object();

            var ldf = new Object();
            var cdf = new Object();
            var rdf = new Object();
            var lcf = new Object();
            var ccf = new Object();
            var rcf = new Object();
            var luf = new Object();
            var cuf = new Object();
            var ruf = new Object();

            var frontFace = new[,] { { luf, cuf, ruf }, { lcf, ccf, rcf }, { ldf, cdf, rdf } };
            var rightFace = new[,] { { ruf, ruc, rub }, { rcf, rcc, rcb }, { rdf, rdc, rdb } };
            var backFace = new[,] { { rub, cub, lub }, { rcb, ccb, lcb }, { rdb, cdb, ldb } };
            var leftFace = new[,] { { lub, luc, luf }, { lcb, lcc, lcf }, { ldb, ldc, ldf } };
            var topFace = new[,] { { lub, cub, rub }, { luc, cuc, ruc }, { luf, cuf, ruf } };
            var downFace = new[,] { { ldf, cdf, rdf }, { ldc, cdc, rdc }, { ldb, cdb, rdb } };

            var config = new CubeConfiguration<object>(topFace, downFace, leftFace, rightFace, frontFace, backFace);
            config.Rotate(Rotations.RightClockwise);

            CollectionAssert.AreEqual(new[,] { { rdf, rcf, ruf }, { rdc, rcc, ruc }, { rdb, rcb, rub } }, config.Faces[FaceType.Right].Items);
            CollectionAssert.AreEqual(new[,] { { lub, cub, ruf }, { luc, cuc, rcf }, { luf, cuf, rdf } }, config.Faces[FaceType.Upper].Items);
        }

        #endregion

        #endregion

        #region Upper Clockwise

        [Test]
        public void Rotate_GivenUpperClockwise_FrontFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.UpperClockwise);

            var expected = new[,]
            {
                {"U", "R", "R"},
                {"F", "F", "F"},
                {"D", "D", "D"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Front].Items);
        }

        [Test]
        public void Rotate_GivenUpperClockwise_BackFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.UpperClockwise);

            var expected = new[,]
            {
                {"L", "L", "D"},
                {"U", "B", "B"},
                {"U", "B", "B"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Back].Items);
        }

        [Test]
        public void Rotate_GivenUpperClockwise_UpperIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.UpperClockwise);

            var expected = new[,]
            {
                {"L", "U", "U"},
                {"L", "U", "U"},
                {"L", "F", "F"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Upper].Items);
        }

        [Test]
        public void Rotate_GivenUpperClockwise_DownFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.UpperClockwise);

            var expected = new[,]
            {
                {"R", "R", "R"},
                {"D", "D", "B"},
                {"D", "D", "B"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Down].Items);
        }

        [Test]
        public void Rotate_GivenUpperClockwise_LeftFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.UpperClockwise);

            var expected = new[,]
            {
                {"F", "F", "F"},
                {"L", "L", "D"},
                {"L", "L", "B"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Left].Items);
        }

        [Test]
        public void Rotate_GivenUpperClockwise_RightFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.UpperClockwise);

            var expected = new[,]
            {
                {"U", "B", "B"},
                {"U", "R", "R"},
                {"F", "R", "R"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Right].Items);
        }

        #endregion

        #region Upper AntiClockwise

        [Test]
        public void Rotate_GivenUpperAntiClockwise_FrontFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.UpperAntiClockwise);

            var expected = new[,]
            {
                {"L", "L", "D"},
                {"F", "F", "F"},
                {"D", "D", "D"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Front].Items);
        }

        [Test]
        public void Rotate_GivenUpperAntiClockwise_BackFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.UpperAntiClockwise);

            var expected = new[,]
            {
                {"U", "R", "R"},
                {"U", "B", "B"},
                {"U", "B", "B"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Back].Items);
        }

        [Test]
        public void Rotate_GivenUpperAntiClockwise_UpperIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.UpperAntiClockwise);

            var expected = new[,]
            {
                {"F", "F", "L"},
                {"U", "U", "L"},
                {"U", "U", "L"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Upper].Items);
        }

        [Test]
        public void Rotate_GivenUpperAntiClockwise_DownFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.UpperAntiClockwise);

            var expected = new[,]
            {
                {"R", "R", "R"},
                {"D", "D", "B"},
                {"D", "D", "B"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Down].Items);
        }

        [Test]
        public void Rotate_GivenUpperAntiClockwise_LeftFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.UpperAntiClockwise);

            var expected = new[,]
            {
                {"U", "B", "B"},
                {"L", "L", "D"},
                {"L", "L", "B"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Left].Items);
        }

        [Test]
        public void Rotate_GivenUpperAntiClockwise_RightFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.UpperAntiClockwise);

            var expected = new[,]
            {
                {"F", "F", "F"},
                {"U", "R", "R"},
                {"F", "R", "R"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Right].Items);
        }

        #endregion

        #region Down Clockwise

        [Test]
        public void Rotate_GivenDownClockwise_FrontFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.DownClockwise);

            var expected = new[,]
            {
                {"F", "F", "F"},
                {"F", "F", "F"},
                {"L", "L", "B"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Front].Items);
        }

        [Test]
        public void Rotate_GivenDownClockwise_BackFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.DownClockwise);

            var expected = new[,]
            {
                {"U", "B", "B"},
                {"U", "B", "B"},
                {"F", "R", "R"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Back].Items);
        }

        [Test]
        public void Rotate_GivenDownClockwise_UpperIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.DownClockwise);

            var expected = new[,]
            {
                {"U", "U", "F"},
                {"U", "U", "F"},
                {"L", "L", "L"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Upper].Items);
        }

        [Test]
        public void Rotate_GivenDownClockwise_DownFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.DownClockwise);

            var expected = new[,]
            {
                {"D", "D", "R"},
                {"D", "D", "R"},
                {"B", "B", "R"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Down].Items);
        }

        [Test]
        public void Rotate_GivenDownClockwise_LeftFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.DownClockwise);

            var expected = new[,]
            {
                {"L", "L", "D"},
                {"L", "L", "D"},
                {"U", "B", "B"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Left].Items);
        }

        [Test]
        public void Rotate_GivenDownClockwise_RightFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.DownClockwise);

            var expected = new[,]
            {
                {"U", "R", "R"},
                {"U", "R", "R"},
                {"D", "D", "D"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Right].Items);
        }

        #endregion

        #region Down AntiClockwise

        [Test]
        public void Rotate_GivenDownAntiClockwise_FrontFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.DownAntiClockwise);

            var expected = new[,]
            {
                {"F", "F", "F"},
                {"F", "F", "F"},
                {"F", "R", "R"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Front].Items);
        }

        [Test]
        public void Rotate_GivenDownAntiClockwise_BackFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.DownAntiClockwise);

            var expected = new[,]
            {
                {"U", "B", "B"},
                {"U", "B", "B"},
                {"L", "L", "B"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Back].Items);
        }

        [Test]
        public void Rotate_GivenDownAntiClockwise_UpperIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.DownAntiClockwise);

            var expected = new[,]
            {
                {"U", "U", "F"},
                {"U", "U", "F"},
                {"L", "L", "L"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Upper].Items);
        }

        [Test]
        public void Rotate_GivenDownAntiClockwise_DownFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.DownAntiClockwise);

            var expected = new[,]
            {
                {"R", "B", "B"},
                {"R", "D", "D"},
                {"R", "D", "D"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Down].Items);
        }

        [Test]
        public void Rotate_GivenDownAntiClockwise_LeftFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.DownAntiClockwise);

            var expected = new[,]
            {
                {"L", "L", "D"},
                {"L", "L", "D"},
                {"D", "D", "D"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Left].Items);
        }

        [Test]
        public void Rotate_GivenDownAntiClockwise_RightFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.DownAntiClockwise);

            var expected = new[,]
            {
                {"U", "R", "R"},
                {"U", "R", "R"},
                {"U", "B", "B"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Right].Items);
        }

        #endregion

        #region Right Clockwise

        [Test]
        public void Rotate_GivenRightClockwise_FrontFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.RightClockwise);

            var expected = new[,]
            {
                {"F", "F", "R"},
                {"F", "F", "B"},
                {"D", "D", "B"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Front].Items);
        }

        [Test]
        public void Rotate_GivenRightClockwise_BackFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.RightClockwise);

            var expected = new[,]
            {
                {"L", "B", "B"},
                {"F", "B", "B"},
                {"F", "B", "B"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Back].Items);
        }

        [Test]
        public void Rotate_GivenRightClockwise_UpperIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.RightClockwise);

            var expected = new[,]
            {
                {"U", "U", "F"},
                {"U", "U", "F"},
                {"L", "L", "D"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Upper].Items);
        }

        [Test]
        public void Rotate_GivenRightClockwise_DownFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.RightClockwise);

            var expected = new[,]
            {
                {"R", "R", "U"},
                {"D", "D", "U"},
                {"D", "D", "U"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Down].Items);
        }

        [Test]
        public void Rotate_GivenRightClockwise_LeftFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.RightClockwise);

            var expected = new[,]
            {
                {"L", "L", "D"},
                {"L", "L", "D"},
                {"L", "L", "B"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Left].Items);
        }

        [Test]
        public void Rotate_GivenRightClockwise_RightFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.RightClockwise);

            var expected = new[,]
            {
                {"F", "U", "U"},
                {"R", "R", "R"},
                {"R", "R", "R"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Right].Items);
        }

        #endregion

        #region Right AntiClockwise

        [Test]
        public void Rotate_GivenRightAntiClockwise_FrontFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.RightAntiClockwise);

            var expected = new[,]
            {
                {"F", "F", "F"},
                {"F", "F", "F"},
                {"D", "D", "L"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Front].Items);
        }

        [Test]
        public void Rotate_GivenRightAntiClockwise_BackFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.RightAntiClockwise);

            var expected = new[,]
            {
                {"B", "B", "B"},
                {"B", "B", "B"},
                {"R", "B", "B"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Back].Items);
        }

        [Test]
        public void Rotate_GivenRightAntiClockwise_UpperIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.RightAntiClockwise);

            var expected = new[,]
            {
                {"U", "U", "U"},
                {"U", "U", "U"},
                {"L", "L", "U"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Upper].Items);
        }

        [Test]
        public void Rotate_GivenRightAntiClockwise_DownFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.RightAntiClockwise);

            var expected = new[,]
            {
                {"R", "R", "F"},
                {"D", "D", "F"},
                {"D", "D", "D"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Down].Items);
        }

        [Test]
        public void Rotate_GivenRightAntiClockwise_LeftFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.RightAntiClockwise);

            var expected = new[,]
            {
                {"L", "L", "D"},
                {"L", "L", "D"},
                {"L", "L", "B"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Left].Items);
        }

        [Test]
        public void Rotate_GivenRightAntiClockwise_RightFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.RightAntiClockwise);

            var expected = new[,]
            {
                {"R", "R", "R"},
                {"R", "R", "R"},
                {"U", "U", "F"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Right].Items);
        }

        #endregion

        #region Left Clockwise

        [Test]
        public void Rotate_GivenLeftClockwise_FrontFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.LeftClockwise);

            var expected = new[,]
            {
                {"U", "F", "F"},
                {"U", "F", "F"},
                {"L", "D", "D"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Front].Items);
        }

        [Test]
        public void Rotate_GivenLeftClockwise_BackFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.LeftClockwise);

            var expected = new[,]
            {
                {"U", "B", "D"},
                {"U", "B", "D"},
                {"U", "B", "R"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Back].Items);
        }

        [Test]
        public void Rotate_GivenLeftClockwise_UpperIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.LeftClockwise);

            var expected = new[,]
            {
                {"B", "U", "F"},
                {"B", "U", "F"},
                {"B", "L", "L"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Upper].Items);
        }

        [Test]
        public void Rotate_GivenLeftClockwise_DownFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.LeftClockwise);

            var expected = new[,]
            {
                {"F", "R", "R"},
                {"F", "D", "B"},
                {"D", "D", "B"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Down].Items);
        }

        [Test]
        public void Rotate_GivenLeftClockwise_LeftFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.LeftClockwise);

            var expected = new[,]
            {
                {"L", "L", "L"},
                {"L", "L", "L"},
                {"B", "D", "D"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Left].Items);
        }

        [Test]
        public void Rotate_GivenLeftClockwise_RightFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.LeftClockwise);

            var expected = new[,]
            {
                {"U", "R", "R"},
                {"U", "R", "R"},
                {"F", "R", "R"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Right].Items);
        }

        #endregion

        #region Left AntiClockwise

        [Test]
        public void Rotate_GivenLeftAntiClockwise_FrontFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.LeftAntiClockwise);

            var expected = new[,]
            {
                {"R", "F", "F"},
                {"D", "F", "F"},
                {"D", "D", "D"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Front].Items);
        }

        [Test]
        public void Rotate_GivenLeftAntiClockwise_BackFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.LeftAntiClockwise);

            var expected = new[,]
            {
                {"U", "B", "L"},
                {"U", "B", "U"},
                {"U", "B", "U"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Back].Items);
        }

        [Test]
        public void Rotate_GivenLeftAntiClockwise_UpperIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.LeftAntiClockwise);

            var expected = new[,]
            {
                {"F", "U", "F"},
                {"F", "U", "F"},
                {"D", "L", "L"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Upper].Items);
        }

        [Test]
        public void Rotate_GivenLeftAntiClockwise_DownFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.LeftAntiClockwise);

            var expected = new[,]
            {
                {"B", "R", "R"},
                {"B", "D", "B"},
                {"B", "D", "B"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Down].Items);
        }

        [Test]
        public void Rotate_GivenLeftAntiClockwise_LeftFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.LeftAntiClockwise);

            var expected = new[,]
            {
                {"D", "D", "B"},
                {"L", "L", "L"},
                {"L", "L", "L"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Left].Items);
        }

        [Test]
        public void Rotate_GivenLeftAntiClockwise_RightFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.LeftAntiClockwise);

            var expected = new[,]
            {
                {"U", "R", "R"},
                {"U", "R", "R"},
                {"F", "R", "R"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Right].Items);
        }

        #endregion

        #region Front Clockwise

        [Test]
        public void Rotate_GivenFrontClockwise_FrontFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.FrontClockwise);

            var expected = new[,]
            {
                {"D", "F", "F"},
                {"D", "F", "F"},
                {"D", "F", "F"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Front].Items);
        }

        [Test]
        public void Rotate_GivenFrontClockwise_BackFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.FrontClockwise);

            var expected = new[,]
            {
                {"U", "B", "B"},
                {"U", "B", "B"},
                {"U", "B", "B"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Back].Items);
        }

        [Test]
        public void Rotate_GivenFrontClockwise_UpperIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.FrontClockwise);

            var expected = new[,]
            {
                {"U", "U", "F"},
                {"U", "U", "F"},
                {"B", "D", "D"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Upper].Items);
        }

        [Test]
        public void Rotate_GivenFrontClockwise_DownFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.FrontClockwise);

            var expected = new[,]
            {
                {"F", "U", "U"},
                {"D", "D", "B"},
                {"D", "D", "B"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Down].Items);
        }

        [Test]
        public void Rotate_GivenFrontClockwise_LeftFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.FrontClockwise);

            var expected = new[,]
            {
                {"L", "L", "R"},
                {"L", "L", "R"},
                {"L", "L", "R"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Left].Items);
        }

        [Test]
        public void Rotate_GivenFrontClockwise_RightFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.FrontClockwise);

            var expected = new[,]
            {
                {"L", "R", "R"},
                {"L", "R", "R"},
                {"L", "R", "R"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Right].Items);
        }

        #endregion

        #region Front AntiClockwise

        [Test]
        public void Rotate_GivenFrontAntiClockwise_FrontFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.FrontAntiClockwise);

            var expected = new[,]
            {
                {"F", "F", "D"},
                {"F", "F", "D"},
                {"F", "F", "D"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Front].Items);
        }

        [Test]
        public void Rotate_GivenFrontAntiClockwise_BackFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.FrontAntiClockwise);

            var expected = new[,]
            {
                {"U", "B", "B"},
                {"U", "B", "B"},
                {"U", "B", "B"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Back].Items);
        }

        [Test]
        public void Rotate_GivenFrontAntiClockwise_UpperIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.FrontAntiClockwise);

            var expected = new[,]
            {
                {"U", "U", "F"},
                {"U", "U", "F"},
                {"U", "U", "F"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Upper].Items);
        }

        [Test]
        public void Rotate_GivenFrontAntiClockwise_DownFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.FrontAntiClockwise);

            var expected = new[,]
            {
                {"D", "D", "B"},
                {"D", "D", "B"},
                {"D", "D", "B"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Down].Items);
        }

        [Test]
        public void Rotate_GivenFrontAntiClockwise_LeftFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.FrontAntiClockwise);

            var expected = new[,]
            {
                {"L", "L", "L"},
                {"L", "L", "L"},
                {"L", "L", "L"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Left].Items);
        }

        [Test]
        public void Rotate_GivenFrontAntiClockwise_RightFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.FrontAntiClockwise);

            var expected = new[,]
            {
                {"R", "R", "R"},
                {"R", "R", "R"},
                {"R", "R", "R"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Right].Items);
        }

        #endregion

        #region Back Clockwise

        [Test]
        public void Rotate_GivenBackClockwise_FrontFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.BackClockwise);

            var expected = new[,]
            {
                {"F", "F", "F"},
                {"F", "F", "F"},
                {"D", "D", "D"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Front].Items);
        }

        [Test]
        public void Rotate_GivenBackClockwise_BackFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.BackClockwise);

            var expected = new[,]
            {
                {"U", "U", "U"},
                {"B", "B", "B"},
                {"B", "B", "B"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Back].Items);
        }

        [Test]
        public void Rotate_GivenBackClockwise_UpperIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.BackClockwise);

            var expected = new[,]
            {
                {"R", "R", "R"},
                {"U", "U", "F"},
                {"L", "L", "L"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Upper].Items);
        }

        [Test]
        public void Rotate_GivenBackClockwise_DownFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.BackClockwise);

            var expected = new[,]
            {
                {"R", "R", "R"},
                {"D", "D", "B"},
                {"L", "L", "L"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Down].Items);
        }

        [Test]
        public void Rotate_GivenBackClockwise_LeftFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.BackClockwise);

            var expected = new[,]
            {
                {"F", "L", "D"},
                {"U", "L", "D"},
                {"U", "L", "B"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Left].Items);
        }

        [Test]
        public void Rotate_GivenBackClockwise_RightFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.BackClockwise);

            var expected = new[,]
            {
                {"U", "R", "B"},
                {"U", "R", "D"},
                {"F", "R", "D"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Right].Items);
        }

        #endregion

        #region Back AntiClockwise

        [Test]
        public void Rotate_GivenBackAntiClockwise_FrontFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.BackAntiClockwise);

            var expected = new[,]
            {
                {"F", "F", "F"},
                {"F", "F", "F"},
                {"D", "D", "D"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Front].Items);
        }

        [Test]
        public void Rotate_GivenBackAntiClockwise_BackFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.BackAntiClockwise);

            var expected = new[,]
            {
                {"B", "B", "B"},
                {"B", "B", "B"},
                {"U", "U", "U"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Back].Items);
        }

        [Test]
        public void Rotate_GivenBackAntiClockwise_UpperIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.BackAntiClockwise);

            var expected = new[,]
            {
                {"L", "L", "L"},
                {"U", "U", "F"},
                {"L", "L", "L"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Upper].Items);
        }

        [Test]
        public void Rotate_GivenBackAntiClockwise_DownFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.BackAntiClockwise);

            var expected = new[,]
            {
                {"R", "R", "R"},
                {"D", "D", "B"},
                {"R", "R", "R"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Down].Items);
        }

        [Test]
        public void Rotate_GivenBackAntiClockwise_LeftFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.BackAntiClockwise);

            var expected = new[,]
            {
                {"D", "L", "D"},
                {"D", "L", "D"},
                {"B", "L", "B"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Left].Items);
        }

        [Test]
        public void Rotate_GivenBackAntiClockwise_RightFaceIsCorrect()
        {
            var cube = CreateCubeConfiguration();

            cube.Rotate(Rotations.BackAntiClockwise);

            var expected = new[,]
            {
                {"U", "R", "U"},
                {"U", "R", "U"},
                {"F", "R", "F"}
            };
            CollectionAssert.AreEqual(expected, cube.Faces[FaceType.Right].Items);
        }

        #endregion

        #region Multi-Rotate Clockwise - Right

        [Test]
        public void MultiRotate_Rotate2()
        {
            var config = CreateBlankCubeConfiguration();

            config.Rotate(Rotations.RightClockwise);
            config.Rotate(Rotations.UpperClockwise);

            CollectionAssert.AreEqual(new[,] { { "U", "U", "U" }, { "U", "U", "U" }, { "F", "F", "F" } }, config.Faces[FaceType.Upper].Items);
            CollectionAssert.AreEqual(new[,] { { "R", "R", "R" }, { "F", "F", "D" }, { "F", "F", "D" } }, config.Faces[FaceType.Front].Items);
            CollectionAssert.AreEqual(new[,] { { "U", "B", "B" }, { "R", "R", "R" }, { "R", "R", "R" } }, config.Faces[FaceType.Right].Items);
            CollectionAssert.AreEqual(new[,] { { "L", "L", "L" }, { "U", "B", "B" }, { "U", "B", "B" } }, config.Faces[FaceType.Back].Items);
            CollectionAssert.AreEqual(new[,] { { "F", "F", "D" }, { "L", "L", "L" }, { "L", "L", "L" } }, config.Faces[FaceType.Left].Items);
            CollectionAssert.AreEqual(new[,] { { "D", "D", "B" }, { "D", "D", "B" }, { "D", "D", "B" } }, config.Faces[FaceType.Down].Items);
        }

        [Test]
        public void MultiRotate_Rotate3()
        {
            var config = CreateBlankCubeConfiguration();

            config.Rotate(Rotations.RightClockwise);
            config.Rotate(Rotations.UpperClockwise);
            config.Rotate(Rotations.RightClockwise);

            CollectionAssert.AreEqual(new[,] { { "U", "U", "R" }, { "U", "U", "D" }, { "F", "F", "D" } }, config.Faces[FaceType.Upper].Items);
            CollectionAssert.AreEqual(new[,] { { "R", "R", "B" }, { "F", "F", "B" }, { "F", "F", "B" } }, config.Faces[FaceType.Front].Items);
            CollectionAssert.AreEqual(new[,] { { "R", "R", "U" }, { "R", "R", "B" }, { "R", "R", "B" } }, config.Faces[FaceType.Right].Items);
            CollectionAssert.AreEqual(new[,] { { "F", "L", "L" }, { "U", "B", "B" }, { "U", "B", "B" } }, config.Faces[FaceType.Back].Items);
            CollectionAssert.AreEqual(new[,] { { "F", "F", "D" }, { "L", "L", "L" }, { "L", "L", "L" } }, config.Faces[FaceType.Left].Items);
            CollectionAssert.AreEqual(new[,] { { "D", "D", "U" }, { "D", "D", "U" }, { "D", "D", "L" } }, config.Faces[FaceType.Down].Items);
        }

        [Test]
        public void MultiRotate_Rotate4()
        {
            var config = CreateBlankCubeConfiguration();

            config.Rotate(Rotations.RightClockwise);
            config.Rotate(Rotations.UpperClockwise);
            config.Rotate(Rotations.RightClockwise);
            config.Rotate(Rotations.UpperClockwise);

            CollectionAssert.AreEqual(new[,] { { "F", "U", "U" }, { "F", "U", "U" }, { "D", "D", "R" } }, config.Faces[FaceType.Upper].Items);
            CollectionAssert.AreEqual(new[,] { { "R", "R", "U" }, { "F", "F", "B" }, { "F", "F", "B" } }, config.Faces[FaceType.Front].Items);
            CollectionAssert.AreEqual(new[,] { { "F", "L", "L" }, { "R", "R", "B" }, { "R", "R", "B" } }, config.Faces[FaceType.Right].Items);
            CollectionAssert.AreEqual(new[,] { { "F", "F", "D" }, { "U", "B", "B" }, { "U", "B", "B" } }, config.Faces[FaceType.Back].Items);
            CollectionAssert.AreEqual(new[,] { { "R", "R", "B" }, { "L", "L", "L" }, { "L", "L", "L" } }, config.Faces[FaceType.Left].Items);
            CollectionAssert.AreEqual(new[,] { { "D", "D", "U" }, { "D", "D", "U" }, { "D", "D", "L" } }, config.Faces[FaceType.Down].Items);
        }

        [Test]
        public void MultiRotate_Rotate5()
        {
            var config = CreateBlankCubeConfiguration();

            config.Rotate(Rotations.RightClockwise);
            config.Rotate(Rotations.UpperClockwise);
            config.Rotate(Rotations.RightClockwise);
            config.Rotate(Rotations.UpperClockwise);
            config.Rotate(Rotations.RightClockwise);

            CollectionAssert.AreEqual(new[,] { { "F", "U", "U" }, { "F", "U", "B" }, { "D", "D", "B" } }, config.Faces[FaceType.Upper].Items);
            CollectionAssert.AreEqual(new[,] { { "R", "R", "U" }, { "F", "F", "U" }, { "F", "F", "L" } }, config.Faces[FaceType.Front].Items);
        }

        #endregion

        #region Multi-Rotate Anti-Clockwise - Right

        [Test]
        public void MultiRotate_Rotate2AntiClockwise()
        {
            var config = CreateBlankCubeConfiguration();

            config.Rotate(Rotations.RightAntiClockwise);
            config.Rotate(Rotations.UpperAntiClockwise);

            CollectionAssert.AreEqual(new[,] { { "B", "B", "B" }, { "U", "U", "U" }, { "U", "U", "U" } }, config.Faces[FaceType.Upper].Items);
            CollectionAssert.AreEqual(new[,] { { "L", "L", "L" }, { "F", "F", "U" }, { "F", "F", "U" } }, config.Faces[FaceType.Front].Items);
            CollectionAssert.AreEqual(new[,] { { "F", "F", "U" }, { "R", "R", "R" }, { "R", "R", "R" } }, config.Faces[FaceType.Right].Items);
            CollectionAssert.AreEqual(new[,] { { "R", "R", "R" }, { "D", "B", "B" }, { "D", "B", "B" } }, config.Faces[FaceType.Back].Items);
            CollectionAssert.AreEqual(new[,] { { "D", "B", "B" }, { "L", "L", "L" }, { "L", "L", "L" } }, config.Faces[FaceType.Left].Items);
            CollectionAssert.AreEqual(new[,] { { "D", "D", "F" }, { "D", "D", "F" }, { "D", "D", "F" } }, config.Faces[FaceType.Down].Items);
        }

        [Test]
        public void MultiRotate_Rotate3AntiClockwise()
        {
            var config = CreateBlankCubeConfiguration();

            config.Rotate(Rotations.RightAntiClockwise);
            config.Rotate(Rotations.UpperAntiClockwise);
            config.Rotate(Rotations.RightAntiClockwise);

            CollectionAssert.AreEqual(new[,] { { "B", "B", "D" }, { "U", "U", "D" }, { "U", "U", "R" } }, config.Faces[FaceType.Upper].Items);
            CollectionAssert.AreEqual(new[,] { { "L", "L", "B" }, { "F", "F", "U" }, { "F", "F", "U" } }, config.Faces[FaceType.Front].Items);
            CollectionAssert.AreEqual(new[,] { { "U", "R", "R" }, { "F", "R", "R" }, { "F", "R", "R" } }, config.Faces[FaceType.Right].Items);
            CollectionAssert.AreEqual(new[,] { { "F", "R", "R" }, { "F", "B", "B" }, { "F", "B", "B" } }, config.Faces[FaceType.Back].Items);
            CollectionAssert.AreEqual(new[,] { { "D", "B", "B" }, { "L", "L", "L" }, { "L", "L", "L" } }, config.Faces[FaceType.Left].Items);
            CollectionAssert.AreEqual(new[,] { { "D", "D", "L" }, { "D", "D", "U" }, { "D", "D", "U" } }, config.Faces[FaceType.Down].Items);
        }

        [Test]
        public void MultiRotate_Rotate4AntiClockwise()
        {
            var config = CreateBlankCubeConfiguration();

            config.Rotate(Rotations.RightAntiClockwise);
            config.Rotate(Rotations.UpperAntiClockwise);
            config.Rotate(Rotations.RightAntiClockwise);
            config.Rotate(Rotations.UpperAntiClockwise);

            CollectionAssert.AreEqual(new[,] { { "D", "D", "R" }, { "B", "U", "U" }, { "B", "U", "U" } }, config.Faces[FaceType.Upper].Items);
            CollectionAssert.AreEqual(new[,] { { "D", "B", "B" }, { "F", "F", "U" }, { "F", "F", "U" } }, config.Faces[FaceType.Front].Items);
            CollectionAssert.AreEqual(new[,] { { "L", "L", "B" }, { "F", "R", "R" }, { "F", "R", "R" } }, config.Faces[FaceType.Right].Items);
            CollectionAssert.AreEqual(new[,] { { "U", "R", "R" }, { "F", "B", "B" }, { "F", "B", "B" } }, config.Faces[FaceType.Back].Items);
            CollectionAssert.AreEqual(new[,] { { "F", "R", "R" }, { "L", "L", "L" }, { "L", "L", "L" } }, config.Faces[FaceType.Left].Items);
            CollectionAssert.AreEqual(new[,] { { "D", "D", "L" }, { "D", "D", "U" }, { "D", "D", "U" } }, config.Faces[FaceType.Down].Items);
        }

        [Test]
        public void MultiRotate_Rotate5AntiClockwise()
        {
            var config = CreateBlankCubeConfiguration();

            config.Rotate(Rotations.RightAntiClockwise);
            config.Rotate(Rotations.UpperAntiClockwise);
            config.Rotate(Rotations.RightAntiClockwise);
            config.Rotate(Rotations.UpperAntiClockwise);
            config.Rotate(Rotations.RightAntiClockwise);

            CollectionAssert.AreEqual(new[,] { { "D", "D", "F" }, { "B", "U", "F" }, { "B", "U", "U" } }, config.Faces[FaceType.Upper].Items);
            CollectionAssert.AreEqual(new[,] { { "D", "B", "R" }, { "F", "F", "U" }, { "F", "F", "U" } }, config.Faces[FaceType.Front].Items);
            CollectionAssert.AreEqual(new[,] { { "B", "R", "R" }, { "L", "R", "R" }, { "L", "F", "F" } }, config.Faces[FaceType.Right].Items);
            CollectionAssert.AreEqual(new[,] { { "U", "R", "R" }, { "U", "B", "B" }, { "L", "B", "B" } }, config.Faces[FaceType.Back].Items);
            CollectionAssert.AreEqual(new[,] { { "F", "R", "R" }, { "L", "L", "L" }, { "L", "L", "L" } }, config.Faces[FaceType.Left].Items);
            CollectionAssert.AreEqual(new[,] { { "D", "D", "B" }, { "D", "D", "U" }, { "D", "D", "U" } }, config.Faces[FaceType.Down].Items);
        }

        #endregion

        #region Multi-Rotate Clockwise - Left

        [Test]
        public void MultiRotate_Rotate2_Left()
        {
            var config = CreateBlankCubeConfiguration();

            config.Rotate(Rotations.LeftClockwise);
            config.Rotate(Rotations.UpperClockwise);

            CollectionAssert.AreEqual(new[,] { { "B", "B", "B" }, { "U", "U", "U" }, { "U", "U", "U" } }, config.Faces[FaceType.Upper].Items);
            CollectionAssert.AreEqual(new[,] { { "R", "R", "R" }, { "U", "F", "F" }, { "U", "F", "F" } }, config.Faces[FaceType.Front].Items);
            CollectionAssert.AreEqual(new[,] { { "B", "B", "D" }, { "R", "R", "R" }, { "R", "R", "R" } }, config.Faces[FaceType.Right].Items);
            CollectionAssert.AreEqual(new[,] { { "L", "L", "L" }, { "B", "B", "D" }, { "B", "B", "D" } }, config.Faces[FaceType.Back].Items);
            CollectionAssert.AreEqual(new[,] { { "U", "F", "F" }, { "L", "L", "L" }, { "L", "L", "L" } }, config.Faces[FaceType.Left].Items);
            CollectionAssert.AreEqual(new[,] { { "F", "D", "D" }, { "F", "D", "D" }, { "F", "D", "D" } }, config.Faces[FaceType.Down].Items);
        }

        [Test]
        public void MultiRotate_Rotate3_Left()
        {
            var config = CreateBlankCubeConfiguration();

            config.Rotate(Rotations.LeftClockwise);
            config.Rotate(Rotations.UpperClockwise);
            config.Rotate(Rotations.LeftClockwise);

            CollectionAssert.AreEqual(new[,] { { "D", "B", "B" }, { "D", "U", "U" }, { "L", "U", "U" } }, config.Faces[FaceType.Upper].Items);
            /* CollectionAssert.AreEqual(new[,] { { "R", "R", "R" }, { "U", "F", "F" }, { "U", "F", "F" } }, config.Faces[FaceType.Front].Items);
             CollectionAssert.AreEqual(new[,] { { "B", "B", "D" }, { "R", "R", "R" }, { "R", "R", "R" } }, config.Faces[FaceType.Right].Items);
             CollectionAssert.AreEqual(new[,] { { "L", "L", "L" }, { "B", "B", "D" }, { "B", "B", "D" } }, config.Faces[FaceType.Back].Items);
             CollectionAssert.AreEqual(new[,] { { "U", "F", "F" }, { "L", "L", "L" }, { "L", "L", "L" } }, config.Faces[FaceType.Left].Items);
             CollectionAssert.AreEqual(new[,] { { "F", "D", "D" }, { "F", "D", "D" }, { "F", "D", "D" } }, config.Faces[FaceType.Down].Items);*/
        }

        //[Test]
        //public void MultiRotate_Rotate4_Left()
        //{
        //    var config = CreateBlankCubeConfiguration();

        //    config.Rotate(Rotations.LeftClockwise);
        //    config.Rotate(Rotations.UpperClockwise);
        //    config.Rotate(Rotations.LeftClockwise);
        //    config.Rotate(Rotations.UpperClockwise);

        //    CollectionAssert.AreEqual(new[,] { { "F", "U", "U" }, { "F", "U", "U" }, { "D", "D", "R" } }, config.Faces[FaceType.Upper].Items);
        //    CollectionAssert.AreEqual(new[,] { { "R", "R", "U" }, { "F", "F", "B" }, { "F", "F", "B" } }, config.Faces[FaceType.Front].Items);
        //    CollectionAssert.AreEqual(new[,] { { "F", "L", "L" }, { "R", "R", "B" }, { "R", "R", "B" } }, config.Faces[FaceType.Right].Items);
        //    CollectionAssert.AreEqual(new[,] { { "F", "F", "D" }, { "U", "B", "B" }, { "U", "B", "B" } }, config.Faces[FaceType.Back].Items);
        //    CollectionAssert.AreEqual(new[,] { { "R", "R", "B" }, { "L", "L", "L" }, { "L", "L", "L" } }, config.Faces[FaceType.Left].Items);
        //    CollectionAssert.AreEqual(new[,] { { "D", "D", "U" }, { "D", "D", "U" }, { "D", "D", "L" } }, config.Faces[FaceType.Down].Items);
        //}

        //[Test]
        //public void MultiRotate_Rotate5_Left()
        //{
        //    var config = CreateBlankCubeConfiguration();

        //    config.Rotate(Rotations.LeftClockwise);
        //    config.Rotate(Rotations.UpperClockwise);
        //    config.Rotate(Rotations.LeftClockwise);
        //    config.Rotate(Rotations.UpperClockwise);
        //    config.Rotate(Rotations.LeftClockwise);

        //    CollectionAssert.AreEqual(new[,] { { "F", "U", "U" }, { "F", "U", "B" }, { "D", "D", "B" } }, config.Faces[FaceType.Upper].Items);
        //    CollectionAssert.AreEqual(new[,] { { "R", "R", "U" }, { "F", "F", "U" }, { "F", "F", "L" } }, config.Faces[FaceType.Front].Items);
        //}

        //#endregion

        //#region Multi-Rotate Anti-Clockwise - Left

        //[Test]
        //public void MultiRotate_Rotate2AntiClockwise_Left()
        //{
        //    var config = CreateBlankCubeConfiguration();

        //    config.Rotate(Rotations.LeftAntiClockwise);
        //    config.Rotate(Rotations.UpperAntiClockwise);

        //    CollectionAssert.AreEqual(new[,] { { "B", "B", "B" }, { "U", "U", "U" }, { "U", "U", "U" } }, config.Faces[FaceType.Upper].Items);
        //    CollectionAssert.AreEqual(new[,] { { "L", "L", "L" }, { "F", "F", "U" }, { "F", "F", "U" } }, config.Faces[FaceType.Front].Items);
        //    CollectionAssert.AreEqual(new[,] { { "F", "F", "U" }, { "R", "R", "R" }, { "R", "R", "R" } }, config.Faces[FaceType.Right].Items);
        //    CollectionAssert.AreEqual(new[,] { { "R", "R", "R" }, { "D", "B", "B" }, { "D", "B", "B" } }, config.Faces[FaceType.Back].Items);
        //    CollectionAssert.AreEqual(new[,] { { "D", "B", "B" }, { "L", "L", "L" }, { "L", "L", "L" } }, config.Faces[FaceType.Left].Items);
        //    CollectionAssert.AreEqual(new[,] { { "D", "D", "F" }, { "D", "D", "F" }, { "D", "D", "F" } }, config.Faces[FaceType.Down].Items);
        //}

        //[Test]
        //public void MultiRotate_Rotate3AntiClockwise_Left()
        //{
        //    var config = CreateBlankCubeConfiguration();

        //    config.Rotate(Rotations.LeftAntiClockwise);
        //    config.Rotate(Rotations.UpperAntiClockwise);
        //    config.Rotate(Rotations.LeftAntiClockwise);

        //    CollectionAssert.AreEqual(new[,] { { "B", "B", "D" }, { "U", "U", "D" }, { "U", "U", "R" } }, config.Faces[FaceType.Upper].Items);
        //    CollectionAssert.AreEqual(new[,] { { "L", "L", "B" }, { "F", "F", "U" }, { "F", "F", "U" } }, config.Faces[FaceType.Front].Items);
        //    CollectionAssert.AreEqual(new[,] { { "U", "R", "R" }, { "F", "R", "R" }, { "F", "R", "R" } }, config.Faces[FaceType.Right].Items);
        //    CollectionAssert.AreEqual(new[,] { { "F", "R", "R" }, { "F", "B", "B" }, { "F", "B", "B" } }, config.Faces[FaceType.Back].Items);
        //    CollectionAssert.AreEqual(new[,] { { "D", "B", "B" }, { "L", "L", "L" }, { "L", "L", "L" } }, config.Faces[FaceType.Left].Items);
        //    CollectionAssert.AreEqual(new[,] { { "D", "D", "L" }, { "D", "D", "U" }, { "D", "D", "U" } }, config.Faces[FaceType.Down].Items);
        //}

        //[Test]
        //public void MultiRotate_Rotate4AntiClockwise_Left()
        //{
        //    var config = CreateBlankCubeConfiguration();

        //    config.Rotate(Rotations.LeftAntiClockwise);
        //    config.Rotate(Rotations.UpperAntiClockwise);
        //    config.Rotate(Rotations.LeftAntiClockwise);
        //    config.Rotate(Rotations.UpperAntiClockwise);

        //    CollectionAssert.AreEqual(new[,] { { "D", "D", "R" }, { "B", "U", "U" }, { "B", "U", "U" } }, config.Faces[FaceType.Upper].Items);
        //    CollectionAssert.AreEqual(new[,] { { "D", "B", "B" }, { "F", "F", "U" }, { "F", "F", "U" } }, config.Faces[FaceType.Front].Items);
        //    CollectionAssert.AreEqual(new[,] { { "L", "L", "B" }, { "F", "R", "R" }, { "F", "R", "R" } }, config.Faces[FaceType.Right].Items);
        //    CollectionAssert.AreEqual(new[,] { { "U", "R", "R" }, { "F", "B", "B" }, { "F", "B", "B" } }, config.Faces[FaceType.Back].Items);
        //    CollectionAssert.AreEqual(new[,] { { "F", "R", "R" }, { "L", "L", "L" }, { "L", "L", "L" } }, config.Faces[FaceType.Left].Items);
        //    CollectionAssert.AreEqual(new[,] { { "D", "D", "L" }, { "D", "D", "U" }, { "D", "D", "U" } }, config.Faces[FaceType.Down].Items);
        //}

        //[Test]
        //public void MultiRotate_Rotate5AntiClockwise_Left()
        //{
        //    var config = CreateBlankCubeConfiguration();

        //    config.Rotate(Rotations.LeftAntiClockwise);
        //    config.Rotate(Rotations.UpperAntiClockwise);
        //    config.Rotate(Rotations.LeftAntiClockwise);
        //    config.Rotate(Rotations.UpperAntiClockwise);
        //    config.Rotate(Rotations.LeftAntiClockwise);

        //    CollectionAssert.AreEqual(new[,] { { "D", "D", "F" }, { "B", "U", "F" }, { "B", "U", "U" } }, config.Faces[FaceType.Upper].Items);
        //    CollectionAssert.AreEqual(new[,] { { "D", "B", "R" }, { "F", "F", "U" }, { "F", "F", "U" } }, config.Faces[FaceType.Front].Items);
        //    CollectionAssert.AreEqual(new[,] { { "B", "R", "R" }, { "L", "R", "R" }, { "L", "F", "F" } }, config.Faces[FaceType.Right].Items);
        //    CollectionAssert.AreEqual(new[,] { { "U", "R", "R" }, { "U", "B", "B" }, { "L", "B", "B" } }, config.Faces[FaceType.Back].Items);
        //    CollectionAssert.AreEqual(new[,] { { "F", "R", "R" }, { "L", "L", "L" }, { "L", "L", "L" } }, config.Faces[FaceType.Left].Items);
        //    CollectionAssert.AreEqual(new[,] { { "D", "D", "B" }, { "D", "D", "U" }, { "D", "D", "U" } }, config.Faces[FaceType.Down].Items);
        //}

        #endregion

        #region Multi-Rotate - Back

        [Test]
        public void MultiRotate_Rotate3_BackAnti()
        {
            var config = CreateBlankCubeConfiguration();

            config.Rotate(Rotations.BackAntiClockwise);
            config.Rotate(Rotations.UpperAntiClockwise);
            config.Rotate(Rotations.BackAntiClockwise);

            CollectionAssert.AreEqual(new[,] { { "D", "D", "B" }, { "L", "U", "U" }, { "L", "U", "U" } }, config.Faces[FaceType.Upper].Items);
            CollectionAssert.AreEqual(new[,] { { "D", "L", "L" }, { "F", "F", "F" }, { "F", "F", "F" } }, config.Faces[FaceType.Front].Items);
            CollectionAssert.AreEqual(new[,] { { "F", "F", "L" }, { "R", "R", "U" }, { "R", "R", "U" } }, config.Faces[FaceType.Right].Items);
            CollectionAssert.AreEqual(new[,] { { "U", "B", "B" }, { "R", "B", "B" }, { "R", "B", "B" } }, config.Faces[FaceType.Back].Items);
            CollectionAssert.AreEqual(new[,] { { "R", "B", "B" }, { "R", "L", "L" }, { "R", "L", "L" } }, config.Faces[FaceType.Left].Items);
            CollectionAssert.AreEqual(new[,] { { "D", "D", "D" }, { "D", "D", "D" }, { "U", "U", "F" } }, config.Faces[FaceType.Down].Items);
        }

        [Test]
        public void MultiRotate_Rotate3_Back()
        {
            var config = CreateBlankCubeConfiguration();

            config.Rotate(Rotations.BackClockwise);
            config.Rotate(Rotations.UpperClockwise);
            config.Rotate(Rotations.BackClockwise);

            CollectionAssert.AreEqual(new[,] { { "B", "D", "D" }, { "U", "U", "R" }, { "U", "U", "R" } }, config.Faces[FaceType.Upper].Items);
            CollectionAssert.AreEqual(new[,] { { "R", "R", "D" }, { "F", "F", "F" }, { "F", "F", "F" } }, config.Faces[FaceType.Front].Items);
            CollectionAssert.AreEqual(new[,] { { "B", "B", "L" }, { "R", "R", "L" }, { "R", "R", "L" } }, config.Faces[FaceType.Right].Items);
            CollectionAssert.AreEqual(new[,] { { "B", "B", "U" }, { "B", "B", "L" }, { "B", "B", "L" } }, config.Faces[FaceType.Back].Items);
            CollectionAssert.AreEqual(new[,] { { "R", "F", "F" }, { "U", "L", "L" }, { "U", "L", "L" } }, config.Faces[FaceType.Left].Items);
            CollectionAssert.AreEqual(new[,] { { "D", "D", "D" }, { "D", "D", "D" }, { "F", "U", "U" } }, config.Faces[FaceType.Down].Items);
        }

        [Test]
        public void MultiRotate_Rotate4_Back()
        {
            var config = CreateBlankCubeConfiguration();

            config.Rotate(Rotations.BackClockwise);
            config.Rotate(Rotations.UpperClockwise);
            config.Rotate(Rotations.BackClockwise);
            config.Rotate(Rotations.UpperClockwise);

            CollectionAssert.AreEqual(new[,] { { "U", "U", "B" }, { "U", "U", "D" }, { "R", "R", "D" } }, config.Faces[FaceType.Upper].Items);
            CollectionAssert.AreEqual(new[,] { { "B", "B", "L" }, { "F", "F", "F" }, { "F", "F", "F" } }, config.Faces[FaceType.Front].Items);
            CollectionAssert.AreEqual(new[,] { { "B", "B", "U" }, { "R", "R", "L" }, { "R", "R", "L" } }, config.Faces[FaceType.Right].Items);
            CollectionAssert.AreEqual(new[,] { { "R", "F", "F" }, { "B", "B", "L" }, { "B", "B", "L" } }, config.Faces[FaceType.Back].Items);
            CollectionAssert.AreEqual(new[,] { { "R", "R", "D" }, { "U", "L", "L" }, { "U", "L", "L" } }, config.Faces[FaceType.Left].Items);
            CollectionAssert.AreEqual(new[,] { { "D", "D", "D" }, { "D", "D", "D" }, { "F", "U", "U" } }, config.Faces[FaceType.Down].Items);
        }

        [Test]
        public void MultiRotate_Rotate5_Back()
        {
            var config = CreateBlankCubeConfiguration();

            config.Rotate(Rotations.BackClockwise);
            config.Rotate(Rotations.UpperClockwise);
            config.Rotate(Rotations.BackClockwise);
            config.Rotate(Rotations.UpperClockwise);
            config.Rotate(Rotations.BackClockwise);

            CollectionAssert.AreEqual(new[,] { { "U", "L", "L" }, { "U", "U", "D" }, { "R", "R", "D" } }, config.Faces[FaceType.Upper].Items);
            CollectionAssert.AreEqual(new[,] { { "B", "B", "L" }, { "F", "F", "F" }, { "F", "F", "F" } }, config.Faces[FaceType.Front].Items);
            CollectionAssert.AreEqual(new[,] { { "B", "B", "U" }, { "R", "R", "U" }, { "R", "R", "F" } }, config.Faces[FaceType.Right].Items);
            CollectionAssert.AreEqual(new[,] { { "B", "B", "R" }, { "B", "B", "F" }, { "L", "L", "F" } }, config.Faces[FaceType.Back].Items);
            CollectionAssert.AreEqual(new[,] { { "B", "R", "D" }, { "U", "L", "L" }, { "U", "L", "L" } }, config.Faces[FaceType.Left].Items);
            CollectionAssert.AreEqual(new[,] { { "D", "D", "D" }, { "D", "D", "D" }, { "R", "U", "U" } }, config.Faces[FaceType.Down].Items);
        }

        #endregion

        #region CubeRotate

        [Test]
        public void CubeRotate_GivenXClockwise_CorrectlyRotatesEverything()
        {
            var config = CreateCubeConfiguration();

            config.RotateCube(CubeRotations.XClockwise);

            CollectionAssert.AreEqual(new[,] { { "D", "D", "B" }, { "L", "L", "L" }, { "L", "L", "L" } }, config.Faces[FaceType.Left].Items);
            CollectionAssert.AreEqual(new[,] { { "B", "B", "U" }, { "B", "B", "U" }, { "B", "B", "U" } }, config.Faces[FaceType.Down].Items);
            CollectionAssert.AreEqual(new[,] { { "F", "U", "U" }, { "R", "R", "R" }, { "R", "R", "R" } }, config.Faces[FaceType.Right].Items);
            CollectionAssert.AreEqual(new[,] { { "F", "F", "F" }, { "F", "F", "F" }, { "D", "D", "D" } }, config.Faces[FaceType.Upper].Items);
            CollectionAssert.AreEqual(new[,] { { "L", "L", "L" }, { "F", "U", "U" }, { "F", "U", "U" } }, config.Faces[FaceType.Back].Items);
            CollectionAssert.AreEqual(new[,] { { "R", "R", "R" }, { "D", "D", "B" }, { "D", "D", "B" } }, config.Faces[FaceType.Front].Items);
        }

        [Test]
        public void CubeRotate_GivenXAntiClockwise_CorrectlyRotatesEverything()
        {
            var config = CreateCubeConfiguration();

            config.RotateCube(CubeRotations.XAntiClockwise);

            CollectionAssert.AreEqual(new[,] { { "L", "L", "L" }, { "L", "L", "L" }, { "B", "D", "D" } }, config.Faces[FaceType.Left].Items);
            CollectionAssert.AreEqual(new[,] { { "F", "F", "F" }, { "F", "F", "F" }, { "D", "D", "D" } }, config.Faces[FaceType.Down].Items);
            CollectionAssert.AreEqual(new[,] { { "R", "R", "R" }, { "R", "R", "R" }, { "U", "U", "F" } }, config.Faces[FaceType.Right].Items);
            CollectionAssert.AreEqual(new[,] { { "B", "B", "U" }, { "B", "B", "U" }, { "B", "B", "U" } }, config.Faces[FaceType.Upper].Items);
            CollectionAssert.AreEqual(new[,] { { "B", "D", "D" }, { "B", "D", "D" }, { "R", "R", "R" } }, config.Faces[FaceType.Back].Items);
            CollectionAssert.AreEqual(new[,] { { "U", "U", "F" }, { "U", "U", "F" }, { "L", "L", "L" } }, config.Faces[FaceType.Front].Items);
        }

        [Test]
        public void CubeRotate_GivenYAntiClockwise_CorrectlyRotatesEverything()
        {
            var config = CreateCubeConfiguration();

            config.RotateCube(CubeRotations.YAntiClockwise);

            CollectionAssert.AreEqual(new[,] { { "U", "B", "B" }, { "U", "B", "B" }, { "U", "B", "B" } }, config.Faces[FaceType.Left].Items);
            CollectionAssert.AreEqual(new[,] { { "L", "L", "D" }, { "L", "L", "D" }, { "L", "L", "B" } }, config.Faces[FaceType.Front].Items);
            CollectionAssert.AreEqual(new[,] { { "D", "D", "R" }, { "D", "D", "R" }, { "B", "B", "R" } }, config.Faces[FaceType.Down].Items);
            CollectionAssert.AreEqual(new[,] { { "F", "F", "F" }, { "F", "F", "F" }, { "D", "D", "D" } }, config.Faces[FaceType.Right].Items);
            CollectionAssert.AreEqual(new[,] { { "F", "F", "L" }, { "U", "U", "L" }, { "U", "U", "L" } }, config.Faces[FaceType.Upper].Items);
            CollectionAssert.AreEqual(new[,] { { "U", "R", "R" }, { "U", "R", "R" }, { "F", "R", "R" } }, config.Faces[FaceType.Back].Items);
        }

        [Test]
        public void CubeRotate_GivenYClockwise_CorrectlyRotatesEverything()
        {
            var config = CreateCubeConfiguration();

            config.RotateCube(CubeRotations.YClockwise);

            CollectionAssert.AreEqual(new[,] { { "F", "F", "F" }, { "F", "F", "F" }, { "D", "D", "D" } }, config.Faces[FaceType.Left].Items);
            CollectionAssert.AreEqual(new[,] { { "R", "B", "B" }, { "R", "D", "D" }, { "R", "D", "D" } }, config.Faces[FaceType.Down].Items);
            CollectionAssert.AreEqual(new[,] { { "U", "B", "B" }, { "U", "B", "B" }, { "U", "B", "B" } }, config.Faces[FaceType.Right].Items);
            CollectionAssert.AreEqual(new[,] { { "L", "U", "U" }, { "L", "U", "U" }, { "L", "F", "F" } }, config.Faces[FaceType.Upper].Items);
            CollectionAssert.AreEqual(new[,] { { "L", "L", "D" }, { "L", "L", "D" }, { "L", "L", "B" } }, config.Faces[FaceType.Back].Items);
            CollectionAssert.AreEqual(new[,] { { "U", "R", "R" }, { "U", "R", "R" }, { "F", "R", "R" } }, config.Faces[FaceType.Front].Items);
        }

        [Test]
        public void CubeRotate_GivenZClockwise_CorrectlyRotatesEverything()
        {
            var config = CreateCubeConfiguration();

            config.RotateCube(CubeRotations.ZClockwise);

            CollectionAssert.AreEqual(new[,] { { "D", "D", "R" }, { "D", "D", "R" }, { "B", "B", "R" } }, config.Faces[FaceType.Left].Items);
            CollectionAssert.AreEqual(new[,] { { "D", "F", "F" }, { "D", "F", "F" }, { "D", "F", "F" } }, config.Faces[FaceType.Front].Items);
            CollectionAssert.AreEqual(new[,] { { "F", "U", "U" }, { "R", "R", "R" }, { "R", "R", "R" } }, config.Faces[FaceType.Down].Items);
            CollectionAssert.AreEqual(new[,] { { "L", "U", "U" }, { "L", "U", "U" }, { "L", "F", "F" } }, config.Faces[FaceType.Right].Items);
            CollectionAssert.AreEqual(new[,] { { "L", "L", "L" }, { "L", "L", "L" }, { "B", "D", "D" } }, config.Faces[FaceType.Upper].Items);
            CollectionAssert.AreEqual(new[,] { { "B", "B", "B" }, { "B", "B", "B" }, { "U", "U", "U" } }, config.Faces[FaceType.Back].Items);
        }

        [Test]
        public void CubeRotate_GivenZAntiClockwise_CorrectlyRotatesEverything()
        {
            var config = CreateCubeConfiguration();

            config.RotateCube(CubeRotations.ZAntiClockwise);

            CollectionAssert.AreEqual(new[,] { { "F", "F", "L" }, { "U", "U", "L" }, { "U", "U", "L" } }, config.Faces[FaceType.Left].Items);
            CollectionAssert.AreEqual(new[,] { { "D", "D", "B" }, { "L", "L", "L" }, { "L", "L", "L" } }, config.Faces[FaceType.Down].Items);
            CollectionAssert.AreEqual(new[,] { { "R", "B", "B" }, { "R", "D", "D" }, { "R", "D", "D" } }, config.Faces[FaceType.Right].Items);
            CollectionAssert.AreEqual(new[,] { { "R", "R", "R" }, { "R", "R", "R" }, { "U", "U", "F" } }, config.Faces[FaceType.Upper].Items);
            CollectionAssert.AreEqual(new[,] { { "U", "U", "U" }, { "B", "B", "B" }, { "B", "B", "B" } }, config.Faces[FaceType.Back].Items);
            CollectionAssert.AreEqual(new[,] { { "F", "F", "D" }, { "F", "F", "D" }, { "F", "F", "D" } }, config.Faces[FaceType.Front].Items);
        }

        #endregion

        #region Helpers

        // CREATE CONFIGURATION BY EXECUTING: R F
        private static CubeConfiguration<string> CreateCubeConfiguration()
        {
            var upper = new[,] { { "U", "U", "F" }, { "U", "U", "F" }, { "L", "L", "L" } };
            var down = new[,] { { "R", "R", "R" }, { "D", "D", "B" }, { "D", "D", "B" } };
            var left = new[,] { { "L", "L", "D" }, { "L", "L", "D" }, { "L", "L", "B" } };
            var right = new[,] { { "U", "R", "R" }, { "U", "R", "R" }, { "F", "R", "R" } };
            var front = new[,] { { "F", "F", "F" }, { "F", "F", "F" }, { "D", "D", "D" } };
            var back = new[,] { { "U", "B", "B" }, { "U", "B", "B" }, { "U", "B", "B" } };

            return new CubeConfiguration<string>(upper, down, left, right, front, back);
        }

        private CubeConfiguration<string> CreateBlankCubeConfiguration()
        {
            return new CubeConfiguration<string>(3, "U", "D", "L", "R", "F", "B");
        }

        private class T
        {
            public string Id { private get; set; }

            public override string ToString()
            {
                return Id;
            }
        }

        #endregion
    }
}
