using System.Collections.Generic;
using System.Threading.Tasks;
using NCubeSolver.Core;
using NCubeSolver.Core.UnitTestHelpers;
using NCubeSolver.Plugins.Solvers.Size3;
using NCubeSolvers.Core;
using NUnit.Framework;

namespace NCubeSolver.Plugins.Solvers.UnitTests.Size3
{
    public class BottomCrossSolverTests
    {
        [Test]
        public async Task Solve_GivenWhiteOnTopLeftFaceMatchingLeftCentre_BottomLeftCentreIsCorrect()
        {
            var solver = new BottomCrossSolver();
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.Left2 });
            var solution = new List<IRotation>();

            await solver.CheckTopFaceForWhite(configuration, solution);

            CubeConfigurationAssert.BottomLeftCentreIsCorrect(configuration);
        }

        [Test]
        public async Task Solve_GivenWhiteOnTopFrontFaceAndMatchingColourOnLeftFace_BottomLeftCentreIsCorrect()
        {
            var solver = new BottomCrossSolver();
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.Left2, Rotations.UpperClockwise });
            var solution = new List<IRotation>();

            await solver.CheckTopFaceForWhite(configuration, solution);

            CubeConfigurationAssert.BottomLeftCentreIsCorrect(configuration);
        }

        [Test]
        public async Task Solve_GivenWhiteOnTopFrontFaceAndMatchingColourOnRightFace_BottomRightCentreIsCorrect()
        {
            var solver = new BottomCrossSolver();
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.Right2, Rotations.UpperClockwise });
            var solution = new List<IRotation>();

            await solver.CheckTopFaceForWhite(configuration, solution);

            CubeConfigurationAssert.BottomRightCentreIsCorrect(configuration);
        }

        [Test]
        public async Task Solve_GivenWhiteOnFrontTopFaceAndMatchingColourOnRightFace_BottomRightCentreIsCorrect()
        {
            var solver = new BottomCrossSolver();
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.RightClockwise, Rotations.FrontAntiClockwise });
            var solution = new List<IRotation>();

            await solver.CheckTopLayerForWhite(configuration, solution);

            CubeConfigurationAssert.BottomRightCentreIsCorrect(configuration);
        }

        [Test]
        public async Task Solve_GivenWhiteOnFrontTopFaceAndMatchingColourOnLeftFace_BottomLeftCentreIsCorrect()
        {
            var solver = new BottomCrossSolver();
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.LeftAntiClockwise, Rotations.FrontClockwise });
            var solution = new List<IRotation>();

            await solver.CheckTopLayerForWhite(configuration, solution);

            CubeConfigurationAssert.BottomLeftCentreIsCorrect(configuration);
        }

        [Test]
        public async Task Solve_GivenWhiteOnFrontTopFaceAndMatchingColourOnBackFace_BottomBackCentreIsCorrect()
        {
            var solver = new BottomCrossSolver();
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.BackClockwise, Rotations.RightAntiClockwise, Rotations.UpperClockwise });
            var solution = new List<IRotation>();

            await solver.CheckTopLayerForWhite(configuration, solution);

            CubeConfigurationAssert.BottomBackCentreIsCorrect(configuration);
        }

        [Test]
        public async Task Solve_GivenWhiteOnFrontTopFaceAndMatchingColourOnFrontFace_BottomFrontCentreIsCorrect()
        {
            var solver = new BottomCrossSolver();
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.FrontClockwise, Rotations.LeftAntiClockwise, Rotations.UpperAntiClockwise });
            var solution = new List<IRotation>();

            await solver.CheckTopLayerForWhite(configuration, solution);

            CubeConfigurationAssert.BottomFrontCentreIsCorrect(configuration);
        }

        [Test]
        public async Task Solve_GivenWhiteOnMiddleRightFaceAndMatchingColourOnLeftFace_BottomLeftCentreIsCorrect()
        {
            var solver = new BottomCrossSolver();
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.LeftAntiClockwise, Rotations.Front2 });
            var solution = new List<IRotation>();

            await solver.CheckMiddleLayerForWhite(configuration, solution);

            CollectionAssert.IsSubsetOf(new[] { Rotations.RightClockwise, Rotations.UpperAntiClockwise, Rotations.RightAntiClockwise }, solution);
        }

        [Test]
        public async Task Solve_GivenWhiteOnMiddleLeftFaceAndMatchingColourOnRightFace_PlacesWhiteOnToTopLayer()
        {
            var solver = new BottomCrossSolver();
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.RightClockwise, Rotations.Front2 });
            var solution = new List<IRotation>();

            await solver.CheckMiddleLayerForWhite(configuration, solution);

            CollectionAssert.IsSubsetOf(new[] { Rotations.LeftAntiClockwise, Rotations.UpperAntiClockwise, Rotations.LeftClockwise }, solution);
        }

        [Test]
        public async Task Solve_GivenWhiteOnMiddleLeftFaceAndMatchingColourOnRightFace_PlacesWhiteOnToBottomLayer()
        {
            var solver = new BottomCrossSolver();
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.LeftAntiClockwise});
            var solution = new List<IRotation>();

            await solver.CheckMiddleLayerForWhite(configuration, solution);

            CollectionAssert.IsSubsetOf(new[] { Rotations.LeftClockwise }, solution);
        }

        [Test]
        public async Task Solve_GivenWhiteInBottomFrontLayerAndJoiningFaceIsOnRight_ExecutesAReverseFrontReverseRightFront()
        {
            var solver = new BottomCrossSolver();
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.RightClockwise, Rotations.FrontClockwise });
            var solution = new List<IRotation>();

            await solver.CheckBottomLayerForWhite(configuration, solution);

            CollectionAssert.IsSubsetOf(new[] { Rotations.FrontAntiClockwise, Rotations.RightAntiClockwise, Rotations.FrontClockwise }, solution);
        }

        [Test]
        public async Task Solve_GivenWhiteInBottomFrontLayerAndJoiningFaceIsOnLeft_ExecutesAFrontLeftReverseFront()
        {
            var solver = new BottomCrossSolver();
            var configuration = Helpers.CreateConfiguration(new[] { Rotations.LeftAntiClockwise, Rotations.FrontAntiClockwise });
            var solution = new List<IRotation>();

            await solver.CheckBottomLayerForWhite(configuration, solution);

            CollectionAssert.IsSubsetOf(new[] { Rotations.FrontClockwise, Rotations.LeftClockwise, Rotations.FrontAntiClockwise }, solution);
        }
    }
}