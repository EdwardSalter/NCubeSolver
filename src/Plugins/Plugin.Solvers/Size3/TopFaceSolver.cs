using System.Collections.Generic;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size3
{
    class TopFaceSolver : IPartialSolver
    {
        public async Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration)
        {
            var solution = new List<IRotation>();

            await Repeat.SolvingUntilNoMovesCanBeMade(solution, async () =>
            {
                var faces = new List<FaceType> {FaceType.Right, FaceType.Back, FaceType.Front, FaceType.Left};
                foreach (var face in faces)
                {
                    await SortFace(face, configuration, solution).ConfigureAwait(false);

                }
            }).ConfigureAwait(false);

            await CorrectTopLayerRotation(solution, configuration).ConfigureAwait(false);


            return solution;
        }

        private static async Task CorrectTopLayerRotation(List<IRotation> solution, CubeConfiguration<FaceColour> configuration)
        {
            var frontColour = configuration.Faces[FaceType.Front].TopCentre();
            var faceOfFrontColour = FaceRules.GetFaceOfColour(frontColour, configuration);
            var relativePosition = FaceRules.RelativePositionBetweenFaces(FaceType.Front, faceOfFrontColour);

            switch (relativePosition)
            {
                case RelativePosition.Opposite:
                    await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                    break;
                case RelativePosition.Right:
                    await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                    break;
                case RelativePosition.Left:
                    await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                    break;
            }
        }

        private static async Task SortFace(FaceType face, CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            if (configuration.Faces[face].TopRight() == FaceColour.Yellow)
            {
                // Move to Right Face
                switch (face)
                {
                    case FaceType.Left:
                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                        break;
                    case FaceType.Front:
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                        break;
                    case FaceType.Back:
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                        break;
                }

                await RightFace(solution, configuration).ConfigureAwait(false);

            }
            if (configuration.Faces[face].TopLeft() == FaceColour.Yellow)
            {
                // Move to Back Face
                switch (face)
                {
                    case FaceType.Front:
                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                        break;
                    case FaceType.Right:
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                        break;
                    case FaceType.Left:
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                        break;
                }

                await BackFace(solution, configuration).ConfigureAwait(false);

            }
        }

        private static async Task RightFace(ICollection<IRotation> solution, IRotatable configuration)
        {
            for (int i = 0; i < 2; i++)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration).ConfigureAwait(false);

                await CommonActions.ApplyAndAddRotation(Rotations.DownClockwise, solution, configuration).ConfigureAwait(false);

                await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration).ConfigureAwait(false);

                await CommonActions.ApplyAndAddRotation(Rotations.DownAntiClockwise, solution, configuration).ConfigureAwait(false);

            }
        }

        private static async Task BackFace(ICollection<IRotation> solution, IRotatable configuration)
        {
            for (int i = 0; i < 2; i++)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.DownClockwise, solution, configuration).ConfigureAwait(false);

                await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration).ConfigureAwait(false);

                await CommonActions.ApplyAndAddRotation(Rotations.DownAntiClockwise, solution, configuration).ConfigureAwait(false);

                await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration).ConfigureAwait(false);

            }
        }
    }
}
