using System.Collections.Generic;
using System.Threading.Tasks;
using NCubeSolver.Core;
using NCubeSolver.Core.UnitTestHelpers;
using NCubeSolver.Plugins.Solvers.Size3;
using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.UnitTests.Size3
{
    [TestFixture]
    public class BottomLayerSolverTests
    {
        #region Top Layer

        #region Front Face
        [Test]
        public async Task TopLayer_GivenWhiteInFrontLeftLayerFromFrontLeft_FrontLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.LeftAntiClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomFrontLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_GivenWhiteInFrontLeftLayerFromFrontRight_FrontRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.FrontAntiClockwise, Rotations.UpperClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomFrontRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_GivenWhiteInFrontLeftLayerFromBackRight_BackRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.RightAntiClockwise, Rotations.Upper2 }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomBackRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_GivenWhiteInFrontLeftLayerFromBackLeft_BackLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.BackAntiClockwise, Rotations.UpperAntiClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomBackLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_GivenWhiteInFrontRightLayerFromFrontRight_FrontRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.RightClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomFrontRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_GivenWhiteInFrontRightLayerFromBackRight_BackRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.BackClockwise, Rotations.UpperClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomBackRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_GivenWhiteInFrontRightLayerFromBackLeft_BackLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.LeftClockwise, Rotations.Upper2 }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomBackLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_GivenWhiteInFrontRightLayerFromFrontLeft_FrontLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.FrontClockwise, Rotations.UpperAntiClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomFrontLeftIsCorrect(configuration);
        }

        #endregion


        #region Back Face
        [Test]
        public async Task TopLayer_GivenWhiteInBackLeftLayerFromFrontLeft_FrontLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.FrontClockwise, Rotations.UpperClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomFrontLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_GivenWhiteInBackLeftLayerFromFrontRight_FrontRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.RightClockwise, Rotations.Upper2 }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomFrontRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_GivenWhiteInBackLeftLayerFromBackRight_BackRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.BackClockwise, Rotations.UpperAntiClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomBackRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_GivenWhiteInBackLeftLayerFromBackLeft_BackLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.LeftClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomBackLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_GivenWhiteInBackRightLayerFromFrontRight_FrontRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.FrontAntiClockwise, Rotations.UpperAntiClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomFrontRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_GivenWhiteInBackRightLayerFromBackRight_BackRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.RightAntiClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomBackRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_GivenWhiteInBackRightLayerFromBackLeft_BackLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.BackAntiClockwise, Rotations.UpperClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomBackLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_GivenWhiteInBackRightLayerFromFrontLeft_FrontLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.LeftAntiClockwise, Rotations.Upper2 }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomFrontLeftIsCorrect(configuration);
        }

        #endregion

        #region Left Face
        [Test]
        public async Task TopLayer_GivenWhiteInLeftBackLayerFromFrontLeft_FrontLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.LeftAntiClockwise, Rotations.UpperClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomFrontLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_GivenWhiteInLeftBackLayerFromFrontRight_FrontRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.FrontAntiClockwise, Rotations.Upper2 }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomFrontRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_GivenWhiteInLeftBackLayerFromBackRight_BackRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.RightAntiClockwise, Rotations.UpperAntiClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomBackRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_GivenWhiteInLeftBackLayerFromBackLeft_BackLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.BackAntiClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomBackLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_GivenWhiteInLeftFrontLayerFromFrontRight_FrontRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.RightClockwise, Rotations.UpperClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomFrontRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_GivenWhiteInLeftFrontLayerFromBackRight_BackRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.BackClockwise, Rotations.Upper2 }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomBackRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_GivenWhiteInLeftFrontLayerFromBackLeft_BackLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.LeftClockwise, Rotations.UpperAntiClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomBackLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_GivenWhiteInLeftFrontLayerFromFrontLeft_FrontLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.FrontClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomFrontLeftIsCorrect(configuration);
        }

        #endregion


        #region Right Face

        [Test]
        public async Task TopLayer_GivenWhiteInRightBackLayerFromFrontLeft_FrontLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.FrontClockwise, Rotations.Upper2 }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomFrontLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_GivenWhiteInRightBackLayerFromFrontRight_FrontRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.RightClockwise, Rotations.UpperAntiClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomFrontRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_GivenWhiteInRightBackLayerFromBackRight_BackRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.BackClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomBackRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_GivenWhiteInRightBackLayerFromBackLeft_BackLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.LeftClockwise, Rotations.UpperClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomBackLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_GivenWhiteInRightFrontLayerFromFrontRight_FrontRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.FrontAntiClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomFrontRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_GivenWhiteInRightFrontLayerFromBackRight_BackRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.RightAntiClockwise, Rotations.UpperClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomBackRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_GivenWhiteInRightFrontLayerFromBackLeft_BackLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.BackAntiClockwise, Rotations.Upper2 }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomBackLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_GivenWhiteInRightFrontLayerFromFrontLeft_FrontLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.LeftAntiClockwise, Rotations.UpperAntiClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomFrontLeftIsCorrect(configuration);
        }

        #endregion

        #endregion


        [Test]
        public async Task TopFace_GivenWhiteOnBackLeftFromFrontLeft_FrontLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.Left2 }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopFaceForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomFrontLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopFace_GivenWhiteOnBackLeftFromBackLeft_BackLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.Left2, Rotations.UpperClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopFaceForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomBackLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopFace_GivenWhiteOnBackLeftFromFrontRight_FrontRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.Right2, Rotations.UpperAntiClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopFaceForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomFrontRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopFace_GivenWhiteOnBackRightFromFrontRight_FrontRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.Right2 }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopFaceForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomFrontRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopFace_GivenWhiteOnBackRightFromBackRight_BackRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.Right2, Rotations.BackClockwise, Rotations.UpperAntiClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopFaceForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomBackRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopFace_GivenWhiteOnBackRightFromFrontLeft_FrontLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.Left2, Rotations.FrontClockwise, Rotations.UpperClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopFaceForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomFrontLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopFace_GivenWhiteOnFrontRightFromBackRight_BackRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.Right2, Rotations.BackClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopFaceForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomBackRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopFace_GivenWhiteOnFrontRightFromFrontRight_FrontRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.Right2, Rotations.UpperClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopFaceForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomFrontRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopFace_GivenWhiteOnFrontRightFromBackLeft_BackLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.Left2, Rotations.UpperAntiClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopFaceForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomBackLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopFace_GivenWhiteOnFrontLeftFromBackLeft_BackLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.Left2, Rotations.BackClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopFaceForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomBackLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopFace_GivenWhiteOnFrontLeftFromFrontLeft_FrontLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.Left2, Rotations.UpperAntiClockwise, Rotations.RightClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopFaceForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomFrontLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopFace_GivenWhiteOnFrontLeftFromBackRight_BackRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.Right2, Rotations.UpperClockwise, Rotations.RightClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckTopFaceForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomBackRightIsCorrect(configuration);
        }

        [Test]
        public async Task BottomLayer_GivenWhiteOnFrontRightFromBackRight_BackRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.RightClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckBottomLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomBackRightIsCorrect(configuration);
        }

        [Test]
        public async Task BottomLayer_GivenWhiteOnFrontLeftFromBackLeft_BackLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.LeftAntiClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckBottomLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomBackLeftIsCorrect(configuration);
        }

        [Test]
        public async Task BottomLayer_GivenWhiteOnRightFrontFromFrontLeft_FrontLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.FrontAntiClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckBottomLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomFrontLeftIsCorrect(configuration);
        }

        [Test]
        public async Task BottomLayer_GivenWhiteOnLeftFrontFromFrontRight_FrontRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.FrontClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckBottomLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomFrontRightIsCorrect(configuration);
        }

        [Test]
        public async Task BottomLayer_GivenWhiteOnRightBackFromBackLeft_BackLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.BackClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckBottomLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomBackLeftIsCorrect(configuration);
        }

        [Test]
        public async Task BottomLayer_GivenWhiteOnLeftBackFromBackRight_BackRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.BackAntiClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckBottomLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomBackRightIsCorrect(configuration);
        }

        [Test]
        public async Task BottomLayer_GivenWhiteOnBackRightFromFrontRight_FrontRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.RightAntiClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckBottomLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomFrontRightIsCorrect(configuration);
        }

        [Test]
        public async Task BottomLayer_GivenWhiteOnBackLeftFromFrontLeft_FrontLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.LeftClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new BottomLayerSolver();

            await solver.CheckBottomLayerForWhite(configuration, solution).ConfigureAwait(true);


            CornerAssert.BottomFrontLeftIsCorrect(configuration);
        }
    }
}