using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size5
{
    internal class SingleTredgeSolver : IPartialSolver
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
                await CheckFlipped(configuration, solution);
            } while (previousSolution.Count != solution.Count);

            return solution;
        }

        private async Task CheckFlipped(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            var frontFaceEdge = configuration.Faces[FaceType.Front].GetEdge(Edge.Left);
            var leftFaceEdge = configuration.Faces[FaceType.Left].GetEdge(Edge.Right);

            var frontColour = frontFaceEdge.Centre();
            var leftColour = leftFaceEdge.Centre();

            if (frontFaceEdge[configuration.MinInnerLayerIndex()] == leftColour && frontFaceEdge[configuration.MaxInnerLayerIndex()] == leftColour &&
                leftFaceEdge[configuration.MinInnerLayerIndex()] == frontColour && leftFaceEdge[configuration.MaxInnerLayerIndex()] == frontColour)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperAntiClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerDownClockwise, solution, configuration);
                await PerformFlip(solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerDownAntiClockwise, solution, configuration);
            }
        }

        private async Task CheckMiddleLayersOnFace(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, FaceType face)
        {
            var frontFaceColour = configuration.Faces[FaceType.Front].LeftCentre();
            var leftFaceColour = configuration.Faces[FaceType.Left].RightCentre();

            var rightEdgeOnFace = configuration.Faces[face].GetEdge(Edge.Right);
            var top = rightEdgeOnFace[configuration.MinInnerLayerIndex()];
            var bottom = rightEdgeOnFace[configuration.MaxInnerLayerIndex()];

            var joiningFace = FaceRules.FaceAtRelativePositionTo(face, RelativePosition.Right);
            var leftEdgeOnJoiningFace = configuration.Faces[joiningFace].GetEdge(Edge.Left);
            var joiningFaceEdgeTop = leftEdgeOnJoiningFace[configuration.MinInnerLayerIndex()];
            var joiningFaceEdgeBottom = leftEdgeOnJoiningFace[configuration.MaxInnerLayerIndex()];


            if ((top == frontFaceColour && joiningFaceEdgeTop == leftFaceColour) ||
                (bottom == frontFaceColour && joiningFaceEdgeBottom == leftFaceColour) ||
                (top == leftFaceColour && joiningFaceEdgeTop == frontFaceColour) ||
                (bottom == leftFaceColour && joiningFaceEdgeBottom == frontFaceColour))
            {
                var rotationToBringEdgeToFront = GetRotationToPutTredgeOnFront((top == frontFaceColour && joiningFaceEdgeTop == leftFaceColour) || (top == leftFaceColour && joiningFaceEdgeTop == frontFaceColour), face, configuration.MinInnerLayerIndex());
                await CommonActions.ApplyAndAddRotation(rotationToBringEdgeToFront, solution, configuration);
            }

            if ((top == frontFaceColour && joiningFaceEdgeTop == leftFaceColour) ||
                (bottom == frontFaceColour && joiningFaceEdgeBottom == leftFaceColour))
            {
                await PerformFlip(solution, configuration);
            }

            rightEdgeOnFace = configuration.Faces[face].GetEdge(Edge.Right);
            top = rightEdgeOnFace[configuration.MinInnerLayerIndex()];
            bottom = rightEdgeOnFace[configuration.MaxInnerLayerIndex()];

            leftEdgeOnJoiningFace = configuration.Faces[joiningFace].GetEdge(Edge.Left);
            joiningFaceEdgeTop = leftEdgeOnJoiningFace[configuration.MinInnerLayerIndex()];
            joiningFaceEdgeBottom = leftEdgeOnJoiningFace[configuration.MaxInnerLayerIndex()];


            if (top == leftFaceColour && joiningFaceEdgeTop == frontFaceColour)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperClockwise, solution, configuration);
            }
            if (bottom == leftFaceColour && joiningFaceEdgeBottom == frontFaceColour)
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
            var frontTopLeft = topEdge[configuration.MinInnerLayerIndex()];
            var frontTopRight = topEdge[configuration.MaxInnerLayerIndex()];

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
            var upperBottomLeft = upperFaceEdge[configuration.MinInnerLayerIndex()];
            var upperBottomRight = upperFaceEdge[configuration.MaxInnerLayerIndex()];

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
            var frontBottomLeft = bottomEdge[configuration.MinInnerLayerIndex()];
            var frontBottomRight = bottomEdge[configuration.MaxInnerLayerIndex()];

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
            var downTopLeft = downLayerEdge[configuration.MinInnerLayerIndex()];
            var downTopRight = downLayerEdge[configuration.MaxInnerLayerIndex()];

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
