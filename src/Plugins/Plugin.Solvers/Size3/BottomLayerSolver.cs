﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size3
{
    internal class BottomLayerSolver : IPartialSolver
    {
        public async Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration)
        {
            var solution = new List<IRotation>();

            var rotationToBottom = await CommonActions.PositionOnBottom(configuration, FaceColour.White).ConfigureAwait(false);

            if (rotationToBottom != null) solution.Add(rotationToBottom);

            await Repeat.SolvingUntilNoMovesCanBeMade(solution, async () =>
            {
                await CheckTopLayerForWhite(configuration, solution).ConfigureAwait(false);

                await CheckTopFaceForWhite(configuration, solution).ConfigureAwait(false);

                await CheckBottomLayerForWhite(configuration, solution).ConfigureAwait(false);

                await CheckBottomFaceForWhite(configuration, solution).ConfigureAwait(false);

            }).ConfigureAwait(false);

            return solution;
        }

        internal async Task CheckBottomFaceForWhite(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            if (configuration.Faces[FaceType.Down].TopRight() == FaceColour.White && configuration.Faces[FaceType.Front].BottomRight() != configuration.Faces[FaceType.Front].Centre)
            {
                await SortBottom(configuration, solution).ConfigureAwait(false);

            }
            if (configuration.Faces[FaceType.Down].TopLeft() == FaceColour.White && configuration.Faces[FaceType.Front].BottomLeft() != configuration.Faces[FaceType.Front].Centre)
            {
                await CommonActions.ApplyAndAddRotation(CubeRotations.YAntiClockwise, solution, configuration).ConfigureAwait(false);

                await SortBottom(configuration, solution).ConfigureAwait(false);

            }
            if (configuration.Faces[FaceType.Down].BottomLeft() == FaceColour.White && configuration.Faces[FaceType.Back].BottomRight() != configuration.Faces[FaceType.Back].Centre)
            {
                await CommonActions.ApplyAndAddRotation(CubeRotations.Y2, solution, configuration).ConfigureAwait(false);

                await SortBottom(configuration, solution).ConfigureAwait(false);

            }
            if (configuration.Faces[FaceType.Down].BottomRight() == FaceColour.White && configuration.Faces[FaceType.Back].BottomLeft() != configuration.Faces[FaceType.Back].Centre)
            {
                await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration).ConfigureAwait(false);

                await SortBottom(configuration, solution).ConfigureAwait(false);

            }
        }

        private static async Task SortBottom(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CheckTopLayerForWhite(configuration, solution, FaceType.Left, false, true).ConfigureAwait(false);

        }

        internal async Task CheckTopLayerForWhite(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            await FrontTopLayer(configuration, solution).ConfigureAwait(false);

            await BackTopLayer(configuration, solution).ConfigureAwait(false);

            await RightTopLayer(configuration, solution).ConfigureAwait(false);

            await LeftTopLayer(configuration, solution).ConfigureAwait(false);

        }

        internal async Task CheckTopFaceForWhite(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            await BackLeftTopFace(configuration, solution).ConfigureAwait(false);

            await BackRightTopFace(configuration, solution).ConfigureAwait(false);

            await FrontRightTopFace(configuration, solution).ConfigureAwait(false);

            await FrontLeftTopFace(configuration, solution).ConfigureAwait(false);

        }

        internal async Task CheckBottomLayerForWhite(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            await BottomFrontLayer(configuration, solution).ConfigureAwait(false);

            await BottomRightLayer(configuration, solution).ConfigureAwait(false);

            await BottomBackLayer(configuration, solution).ConfigureAwait(false);

            await BottomLeftLayer(configuration, solution).ConfigureAwait(false);

        }

        private static async Task FrontTopLayer(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            if (configuration.Faces[FaceType.Front].TopLeft() == FaceColour.White)
            {
                var joiningColour = configuration.Faces[FaceType.Left].TopRight();
                var joiningPosition = FaceRules.RelativePositionBetweenFaces(FaceType.Left, FaceRules.GetFaceOfColour(joiningColour, configuration));

                switch (joiningPosition)
                {
                    case RelativePosition.Same:
                        await CommonActions.ApplyAndAddRotation(CubeRotations.YAntiClockwise, solution, configuration).ConfigureAwait(false);

                        break;
                    case RelativePosition.Right:
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                        break;

                    case RelativePosition.Left:
                        await CommonActions.ApplyAndAddRotation(CubeRotations.Y2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                        break;

                    case RelativePosition.Opposite:
                        await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                        break;
                }

                await TopLayerLeft(configuration, solution).ConfigureAwait(false);

            }
            if (configuration.Faces[FaceType.Front].TopRight() == FaceColour.White)
            {
                var joiningUpperColour = configuration.Faces[FaceType.Upper].BottomRight();
                var joiningUpperFace = FaceRules.GetFaceOfColour(joiningUpperColour, configuration);

                var relativePositionToUpperJoining = FaceRules.RelativePositionBetweenFaces(FaceType.Front, joiningUpperFace);
                switch (relativePositionToUpperJoining)
                {
                    case RelativePosition.Right:
                        await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                        break;

                    case RelativePosition.Left:
                        await CommonActions.ApplyAndAddRotation(CubeRotations.YAntiClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                        break;

                    case RelativePosition.Opposite:
                        await CommonActions.ApplyAndAddRotation(CubeRotations.Y2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                        break;
                }

                await TopLayerRight(configuration, solution).ConfigureAwait(false);

            }
        }

        private static async Task BackTopLayer(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            if (configuration.Faces[FaceType.Back].TopLeft() == FaceColour.White)
            {
                var joiningColour = configuration.Faces[FaceType.Right].TopRight();
                var joiningPosition = FaceRules.RelativePositionBetweenFaces(FaceType.Right, FaceRules.GetFaceOfColour(joiningColour, configuration));

                switch (joiningPosition)
                {
                    case RelativePosition.Same:
                        await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration).ConfigureAwait(false);

                        break;
                    case RelativePosition.Right:
                        await CommonActions.ApplyAndAddRotation(CubeRotations.Y2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                        break;

                    case RelativePosition.Left:
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                        break;

                    case RelativePosition.Opposite:
                        await CommonActions.ApplyAndAddRotation(CubeRotations.YAntiClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                        break;
                }

                await TopLayerLeft(configuration, solution).ConfigureAwait(false);

            }
            if (configuration.Faces[FaceType.Back].TopRight() == FaceColour.White)
            {
                var joiningUpperColour = configuration.Faces[FaceType.Upper].TopLeft();
                var joiningUpperFace = FaceRules.GetFaceOfColour(joiningUpperColour, configuration);

                var relativePositionToUpperJoining = FaceRules.RelativePositionBetweenFaces(FaceType.Back, joiningUpperFace);
                switch (relativePositionToUpperJoining)
                {
                    case RelativePosition.Same:
                        await CommonActions.ApplyAndAddRotation(CubeRotations.Y2, solution, configuration).ConfigureAwait(false);

                        break;
                    case RelativePosition.Right:
                        await CommonActions.ApplyAndAddRotation(CubeRotations.YAntiClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                        break;

                    case RelativePosition.Left:
                        await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                        break;

                    case RelativePosition.Opposite:
                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                        break;
                }

                await TopLayerRight(configuration, solution).ConfigureAwait(false);

            }
        }

        private static async Task RightTopLayer(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            if (configuration.Faces[FaceType.Right].TopLeft() == FaceColour.White)
            {
                var joiningColour = configuration.Faces[FaceType.Front].TopRight();
                var joiningPosition = FaceRules.RelativePositionBetweenFaces(FaceType.Front, FaceRules.GetFaceOfColour(joiningColour, configuration));

                switch (joiningPosition)
                {
                    case RelativePosition.Same:
                        break;
                    case RelativePosition.Right:
                        await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                        break;

                    case RelativePosition.Left:
                        await CommonActions.ApplyAndAddRotation(CubeRotations.YAntiClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                        break;

                    case RelativePosition.Opposite:
                        await CommonActions.ApplyAndAddRotation(CubeRotations.Y2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                        break;
                }

                await TopLayerLeft(configuration, solution).ConfigureAwait(false);

            }
            if (configuration.Faces[FaceType.Right].TopRight() == FaceColour.White)
            {
                var joiningUpperColour = configuration.Faces[FaceType.Upper].TopRight();
                var joiningUpperFace = FaceRules.GetFaceOfColour(joiningUpperColour, configuration);

                var relativePositionToUpperJoining = FaceRules.RelativePositionBetweenFaces(FaceType.Right, joiningUpperFace);
                switch (relativePositionToUpperJoining)
                {
                    case RelativePosition.Same:
                        await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration).ConfigureAwait(false);

                        break;

                    case RelativePosition.Right:
                        await CommonActions.ApplyAndAddRotation(CubeRotations.Y2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                        break;

                    case RelativePosition.Left:
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                        break;

                    case RelativePosition.Opposite:
                        await CommonActions.ApplyAndAddRotation(CubeRotations.YAntiClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                        break;
                }

                await TopLayerRight(configuration, solution).ConfigureAwait(false);

            }
        }

        private static async Task LeftTopLayer(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            if (configuration.Faces[FaceType.Left].TopLeft() == FaceColour.White)
            {
                var joiningColour = configuration.Faces[FaceType.Back].TopRight();
                var joiningPosition = FaceRules.RelativePositionBetweenFaces(FaceType.Back, FaceRules.GetFaceOfColour(joiningColour, configuration));

                switch (joiningPosition)
                {
                    case RelativePosition.Same:
                        await CommonActions.ApplyAndAddRotation(CubeRotations.Y2, solution, configuration).ConfigureAwait(false);

                        break;
                    case RelativePosition.Right:
                        await CommonActions.ApplyAndAddRotation(CubeRotations.YAntiClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                        break;

                    case RelativePosition.Left:
                        await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                        break;

                    case RelativePosition.Opposite:
                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                        break;
                }

                await TopLayerLeft(configuration, solution).ConfigureAwait(false);

            }
            if (configuration.Faces[FaceType.Left].TopRight() == FaceColour.White)
            {
                var joiningUpperColour = configuration.Faces[FaceType.Upper].BottomLeft();
                var joiningUpperFace = FaceRules.GetFaceOfColour(joiningUpperColour, configuration);

                var relativePositionToUpperJoining = FaceRules.RelativePositionBetweenFaces(FaceType.Left, joiningUpperFace);
                switch (relativePositionToUpperJoining)
                {
                    case RelativePosition.Same:
                        await CommonActions.ApplyAndAddRotation(CubeRotations.YAntiClockwise, solution, configuration).ConfigureAwait(false);

                        break;

                    case RelativePosition.Right:
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                        break;

                    case RelativePosition.Left:
                        await CommonActions.ApplyAndAddRotation(CubeRotations.Y2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                        break;

                    case RelativePosition.Opposite:
                        await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                        break;
                }

                await TopLayerRight(configuration, solution).ConfigureAwait(false);

            }
        }

        private static async Task TopLayerLeft(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration).ConfigureAwait(false);

        }

        private static async Task TopLayerRight(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration).ConfigureAwait(false);

        }

        private static async Task BottomFrontLayer(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            var face = configuration.Faces[FaceType.Front];
            if (face.BottomRight() == FaceColour.White)
            {
                await MoveCornerOffBottom(configuration, solution).ConfigureAwait(false);

                await FrontLeftTopFace(configuration, solution).ConfigureAwait(false);

            }
            else if (face.BottomLeft() == FaceColour.White)
            {
                await CommonActions.ApplyAndAddRotation(CubeRotations.YAntiClockwise, solution, configuration).ConfigureAwait(false);

                await MoveCornerOffBottom(configuration, solution).ConfigureAwait(false);

                await CheckTopLayerForWhite(configuration, solution, FaceType.Front, false, true).ConfigureAwait(false);

            }
        }

        private static async Task BottomRightLayer(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            var face = configuration.Faces[FaceType.Right];
            if (face.BottomRight() == FaceColour.White)
            {
                await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration).ConfigureAwait(false);

                await MoveCornerOffBottom(configuration, solution).ConfigureAwait(false);

                await FrontLeftTopFace(configuration, solution).ConfigureAwait(false);

            }
            else if (face.BottomLeft() == FaceColour.White)
            {
                await MoveCornerOffBottom(configuration, solution).ConfigureAwait(false);

                await CheckTopLayerForWhite(configuration, solution, FaceType.Front, false, true).ConfigureAwait(false);

            }
        }

        private static async Task BottomLeftLayer(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            var face = configuration.Faces[FaceType.Left];
            if (face.BottomRight() == FaceColour.White)
            {
                await CommonActions.ApplyAndAddRotation(CubeRotations.YAntiClockwise, solution, configuration).ConfigureAwait(false);

                await MoveCornerOffBottom(configuration, solution).ConfigureAwait(false);

                await FrontLeftTopFace(configuration, solution).ConfigureAwait(false);

            }
            else if (face.BottomLeft() == FaceColour.White)
            {
                await CommonActions.ApplyAndAddRotation(CubeRotations.Y2, solution, configuration).ConfigureAwait(false);

                await MoveCornerOffBottom(configuration, solution).ConfigureAwait(false);

                await CheckTopLayerForWhite(configuration, solution, FaceType.Front, false, true).ConfigureAwait(false);

            }
        }

        private static async Task BottomBackLayer(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            var face = configuration.Faces[FaceType.Back];
            if (face.BottomRight() == FaceColour.White)
            {
                await CommonActions.ApplyAndAddRotation(CubeRotations.Y2, solution, configuration).ConfigureAwait(false);

                await MoveCornerOffBottom(configuration, solution).ConfigureAwait(false);

                await FrontLeftTopFace(configuration, solution).ConfigureAwait(false);

            }
            else if (face.BottomLeft() == FaceColour.White)
            {
                await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration).ConfigureAwait(false);

                await MoveCornerOffBottom(configuration, solution).ConfigureAwait(false);

                await CheckTopLayerForWhite(configuration, solution, FaceType.Front, false, true).ConfigureAwait(false);

            }
        }

        private static async Task MoveCornerOffBottom(IRotatable configuration, ICollection<IRotation> solution)
        {
            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration).ConfigureAwait(false);

        }

        private static async Task FrontLeftTopFace(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            if (configuration.Faces[FaceType.Upper].BottomLeft() != FaceColour.White) return;

            var joiningLeft = configuration.Faces[FaceType.Left].TopRight();
            var faceOfJoiningColour = FaceRules.GetFaceOfColour(joiningLeft, configuration);
            var leftRelativePosition = FaceRules.RelativePositionBetweenFaces(faceOfJoiningColour, FaceType.Left);

            switch (leftRelativePosition)
            {
                case RelativePosition.Same:
                    await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                    await CommonActions.ApplyAndAddRotation(CubeRotations.Y2, solution, configuration).ConfigureAwait(false);

                    break;

                case RelativePosition.Right:
                    await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                    await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration).ConfigureAwait(false);

                    break;

                case RelativePosition.Left:
                    await CommonActions.ApplyAndAddRotation(CubeRotations.YAntiClockwise, solution, configuration).ConfigureAwait(false);

                    break;
            }

            await MoveTopCornerToBottom(configuration, solution).ConfigureAwait(false);

        }

        private static async Task FrontRightTopFace(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            if (configuration.Faces[FaceType.Upper].BottomRight() != FaceColour.White) return;

            var joiningRight = configuration.Faces[FaceType.Right].TopLeft();
            var rightRelativePosition = FaceRules.RelativePositionBetweenFaces(FaceRules.GetFaceOfColour(joiningRight, configuration), FaceType.Right);

            switch (rightRelativePosition)
            {
                case RelativePosition.Same:
                    await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                    await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration).ConfigureAwait(false);

                    break;

                case RelativePosition.Right:
                    break;

                case RelativePosition.Left:
                    await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                    await CommonActions.ApplyAndAddRotation(CubeRotations.Y2, solution, configuration).ConfigureAwait(false);

                    break;
            }

            await MoveTopCornerToBottom(configuration, solution).ConfigureAwait(false);

        }

        private static async Task BackRightTopFace(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            if (configuration.Faces[FaceType.Upper].TopRight() != FaceColour.White) return;

            var joiningRight = configuration.Faces[FaceType.Right].TopRight();
            var rightRelativePosition = FaceRules.RelativePositionBetweenFaces(FaceRules.GetFaceOfColour(joiningRight, configuration), FaceType.Right);

            switch (rightRelativePosition)
            {
                case RelativePosition.Same:
                    await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                    break;

                case RelativePosition.Right:
                    await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                    await CommonActions.ApplyAndAddRotation(CubeRotations.YAntiClockwise, solution, configuration).ConfigureAwait(false);

                    break;

                case RelativePosition.Left:
                    await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration).ConfigureAwait(false);

                    break;
            }

            await MoveTopCornerToBottom(configuration, solution).ConfigureAwait(false);

        }

        private static async Task BackLeftTopFace(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            if (configuration.Faces[FaceType.Upper].TopLeft() != FaceColour.White) return;

            var joiningLeft = configuration.Faces[FaceType.Left].TopLeft();
            var leftRelativePosition = FaceRules.RelativePositionBetweenFaces(FaceRules.GetFaceOfColour(joiningLeft, configuration), FaceType.Left);

            switch (leftRelativePosition)
            {
                case RelativePosition.Same:
                    await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                    await CommonActions.ApplyAndAddRotation(CubeRotations.YAntiClockwise, solution, configuration).ConfigureAwait(false);

                    break;

                case RelativePosition.Right:
                    await CommonActions.ApplyAndAddRotation(CubeRotations.Y2, solution, configuration).ConfigureAwait(false);

                    break;

                case RelativePosition.Left:
                    await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                    break;
            }

            await MoveTopCornerToBottom(configuration, solution).ConfigureAwait(false);

        }

        private static async Task MoveTopCornerToBottom(IRotatable configuration, ICollection<IRotation> solution)
        {
            // Move into top layer
            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);


            // Move to bottom face
            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration).ConfigureAwait(false);

        }

        private static async Task CheckTopLayerForWhite(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution, FaceType face, bool leftIsLastInTopEdge = false, bool rightIsLastInTopEdge = false)
        {
            // TODO: CUBE ROTATE INSTEAD SO WE ARE ONLY USING RIGHT....?
            var topEdge = FaceRules.EdgeJoiningFaceToFace(face, FaceType.Upper);
            var edgeInTopFace = configuration.Faces[FaceType.Upper].GetEdge(topEdge);
            var rotatingFace = configuration.Faces[face];

            if (rotatingFace.TopLeft() == FaceColour.White)
            {
                var joiningUpperColour = leftIsLastInTopEdge ? edgeInTopFace.Last() : edgeInTopFace.First();
                var joiningUpperFace = FaceRules.GetFaceOfColour(joiningUpperColour, configuration);

                var relativePositionToUpperJoining = FaceRules.RelativePositionBetweenFaces(face, joiningUpperFace);
                switch (relativePositionToUpperJoining)
                {
                    case RelativePosition.Right:
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                        break;

                    case RelativePosition.Left:
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                        break;

                    case RelativePosition.Opposite:
                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                        break;
                }

                await CommonActions.ApplyAndAddRotation(Rotations.ByFace(joiningUpperFace, RotationDirection.Clockwise), solution, configuration).ConfigureAwait(false);

                await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                await CommonActions.ApplyAndAddRotation(Rotations.ByFace(joiningUpperFace, RotationDirection.AntiClockwise), solution, configuration).ConfigureAwait(false);

            }
            else if (rotatingFace.TopRight() == FaceColour.White)
            {
                var joiningUpperColour = rightIsLastInTopEdge ? edgeInTopFace.Last() : edgeInTopFace.First();
                var joiningUpperFace = FaceRules.GetFaceOfColour(joiningUpperColour, configuration);

                var relativePositionToUpperJoining = FaceRules.RelativePositionBetweenFaces(face, joiningUpperFace);
                switch (relativePositionToUpperJoining)
                {
                    case RelativePosition.Same:
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                        break;

                    case RelativePosition.Left:
                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                        break;

                    case RelativePosition.Opposite:
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                        break;
                }

                var faceToRotate = FaceRules.FaceAtRelativePositionTo(joiningUpperFace, RelativePosition.Right);
                await CommonActions.ApplyAndAddRotation(Rotations.ByFace(faceToRotate, RotationDirection.Clockwise), solution, configuration).ConfigureAwait(false);

                await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                await CommonActions.ApplyAndAddRotation(Rotations.ByFace(faceToRotate, RotationDirection.AntiClockwise), solution, configuration).ConfigureAwait(false);

            }
        }
    }
}