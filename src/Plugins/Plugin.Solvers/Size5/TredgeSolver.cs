using System.Collections.Generic;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size5
{
    internal class TredgeSolver : IPartialSolver
    {
        private enum TredgeMatch
        {
            None = 0,
            FrontLeftMatchesCenter,
            FrontRightMatchesCenter,
            UpperLeftMatchesCenter,
            UpperRightMatchesCenter,
        }

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
            var frontFaceColour = configuration.Faces[FaceType.Front].LeftCentre();
            var leftFaceColour = configuration.Faces[FaceType.Left].RightCentre();
            var match = MatchColoursOnUpperFaceEdge(configuration, frontFaceColour, leftFaceColour, FaceType.Front);
            if (match == TredgeMatch.None) return;

            if (match == TredgeMatch.FrontLeftMatchesCenter || match == TredgeMatch.FrontRightMatchesCenter)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration);

                if (match == TredgeMatch.FrontLeftMatchesCenter)
                {
                    await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerDownAntiClockwise, solution, configuration);
                }
                else
                {
                    await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperClockwise, solution, configuration);
                }
                return;
            }

            // TODO: CHECK THIS
            await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.FrontAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration);

            if (match == TredgeMatch.UpperLeftMatchesCenter)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerDownAntiClockwise, solution, configuration);
            }
            else
            {
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperClockwise, solution, configuration);
            }
        }

        private static TredgeMatch MatchColoursOnUpperFaceEdge(CubeConfiguration<FaceColour> configuration, FaceColour frontFaceColour, FaceColour leftColour, FaceType face)
        {
            var frontTopLeft = configuration.Faces[face].GetEdge(Edge.Top)[1];
            var upperBottomLeft = configuration.Faces[FaceType.Upper].GetEdge(Edge.Bottom)[1];
            var frontTopRight = configuration.Faces[face].GetEdge(Edge.Top)[3];
            var upperBottomRight = configuration.Faces[FaceType.Upper].GetEdge(Edge.Bottom)[3];
            if (frontTopLeft == frontFaceColour && upperBottomLeft == leftColour)
            {
                return TredgeMatch.FrontLeftMatchesCenter;
            }
            if (frontTopRight == frontFaceColour && upperBottomRight == leftColour)
            {
                return TredgeMatch.FrontRightMatchesCenter;
            }
            
            if (frontTopLeft == leftColour && upperBottomLeft == frontFaceColour)
            {
                return TredgeMatch.UpperLeftMatchesCenter;
            }
            if (frontTopRight == leftColour && upperBottomRight == frontFaceColour)
            {
                return TredgeMatch.UpperRightMatchesCenter;
            }

            return TredgeMatch.None;
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
