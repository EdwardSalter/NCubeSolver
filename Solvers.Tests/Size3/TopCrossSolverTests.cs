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
    public class TopCrossSolverTests
    {
        [Test]
        public async Task LineOnTop_GivenHorizontalLine_CreatesACrossOnTop()
        {
            var configuration = CreateConfigurationWithHorizontalLine();
            var solver = new TopCrossSolver();

            await solver.Solve(configuration);

            CubeConfigurationAssert.FaceHasCrossOfColour(configuration, FaceType.Upper, FaceColour.Yellow);
        }

        [Test]
        public async Task LineOnTop_GivenVerticalLine_CreatesACrossOnTop()
        {
            var config = Helpers.CreateSolvedMiddleLayerConfiguration(new[] { Rotations.Right2, Rotations.FrontAntiClockwise });
            await config.Rotate(Rotations.UpperClockwise);
            var configuration = config;
            var solver = new TopCrossSolver();

            await solver.Solve(configuration);

            CubeConfigurationAssert.FaceHasCrossOfColour(configuration, FaceType.Upper, FaceColour.Yellow);
        }

        [Test]
        public async Task LineOnTop_GivenLShapeOnBackLeft_CreatesACrossOnTop()
        {
            var configuration = CreateConfigurationWithLOnBackAndLeft();
            var solver = new TopCrossSolver();

            await solver.Solve(configuration);

            CubeConfigurationAssert.FaceHasCrossOfColour(configuration, FaceType.Upper, FaceColour.Yellow);
        }

        [Test]
        public async Task LineOnTop_GivenLShapeOnBackRight_CreatesACrossOnTop()
        {
            var configuration = CreateConfigurationWithLOnBackAndLeft();
            await configuration.Rotate(Rotations.UpperClockwise);
            var solver = new TopCrossSolver();

            await solver.Solve(configuration);

            CubeConfigurationAssert.FaceHasCrossOfColour(configuration, FaceType.Upper, FaceColour.Yellow);
        }

        [Test]
        public async Task LineOnTop_GivenLShapeOnFrontRight_CreatesACrossOnTop()
        {
            var configuration = CreateConfigurationWithLOnBackAndLeft();
            await configuration.Rotate(Rotations.Upper2);
            var solver = new TopCrossSolver();

            await solver.Solve(configuration);

            CubeConfigurationAssert.FaceHasCrossOfColour(configuration, FaceType.Upper, FaceColour.Yellow);
        }

        [Test]
        public async Task LineOnTop_GivenLShapeOnFrontLeft_CreatesACrossOnTop()
        {
            var configuration = CreateConfigurationWithLOnBackAndLeft();
            await configuration.Rotate(Rotations.UpperAntiClockwise);
            var solver = new TopCrossSolver();

            await solver.Solve(configuration);

            CubeConfigurationAssert.FaceHasCrossOfColour(configuration, FaceType.Upper, FaceColour.Yellow);
        }

        [Test]
        public async Task LineOnTop_GivenDot_CreatesACrossOnTop()
        {
            var configuration = CreateConfigurationWithDot();
            var solver = new TopCrossSolver();

            await solver.Solve(configuration);

            CubeConfigurationAssert.FaceHasCrossOfColour(configuration, FaceType.Upper, FaceColour.Yellow);
        }

        [Test]
        public async Task Permute_GivenAnAlreadySolvedCrossRotatedClockwise_CompletesSolvedCross()
        {
            var configuration = CreateConfigurationWithSolvedCross();
            await configuration.Rotate(Rotations.UpperClockwise);
            var solver = new TopCrossSolver();

            await solver.Solve(configuration);

            CubeConfigurationAssert.TopLayerCrossIsCorrect(configuration);
        }

        [Test]
        public async Task Permute_GivenAnAlreadySolvedCrossRotatedAntiClockwise_CompletesSolvedCross()
        {
            var configuration = CreateConfigurationWithSolvedCross();
            await configuration.Rotate(Rotations.UpperAntiClockwise);
            var solver = new TopCrossSolver();

            await solver.Solve(configuration);

            CubeConfigurationAssert.TopLayerCrossIsCorrect(configuration);
        }

        [Test]
        public async Task Permute_GivenAnAlreadySolvedCrossRotatedTwice_CompletesSolvedCross()
        {
            var configuration = CreateConfigurationWithSolvedCross();
            await configuration.Rotate(Rotations.Upper2);
            var solver = new TopCrossSolver();

            await solver.Solve(configuration);

            CubeConfigurationAssert.TopLayerCrossIsCorrect(configuration);
        }

        [Test]
        public async Task Permute_GivenAnAlreadySolvedCross_CompletesSolvedCross()
        {
            var configuration = CreateConfigurationWithSolvedCross();
            var solver = new TopCrossSolver();

            await solver.Solve(configuration);

            CubeConfigurationAssert.TopLayerCrossIsCorrect(configuration);
        }

        [Test]
        public async Task Permute_GivenCrossWithFrontAndBackReversed_CompletesSolvedCross()
        {
            var configuration = CreateConfigurationWithCrossWithOneOpposingEdge();
            var solver = new TopCrossSolver();

            await solver.Solve(configuration);

            CubeConfigurationAssert.TopLayerCrossIsCorrect(configuration);
        }

        [Test]
        public async Task Permute_GivenCrossWithDiagonalsReversed_CompletesSolvedCross()
        {
            var configuration = CreateConfigurationWithCrossWithOneOpposingEdge();
            await configuration.Rotate(Rotations.UpperClockwise);
            var solver = new TopCrossSolver();

            await solver.Solve(configuration);

            CubeConfigurationAssert.TopLayerCrossIsCorrect(configuration);
        }

        [Test]
        public async Task Permute_GivenCrossWithFrontCorrectOthersNeedClockwisePermutation_CompletesSolvedCross()
        {
            var configuration = Helpers.CreateSolvedMiddleLayerConfiguration(new[] { Rotations.DownAntiClockwise, Rotations.BackClockwise });
            var solver = new TopCrossSolver();

            await solver.Solve(configuration);

            CubeConfigurationAssert.TopLayerCrossIsCorrect(configuration);
        }

        [Test]
        public async Task Permute_GivenCrossWithLeftCorrectOthersNeedAntiClockwisePermutation_CompletesSolvedCross()
        {
            var configuration = Helpers.CreateSolvedMiddleLayerConfiguration(new[] { Rotations.DownAntiClockwise, Rotations.BackClockwise });
            var solver = new TopCrossSolver();
            await solver.SolveCross(configuration, new List<IRotation>());
            await configuration.Rotate(Rotations.Upper2);

            await solver.Solve(configuration);

            CubeConfigurationAssert.TopLayerCrossIsCorrect(configuration);
        }

        private static CubeConfiguration<FaceColour> CreateConfigurationWithHorizontalLine()
        {
            return Helpers.CreateSolvedMiddleLayerConfiguration(new[] { Rotations.Right2, Rotations.FrontAntiClockwise });
        }

        private static CubeConfiguration<FaceColour> CreateConfigurationWithLOnBackAndLeft()
        {
            return Helpers.CreateSolvedMiddleLayerConfiguration(new[] { Rotations.RightAntiClockwise, Rotations.Upper2 });
        }

        private static CubeConfiguration<FaceColour> CreateConfigurationWithDot()
        {
            return Helpers.CreateSolvedMiddleLayerConfiguration(new[] { Rotations.DownAntiClockwise, Rotations.FrontClockwise });
        }

        private static CubeConfiguration<FaceColour> CreateConfigurationWithSolvedCross()
        {
            return Helpers.CreateSolvedMiddleLayerConfiguration(new[] { Rotations.Back2, Rotations.UpperAntiClockwise });
        }

        private static CubeConfiguration<FaceColour> CreateConfigurationWithCrossWithOneOpposingEdge()
        {
            return Helpers.CreateSolvedMiddleLayerConfiguration(new[] { Rotations.LeftAntiClockwise, Rotations.UpperAntiClockwise });
        }
    }
}