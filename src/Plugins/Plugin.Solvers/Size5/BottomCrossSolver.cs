using System.Collections.Generic;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size5
{
    internal class BottomCrossSolver : IPartialSolver
    {
        private readonly FaceColour m_faceColour;

        public BottomCrossSolver()
        {
            m_faceColour = FaceColour.White;
        }

        public async Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration)
        {
            var solution = new List<IRotation>();
            List<IRotation> previousSolution;

            do
            {
                previousSolution = new List<IRotation>(solution);

                var rotationToBottom = await CommonActions.PositionOnFront(configuration, m_faceColour);
                if (rotationToBottom != null) solution.Add(rotationToBottom);

                await CheckFace(configuration, FaceType.Upper, solution);

            } while (previousSolution.Count != solution.Count);

            return solution;
        }

        private async Task CheckFace(CubeConfiguration<FaceColour> configuration, FaceType faceToCheck, List<IRotation> solution)
        {
            var checkFace = configuration.Faces[faceToCheck];
            var frontFace = configuration.Faces[FaceType.Front];

            await CheckEdgeOnFace(configuration, solution, checkFace, Edge.Left, frontFace, FaceType.Left);
            await CheckEdgeOnFace(configuration, solution, checkFace, Edge.Right, frontFace, FaceType.Right, true);

            // TODO: THIS CAN BE IMPROVED, INSTEAD MOVE THE FRONT LAYER FIRST, YOU CAN CHOOSE EITHER THE LEFT OR RIGHT DEPENDING ON WHICH HOLDS THE EMPTY COLOUR
            if (checkFace.GetEdge(1, Edge.Top).Centre() == m_faceColour)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.ByFace(faceToCheck, RotationDirection.AntiClockwise), solution, configuration);
                await CheckEdgeOnFace(configuration, solution, checkFace, Edge.Left, frontFace, FaceType.Left);
            }
            if (checkFace.GetEdge(1, Edge.Bottom).Centre() == m_faceColour)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.ByFace(faceToCheck, RotationDirection.AntiClockwise), solution, configuration);
                await CheckEdgeOnFace(configuration, solution, checkFace, Edge.Right, frontFace, FaceType.Right);
            }
        }

        private async Task CheckEdgeOnFace(CubeConfiguration<FaceColour> configuration, List<IRotation> solution, Face<FaceColour> checkFace, Edge edge, Face<FaceColour> frontFace, FaceType movementFace, bool reverseDirection = false)
        {
            var center = checkFace.GetEdge(1, edge).Centre();
            if (center == m_faceColour)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (frontFace.GetEdge(1, edge).Centre() == m_faceColour) break;

                    await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration);
                }

                var direction = !reverseDirection ? RotationDirection.Clockwise : RotationDirection.AntiClockwise;
                await CommonActions.ApplyAndAddRotation(Rotations.ByFace(movementFace, direction, 1), solution, configuration);

                for (int i = 0; i <= 3; i++)
                {
                    if (frontFace.GetEdge(1, edge).Centre() != m_faceColour) break;

                    await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration);
                }

                direction = !reverseDirection ? RotationDirection.AntiClockwise : RotationDirection.Clockwise;
                await CommonActions.ApplyAndAddRotation(Rotations.ByFace(movementFace, direction, 1), solution, configuration);
            }
        }
    }
}
