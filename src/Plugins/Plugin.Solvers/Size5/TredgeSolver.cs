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
            } while (previousSolution.Count != solution.Count);

            await CheckMiddleLayersOnFace(configuration, solution, FaceType.Left);
            // todo check other faces

            return solution;
        }

        private Task CheckMiddleLayersOnFace(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, FaceType face)
        {
            // TODO: DO THIS
            //var rightEdgeOnFace = configuration.Faces[face].GetEdge(Edge.Right);
            //var top = rightEdgeOnFace[1];
            //var bottom = rightEdgeOnFace[3];

            //var leftEdgeOnJoiningFace = 

            return TaskEx.Completed;
        }

        private static IRotation GetRotationToPutTredgeOnFront(bool upperLayer, FaceType face)
        {
            if (upperLayer)
            {
                switch (face)
                {
                    case FaceType.Front:
                        return null;
                    case FaceType.Left:
                        return Rotations.UpperAntiClockwise;
                    case FaceType.Right:
                        return Rotations.UpperClockwise;
                    case FaceType.Back:
                        return Rotations.Upper2;
                }
            }
            else
            {
                switch (face)
                {
                    case FaceType.Front:
                        return null;
                    case FaceType.Left:
                        return Rotations.DownClockwise;
                    case FaceType.Right:
                        return Rotations.DownAntiClockwise;
                    case FaceType.Back:
                        return Rotations.Down2;
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
            if (face == FaceType.Back)
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
