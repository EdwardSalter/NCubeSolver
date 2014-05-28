using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using NCubeSolver.Core;
using NCubeSolver.Core.UnitTestHelpers;
using NCubeSolver.Plugins.Solvers.Size3;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.UnitTests.Size3
{
    [TestFixture]
    public class MiddleLayerSolverTests
    {
        [Test]
        public async Task TopLayer_MiddleFrontLeftOnTopFrontCentre_MiddleFrontLeftIsCorrect()
        {
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.FrontClockwise, Rotations.LeftClockwise });
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopFrontLayer(configuration, solution);

            CubeConfigurationAssert.MiddleFrontLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleFrontRightOnTopFrontCentre_MiddleFrontRightIsCorrect()
        {
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.FrontAntiClockwise });
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopFrontLayer(configuration, solution);

            CubeConfigurationAssert.MiddleFrontRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleBackRightOnTopFrontCentre_MiddleBackRightIsCorrect()
        {
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.RightAntiClockwise, Rotations.UpperClockwise });
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopFrontLayer(configuration, solution);

            CubeConfigurationAssert.MiddleBackRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleBackLeftOnTopFrontCentre_MiddleBackLeftIsCorrect()
        {
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.LeftClockwise, Rotations.UpperAntiClockwise });
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopFrontLayer(configuration, solution);

            CubeConfigurationAssert.MiddleBackLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleBackLeftOnTopFrontCentre_MiddleBackLeftIsCorrect2()
        {
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.BackAntiClockwise, Rotations.Upper2 });
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopFrontLayer(configuration, solution);

            CubeConfigurationAssert.MiddleBackLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleFrontLeftOnTopLeftCentre_MiddleFrontLeftIsCorrect()
        {
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.LeftAntiClockwise });
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopLeftLayer(configuration, solution);

            CubeConfigurationAssert.MiddleFrontLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleBackLeftOnTopLeftCentre_MiddleBackLeftIsCorrect()
        {
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.LeftClockwise });
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopLeftLayer(configuration, solution);

            CubeConfigurationAssert.MiddleBackLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleFrontLeftOnTopLeftCentre_MiddleFrontLeftIsCorrect2()
        {
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.FrontClockwise, Rotations.UpperClockwise });
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopLeftLayer(configuration, solution);

            CubeConfigurationAssert.MiddleFrontLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleBackLeftOnTopLeftCentre_MiddleBackLeftIsCorrect2()
        {
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.BackAntiClockwise, Rotations.UpperAntiClockwise });
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopLeftLayer(configuration, solution);

            CubeConfigurationAssert.MiddleBackLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleFrontRightOnTopLeftCentre_MiddleFrontRightIsCorrect()
        {
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.RightClockwise, Rotations.Upper2 });
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopLeftLayer(configuration, solution);

            CubeConfigurationAssert.MiddleFrontRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleBackLeftOnTopBBackCentre_MiddleBackLeftIsCorrect()
        {
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.BackAntiClockwise });
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopBackLayer(configuration, solution);

            CubeConfigurationAssert.MiddleBackLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleBackRightOnTopBackCentre_MiddleBackRightIsCorrect()
        {
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.BackClockwise });
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopBackLayer(configuration, solution);

            CubeConfigurationAssert.MiddleBackRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleFrontBackOnTopBackCentre_MiddleFrontBackIsCorrect2()
        {
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.LeftAntiClockwise, Rotations.UpperClockwise });
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopBackLayer(configuration, solution);

            CubeConfigurationAssert.MiddleFrontLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleFrontRightOnTopBackCentre_MiddleFrontRightIsCorrect2()
        {
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.RightClockwise, Rotations.UpperAntiClockwise });
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopBackLayer(configuration, solution);

            CubeConfigurationAssert.MiddleFrontRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleFrontRightOnTopBackCentre_MiddleFrontRightIsCorrect()
        {
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.FrontAntiClockwise, Rotations.Upper2 });
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopBackLayer(configuration, solution);

            CubeConfigurationAssert.MiddleFrontRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleBackRightOnTopRightCentre_MiddleBackRightIsCorrect()
        {
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.RightAntiClockwise });
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopRightLayer(configuration, solution);

            CubeConfigurationAssert.MiddleBackRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleFrontRightOnTopRightCentre_MiddleFrontRightIsCorrect()
        {
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.RightClockwise });
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopRightLayer(configuration, solution);

            CubeConfigurationAssert.MiddleFrontRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleBackLeftOnTopRightCentre_MiddleBackLeftIsCorrect()
        {
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.BackAntiClockwise, Rotations.UpperClockwise });
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopRightLayer(configuration, solution);

            CubeConfigurationAssert.MiddleBackLeftIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleFrontRightOnTopRightCentre_MiddleFrontRightIsCorrect2()
        {
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.FrontAntiClockwise, Rotations.UpperAntiClockwise });
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopRightLayer(configuration, solution);

            CubeConfigurationAssert.MiddleFrontRightIsCorrect(configuration);
        }

        [Test]
        public async Task TopLayer_MiddleFrontLeftOnTopRightCentre_MiddleFrontLeftIsCorrect()
        {
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.LeftAntiClockwise, Rotations.Upper2 });
            var solution = new List<IRotation>();
            var solver = new MiddleLayerSolver();

            await solver.CheckTopRightLayer(configuration, solution);

            CubeConfigurationAssert.MiddleFrontLeftIsCorrect(configuration);
        }
    }
}