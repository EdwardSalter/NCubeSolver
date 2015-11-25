using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size5
{
    internal class UpperAndDownFaceTredgesSolver : IPartialSolver
    {
        private readonly SingleTredgeSolver m_solver;

        public UpperAndDownFaceTredgesSolver()
        {
            m_solver = new SingleTredgeSolver();
        }

        public async Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration)
        {
            var solution = new List<IRotation>();
            solution.Add(await CommonActions.PositionOnBottom(configuration, FaceColour.Yellow).ConfigureAwait(false));


            for (int i = 0; i <= 3; i++)
            {
                var stepsToSolveTredge = await m_solver.Solve(configuration).ConfigureAwait(false);

                solution.AddRange(stepsToSolveTredge);
                await MoveTredgeToUpperLayer(configuration, solution).ConfigureAwait(false);

            }
            await CommonActions.ApplyAndAddRotation(CubeRotations.Z2, solution, configuration).ConfigureAwait(false);

            for (int i = 0; i <= 3; i++)
            {
                var stepsToSolveTredge = await m_solver.Solve(configuration).ConfigureAwait(false);

                solution.AddRange(stepsToSolveTredge);
                await MoveTredgeToUpperLayer(configuration, solution).ConfigureAwait(false);

            }

            await FixInnerSquare(solution, configuration).ConfigureAwait(false);

            return solution;
        }

        private async Task FixInnerSquare(List<IRotation> solution, CubeConfiguration<FaceColour> configuration)
        {
            var upperColour = configuration.Faces[FaceType.Front].GetEdge(configuration.MinInnerLayerIndex(), Edge.Top).Centre();
            var downColour = configuration.Faces[FaceType.Front].GetEdge(configuration.MinInnerLayerIndex(), Edge.Bottom).Centre();

            var correctFaceForTopRow = FaceRules.GetFaceOfColour(upperColour, configuration);
            var correctFaceForBottomRow = FaceRules.GetFaceOfColour(downColour, configuration);

            await MoveRowToCorrectFace(correctFaceForTopRow, solution, configuration, true).ConfigureAwait(false);

            await MoveRowToCorrectFace(correctFaceForBottomRow, solution, configuration, false).ConfigureAwait(false);

        }

        private async Task MoveRowToCorrectFace(FaceType faceToMoveToo, List<IRotation> solution, CubeConfiguration<FaceColour> configuration, bool upper)
        {
            // If we get an upper or down face it would imply that we do not have completed rows meaning we have probably skipped steps (some tests do this)
            if (faceToMoveToo == FaceType.Upper || faceToMoveToo == FaceType.Down)
            {
                return;
            }

            IRotation rotation = null;
            var face = upper ? FaceType.Upper : FaceType.Down;
            RotationDirection direction;

            var position = FaceRules.RelativePositionBetweenFaces(FaceType.Front, faceToMoveToo);
            switch (position)
            {
                case RelativePosition.Left:
                    direction = upper ? RotationDirection.Clockwise : RotationDirection.AntiClockwise;
                    rotation = Rotations.ByFace(face, direction, configuration.MinInnerLayerIndex());
                    break;

                case RelativePosition.Right:
                    direction = !upper ? RotationDirection.Clockwise : RotationDirection.AntiClockwise;
                    rotation = Rotations.ByFace(face, direction, configuration.MinInnerLayerIndex());
                    break;

                case RelativePosition.Opposite:
                    rotation = Rotations.ByFaceTwice(face, configuration.MinInnerLayerIndex());
                    break;
            }

            if (rotation != null)
            {
                await CommonActions.ApplyAndAddRotation(rotation, solution, configuration).ConfigureAwait(false);

            }
        }

        private async Task MoveTredgeToUpperLayer(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            await CommonActions.ApplyAndAddRotation(CubeRotations.YAntiClockwise, solution, configuration).ConfigureAwait(false);


            if (GetNumberOfCompletedTredgesOnTopLayer(configuration) == 3)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (Utilities.IsInnerEdgeComplete(FaceType.Right, FaceType.Upper, configuration))
                    {
                        break;
                    }

                    await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                }
            }

            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration).ConfigureAwait(false);

            for (int i = 0; i <= 3; i++)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);


                if (!Utilities.IsInnerEdgeComplete(FaceType.Right, FaceType.Upper, configuration))
                {
                    break;
                }
            }
            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration).ConfigureAwait(false);

        }

        private int GetNumberOfCompletedTredgesOnTopLayer(CubeConfiguration<FaceColour> configuration)
        {
            var allFaces = new[] { FaceType.Front, FaceType.Left, FaceType.Back, FaceType.Right };
            return allFaces.Count(f => Utilities.IsInnerEdgeComplete(f, FaceType.Upper, configuration));
        }
    }
}
