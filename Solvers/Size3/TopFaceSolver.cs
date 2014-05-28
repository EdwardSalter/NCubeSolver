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
            List<IRotation> previousSolution;

            do
            {
                previousSolution = new List<IRotation>(solution);

                var faces = new List<FaceType> {FaceType.Right, FaceType.Back, FaceType.Front, FaceType.Left};
                foreach (var face in faces)
                {
                    await SortFace(face, configuration, solution);
                }
            } while (previousSolution.Count != solution.Count);

            await CorrectTopLayerRotation(solution, configuration);

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
                    await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
                    break;
                case RelativePosition.Right:
                    await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                    break;
                case RelativePosition.Left:
                    await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
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
                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
                        break;
                    case FaceType.Front:
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                        break;
                    case FaceType.Back:
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
                        break;
                }

                await RightFace(solution, configuration);
            }
            if (configuration.Faces[face].TopLeft() == FaceColour.Yellow)
            {
                // Move to Back Face
                switch (face)
                {
                    case FaceType.Front:
                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
                        break;
                    case FaceType.Right:
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                        break;
                    case FaceType.Left:
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
                        break;
                }

                await BackFace(solution, configuration);
            }
        }

        private static async Task RightFace(ICollection<IRotation> solution, IRotatable configuration)
        {
            for (int i = 0; i < 2; i++)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.DownClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.DownAntiClockwise, solution, configuration);
            }
        }

        private static async Task BackFace(ICollection<IRotation> solution, IRotatable configuration)
        {
            for (int i = 0; i < 2; i++)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.DownClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.DownAntiClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration);
            }
        }
    }
}
