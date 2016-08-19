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

            await Repeat.SolvingUntilNoMovesCanBeMade(solution, async () =>
            {
                await CheckUpperAndDownEdgesOnFace(configuration, solution, FaceType.Front).ConfigureAwait(false);
                await CheckUpperAndDownEdgesOnFace(configuration, solution, FaceType.Left).ConfigureAwait(false);
                await CheckUpperAndDownEdgesOnFace(configuration, solution, FaceType.Back).ConfigureAwait(false);
                await CheckUpperAndDownEdgesOnFace(configuration, solution, FaceType.Right).ConfigureAwait(false);
                await CheckMiddleLayersOnFace(configuration, solution, FaceType.Right).ConfigureAwait(false);
                await CheckMiddleLayersOnFace(configuration, solution, FaceType.Back).ConfigureAwait(false);
                await CheckMiddleLayersOnFace(configuration, solution, FaceType.Front).ConfigureAwait(false);
                await CheckFlipped(configuration, solution).ConfigureAwait(false);
            }).ConfigureAwait(false);


            return solution;
        }

        private static async Task CheckFlipped(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            var frontFaceEdge = configuration.Faces[FaceType.Front].GetEdge(Edge.Left);
            var leftFaceEdge = configuration.Faces[FaceType.Left].GetEdge(Edge.Right);

            var frontColour = frontFaceEdge.Centre();
            var leftColour = leftFaceEdge.Centre();

            var centreLayer = configuration.GetCentreLayer();
            for (int layer = 1; layer < centreLayer; layer++)
            {
                var outerLayer = centreLayer + (centreLayer - layer);

                if (frontFaceEdge[layer] == leftColour && frontFaceEdge[outerLayer] == leftColour &&
                    leftFaceEdge[layer] == frontColour && leftFaceEdge[outerLayer] == frontColour)
                {
                    var upperRotation = Rotations.ByFace(FaceType.Upper, RotationDirection.AntiClockwise, layer);
                    var downRotation = Rotations.ByFace(FaceType.Down, RotationDirection.Clockwise, layer);

                    await CommonActions.ApplyAndAddRotation(upperRotation, solution, configuration).ConfigureAwait(false);
                    await CommonActions.ApplyAndAddRotation(downRotation, solution, configuration).ConfigureAwait(false);
                    await PerformFlip(solution, configuration).ConfigureAwait(false);
                    await CommonActions.ApplyAndAddRotation(upperRotation.Reverse(), solution, configuration).ConfigureAwait(false);
                    await CommonActions.ApplyAndAddRotation(downRotation.Reverse(), solution, configuration).ConfigureAwait(false);

                }
            }
        }

        private static async Task CheckMiddleLayersOnFace(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution, FaceType face)
        {
            var frontFaceColour = configuration.Faces[FaceType.Front].LeftCentre();
            var leftFaceColour = configuration.Faces[FaceType.Left].RightCentre();

            var centreLayer = configuration.GetCentreLayer();
            for (int layer = 1; layer < centreLayer; layer++)
            {
                var outerLayer = configuration.GetCentreLayer() + (configuration.GetCentreLayer() - layer);

                var rightEdgeOnFace = configuration.Faces[face].GetEdge(Edge.Right);
                var top = rightEdgeOnFace[layer];
                var bottom = rightEdgeOnFace[outerLayer];

                var joiningFace = FaceRules.FaceAtRelativePositionTo(face, RelativePosition.Right);
                var leftEdgeOnJoiningFace = configuration.Faces[joiningFace].GetEdge(Edge.Left);
                var joiningFaceEdgeTop = leftEdgeOnJoiningFace[layer];
                var joiningFaceEdgeBottom = leftEdgeOnJoiningFace[outerLayer];


                if ((top == frontFaceColour && joiningFaceEdgeTop == leftFaceColour) ||
                    (bottom == frontFaceColour && joiningFaceEdgeBottom == leftFaceColour) ||
                    (top == leftFaceColour && joiningFaceEdgeTop == frontFaceColour) ||
                    (bottom == leftFaceColour && joiningFaceEdgeBottom == frontFaceColour))
                {
                    var rotationToBringEdgeToFront = GetRotationToPutTredgeOnFront((top == frontFaceColour && joiningFaceEdgeTop == leftFaceColour) || (top == leftFaceColour && joiningFaceEdgeTop == frontFaceColour), face, layer);
                    await CommonActions.ApplyAndAddRotation(rotationToBringEdgeToFront, solution, configuration).ConfigureAwait(false);

                }

                if ((top == frontFaceColour && joiningFaceEdgeTop == leftFaceColour) ||
                    (bottom == frontFaceColour && joiningFaceEdgeBottom == leftFaceColour))
                {
                    await PerformFlip(solution, configuration).ConfigureAwait(false);

                }

                rightEdgeOnFace = configuration.Faces[face].GetEdge(Edge.Right);
                top = rightEdgeOnFace[layer];
                bottom = rightEdgeOnFace[outerLayer];

                leftEdgeOnJoiningFace = configuration.Faces[joiningFace].GetEdge(Edge.Left);
                joiningFaceEdgeTop = leftEdgeOnJoiningFace[layer];
                joiningFaceEdgeBottom = leftEdgeOnJoiningFace[outerLayer];


                if (top == leftFaceColour && joiningFaceEdgeTop == frontFaceColour)
                {
                    var rotation = Rotations.ByFace(FaceType.Upper, RotationDirection.Clockwise, layer);
                    await CommonActions.ApplyAndAddRotation(rotation, solution, configuration).ConfigureAwait(false);
                }
                if (bottom == leftFaceColour && joiningFaceEdgeBottom == frontFaceColour)
                {
                    var rotation = Rotations.ByFace(FaceType.Down, RotationDirection.AntiClockwise, layer);
                    await CommonActions.ApplyAndAddRotation(rotation, solution, configuration).ConfigureAwait(false);
                }
            }
        }

        private static async Task PerformFlip(ICollection<IRotation> solution, IRotatable configuration)
        {
            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration).ConfigureAwait(false);
            await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);
            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration).ConfigureAwait(false);
            await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration).ConfigureAwait(false);
            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration).ConfigureAwait(false);
            await CommonActions.ApplyAndAddRotation(Rotations.FrontAntiClockwise, solution, configuration).ConfigureAwait(false);
            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration).ConfigureAwait(false);
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

            var centreLayer = configuration.GetCentreLayer();
            for (int layer = 1; layer < centreLayer; layer++)
            {
                var match = MatchColoursOnUpperFaceEdge(configuration, frontFaceColour, leftFaceColour, faceToCheck, layer);
                if (match != TredgeMatch.None)
                {
                    var rotation = GetRotationToPutTredgeOnFront(true, faceToCheck);
                    await CommonActions.ApplyAndAddRotation(rotation, solution, configuration).ConfigureAwait(false);

                    await CheckFrontUpper(configuration, solution, frontFaceColour, leftFaceColour, layer).ConfigureAwait(false);
                }

                match = MatchColoursOnDownFaceEdge(configuration, frontFaceColour, leftFaceColour, faceToCheck, layer);
                if (match != TredgeMatch.None)
                {
                    var rotation = GetRotationToPutTredgeOnFront(false, faceToCheck);
                    await CommonActions.ApplyAndAddRotation(rotation, solution, configuration).ConfigureAwait(false);

                    await CheckFrontDown(configuration, solution, frontFaceColour, leftFaceColour, layer).ConfigureAwait(false);
                }
            }
        }

        private static async Task CheckFrontDown(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, FaceColour frontFaceColour, FaceColour leftFaceColour, int layer)
        {
            var match = MatchColoursOnDownFaceEdge(configuration, frontFaceColour, leftFaceColour, FaceType.Front, layer);
            if (match != TredgeMatch.None)
            {
                await MatchingTredgeIsOnDownFront(configuration, solution, match, layer).ConfigureAwait(false);
            }
        }

        private static async Task CheckFrontUpper(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution, FaceColour frontFaceColour, FaceColour leftFaceColour, int layer)
        {
            var match = MatchColoursOnUpperFaceEdge(configuration, frontFaceColour, leftFaceColour, FaceType.Front, layer);
            if (match != TredgeMatch.None)
            {
                await MatchingTredgeIsOnUpperFront(configuration, solution, match, layer).ConfigureAwait(false);
            }
        }

        private static async Task MatchingTredgeIsOnUpperFront(IRotatable configuration, ICollection<IRotation> solution, TredgeMatch match, int layer)
        {
            if (match == TredgeMatch.FrontLeftMatchesCenter || match == TredgeMatch.FrontRightMatchesCenter)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration).ConfigureAwait(false);
                await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);
                await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration).ConfigureAwait(false);


                if (match == TredgeMatch.FrontLeftMatchesCenter)
                {
                    var rotation = Rotations.ByFace(FaceType.Down, RotationDirection.AntiClockwise, layer);
                    await CommonActions.ApplyAndAddRotation(rotation, solution, configuration).ConfigureAwait(false);
                }
                else
                {
                    var rotation = Rotations.ByFace(FaceType.Upper, RotationDirection.Clockwise, layer);
                    await CommonActions.ApplyAndAddRotation(rotation, solution, configuration).ConfigureAwait(false);
                }
                return;
            }

            await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);
            await CommonActions.ApplyAndAddRotation(Rotations.FrontAntiClockwise, solution, configuration).ConfigureAwait(false);
            await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);
            await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration).ConfigureAwait(false);

            if (match == TredgeMatch.UpperLeftMatchesCenter)
            {
                var rotation = Rotations.ByFace(FaceType.Upper, RotationDirection.Clockwise, layer);
                await CommonActions.ApplyAndAddRotation(rotation, solution, configuration).ConfigureAwait(false);
            }
            else
            {
                var rotation = Rotations.ByFace(FaceType.Down, RotationDirection.AntiClockwise, layer);
                await CommonActions.ApplyAndAddRotation(rotation, solution, configuration).ConfigureAwait(false);
            }
        }

        private static TredgeMatch MatchColoursOnUpperFaceEdge(CubeConfiguration<FaceColour> configuration, FaceColour frontFaceColour, FaceColour leftColour, FaceType face, int layer)
        {
            var outerLayer = configuration.GetCentreLayer() + (configuration.GetCentreLayer() - layer);
            var topEdge = configuration.Faces[face].GetEdge(Edge.Top);
            if (face == FaceType.Back || face == FaceType.Right)
            {
                topEdge = topEdge.Reverse().ToArray();
            }
            var frontTopLeft = topEdge[layer];
            var frontTopRight = topEdge[outerLayer];

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
            var upperBottomLeft = upperFaceEdge[layer];
            var upperBottomRight = upperFaceEdge[outerLayer];

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

        private static TredgeMatch MatchColoursOnDownFaceEdge(CubeConfiguration<FaceColour> configuration, FaceColour frontColour, FaceColour leftColour, FaceType face, int layer)
        {
            var outerLayer = configuration.GetCentreLayer() + (configuration.GetCentreLayer() - layer);
            var bottomEdge = configuration.Faces[face].GetEdge(Edge.Bottom);
            var frontBottomLeft = bottomEdge[layer];
            var frontBottomRight = bottomEdge[outerLayer];

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
            var downTopLeft = downLayerEdge[layer];
            var downTopRight = downLayerEdge[outerLayer];

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

        private static async Task MatchingTredgeIsOnDownFront(IRotatable configuration, ICollection<IRotation> solution, TredgeMatch match, int layer)
        {
            if (match == TredgeMatch.FrontLeftMatchesCenter || match == TredgeMatch.FrontRightMatchesCenter)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration).ConfigureAwait(false);
                await CommonActions.ApplyAndAddRotation(Rotations.DownClockwise, solution, configuration).ConfigureAwait(false);
                await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration).ConfigureAwait(false);

                if (match == TredgeMatch.FrontLeftMatchesCenter)
                {
                    var rotation = Rotations.ByFace(FaceType.Upper, RotationDirection.Clockwise, layer);
                    await CommonActions.ApplyAndAddRotation(rotation, solution, configuration).ConfigureAwait(false);
                }
                else
                {
                    var rotation = Rotations.ByFace(FaceType.Down, RotationDirection.AntiClockwise, layer);
                    await CommonActions.ApplyAndAddRotation(rotation, solution, configuration).ConfigureAwait(false);
                }
                return;
            }

            await CommonActions.ApplyAndAddRotation(Rotations.DownClockwise, solution, configuration).ConfigureAwait(false);
            await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration).ConfigureAwait(false);
            await CommonActions.ApplyAndAddRotation(Rotations.DownAntiClockwise, solution, configuration).ConfigureAwait(false);
            await CommonActions.ApplyAndAddRotation(Rotations.FrontAntiClockwise, solution, configuration).ConfigureAwait(false);

            if (match == TredgeMatch.UpperLeftMatchesCenter)
            {
                var rotation = Rotations.ByFace(FaceType.Down, RotationDirection.AntiClockwise, layer);
                await CommonActions.ApplyAndAddRotation(rotation, solution, configuration).ConfigureAwait(false);
            }
            else
            {
                var rotation = Rotations.ByFace(FaceType.Upper, RotationDirection.Clockwise, layer);
                await CommonActions.ApplyAndAddRotation(rotation, solution, configuration).ConfigureAwait(false);
            }
        }
    }
}
