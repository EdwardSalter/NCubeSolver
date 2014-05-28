using System.Collections.Generic;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size3
{
    internal class MiddleLayerSolver : IPartialSolver
    {
        public async Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration)
        {
            var solution = new List<IRotation>();
            IList<IRotation> previousSolution;

            do
            {
                previousSolution = new List<IRotation>(solution);

                var rotationToBottom = await CommonActions.PositionOnBottom(configuration, FaceColour.White);
                if (rotationToBottom != null) solution.Add(rotationToBottom);

                await CheckTopLayer(configuration, solution);

            } while (previousSolution.Count != solution.Count);

            do
            {
                previousSolution = new List<IRotation>(solution);

                var rotationToBottom = await CommonActions.PositionOnBottom(configuration, FaceColour.White);
                if (rotationToBottom != null) solution.Add(rotationToBottom);

                await CheckMiddleLayer(configuration, solution);
                await CheckTopLayer(configuration, solution);

            } while (previousSolution.Count != solution.Count);

            return solution;
        }

        private async Task CheckMiddleLayer(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            if (configuration.Faces[FaceType.Front].LeftCentre() != configuration.Faces[FaceType.Front].Centre)
            {
                await CommonActions.ApplyAndAddRotation(CubeRotations.YAntiClockwise, solution, configuration);
                await MoveTopCentreToMiddleRight(configuration, solution);
                await CheckTopBackLayer(configuration, solution);
            }

            if (configuration.Faces[FaceType.Front].RightCentre() != configuration.Faces[FaceType.Front].Centre)
            {
                await MoveTopCentreToMiddleRight(configuration, solution);
                await CheckTopBackLayer(configuration, solution);
            }

            if (configuration.Faces[FaceType.Right].RightCentre() != configuration.Faces[FaceType.Right].Centre)
            {
                await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration);
                await MoveTopCentreToMiddleRight(configuration, solution);
                await CheckTopBackLayer(configuration, solution);
            }

            if (configuration.Faces[FaceType.Left].LeftCentre() != configuration.Faces[FaceType.Left].Centre)
            {
                await CommonActions.ApplyAndAddRotation(CubeRotations.Y2, solution, configuration);
                await CheckTopBackLayer(configuration, solution);
            }
        }

        private async Task CheckTopLayer(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            await CheckTopFrontLayer(configuration, solution);
            await CheckTopLeftLayer(configuration, solution);
            await CheckTopBackLayer(configuration, solution);
            await CheckTopRightLayer(configuration, solution);
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
                    await CommonActions.ApplyAndAddRotation(CubeRotations.Y2, solution, configuration);
                    break;
                case RelativePosition.Right:
                    await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                    await CommonActions.ApplyAndAddRotation(CubeRotations.YAntiClockwise, solution, configuration);
                    break;
                case RelativePosition.Left:
                    await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
                    await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration);
                    break;
                case RelativePosition.Opposite:
                    await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
                    break;
            }

            await MoveTopCentreToMiddleLayer(configuration, solution, upperColour);
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
                    await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration);
                    break;
                case RelativePosition.Right:
                    await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                    await CommonActions.ApplyAndAddRotation(CubeRotations.Y2, solution, configuration);
                    break;
                case RelativePosition.Left:
                    await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
                    break;
                case RelativePosition.Opposite:
                    await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
                    await CommonActions.ApplyAndAddRotation(CubeRotations.YAntiClockwise, solution, configuration);
                    break;
            }

            await MoveTopCentreToMiddleLayer(configuration, solution, upperColour);
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
                    await CommonActions.ApplyAndAddRotation(CubeRotations.YAntiClockwise, solution, configuration);
                    break;
                case RelativePosition.Right:
                    await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                    break;
                case RelativePosition.Left:
                    await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
                    await CommonActions.ApplyAndAddRotation(CubeRotations.Y2, solution, configuration);
                    break;
                case RelativePosition.Opposite:
                    await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
                    await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration);
                    break;
            }

            await MoveTopCentreToMiddleLayer(configuration, solution, upperColour);
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
                    await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                    await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration);
                    break;
                case RelativePosition.Left:
                    await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
                    await CommonActions.ApplyAndAddRotation(CubeRotations.YAntiClockwise, solution, configuration);
                    break;
                case RelativePosition.Opposite:
                    await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
                    await CommonActions.ApplyAndAddRotation(CubeRotations.Y2, solution, configuration);
                    break;
            }

            await MoveTopCentreToMiddleLayer(configuration, solution, upperColour);
        }

        private static async Task MoveTopCentreToMiddleLayer(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution, FaceColour upperColour)
        {
            var faceOfUpperColour = FaceRules.GetFaceOfColour(upperColour, configuration);
            var upperRelation = FaceRules.RelativePositionBetweenFaces(FaceType.Front, faceOfUpperColour);
            switch (upperRelation)
            {
                case RelativePosition.Left:
                    await MoveTopCentreToMiddleLeft(configuration, solution);
                    break;
                case RelativePosition.Right:
                    await MoveTopCentreToMiddleRight(configuration, solution);
                    break;
            }
        }

        private static async Task MoveTopCentreToMiddleLeft(IRotatable configuration, ICollection<IRotation> solution)
        {
            await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.LeftAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.LeftClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.FrontAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.LeftClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.LeftAntiClockwise, solution, configuration);
        }

        private static async Task MoveTopCentreToMiddleRight(IRotatable configuration, ICollection<IRotation> solution)
        {
            await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.FrontAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration);
        }
    }
}
