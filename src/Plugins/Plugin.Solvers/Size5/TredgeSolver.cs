using System.Collections.Generic;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size5
{
    internal class TredgeSolver : IPartialSolver
    {
        public async Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration)
        {
            var solution = new List<IRotation>();
            solution.Add(await CommonActions.PositionOnBottom(configuration, FaceColour.Yellow));

            await CheckFrontUpper(configuration, solution);
            await CheckFrontDown(configuration, solution);

            return solution;
        }

        private static async Task CheckFrontUpper(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            var frontColour = configuration.Faces[FaceType.Front].LeftCentre();
            var leftColour = configuration.Faces[FaceType.Left].RightCentre();

            var frontTopLeft = configuration.Faces[FaceType.Front].GetEdge(Edge.Top)[1];
            var frontTopRight = configuration.Faces[FaceType.Front].GetEdge(Edge.Top)[3];
            var upperBottomLeft = configuration.Faces[FaceType.Upper].GetEdge(Edge.Bottom)[1];
            var upperBottomRight = configuration.Faces[FaceType.Upper].GetEdge(Edge.Bottom)[3];
            if ((frontTopLeft == frontColour && upperBottomLeft == leftColour) ||
                (frontTopRight == frontColour && upperBottomRight == leftColour))
            {
                await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration);

                if (frontTopLeft == frontColour && upperBottomLeft == leftColour)
                {
                    await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerDownAntiClockwise, solution, configuration);
                }
                else
                {
                    await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperClockwise, solution, configuration);
                }
            }
            // TODO: CHECK THIS
            if ((frontTopLeft == leftColour && upperBottomLeft == frontColour) ||
                (frontTopRight == leftColour && upperBottomRight == frontColour))
            {
                await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.FrontAntiClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration);

                if (frontTopLeft == leftColour && upperBottomLeft == frontColour)
                {
                    await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerDownAntiClockwise, solution, configuration);
                }
                else
                {
                    await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperClockwise, solution, configuration);
                }
            }
        }

        private static async Task CheckFrontDown(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            var frontColour = configuration.Faces[FaceType.Front].LeftCentre();
            var leftColour = configuration.Faces[FaceType.Left].RightCentre();

            var frontBottomLeft = configuration.Faces[FaceType.Front].GetEdge(Edge.Bottom)[1];
            var frontBottomRight = configuration.Faces[FaceType.Front].GetEdge(Edge.Bottom)[3];
            var downTopLeft = configuration.Faces[FaceType.Down].GetEdge(Edge.Top)[1];
            var downTopRight = configuration.Faces[FaceType.Down].GetEdge(Edge.Top)[3];
            if ((frontBottomLeft == frontColour && downTopLeft == leftColour) ||
                (frontBottomRight == frontColour && downTopRight == leftColour))
            {
                await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.DownClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration);

                if (frontBottomLeft == frontColour && downTopLeft == leftColour)
                {
                    await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerDownAntiClockwise, solution, configuration);
                }
                else
                {
                    await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperClockwise, solution, configuration);
                }
            }
            // TODO: CHECK THIS
            if ((frontBottomLeft == leftColour && downTopLeft == frontColour) ||
                (frontBottomRight == leftColour && downTopRight == frontColour))
            {
                await CommonActions.ApplyAndAddRotation(Rotations.DownAntiClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.FrontAntiClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.DownClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration);

                if (frontBottomLeft == leftColour && downTopLeft == frontColour)
                {
                    await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerDownAntiClockwise, solution, configuration);
                }
                else
                {
                    await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperClockwise, solution, configuration);
                }
            }
        }
    }
}
