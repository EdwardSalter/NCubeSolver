﻿using System.Collections.Generic;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size3
{
    internal class MiddleLayerSolver : IPartialSolver
    {
        public async Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration)
        {
            var solution = new List<IRotation>();

            var rotationToBottom = await CommonActions.PositionOnBottom(configuration, FaceColour.White).ConfigureAwait(false);

            if (rotationToBottom != null) solution.Add(rotationToBottom);


            await Repeat.SolvingUntilNoMovesCanBeMade(solution, async () =>
            {
                await CheckTopLayer(configuration, solution).ConfigureAwait(false);
            }).ConfigureAwait(false);

            await Repeat.SolvingUntilNoMovesCanBeMade(solution, async () =>
            {
                await CheckMiddleLayer(configuration, solution).ConfigureAwait(false);
                await CheckTopLayer(configuration, solution).ConfigureAwait(false);
            }).ConfigureAwait(false);

            return solution;
        }

        private async Task CheckMiddleLayer(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            if (configuration.Faces[FaceType.Front].LeftCentre() != configuration.Faces[FaceType.Front].Centre)
            {
                await CommonActions.ApplyAndAddRotation(CubeRotations.YAntiClockwise, solution, configuration).ConfigureAwait(false);

                await MoveTopCentreToMiddleRight(configuration, solution).ConfigureAwait(false);

                await CheckTopBackLayer(configuration, solution).ConfigureAwait(false);

            }

            if (configuration.Faces[FaceType.Front].RightCentre() != configuration.Faces[FaceType.Front].Centre)
            {
                await MoveTopCentreToMiddleRight(configuration, solution).ConfigureAwait(false);

                await CheckTopBackLayer(configuration, solution).ConfigureAwait(false);

            }

            if (configuration.Faces[FaceType.Right].RightCentre() != configuration.Faces[FaceType.Right].Centre)
            {
                await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration).ConfigureAwait(false);

                await MoveTopCentreToMiddleRight(configuration, solution).ConfigureAwait(false);

                await CheckTopBackLayer(configuration, solution).ConfigureAwait(false);

            }

            if (configuration.Faces[FaceType.Left].LeftCentre() != configuration.Faces[FaceType.Left].Centre)
            {
                await CommonActions.ApplyAndAddRotation(CubeRotations.Y2, solution, configuration).ConfigureAwait(false);

                await CheckTopBackLayer(configuration, solution).ConfigureAwait(false);

            }
        }

        private async Task CheckTopLayer(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            await CheckTopFrontLayer(configuration, solution).ConfigureAwait(false);

            await CheckTopLeftLayer(configuration, solution).ConfigureAwait(false);

            await CheckTopBackLayer(configuration, solution).ConfigureAwait(false);

            await CheckTopRightLayer(configuration, solution).ConfigureAwait(false);

        }

        internal async Task CheckTopBackLayer(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            var upperColour = configuration.Faces[FaceType.Upper].TopCentre();
            if (upperColour == FaceColour.Yellow) return;

            var joiningColour = configuration.Faces[FaceType.Back].TopCentre();
            if (joiningColour == FaceColour.Yellow) return;

            var faceOfJoiningColour = FaceRules.GetFaceOfColour(joiningColour, configuration);
            var relation = FaceRules.RelativePositionBetweenFaces(FaceType.Back, faceOfJoiningColour);

            switch (relation)
            {
                case RelativePosition.Same:
                    await CommonActions.ApplyAndAddRotation(CubeRotations.Y2, solution, configuration).ConfigureAwait(false);

                    break;
                case RelativePosition.Right:
                    await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                    await CommonActions.ApplyAndAddRotation(CubeRotations.YAntiClockwise, solution, configuration).ConfigureAwait(false);

                    break;
                case RelativePosition.Left:
                    await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                    await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration).ConfigureAwait(false);

                    break;
                case RelativePosition.Opposite:
                    await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                    break;
            }

            await MoveTopCentreToMiddleLayer(configuration, solution, upperColour).ConfigureAwait(false);

        }

        internal async Task CheckTopRightLayer(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            var upperColour = configuration.Faces[FaceType.Upper].RightCentre();
            if (upperColour == FaceColour.Yellow) return;

            var joiningColour = configuration.Faces[FaceType.Right].TopCentre();
            if (joiningColour == FaceColour.Yellow) return;

            var faceOfJoiningColour = FaceRules.GetFaceOfColour(joiningColour, configuration);
            var relation = FaceRules.RelativePositionBetweenFaces(FaceType.Right, faceOfJoiningColour);

            switch (relation)
            {
                case RelativePosition.Same:
                    await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration).ConfigureAwait(false);

                    break;
                case RelativePosition.Right:
                    await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                    await CommonActions.ApplyAndAddRotation(CubeRotations.Y2, solution, configuration).ConfigureAwait(false);

                    break;
                case RelativePosition.Left:
                    await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                    break;
                case RelativePosition.Opposite:
                    await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                    await CommonActions.ApplyAndAddRotation(CubeRotations.YAntiClockwise, solution, configuration).ConfigureAwait(false);

                    break;
            }

            await MoveTopCentreToMiddleLayer(configuration, solution, upperColour).ConfigureAwait(false);

        }

        internal async Task CheckTopLeftLayer(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            var upperColour = configuration.Faces[FaceType.Upper].LeftCentre();
            if (upperColour == FaceColour.Yellow) return;

            var joiningColour = configuration.Faces[FaceType.Left].TopCentre();
            if (joiningColour == FaceColour.Yellow) return;

            var faceOfJoiningColour = FaceRules.GetFaceOfColour(joiningColour, configuration);
            var relation = FaceRules.RelativePositionBetweenFaces(FaceType.Left, faceOfJoiningColour);

            switch (relation)
            {
                case RelativePosition.Same:
                    await CommonActions.ApplyAndAddRotation(CubeRotations.YAntiClockwise, solution, configuration).ConfigureAwait(false);

                    break;
                case RelativePosition.Right:
                    await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                    break;
                case RelativePosition.Left:
                    await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                    await CommonActions.ApplyAndAddRotation(CubeRotations.Y2, solution, configuration).ConfigureAwait(false);

                    break;
                case RelativePosition.Opposite:
                    await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                    await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration).ConfigureAwait(false);

                    break;
            }

            await MoveTopCentreToMiddleLayer(configuration, solution, upperColour).ConfigureAwait(false);

        }

        internal async Task CheckTopFrontLayer(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            var upperColour = configuration.Faces[FaceType.Upper].BottomCentre();
            if (upperColour == FaceColour.Yellow) return;

            var joiningColour = configuration.Faces[FaceType.Front].TopCentre();
            if (joiningColour == FaceColour.Yellow) return;

            var faceOfJoiningColour = FaceRules.GetFaceOfColour(joiningColour, configuration);
            var relation = FaceRules.RelativePositionBetweenFaces(FaceType.Front, faceOfJoiningColour);

            switch (relation)
            {
                case RelativePosition.Same:
                    break;
                case RelativePosition.Right:
                    await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                    await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration).ConfigureAwait(false);

                    break;
                case RelativePosition.Left:
                    await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                    await CommonActions.ApplyAndAddRotation(CubeRotations.YAntiClockwise, solution, configuration).ConfigureAwait(false);

                    break;
                case RelativePosition.Opposite:
                    await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                    await CommonActions.ApplyAndAddRotation(CubeRotations.Y2, solution, configuration).ConfigureAwait(false);

                    break;
            }

            await MoveTopCentreToMiddleLayer(configuration, solution, upperColour).ConfigureAwait(false);

        }

        private static async Task MoveTopCentreToMiddleLayer(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution, FaceColour upperColour)
        {
            var faceOfUpperColour = FaceRules.GetFaceOfColour(upperColour, configuration);
            var upperRelation = FaceRules.RelativePositionBetweenFaces(FaceType.Front, faceOfUpperColour);
            switch (upperRelation)
            {
                case RelativePosition.Left:
                    await MoveTopCentreToMiddleLeft(configuration, solution).ConfigureAwait(false);

                    break;
                case RelativePosition.Right:
                    await MoveTopCentreToMiddleRight(configuration, solution).ConfigureAwait(false);

                    break;
            }
        }

        private static async Task MoveTopCentreToMiddleLeft(IRotatable configuration, ICollection<IRotation> solution)
        {
            await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.LeftAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.LeftClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.FrontAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.LeftClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.LeftAntiClockwise, solution, configuration).ConfigureAwait(false);

        }

        private static async Task MoveTopCentreToMiddleRight(IRotatable configuration, ICollection<IRotation> solution)
        {
            await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.FrontAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration).ConfigureAwait(false);

        }
    }
}
