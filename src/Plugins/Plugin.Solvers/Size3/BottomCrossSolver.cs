using System.Collections.Generic;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size3
{
    internal class BottomCrossSolver : IPartialSolver
    {
        public async Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration)
        {
            var solution = new List<IRotation>();

            var rotationToBottom = await CommonActions.PositionOnBottom(configuration, FaceColour.White).ConfigureAwait(false);

            if (rotationToBottom != null) solution.Add(rotationToBottom);

            await Repeat.SolvingUntilNoMovesCanBeMade(solution, async () =>
            {
                await CheckTopFaceForWhite(configuration, solution).ConfigureAwait(false);

                await CheckTopLayerForWhite(configuration, solution).ConfigureAwait(false);

                await CheckMiddleLayerForWhite(configuration, solution).ConfigureAwait(false);

                await CheckBottomLayerForWhite(configuration, solution).ConfigureAwait(false);

                await CheckBottomFaceForWhite(configuration, solution).ConfigureAwait(false);

            }).ConfigureAwait(false);

            return solution;
        }

        internal async Task CheckTopFaceForWhite(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            await CheckTopFaceForWhite(configuration, FaceType.Left, solution).ConfigureAwait(false);

            await CheckTopFaceForWhite(configuration, FaceType.Right, solution).ConfigureAwait(false);

            await CheckTopFaceForWhite(configuration, FaceType.Front, solution).ConfigureAwait(false);

            await CheckTopFaceForWhite(configuration, FaceType.Back, solution).ConfigureAwait(false);

        }

        internal async Task CheckTopLayerForWhite(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            await CheckTopLayerForWhite(configuration, configuration.Faces[FaceType.Left], FaceType.Left, solution).ConfigureAwait(false);

            await CheckTopLayerForWhite(configuration, configuration.Faces[FaceType.Right], FaceType.Right, solution).ConfigureAwait(false);

            await CheckTopLayerForWhite(configuration, configuration.Faces[FaceType.Front], FaceType.Front, solution).ConfigureAwait(false);

            await CheckTopLayerForWhite(configuration, configuration.Faces[FaceType.Back], FaceType.Back, solution).ConfigureAwait(false);

        }
        internal async Task CheckMiddleLayerForWhite(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            await CheckMiddleLayerForWhite(configuration, configuration.Faces[FaceType.Left], FaceType.Left, solution).ConfigureAwait(false);

            await CheckMiddleLayerForWhite(configuration, configuration.Faces[FaceType.Right], FaceType.Right, solution).ConfigureAwait(false);

            await CheckMiddleLayerForWhite(configuration, configuration.Faces[FaceType.Front], FaceType.Front, solution).ConfigureAwait(false);

            await CheckMiddleLayerForWhite(configuration, configuration.Faces[FaceType.Back], FaceType.Back, solution).ConfigureAwait(false);

        }

        internal async Task CheckBottomLayerForWhite(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            await CheckBottomLayerForWhite(configuration, FaceType.Left, solution).ConfigureAwait(false);

            await CheckBottomLayerForWhite(configuration, FaceType.Right, solution).ConfigureAwait(false);

            await CheckBottomLayerForWhite(configuration, FaceType.Front, solution).ConfigureAwait(false);

            await CheckBottomLayerForWhite(configuration, FaceType.Back, solution).ConfigureAwait(false);

        }

        internal async Task CheckBottomFaceForWhite(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            await CheckBottomFaceForWhite(configuration, FaceType.Left, solution).ConfigureAwait(false);

            await CheckBottomFaceForWhite(configuration, FaceType.Right, solution).ConfigureAwait(false);

            await CheckBottomFaceForWhite(configuration, FaceType.Front, solution).ConfigureAwait(false);

            await CheckBottomFaceForWhite(configuration, FaceType.Back, solution).ConfigureAwait(false);

        }

        private static async Task CheckBottomFaceForWhite(CubeConfiguration<FaceColour> configuration, FaceType faceType, ICollection<IRotation> solution)
        {
            var edge = FaceRules.EdgeJoiningFaceToFace(faceType, FaceType.Down);
            if (configuration.Faces[FaceType.Down].GetEdge(edge).Centre() != FaceColour.White) return;

            var joiningFace = configuration.Faces[faceType];
            var joiningColour = joiningFace.GetEdge(Edge.Bottom).Centre();

            // If the colour does not match, put it on the top face
            if (joiningColour != joiningFace.Centre)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.ByFaceTwice(faceType), solution, configuration).ConfigureAwait(false);

            }
        }

        private static async Task CheckBottomLayerForWhite(CubeConfiguration<FaceColour> configuration, FaceType faceType, ICollection<IRotation> solution)
        {
            if (configuration.Faces[faceType].GetEdge(Edge.Bottom).Centre() != FaceColour.White) return;

            var bottomFaceEdge = FaceRules.EdgeJoiningFaceToFace(faceType, FaceType.Down);
            var joiningColour = configuration.Faces[FaceType.Down].GetEdge(bottomFaceEdge).Centre();
            var faceWithJoiningColour = FaceRules.GetFaceOfColour(joiningColour, configuration);

            var relativePostion = FaceRules.RelativePositionBetweenFaces(faceType, faceWithJoiningColour);
            switch (relativePostion)
            {
                case RelativePosition.Left:
                    await CommonActions.ApplyAndAddRotation(Rotations.ByFace(faceType, RotationDirection.Clockwise), solution, configuration).ConfigureAwait(false);

                    await CommonActions.ApplyAndAddRotation(Rotations.ByFace(faceWithJoiningColour, RotationDirection.Clockwise), solution, configuration).ConfigureAwait(false);

                    await CommonActions.ApplyAndAddRotation(Rotations.ByFace(faceType, RotationDirection.AntiClockwise), solution, configuration).ConfigureAwait(false);

                    break;
                case RelativePosition.Right:
                    await CommonActions.ApplyAndAddRotation(Rotations.ByFace(faceType, RotationDirection.AntiClockwise), solution, configuration).ConfigureAwait(false);

                    await CommonActions.ApplyAndAddRotation(Rotations.ByFace(faceWithJoiningColour, RotationDirection.AntiClockwise), solution, configuration).ConfigureAwait(false);

                    await CommonActions.ApplyAndAddRotation(Rotations.ByFace(faceType, RotationDirection.Clockwise), solution, configuration).ConfigureAwait(false);

                    break;
                case RelativePosition.Same:
                case RelativePosition.Opposite:
                    await CommonActions.ApplyAndAddRotation(Rotations.ByFace(faceType, RotationDirection.AntiClockwise), solution, configuration).ConfigureAwait(false);

                    await CommonActions.ApplyAndAddRotation(Rotations.ByFace(faceWithJoiningColour, RotationDirection.Clockwise), solution, configuration).ConfigureAwait(false);

                    await CommonActions.ApplyAndAddRotation(Rotations.ByFace(faceType, RotationDirection.Clockwise), solution, configuration).ConfigureAwait(false);

                    break;
            }
        }

        private static async Task CheckMiddleLayerForWhite(CubeConfiguration<FaceColour> configuration, Face<FaceColour> face, FaceType faceType, ICollection<IRotation> solution)
        {
            if (face.GetEdge(Edge.Left).Centre() == FaceColour.White)
            {
                await MoveWhiteFromMiddleLayer(configuration, faceType, solution, RelativePosition.Left, Edge.Right, RotationDirection.Clockwise).ConfigureAwait(false);

            }
            if (face.GetEdge(Edge.Right).Centre() == FaceColour.White)
            {
                await MoveWhiteFromMiddleLayer(configuration, faceType, solution, RelativePosition.Right, Edge.Left, RotationDirection.AntiClockwise).ConfigureAwait(false);

            }
        }

        private static async Task MoveWhiteFromMiddleLayer(CubeConfiguration<FaceColour> configuration, FaceType faceType, ICollection<IRotation> solution, RelativePosition relativePosition, Edge edge, RotationDirection downDirection)
        {
            var joiningFace = FaceRules.FaceAtRelativePositionTo(faceType, relativePosition);
            var joiningColour = configuration.Faces[joiningFace].GetEdge(edge).Centre();

            var faceWithJoiningColour = FaceRules.GetFaceOfColour(joiningColour, configuration);

            var relativePostion = FaceRules.RelativePositionBetweenFaces(faceWithJoiningColour, joiningFace);
            switch (relativePostion)
            {
                case RelativePosition.Same:
                    await CommonActions.ApplyAndAddRotation(Rotations.ByFace(joiningFace, downDirection), solution, configuration).ConfigureAwait(false);

                    break;

                case RelativePosition.Left:
                case RelativePosition.Right:
                case RelativePosition.Opposite:
                    // TODO: ROTATE SO THAT WE ARE ONLY DOING RIGHT... MOVES
                    await CommonActions.ApplyAndAddRotation(Rotations.ByFace(joiningFace, RotationDirectionEx.Reverse(downDirection)), solution, configuration).ConfigureAwait(false);

                    await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                    await CommonActions.ApplyAndAddRotation(Rotations.ByFace(joiningFace, downDirection), solution, configuration).ConfigureAwait(false);

                    await CheckTopFaceForWhite(configuration, joiningFace, solution).ConfigureAwait(false);

                    break;
            }
        }

        private static async Task CheckTopFaceForWhite(CubeConfiguration<FaceColour> configuration, FaceType faceType, ICollection<IRotation> solution)
        {
            var edge = FaceRules.EdgeJoiningFaceToFace(faceType, FaceType.Upper);
            if (configuration.Faces[FaceType.Upper].GetEdge(edge).Centre() != FaceColour.White) return;


            var joiningColour = configuration.Faces[faceType].GetEdge(Edge.Top).Centre();
            var faceWithJoiningColour = FaceRules.GetFaceOfColour(joiningColour, configuration);

            var relativePostion = FaceRules.RelativePositionBetweenFaces(faceWithJoiningColour, faceType);

            IRotation topRotation = null;
            switch (relativePostion)
            {
                case RelativePosition.Same:
                    break;

                case RelativePosition.Right:
                    topRotation = Rotations.UpperClockwise;
                    break;

                case RelativePosition.Opposite:
                    topRotation = Rotations.Upper2;
                    break;

                case RelativePosition.Left:
                    topRotation = Rotations.UpperAntiClockwise;
                    break;
            }

            IRotation rotationToBottom = Rotations.ByFaceTwice(faceWithJoiningColour);
            await CommonActions.ApplyAndAddRotation(topRotation, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(rotationToBottom, solution, configuration).ConfigureAwait(false);

        }


        private static async Task CheckTopLayerForWhite(CubeConfiguration<FaceColour> configuration, Face<FaceColour> face, FaceType faceType, ICollection<IRotation> solution)
        {
            if (face.GetEdge(Edge.Top).Centre() != FaceColour.White) return;
            var edge = FaceRules.EdgeJoiningFaceToFace(faceType, FaceType.Upper);

            var joiningColour = configuration.Faces[FaceType.Upper].GetEdge(edge).Centre();
            var faceWithJoiningColour = FaceRules.GetFaceOfColour(joiningColour, configuration);
            var relativePostion = FaceRules.RelativePositionBetweenFaces(faceWithJoiningColour, faceType);

            switch (relativePostion)
            {
                case RelativePosition.Same:
                    await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                    await CheckTopLayerForWhite(configuration, configuration.Faces[FaceType.Left], FaceType.Left, solution).ConfigureAwait(false);

                    break;

                case RelativePosition.Right:
                    await CommonActions.ApplyAndAddRotation(Rotations.ByFace(faceType, RotationDirection.AntiClockwise), solution, configuration).ConfigureAwait(false);

                    await CommonActions.ApplyAndAddRotation(Rotations.ByFace(faceWithJoiningColour, RotationDirection.Clockwise), solution, configuration).ConfigureAwait(false);

                    await CommonActions.ApplyAndAddRotation(Rotations.ByFace(faceType, RotationDirection.Clockwise), solution, configuration).ConfigureAwait(false);

                    break;

                case RelativePosition.Opposite:
                    await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

                    await CheckTopLayerForWhite(configuration, configuration.Faces[FaceType.Left], FaceType.Left, solution).ConfigureAwait(false);

                    break;

                case RelativePosition.Left:
                    await CommonActions.ApplyAndAddRotation(Rotations.ByFace(faceType, RotationDirection.Clockwise), solution, configuration).ConfigureAwait(false);

                    await CommonActions.ApplyAndAddRotation(Rotations.ByFace(faceWithJoiningColour, RotationDirection.AntiClockwise), solution, configuration).ConfigureAwait(false);

                    await CommonActions.ApplyAndAddRotation(Rotations.ByFace(faceType, RotationDirection.AntiClockwise), solution, configuration).ConfigureAwait(false);

                    break;
            }
        }
    }
}
