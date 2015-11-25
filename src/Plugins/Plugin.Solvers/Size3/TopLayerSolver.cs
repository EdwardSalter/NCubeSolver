using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size3
{
    class TopLayerSolver : IPartialSolver
    {
        public async Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration)
        {
            var solution = new List<IRotation>();

            var solved = configuration.Faces.Values.All(face => face.Items.AsEnumerable().All(colour => colour == face.Items[0, 0]));

            if (!solved)
            {
                solved = await OneCornerCorrect(configuration, solution).ConfigureAwait(false);


                if (!solved)
                {
                    solved = await CrossedCorners(configuration, solution).ConfigureAwait(false);


                    if (!solved)
                    {
                        await CornersSwitchedInParallel(configuration, solution).ConfigureAwait(false);

                    }
                }
            }

            return solution;
        }

        private static async Task CornersSwitchedInParallel(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            var front = configuration.Faces[FaceType.Front];
            var colourOfTopLeft = front.TopLeft();
            var faceOfColour = FaceRules.GetFaceOfColour(colourOfTopLeft, configuration);
            var relativePosition = FaceRules.RelativePositionBetweenFaces(FaceType.Front, faceOfColour);
            if (relativePosition == RelativePosition.Left)
                await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration).ConfigureAwait(false);


            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.FrontAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.LeftAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.FrontAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.LeftClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.FrontAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.LeftClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.FrontAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.LeftAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration).ConfigureAwait(false);

        }

        private static async Task<bool> CrossedCorners(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            var frontFace = configuration.Faces[FaceType.Front];
            var rightFace = configuration.Faces[FaceType.Right];
            if (frontFace.TopLeft() == frontFace.TopRight() && frontFace.TopLeft() != frontFace.Centre && rightFace.TopLeft() == rightFace.TopRight() && rightFace.TopLeft() != rightFace.Centre)
            {
                await PermuteCross(configuration, solution).ConfigureAwait(false);

                return true;
            }

            return false;
        }

        private static async Task PermuteCross(IRotatable configuration, ICollection<IRotation> solution)
        {
            await CommonActions.ApplyAndAddRotation(CubeRotations.X2, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.Right2, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.Left2, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.Right2, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.Left2, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.Right2, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.Left2, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.Right2, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.Left2, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(CubeRotations.X2, solution, configuration).ConfigureAwait(false);

        }

        
        private static async Task<bool> OneCornerCorrect(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            var faces = new List<FaceType> { FaceType.Front, FaceType.Right, FaceType.Back, FaceType.Left };

            foreach (var faceType in faces)
            {
                var face = configuration.Faces[faceType];
                if (face.TopLeft() == face.Centre)
                {
                    var cubeRotation = await CommonActions.PositionOnFront(configuration, face.Centre).ConfigureAwait(false);

                    if (cubeRotation != null)
                    {
                        solution.Add(cubeRotation);
                    }

                    // Check whether we need to do a clockwise or anti-clockwise permutation
                    var rightFace = configuration.Faces[FaceType.Right];
                    if (rightFace.TopLeft() == rightFace.TopRight())
                    {
                        await PermuteAntiClockwise(solution, configuration).ConfigureAwait(false);

                    }
                    else
                    {
                        await PermuteClockwise(solution, configuration).ConfigureAwait(false);

                    }

                    return true;
                }
            }

            return false;
        }

        private static async Task PermuteClockwise(ICollection<IRotation> solution, IRotatable configuration)
        {
            await CommonActions.ApplyAndAddRotation(CubeRotations.XClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.Down2, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.Down2, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.Right2, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(CubeRotations.XAntiClockwise, solution, configuration).ConfigureAwait(false);

        }

        private static async Task PermuteAntiClockwise(ICollection<IRotation> solution, IRotatable configuration)
        {
            await CommonActions.ApplyAndAddRotation(CubeRotations.XClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.Right2, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.Down2, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.Down2, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(CubeRotations.XAntiClockwise, solution, configuration).ConfigureAwait(false);

        }
    }
}
