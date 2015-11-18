using System.Collections.Generic;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size5
{
    internal class SingleFaceCrossSolver : IPartialSolver
    {
        private readonly FaceColour m_faceColour;
        private int m_layer = -1;

        public SingleFaceCrossSolver(FaceColour faceColour, int? layer = null)
        {
            if (layer.HasValue)
            {
                m_layer = layer.Value;
            }
            m_faceColour = faceColour;
        }

        public async Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration)
        {
            if (m_layer < 0)
            {
                m_layer = configuration.MinInnerLayerIndex();
            }

            var solution = new List<IRotation>();

            var rotationToBottom = await CommonActions.PositionOnFront(configuration, m_faceColour);
            if (rotationToBottom != null) solution.Add(rotationToBottom);

            for (int i = 0; i <= 3; i++)
            {
                await CheckFace(configuration, FaceType.Upper, solution);
                await CommonActions.ApplyAndAddRotation(CubeRotations.ZClockwise, solution, configuration);
            }

            await CheckFace(configuration, FaceType.Back, solution, false, true);

            return solution;
        }

        private async Task CheckFace(CubeConfiguration<FaceColour> configuration, FaceType faceToCheck, List<IRotation> solution, bool reverseDirection = false, bool doubleMoves = false)
        {
            var checkFace = configuration.Faces[faceToCheck];
            var frontFace = configuration.Faces[FaceType.Front];
            var isBack = faceToCheck == FaceType.Back;

            await CheckEdgeOnFace(configuration, solution, checkFace, Edge.Left, !isBack ? Edge.Left : Edge.Right, frontFace, FaceType.Left, reverseDirection, doubleMoves);
            await CheckEdgeOnFace(configuration, solution, checkFace, Edge.Right, isBack ? Edge.Left : Edge.Right, frontFace, FaceType.Right, !reverseDirection, doubleMoves);

            // TODO: THIS CAN BE IMPROVED, INSTEAD MOVE THE FRONT LAYER FIRST, YOU CAN CHOOSE EITHER THE LEFT OR RIGHT DEPENDING ON WHICH HOLDS THE EMPTY COLOUR
            if (checkFace.GetEdge(m_layer, Edge.Top).Centre() == m_faceColour)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.ByFace(faceToCheck, RotationDirection.AntiClockwise), solution, configuration);
                await CheckEdgeOnFace(configuration, solution, checkFace, !isBack ? Edge.Left : Edge.Right, Edge.Left, frontFace, !isBack ? FaceType.Left : FaceType.Right, reverseDirection, doubleMoves);
            }
            if (checkFace.GetEdge(m_layer, Edge.Bottom).Centre() == m_faceColour)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.ByFace(faceToCheck, RotationDirection.AntiClockwise), solution, configuration);
                await CheckEdgeOnFace(configuration, solution, checkFace, isBack ? Edge.Left : Edge.Right, Edge.Right, frontFace, !isBack ? FaceType.Right : FaceType.Left, reverseDirection, doubleMoves);
            }

            await CheckEdgeOnFace(configuration, solution, checkFace, Edge.Left, !isBack ? Edge.Left : Edge.Right, frontFace, FaceType.Left, reverseDirection, doubleMoves);
            await CheckEdgeOnFace(configuration, solution, checkFace, Edge.Right, isBack ? Edge.Left : Edge.Right, frontFace, FaceType.Right, !reverseDirection, doubleMoves);
        }

        private async Task CheckEdgeOnFace(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, Face<FaceColour> checkFace, Edge frontEdge, Edge checkEdge, Face<FaceColour> frontFace, FaceType movementFace, bool reverseDirection = false, bool doubleMoves = false)
        {
            var numMoves = doubleMoves ? 2 : 1;
            var center = checkFace.GetEdge(m_layer, checkEdge).Centre();
            if (center == m_faceColour)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (frontFace.GetEdge(m_layer, frontEdge).Centre() == m_faceColour) break;

                    await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration);
                }

                var direction = !reverseDirection ? RotationDirection.Clockwise : RotationDirection.AntiClockwise;
                var rotation = numMoves == 1 ? Rotations.ByFace(movementFace, direction, m_layer) : Rotations.ByFaceTwice(movementFace, m_layer);
                await CommonActions.ApplyAndAddRotation(rotation, solution, configuration);

                for (int i = 0; i <= 3; i++)
                {
                    if (frontFace.GetEdge(m_layer, frontEdge).Centre() != m_faceColour) break;

                    await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration);
                }

                direction = !reverseDirection ? RotationDirection.AntiClockwise : RotationDirection.Clockwise;
                rotation = numMoves == 1 ? Rotations.ByFace(movementFace, direction, m_layer) : Rotations.ByFaceTwice(movementFace, m_layer);
                await CommonActions.ApplyAndAddRotation(rotation, solution, configuration);
            }
        }
    }
}
