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

            for (int i = 0; i <= 3; i++)
            {
                await CheckUpperFace(configuration, solution);
                await CommonActions.ApplyAndAddRotation(CubeRotations.ZClockwise, solution, configuration);
            }

            await CheckBackFace(configuration, solution);

            return solution;
        }

        private static async Task CheckUpperFace(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            List<IRotation> previousSolution;
            var frontFaceColour = configuration.Faces[FaceType.Front].Centre;

            do
            {
                previousSolution = new List<IRotation>(solution);

                await CheckUpperTopLeft(configuration, solution, frontFaceColour);
                await CheckUpperTopRight(configuration, solution, frontFaceColour);
                await CheckUpperBottomRight(configuration, solution, frontFaceColour);
                await CheckUpperBottomLeft(configuration, solution, frontFaceColour);
            } while (solution.Count != previousSolution.Count);
        }

        private static async Task CheckBackFace(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            List<IRotation> previousSolution;
            var frontFaceColour = configuration.Faces[FaceType.Front].Centre;

            do
            {
                previousSolution = new List<IRotation>(solution);

                //await CheckBackTopLeft(configuration, solution, frontFaceColour);
                //await CheckBackTopRight(configuration, solution, frontFaceColour);
                await CheckBackBottomRight(configuration, solution, frontFaceColour);
                //await CheckBackBottomLeft(configuration, solution, frontFaceColour);
            } while (solution.Count != previousSolution.Count);
        }

        private static async Task CheckUpperBottomLeft(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, FaceColour frontFaceColour)
        {
            var bottomLeft = configuration.Faces[FaceType.Upper].GetEdge(1, Edge.Bottom)[1];
            if (bottomLeft == frontFaceColour)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (configuration.Faces[FaceType.Front].GetEdge(1, Edge.Top)[1] != frontFaceColour)
                    {
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeftAntiClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeftClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeftAntiClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeftClockwise, solution, configuration);
                        break;
                    }

                    await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration);
                }
            }
        }

        private static async Task CheckUpperBottomRight(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, FaceColour frontFaceColour)
        {
            var topRight = configuration.Faces[FaceType.Upper].GetEdge(1, Edge.Bottom)[3];
            if (topRight == frontFaceColour)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (configuration.Faces[FaceType.Front].GetEdge(1, Edge.Top)[3] != frontFaceColour)
                    {
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRightClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRightAntiClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRightClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRightAntiClockwise, solution, configuration);
                        break;
                    }

                    await CommonActions.ApplyAndAddRotation(Rotations.FrontAntiClockwise, solution, configuration);
                }
            }
        }

        private static async Task CheckUpperTopRight(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, FaceColour frontFaceColour)
        {
            var topRight = configuration.Faces[FaceType.Upper].GetEdge(1, Edge.Top)[3];
            if (topRight == frontFaceColour)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (configuration.Faces[FaceType.Front].GetEdge(1, Edge.Bottom)[3] != frontFaceColour)
                    {
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRightClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRightAntiClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRightClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRightAntiClockwise, solution, configuration);
                        break;
                    }

                    await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration);
                }
            }
        }

        private static async Task CheckUpperTopLeft(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, FaceColour frontFaceColour)
        {
            var topLeft = configuration.Faces[FaceType.Upper].GetEdge(1, Edge.Top)[1];
            if (topLeft == frontFaceColour)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (configuration.Faces[FaceType.Front].GetEdge(1, Edge.Bottom)[1] != frontFaceColour)
                    {
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeftAntiClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeftClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeftAntiClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeftClockwise, solution, configuration);
                        break;
                    }

                    await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration);
                }
            }
        }

        private static async Task CheckBackBottomLeft(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, FaceColour frontFaceColour)
        {
            var bottomLeft = configuration.Faces[FaceType.Back].GetEdge(1, Edge.Bottom)[1];
            if (bottomLeft == frontFaceColour)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (configuration.Faces[FaceType.Front].GetEdge(1, Edge.Bottom)[3] != frontFaceColour)
                    {
                        await CommonActions.ApplyAndAddRotation(CubeRotations.XClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRight2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRight2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRight2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRight2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(CubeRotations.XAntiClockwise, solution, configuration);
                        break;
                    }

                    await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration);
                }
            }
        }

        private static async Task CheckBackBottomRight(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, FaceColour frontFaceColour)
        {
            var topRight = configuration.Faces[FaceType.Back].GetEdge(1, Edge.Bottom)[3];
            if (topRight == frontFaceColour)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (configuration.Faces[FaceType.Front].GetEdge(1, Edge.Bottom)[1] != frontFaceColour)
                    {
                        var frontFace = configuration.Faces[FaceType.Front].Items;
                        await CommonActions.ApplyAndAddRotation(CubeRotations.XClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeft2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeft2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeft2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeft2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(CubeRotations.XAntiClockwise, solution, configuration);
                        var frontFace2 = configuration.Faces[FaceType.Front].Items;
                        break;
                    }

                    await CommonActions.ApplyAndAddRotation(Rotations.FrontAntiClockwise, solution, configuration);
                }
            }
        }

        private static async Task CheckBackTopRight(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, FaceColour frontFaceColour)
        {
            var topRight = configuration.Faces[FaceType.Back].GetEdge(1, Edge.Top)[3];
            if (topRight == frontFaceColour)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (configuration.Faces[FaceType.Front].GetEdge(1, Edge.Top)[1] != frontFaceColour)
                    {
                        await CommonActions.ApplyAndAddRotation(CubeRotations.XClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeft2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeft2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeft2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeft2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(CubeRotations.XAntiClockwise, solution, configuration);
                        break;
                    }

                    await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration);
                }
            }
        }

        private static async Task CheckBackTopLeft(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, FaceColour frontFaceColour)
        {
            var topLeft = configuration.Faces[FaceType.Back].GetEdge(1, Edge.Top)[1];
            if (topLeft == frontFaceColour)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (configuration.Faces[FaceType.Front].GetEdge(1, Edge.Top)[3] != frontFaceColour)
                    {
                        await CommonActions.ApplyAndAddRotation(CubeRotations.XClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRight2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRight2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRight2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRight2, solution, configuration);
                        await CommonActions.ApplyAndAddRotation(CubeRotations.XAntiClockwise, solution, configuration);
                        break;
                    }

                    await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration);
                }
            }
        }
    }
}
