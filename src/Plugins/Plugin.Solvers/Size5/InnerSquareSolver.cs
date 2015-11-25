using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size5
{
    internal class InnerSquareSolver : IPartialSolver
    {
        public async Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration)
        {
            var solution = new List<IRotation>();

            var allColours = new[] { FaceColour.Red, FaceColour.Blue, FaceColour.Yellow, FaceColour.Orange, FaceColour.Green, FaceColour.White, };
            var colourIndex = Array.IndexOf(allColours, configuration.Faces[FaceType.Front].Centre);
            var startIndex = colourIndex;

            await SolveFrontFace(configuration, solution).ConfigureAwait(false);


            int count = 0;
            do
            {
                colourIndex++;
                if (colourIndex >= allColours.Length)
                {
                    colourIndex = 0;
                }
                var nextColour = allColours[colourIndex];
                solution.Add(await CommonActions.PositionOnFront(configuration, nextColour).ConfigureAwait(false));


                await SolveFrontFace(configuration, solution).ConfigureAwait(false);


                if (++count > 10)
                {
                    throw new SolveFailureException("Could not solve the inner square in 10 or less tries");
                }
            } while (colourIndex != startIndex);

            return solution;
        }

        private async Task SolveFrontFace(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            await CheckUpperFace(configuration, solution).ConfigureAwait(false);

            for (int i = 0; i <= 2; i++)
            {
                await CommonActions.ApplyAndAddRotation(CubeRotations.ZClockwise, solution, configuration).ConfigureAwait(false);

                await CheckUpperFace(configuration, solution).ConfigureAwait(false);

            }

            await CheckBackFace(configuration, solution).ConfigureAwait(false);

        }

        private static async Task CheckUpperFace(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            var frontFaceColour = configuration.Faces[FaceType.Front].Centre;

            for (int i = 0; i < 4; i++)
            {
                await CheckUpperTopLeft(configuration, solution, frontFaceColour).ConfigureAwait(false);

                await CheckUpperTopRight(configuration, solution, frontFaceColour).ConfigureAwait(false);

                await CheckUpperBottomRight(configuration, solution, frontFaceColour).ConfigureAwait(false);

                await CheckUpperBottomLeft(configuration, solution, frontFaceColour).ConfigureAwait(false);

            }
        }

        private static async Task CheckBackFace(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            var frontFaceColour = configuration.Faces[FaceType.Front].Centre;

            for(int i = 0; i < 4; i++)
            {
                await CheckBackTopLeft(configuration, solution, frontFaceColour).ConfigureAwait(false);

                await CheckBackTopRight(configuration, solution, frontFaceColour).ConfigureAwait(false);

                await CheckBackBottomRight(configuration, solution, frontFaceColour).ConfigureAwait(false);

                await CheckBackBottomLeft(configuration, solution, frontFaceColour).ConfigureAwait(false);

            }
        }

        private static async Task CheckUpperBottomLeft(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, FaceColour frontFaceColour)
        {
            var bottomLeft = configuration.Faces[FaceType.Upper].GetEdge(configuration.MinInnerLayerIndex(), Edge.Bottom)[configuration.MinInnerLayerIndex()];
            if (bottomLeft == frontFaceColour)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (configuration.Faces[FaceType.Front].GetEdge(configuration.MinInnerLayerIndex(), Edge.Top)[configuration.MinInnerLayerIndex()] != frontFaceColour)
                    {
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeftAntiClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeftClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeftAntiClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeftClockwise, solution, configuration).ConfigureAwait(false);

                        break;
                    }

                    await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration).ConfigureAwait(false);

                }
            }
        }

        private static async Task CheckUpperBottomRight(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, FaceColour frontFaceColour)
        {
            var topRight = configuration.Faces[FaceType.Upper].GetEdge(configuration.MinInnerLayerIndex(), Edge.Bottom)[configuration.MaxInnerLayerIndex()];
            if (topRight == frontFaceColour)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (configuration.Faces[FaceType.Front].GetEdge(configuration.MinInnerLayerIndex(), Edge.Top)[configuration.MaxInnerLayerIndex()] != frontFaceColour)
                    {
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRightClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRightAntiClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRightClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRightAntiClockwise, solution, configuration).ConfigureAwait(false);

                        break;
                    }

                    await CommonActions.ApplyAndAddRotation(Rotations.FrontAntiClockwise, solution, configuration).ConfigureAwait(false);

                }
            }
        }

        private static async Task CheckUpperTopRight(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, FaceColour frontFaceColour)
        {
            var topRight = configuration.Faces[FaceType.Upper].GetEdge(configuration.MinInnerLayerIndex(), Edge.Top)[configuration.MaxInnerLayerIndex()];
            if (topRight == frontFaceColour)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (configuration.Faces[FaceType.Front].GetEdge(configuration.MinInnerLayerIndex(), Edge.Bottom)[configuration.MaxInnerLayerIndex()] != frontFaceColour)
                    {
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRightClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRightAntiClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRightClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRightAntiClockwise, solution, configuration).ConfigureAwait(false);

                        break;
                    }

                    await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration).ConfigureAwait(false);

                }
            }
        }

        private static async Task CheckUpperTopLeft(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, FaceColour frontFaceColour)
        {
            var topLeft = configuration.Faces[FaceType.Upper].GetEdge(configuration.MinInnerLayerIndex(), Edge.Top)[configuration.MinInnerLayerIndex()];
            if (topLeft == frontFaceColour)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (configuration.Faces[FaceType.Front].GetEdge(configuration.MinInnerLayerIndex(), Edge.Bottom)[configuration.MinInnerLayerIndex()] != frontFaceColour)
                    {
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeftAntiClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeftClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeftAntiClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeftClockwise, solution, configuration).ConfigureAwait(false);

                        break;
                    }

                    await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration).ConfigureAwait(false);

                }
            }
        }

        private static async Task CheckBackBottomLeft(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, FaceColour frontFaceColour)
        {
            var bottomLeft = configuration.Faces[FaceType.Back].GetEdge(configuration.MinInnerLayerIndex(), Edge.Bottom)[configuration.MinInnerLayerIndex()];
            if (bottomLeft == frontFaceColour)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (configuration.Faces[FaceType.Front].GetEdge(configuration.MinInnerLayerIndex(), Edge.Bottom)[configuration.MaxInnerLayerIndex()] != frontFaceColour)
                    {
                        await CommonActions.ApplyAndAddRotation(CubeRotations.XClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRight2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRight2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRight2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRight2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(CubeRotations.XAntiClockwise, solution, configuration).ConfigureAwait(false);

                        break;
                    }

                    await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration).ConfigureAwait(false);

                }
            }
        }

        private static async Task CheckBackBottomRight(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, FaceColour frontFaceColour)
        {
            var topRight = configuration.Faces[FaceType.Back].GetEdge(configuration.MinInnerLayerIndex(), Edge.Bottom)[configuration.MaxInnerLayerIndex()];
            if (topRight == frontFaceColour)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (configuration.Faces[FaceType.Front].GetEdge(configuration.MinInnerLayerIndex(), Edge.Bottom)[configuration.MinInnerLayerIndex()] != frontFaceColour)
                    {
                        await CommonActions.ApplyAndAddRotation(CubeRotations.XClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeft2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeft2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeft2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeft2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(CubeRotations.XAntiClockwise, solution, configuration).ConfigureAwait(false);

                        break;
                    }

                    await CommonActions.ApplyAndAddRotation(Rotations.FrontAntiClockwise, solution, configuration).ConfigureAwait(false);

                }
            }
        }

        private static async Task CheckBackTopRight(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, FaceColour frontFaceColour)
        {
            var topRight = configuration.Faces[FaceType.Back].GetEdge(configuration.MinInnerLayerIndex(), Edge.Top)[configuration.MaxInnerLayerIndex()];
            if (topRight == frontFaceColour)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (configuration.Faces[FaceType.Front].GetEdge(configuration.MinInnerLayerIndex(), Edge.Top)[configuration.MinInnerLayerIndex()] != frontFaceColour)
                    {
                        await CommonActions.ApplyAndAddRotation(CubeRotations.XClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeft2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeft2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeft2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeft2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(CubeRotations.XAntiClockwise, solution, configuration).ConfigureAwait(false);

                        break;
                    }

                    await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration).ConfigureAwait(false);

                }
            }
        }

        private static async Task CheckBackTopLeft(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, FaceColour frontFaceColour)
        {
            var topLeft = configuration.Faces[FaceType.Back].GetEdge(configuration.MinInnerLayerIndex(), Edge.Top)[configuration.MinInnerLayerIndex()];
            if (topLeft == frontFaceColour)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (configuration.Faces[FaceType.Front].GetEdge(configuration.MinInnerLayerIndex(), Edge.Top)[configuration.MaxInnerLayerIndex()] != frontFaceColour)
                    {
                        await CommonActions.ApplyAndAddRotation(CubeRotations.XClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRight2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRight2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRight2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRight2, solution, configuration).ConfigureAwait(false);

                        await CommonActions.ApplyAndAddRotation(CubeRotations.XAntiClockwise, solution, configuration).ConfigureAwait(false);

                        break;
                    }

                    await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration).ConfigureAwait(false);

                }
            }
        }
    }
}
