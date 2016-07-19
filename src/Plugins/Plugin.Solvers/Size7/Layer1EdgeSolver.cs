using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size7
{
    internal class Layer1EdgeSolver : IPartialSolver
    {
        public async Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration)
        {
            var solution = new List<IRotation>();
            
            // Solve Top Face
            var wantedColour = configuration.Faces[FaceType.Upper].Centre;

            await CheckFront(configuration, solution, wantedColour).ConfigureAwait(false);
            await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration).ConfigureAwait(false);
            await CheckFront(configuration, solution, wantedColour).ConfigureAwait(false);
            await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration).ConfigureAwait(false);
            await CheckFront(configuration, solution, wantedColour).ConfigureAwait(false);
            await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration).ConfigureAwait(false);
            await CheckFront(configuration, solution, wantedColour).ConfigureAwait(false);

            return solution;
        }

        private static async Task<bool> CheckFront(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution, FaceColour wantedColour)
        {
            bool left, right;
            int timesInvoked = 0;
            do
            {
                left = await CheckFrontLeft(configuration, solution, wantedColour).ConfigureAwait(false);
                right = await CheckFrontRight(configuration, solution, wantedColour).ConfigureAwait(false);
                timesInvoked++;
            } while (!left && !right);

            return timesInvoked > 1;
        }

        private static async Task<bool> CheckFrontLeft(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution, FaceColour wantedColour)
        {
            var hasColour = await RotateFaceUntilLayer1OffsetIsColour(configuration, FaceType.Front, wantedColour, Edge.Left, solution).ConfigureAwait(false);
            if (!hasColour) return false;

            var layerIsNotSolved = await RotateFaceUntilLayer1OffsetIsNotColour(configuration, FaceType.Upper, wantedColour, Edge.Left, solution).ConfigureAwait(false);
            if (!layerIsNotSolved) throw new SolveFailureException("Layer 1 is expected to be unsolved in this case");

            var rotationsToAdd = new[]
            {
                Rotations.ByFace(FaceType.Left, RotationDirection.AntiClockwise, 2),
                Rotations.UpperClockwise,
                Rotations.SecondLayerRightClockwise,
                Rotations.UpperAntiClockwise,
                Rotations.ByFace(FaceType.Left, RotationDirection.Clockwise, 2),
                Rotations.UpperClockwise,
                Rotations.SecondLayerRightAntiClockwise,
            };
            await CommonActions.ApplyAndAddRotations(rotationsToAdd, solution, configuration).ConfigureAwait(false);

            return true;
        }

        private static async Task<bool> CheckFrontRight(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution, FaceColour wantedColour)
        {
            var hasColour = await RotateFaceUntilLayer1OffsetIsColour(configuration, FaceType.Front, wantedColour, Edge.Right, solution).ConfigureAwait(false);
            if (!hasColour) return false;

            var layerIsNotSolved = await RotateFaceUntilLayer1OffsetIsNotColour(configuration, FaceType.Upper, wantedColour, Edge.Right, solution).ConfigureAwait(false);
            if (!layerIsNotSolved) throw new SolveFailureException("Layer 1 is expected to be unsolved in this case");

            var rotationsToAdd = new[]
            {
                Rotations.ByFace(FaceType.Right, RotationDirection.Clockwise, 2),
                Rotations.UpperAntiClockwise,
                Rotations.SecondLayerLeftAntiClockwise,
                Rotations.UpperClockwise,
                Rotations.ByFace(FaceType.Right, RotationDirection.AntiClockwise, 2),
                Rotations.UpperAntiClockwise,
                Rotations.SecondLayerLeftClockwise,
            };
            await CommonActions.ApplyAndAddRotations(rotationsToAdd, solution, configuration).ConfigureAwait(false);

            return true;
        }

        private static async Task<bool> RotateFaceUntilLayer1OffsetIsNotColour(CubeConfiguration<FaceColour> configuration, FaceType faceType, FaceColour wantedColour, Edge edge, ICollection<IRotation> solution)
        {
            if (edge != Edge.Left && edge != Edge.Right) throw new ArgumentOutOfRangeException(nameof(edge), "Only left and right edges are supported here");
            var nearIndex = edge == Edge.Left ? 2 : 4;
            var farIndex = edge == Edge.Left ? 4 : 2;

            var face = configuration.Faces[faceType];
            // Check top edge
            if (face.GetEdge(1, Edge.Top)[nearIndex] != wantedColour)
            {
                return true;
            }

            // Left edge
            if (face.GetEdge(1, Edge.Left)[nearIndex] != wantedColour)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.ByFace(faceType, RotationDirection.Clockwise), solution, configuration).ConfigureAwait(false);
                return true;
            }

            // Bottom edge
            if (face.GetEdge(1, Edge.Bottom)[farIndex] != wantedColour)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.ByFaceTwice(faceType), solution, configuration).ConfigureAwait(false);
                return true;
            }

            // Right edge
            if (face.GetEdge(1, Edge.Right)[farIndex] != wantedColour)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.ByFace(faceType, RotationDirection.AntiClockwise), solution, configuration).ConfigureAwait(false);
                return true;
            }

            return false;
        }

        private static async Task<bool> RotateFaceUntilLayer1OffsetIsColour(CubeConfiguration<FaceColour> configuration, FaceType faceType, FaceColour wantedColour, Edge edge, ICollection<IRotation> solution)
        {
            if (edge != Edge.Left && edge != Edge.Right) throw new ArgumentOutOfRangeException(nameof(edge), "Only left and right edges are supported here");
            var nearIndex = edge == Edge.Left ? 2 : 4;
            var farIndex = edge == Edge.Left ? 4 : 2;

            var face = configuration.Faces[faceType];
            // Check top edge
            if (face.GetEdge(1, Edge.Top)[nearIndex] == wantedColour)
            {
                return true;
            }

            // Left edge
            if (face.GetEdge(1, Edge.Left)[nearIndex] == wantedColour)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.ByFace(faceType, RotationDirection.Clockwise), solution, configuration).ConfigureAwait(false);
                return true;
            }

            // Bottom edge
            if (face.GetEdge(1, Edge.Bottom)[farIndex] == wantedColour)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.ByFaceTwice(faceType), solution, configuration).ConfigureAwait(false);
                return true;
            }

            // Right edge
            if (face.GetEdge(1, Edge.Right)[nearIndex] == wantedColour)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.ByFace(faceType, RotationDirection.AntiClockwise), solution, configuration).ConfigureAwait(false);
                return true;
            }

            return false;
        }
    }
}
