using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size5
{
    internal class InnerSquareSolver : IPartialSolver
    {
        public async Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration)
        {
            var solution = new List<IRotation>();

            await CheckUpperFace(configuration, solution);

            return solution;
        }

        private async Task CheckUpperFace(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            List<IRotation> previousSolution;
            var frontFaceColour = configuration.Faces[FaceType.Front].Centre;

            do
            {
                previousSolution = new List<IRotation>(solution);

                await CheckTopLeft(configuration, solution, frontFaceColour);
                await CheckTopRight(configuration, solution, frontFaceColour);
                await CheckBottomRight(configuration, solution, frontFaceColour);
                await CheckBottomLeft(configuration, solution, frontFaceColour);
            } while (solution.Count != previousSolution.Count);
        }

        private static async Task CheckBottomLeft(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, FaceColour frontFaceColour)
        {
            var bottomLeft = configuration.Faces[FaceType.Upper].GetEdge(1, Edge.Bottom).First();
            if (bottomLeft == frontFaceColour)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (configuration.Faces[FaceType.Front].GetEdge(1, Edge.Top).First() != frontFaceColour)
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

        private static async Task CheckBottomRight(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, FaceColour frontFaceColour)
        {
            var topRight = configuration.Faces[FaceType.Upper].GetEdge(1, Edge.Bottom).Last();
            if (topRight == frontFaceColour)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (configuration.Faces[FaceType.Front].GetEdge(1, Edge.Top).Last() != frontFaceColour)
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

        private static async Task CheckTopRight(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, FaceColour frontFaceColour)
        {
            var topRight = configuration.Faces[FaceType.Upper].GetEdge(1, Edge.Top).Last();
            if (topRight == frontFaceColour)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (configuration.Faces[FaceType.Front].GetEdge(1, Edge.Bottom).Last() != frontFaceColour)
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

        private static async Task CheckTopLeft(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, FaceColour frontFaceColour)
        {
            var topLeft = configuration.Faces[FaceType.Upper].GetEdge(1, Edge.Top).First();
            if (topLeft == frontFaceColour)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (configuration.Faces[FaceType.Front].GetEdge(1, Edge.Bottom).First() != frontFaceColour)
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
    }
}
