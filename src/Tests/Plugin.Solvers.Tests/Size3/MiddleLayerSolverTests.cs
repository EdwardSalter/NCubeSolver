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
    public class MiddleLayerSolverTests
    {
        [Test]
        public async Task TopLayer_MiddleFrontLeftOnTopFrontCentre_MiddleFrontLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.FrontClockwise, Rotations.LeftClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopFrontLayer(configuration, solution);

            CubeConfigurationAssert.MiddleFrontLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleFrontRightOnTopFrontCentre_MiddleFrontRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.FrontAntiClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopFrontLayer(configuration, solution);

            CubeConfigurationAssert.MiddleFrontRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleBackRightOnTopFrontCentre_MiddleBackRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.RightAntiClockwise, Rotations.UpperClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopFrontLayer(configuration, solution);

            CubeConfigurationAssert.MiddleBackRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleBackLeftOnTopFrontCentre_MiddleBackLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.LeftClockwise, Rotations.UpperAntiClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopFrontLayer(configuration, solution);

            CubeConfigurationAssert.MiddleBackLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleBackLeftOnTopFrontCentre_MiddleBackLeftIsCorrect2()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.BackAntiClockwise, Rotations.Upper2 }, 3);
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopFrontLayer(configuration, solution);

            CubeConfigurationAssert.MiddleBackLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleFrontLeftOnTopLeftCentre_MiddleFrontLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.LeftAntiClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopLeftLayer(configuration, solution);

            CubeConfigurationAssert.MiddleFrontLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleBackLeftOnTopLeftCentre_MiddleBackLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.LeftClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopLeftLayer(configuration, solution);

            CubeConfigurationAssert.MiddleBackLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleFrontLeftOnTopLeftCentre_MiddleFrontLeftIsCorrect2()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.FrontClockwise, Rotations.UpperClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopLeftLayer(configuration, solution);

            CubeConfigurationAssert.MiddleFrontLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleBackLeftOnTopLeftCentre_MiddleBackLeftIsCorrect2()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.BackAntiClockwise, Rotations.UpperAntiClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopLeftLayer(configuration, solution);

            CubeConfigurationAssert.MiddleBackLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleFrontRightOnTopLeftCentre_MiddleFrontRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.RightClockwise, Rotations.Upper2 }, 3);
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopLeftLayer(configuration, solution);

            CubeConfigurationAssert.MiddleFrontRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleBackLeftOnTopBBackCentre_MiddleBackLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.BackAntiClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopBackLayer(configuration, solution);

            CubeConfigurationAssert.MiddleBackLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleBackRightOnTopBackCentre_MiddleBackRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.BackClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopBackLayer(configuration, solution);

            CubeConfigurationAssert.MiddleBackRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleFrontBackOnTopBackCentre_MiddleFrontBackIsCorrect2()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.LeftAntiClockwise, Rotations.UpperClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopBackLayer(configuration, solution);

            CubeConfigurationAssert.MiddleFrontLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleFrontRightOnTopBackCentre_MiddleFrontRightIsCorrect2()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.RightClockwise, Rotations.UpperAntiClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopBackLayer(configuration, solution);

            CubeConfigurationAssert.MiddleFrontRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleFrontRightOnTopBackCentre_MiddleFrontRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.FrontAntiClockwise, Rotations.Upper2 }, 3);
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopBackLayer(configuration, solution);

            CubeConfigurationAssert.MiddleFrontRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleBackRightOnTopRightCentre_MiddleBackRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.RightAntiClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopRightLayer(configuration, solution);

            CubeConfigurationAssert.MiddleBackRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleFrontRightOnTopRightCentre_MiddleFrontRightIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.RightClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopRightLayer(configuration, solution);

            CubeConfigurationAssert.MiddleFrontRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleBackLeftOnTopRightCentre_MiddleBackLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.BackAntiClockwise, Rotations.UpperClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopRightLayer(configuration, solution);

            CubeConfigurationAssert.MiddleBackLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleFrontRightOnTopRightCentre_MiddleFrontRightIsCorrect2()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.FrontAntiClockwise, Rotations.UpperAntiClockwise }, 3);
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopRightLayer(configuration, solution);

            CubeConfigurationAssert.MiddleFrontRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleFrontLeftOnTopRightCentre_MiddleFrontLeftIsCorrect()
        {
            var configuration = UnitTests.Helpers.CreateConfiguration(new[] { Rotations.LeftAntiClockwise, Rotations.Upper2 }, 3);
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopRightLayer(configuration, solution);

            CubeConfigurationAssert.MiddleFrontLeftIsCorrect(configuration);
        }
    }
}