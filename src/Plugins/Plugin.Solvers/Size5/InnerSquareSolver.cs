﻿using System;
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

            await SolveFrontFace(configuration, solution);

            int count = 0;
            do
            {
                colourIndex++;
                if (colourIndex >= allColours.Length)
                {
                    colourIndex = 0;
                }
                var nextColour = allColours[colourIndex];
                solution.Add(await CommonActions.PositionOnFront(configuration, nextColour));

                await SolveFrontFace(configuration, solution);

                if (++count > 10)
                {
                    throw new SolveFailureException("Could not solve the inner square in 10 or less tries");
                }
            } while (colourIndex != startIndex);

            return solution;
        }

        private async Task SolveFrontFace(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            await CheckUpperFace(configuration, solution);
            for (int i = 0; i <= 2; i++)
            {
                await CommonActions.ApplyAndAddRotation(CubeRotations.ZClockwise, solution, configuration);
                await CheckUpperFace(configuration, solution);
            }

            await CheckBackFace(configuration, solution);
        }

        private static async Task CheckUpperFace(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            var frontFaceColour = configuration.Faces[FaceType.Front].Centre;

            for (int i = 0; i < 4; i++)
            {
                await CheckUpperTopLeft(configuration, solution, frontFaceColour);
                await CheckUpperTopRight(configuration, solution, frontFaceColour);
                await CheckUpperBottomRight(configuration, solution, frontFaceColour);
                await CheckUpperBottomLeft(configuration, solution, frontFaceColour);
            }
        }

        private static async Task CheckBackFace(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            var frontFaceColour = configuration.Faces[FaceType.Front].Centre;

            for(int i = 0; i < 4; i++)
            {
                await CheckBackTopLeft(configuration, solution, frontFaceColour);
                await CheckBackTopRight(configuration, solution, frontFaceColour);
                await CheckBackBottomRight(configuration, solution, frontFaceColour);
                await CheckBackBottomLeft(configuration, solution, frontFaceColour);
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
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFace(FaceType.Left, RotationDirection.AntiClockwise, configuration.MinInnerLayerIndex()), solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFace(FaceType.Left, RotationDirection.Clockwise, configuration.MinInnerLayerIndex()), solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFace(FaceType.Left, RotationDirection.AntiClockwise, configuration.MinInnerLayerIndex()), solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFace(FaceType.Left, RotationDirection.Clockwise, configuration.MinInnerLayerIndex()), solution, configuration);
                        break;
                    }

                    await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration);
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
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFace(FaceType.Right, RotationDirection.Clockwise, configuration.MinInnerLayerIndex()), solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFace(FaceType.Right, RotationDirection.AntiClockwise, configuration.MinInnerLayerIndex()), solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFace(FaceType.Right, RotationDirection.Clockwise, configuration.MinInnerLayerIndex()), solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFace(FaceType.Right, RotationDirection.AntiClockwise, configuration.MinInnerLayerIndex()), solution, configuration);
                        break;
                    }

                    await CommonActions.ApplyAndAddRotation(Rotations.FrontAntiClockwise, solution, configuration);
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
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFace(FaceType.Right, RotationDirection.Clockwise, configuration.MinInnerLayerIndex()), solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFace(FaceType.Right, RotationDirection.AntiClockwise, configuration.MinInnerLayerIndex()), solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFace(FaceType.Right, RotationDirection.Clockwise, configuration.MinInnerLayerIndex()), solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFace(FaceType.Right, RotationDirection.AntiClockwise, configuration.MinInnerLayerIndex()), solution, configuration);
                        break;
                    }

                    await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration);
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
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFace(FaceType.Left, RotationDirection.AntiClockwise, configuration.MinInnerLayerIndex()), solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFace(FaceType.Left, RotationDirection.Clockwise, configuration.MinInnerLayerIndex()), solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFace(FaceType.Left, RotationDirection.AntiClockwise, configuration.MinInnerLayerIndex()), solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFace(FaceType.Left, RotationDirection.Clockwise, configuration.MinInnerLayerIndex()), solution, configuration);
                        break;
                    }

                    await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration);
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
                        await CommonActions.ApplyAndAddRotation(CubeRotations.XClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFaceTwice(FaceType.Right, configuration.MinInnerLayerIndex()), solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFaceTwice(FaceType.Right, configuration.MinInnerLayerIndex()), solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFaceTwice(FaceType.Right, configuration.MinInnerLayerIndex()), solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFaceTwice(FaceType.Right, configuration.MinInnerLayerIndex()), solution, configuration);
                        await CommonActions.ApplyAndAddRotation(CubeRotations.XAntiClockwise, solution, configuration);
                        break;
                    }

                    await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration);
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
                        await CommonActions.ApplyAndAddRotation(CubeRotations.XClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFaceTwice(FaceType.Left, configuration.MinInnerLayerIndex()), solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFaceTwice(FaceType.Left, configuration.MinInnerLayerIndex()), solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFaceTwice(FaceType.Left, configuration.MinInnerLayerIndex()), solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFaceTwice(FaceType.Left, configuration.MinInnerLayerIndex()), solution, configuration);
                        await CommonActions.ApplyAndAddRotation(CubeRotations.XAntiClockwise, solution, configuration);
                        break;
                    }

                    await CommonActions.ApplyAndAddRotation(Rotations.FrontAntiClockwise, solution, configuration);
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
                        await CommonActions.ApplyAndAddRotation(CubeRotations.XClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFaceTwice(FaceType.Left, configuration.MinInnerLayerIndex()), solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFaceTwice(FaceType.Left, configuration.MinInnerLayerIndex()), solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFaceTwice(FaceType.Left, configuration.MinInnerLayerIndex()), solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFaceTwice(FaceType.Left, configuration.MinInnerLayerIndex()), solution, configuration);
                        await CommonActions.ApplyAndAddRotation(CubeRotations.XAntiClockwise, solution, configuration);
                        break;
                    }

                    await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration);
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
                        await CommonActions.ApplyAndAddRotation(CubeRotations.XClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFaceTwice(FaceType.Right, configuration.MinInnerLayerIndex()), solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFaceTwice(FaceType.Right, configuration.MinInnerLayerIndex()), solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFaceTwice(FaceType.Right, configuration.MinInnerLayerIndex()), solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.ByFaceTwice(FaceType.Right, configuration.MinInnerLayerIndex()), solution, configuration);
                        await CommonActions.ApplyAndAddRotation(CubeRotations.XAntiClockwise, solution, configuration);
                        break;
                    }

                    await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration);
                }
            }
        }
    }
}
