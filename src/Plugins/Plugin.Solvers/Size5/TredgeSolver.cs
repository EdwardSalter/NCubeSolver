using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NCubeSolvers.Core;
using NCubeSolvers.Core.Extensions;

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
            DownLeftMatchesCenter,
            DownRightMatchesCenter
        }

        public async Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration)
        {
            var solution = new List<IRotation>();
            solution.Add(await CommonActions.PositionOnBottom(configuration, FaceColour.Yellow));
            List<IRotation> previousSolution;
            do
            {
                previousSolution = new List<IRotation>(solution);

                await CheckUpperAndDownEdgesOnFace(configuration, solution, FaceType.Front);
                await CheckUpperAndDownEdgesOnFace(configuration, solution, FaceType.Left);
                await CheckUpperAndDownEdgesOnFace(configuration, solution, FaceType.Back);
                await CheckUpperAndDownEdgesOnFace(configuration, solution, FaceType.Right);

                await CheckMiddleLayersOnFace(configuration, solution, FaceType.Right);
                await CheckMiddleLayersOnFace(configuration, solution, FaceType.Back);
                await CheckMiddleLayersOnFace(configuration, solution, FaceType.Front);
            } while (previousSolution.Count != solution.Count);

            return solution;
        }

        private async Task CheckMiddleLayersOnFace(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, FaceType face)
        {
            var frontFaceColour = configuration.Faces[FaceType.Front].LeftCentre();
            var leftFaceColour = configuration.Faces[FaceType.Left].RightCentre();

            var rightEdgeOnFace = configuration.Faces[face].GetEdge(Edge.Right);
            var top = rightEdgeOnFace[1];
            var bottom = rightEdgeOnFace[3];

            var joiningFace = FaceRules.FaceAtRelativePositionTo(face, RelativePosition.Right);
            var leftEdgeOnJoiningFace = configuration.Faces[joiningFace].GetEdge(Edge.Left);
            var joingingFaceEdgeTop = leftEdgeOnJoiningFace[1];
            var joingingFaceEdgeBottom = leftEdgeOnJoiningFace[3];


            if ((top == frontFaceColour && joingingFaceEdgeTop == leftFaceColour) ||
                (bottom == frontFaceColour && joingingFaceEdgeBottom == leftFaceColour) ||
                (top == leftFaceColour && joingingFaceEdgeTop == frontFaceColour) ||
                (bottom == leftFaceColour && joingingFaceEdgeBottom == frontFaceColour))
            {
                var rotationToBringEdgeToFront = GetRotationToPutTredgeOnFront((top == frontFaceColour && joingingFaceEdgeTop == leftFaceColour) || (top == leftFaceColour && joingingFaceEdgeTop == frontFaceColour), face, 1);
                await CommonActions.ApplyAndAddRotation(rotationToBringEdgeToFront, solution, configuration);
            }

            if ((top == frontFaceColour && joingingFaceEdgeTop == leftFaceColour) ||
                (bottom == frontFaceColour && joingingFaceEdgeBottom == leftFaceColour))
            {
                await PerformFlip(solution, configuration);
            }

            rightEdgeOnFace = configuration.Faces[face].GetEdge(Edge.Right);
            top = rightEdgeOnFace[1];
            bottom = rightEdgeOnFace[3];

            leftEdgeOnJoiningFace = configuration.Faces[joiningFace].GetEdge(Edge.Left);
            joingingFaceEdgeTop = leftEdgeOnJoiningFace[1];
            joingingFaceEdgeBottom = leftEdgeOnJoiningFace[3];


            if (top == leftFaceColour && joingingFaceEdgeTop == frontFaceColour)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperClockwise, solution, configuration);
            }
            if (bottom == leftFaceColour && joingingFaceEdgeBottom == frontFaceColour)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerDownAntiClockwise, solution, configuration);
            }
        }

        private async Task PerformFlip(List<IRotation> solution, CubeConfiguration<FaceColour> configuration)
        {
            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.FrontAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration);
        }

        private static IRotation GetRotationToPutTredgeOnFront(bool upperLayer, FaceType face, int layer = 0)
        {
            if (upperLayer)
            {
                switch (face)
                {
                    case FaceType.Front:
                        return null;
                    case FaceType.Left:
                        return Rotations.ByFace(FaceType.Upper, RotationDirection.AntiClockwise, layer);
                    case FaceType.Right:
                        return Rotations.ByFace(FaceType.Upper, RotationDirection.Clockwise, layer);
                    case FaceType.Back:
                        return Rotations.ByFaceTwice(FaceType.Upper, layer);
                }
            }
            else
            {
                switch (face)
                {
                    case FaceType.Front:
                        return null;
                    case FaceType.Left:
                        return Rotations.ByFace(FaceType.Down, RotationDirection.Clockwise, layer);

                    case FaceType.Right:
                        return Rotations.ByFace(FaceType.Down, RotationDirection.AntiClockwise, layer);
                    case FaceType.Back:
                        return Rotations.ByFaceTwice(FaceType.Down, layer);
                }
            }

            throw new InvalidOperationException("Unhandled tredge combo");
        }

        private static async Task CheckUpperAndDownEdgesOnFace(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, FaceType faceToCheck)
        {
            var frontFaceColour = configuration.Faces[FaceType.Front].LeftCentre();
            var leftFaceColour = configuration.Faces[FaceType.Left].RightCentre();

            var match = MatchColoursOnUpperFaceEdge(configuration, frontFaceColour, leftFaceColour, faceToCheck);
            if (match != TredgeMatch.None)
            {
                var rotation = GetRotationToPutTredgeOnFront(true, faceToCheck);
                await CommonActions.ApplyAndAddRotation(rotation, solution, configuration);
                await CheckFrontUpper(configuration, solution, frontFaceColour, leftFaceColour);
            }

            match = MatchColoursOnDownFaceEdge(configuration, frontFaceColour, leftFaceColour, faceToCheck);
            if (match != TredgeMatch.None)
            {
                var rotation = GetRotationToPutTredgeOnFront(false, faceToCheck);
                await CommonActions.ApplyAndAddRotation(rotation, solution, configuration);
                await CheckFrontDown(configuration, solution, frontFaceColour, leftFaceColour);
            }
        }

        private static async Task CheckFrontDown(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, FaceColour frontFaceColour, FaceColour leftFaceColour)
        {
            var match = MatchColoursOnDownFaceEdge(configuration, frontFaceColour, leftFaceColour, FaceType.Front);
            if (match != TredgeMatch.None)
            {
                await MatchingTredgeIsOnDownFront(configuration, solution, match);
            }
        }

        private static async Task CheckFrontUpper(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, FaceColour frontFaceColour, FaceColour leftFaceColour)
        {
            var match = MatchColoursOnUpperFaceEdge(configuration, frontFaceColour, leftFaceColour, FaceType.Front);
            if (match != TredgeMatch.None)
            {
                await MatchingTredgeIsOnUpperFront(configuration, solution, match);
            }
        }

        private static async Task MatchingTredgeIsOnUpperFront(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, TredgeMatch match)
        {
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

            await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.FrontAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration);

            if (match == TredgeMatch.UpperLeftMatchesCenter)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperClockwise, solution, configuration);
            }
            else
            {
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerDownAntiClockwise, solution, configuration);
            }
        }

        private static TredgeMatch MatchColoursOnUpperFaceEdge(CubeConfiguration<FaceColour> configuration, FaceColour frontFaceColour, FaceColour leftColour, FaceType face)
        {
            var topEdge = configuration.Faces[face].GetEdge(Edge.Top);
            if (face == FaceType.Back || face == FaceType.Right)
            {
                topEdge = topEdge.Reverse().ToArray();
            }
            var frontTopLeft = topEdge[1];
            var frontTopRight = topEdge[3];

            Edge edge;
            switch (face)
            {
                case FaceType.Front:
                    edge = Edge.Bottom; break;
                case FaceType.Back:
                    edge = Edge.Top; break;
                case FaceType.Left:
                    edge = Edge.Left; break;
                case FaceType.Right:
                    edge = Edge.Right; break;
                default:
                    throw new InvalidOperationException("Cannot get connecting edge as down layer does not connect to " + face);
            }
            var upperFaceEdge = configuration.Faces[FaceType.Upper].GetEdge(edge);
            var upperBottomLeft = upperFaceEdge[1];
            var upperBottomRight = upperFaceEdge[3];

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

        private static TredgeMatch MatchColoursOnDownFaceEdge(CubeConfiguration<FaceColour> configuration, FaceColour frontColour, FaceColour leftColour, FaceType face)
        {
            var bottomEdge = configuration.Faces[face].GetEdge(Edge.Bottom);
            var frontBottomLeft = bottomEdge[1];
            var frontBottomRight = bottomEdge[3];

            Edge edge;
            switch (face)
            {
                case FaceType.Front:
                    edge = Edge.Top; break;
                case FaceType.Back:
                    edge = Edge.Bottom; break;
                case FaceType.Left:
                    edge = Edge.Left; break;
                case FaceType.Right:
                    edge = Edge.Right; break;
                default:
                    throw new InvalidOperationException("Cannot get connecting edge as down layer does not connect to " + face);
            }

            var downLayerEdge = configuration.Faces[FaceType.Down].GetEdge(edge);
            if (face == FaceType.Back || face == FaceType.Left)
            {
                downLayerEdge = downLayerEdge.Reverse().ToArray();
            }
            var downTopLeft = downLayerEdge[1];
            var downTopRight = downLayerEdge[3];

            if (frontBottomLeft == frontColour && downTopLeft == leftColour)
            {
                return TredgeMatch.FrontLeftMatchesCenter;
            }
            if (frontBottomRight == frontColour && downTopRight == leftColour)
            {
                return TredgeMatch.FrontRightMatchesCenter;
            }

            if (frontBottomLeft == leftColour && downTopLeft == frontColour)
            {
                return TredgeMatch.DownLeftMatchesCenter;
            }
            if (frontBottomRight == leftColour && downTopRight == frontColour)
            {
                return TredgeMatch.DownRightMatchesCenter;
            }

            return TredgeMatch.None;
        }

        private static async Task MatchingTredgeIsOnDownFront(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, TredgeMatch match)
        {
            if (match == TredgeMatch.FrontLeftMatchesCenter || match == TredgeMatch.FrontRightMatchesCenter)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.DownClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration);

                if (match == TredgeMatch.FrontLeftMatchesCenter)
                {
                    await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperClockwise, solution, configuration);
                }
                else
                {
                    await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerDownAntiClockwise, solution, configuration);
                }
                return;
            }

            await CommonActions.ApplyAndAddRotation(Rotations.DownClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.DownAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.FrontAntiClockwise, solution, configuration);

            if (match == TredgeMatch.UpperLeftMatchesCenter)
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
