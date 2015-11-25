using System.Collections.Generic;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size5
{
    internal class MiddleLayerTredgeSolver : IPartialSolver
    {
        public async Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration)
        {
            var solution = new List<IRotation>();

            await Repeat.SolvingUntilNoMovesCanBeMade(solution, async () =>
            {

                await CheckFrontRightEdge(configuration, solution).ConfigureAwait(false);

                await CheckRightBackEdge(configuration, solution).ConfigureAwait(false);

                await CheckBackLeftEdge(configuration, solution).ConfigureAwait(false);

                await CheckLeftFrontEdge(configuration, solution).ConfigureAwait(false);

            }).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration).ConfigureAwait(false);


            await Repeat.SolvingUntilNoMovesCanBeMade(solution, async () =>
            {
                await CheckFrontRightEdge(configuration, solution).ConfigureAwait(false);

                await CheckRightBackEdge(configuration, solution).ConfigureAwait(false);

                await CheckLeftFrontEdge(configuration, solution).ConfigureAwait(false);

            }).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration).ConfigureAwait(false);


            await Repeat.SolvingUntilNoMovesCanBeMade(solution, async () =>
            {
                await CheckFrontRightEdge(configuration, solution).ConfigureAwait(false);

                await CheckLeftFrontEdge(configuration, solution).ConfigureAwait(false);

            }).ConfigureAwait(false);

            await SolveLastTredge(configuration, solution).ConfigureAwait(false);


            return solution;
        }

        private async Task SolveLastTredge(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            if (Utilities.IsInnerEdgeComplete(FaceType.Front, FaceType.Right, configuration))
            {
                return;
            }

            await CommonActions.ApplyAndAddRotation(CubeRotations.ZAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRight2, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.Back2, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeftClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRightAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRightClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.Front2, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRightClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.Front2, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeftAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.Back2, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRight2, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(CubeRotations.ZClockwise, solution, configuration).ConfigureAwait(false); // OPTIONAL
        }

        private static async Task CheckFrontRightEdge(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            var frontFaceColour = configuration.Faces[FaceType.Front].LeftCentre();
            var leftFaceColour = configuration.Faces[FaceType.Left].RightCentre();

            var rightEdgeOnFace = configuration.Faces[FaceType.Front].GetEdge(Edge.Right);
            var frontFaceTop = rightEdgeOnFace[configuration.MinInnerLayerIndex()];
            var frontFaceBottom = rightEdgeOnFace[configuration.MaxInnerLayerIndex()];

            var leftEdgeOnRightFace = configuration.Faces[FaceType.Right].GetEdge(Edge.Left);
            var rightFaceTop = leftEdgeOnRightFace[configuration.MinInnerLayerIndex()];
            var rightFaceBottom = leftEdgeOnRightFace[configuration.MaxInnerLayerIndex()];

            if (frontFaceTop == frontFaceColour && rightFaceTop == leftFaceColour)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerDownClockwise, solution, configuration).ConfigureAwait(false);

                await PerformFlip(solution, configuration).ConfigureAwait(false);

                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerDownAntiClockwise, solution, configuration).ConfigureAwait(false);

            }

            if (frontFaceBottom == frontFaceColour && rightFaceBottom == leftFaceColour)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                await PerformFlip(solution, configuration).ConfigureAwait(false);

                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperClockwise, solution, configuration).ConfigureAwait(false);

            }

            if (frontFaceTop == leftFaceColour && rightFaceTop == frontFaceColour)
            {
                await PerformFlip(solution, configuration).ConfigureAwait(false);

                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                await PerformFlip(solution, configuration).ConfigureAwait(false);

                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperClockwise, solution, configuration).ConfigureAwait(false);

            }

            if (frontFaceBottom == leftFaceColour && rightFaceBottom == frontFaceColour)
            {
                await PerformFlip(solution, configuration).ConfigureAwait(false);

                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                await PerformFlip(solution, configuration).ConfigureAwait(false);

                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperClockwise, solution, configuration).ConfigureAwait(false);

            }
        }

        private static async Task CheckRightBackEdge(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            var frontFaceColour = configuration.Faces[FaceType.Front].LeftCentre();
            var leftFaceColour = configuration.Faces[FaceType.Left].RightCentre();

            var rightEdgeOnFace = configuration.Faces[FaceType.Right].GetEdge(Edge.Right);
            var rightFaceTop = rightEdgeOnFace[configuration.MinInnerLayerIndex()];
            var rightFaceBottom = rightEdgeOnFace[configuration.MaxInnerLayerIndex()];

            var leftEdgeOnBackFace = configuration.Faces[FaceType.Back].GetEdge(Edge.Left);
            var backFaceTop = leftEdgeOnBackFace[configuration.MinInnerLayerIndex()];
            var backFaceBottom = leftEdgeOnBackFace[configuration.MaxInnerLayerIndex()];

            if ((rightFaceTop == frontFaceColour && backFaceTop == leftFaceColour) ||
                (rightFaceBottom == frontFaceColour && backFaceBottom == leftFaceColour) ||
                (rightFaceTop == leftFaceColour && backFaceTop == frontFaceColour) ||
                (rightFaceBottom == leftFaceColour && backFaceBottom == frontFaceColour))
            {
                await CommonActions.ApplyAndAddRotation(Rotations.Right2, solution, configuration).ConfigureAwait(false);

                await CheckFrontRightEdge(configuration, solution).ConfigureAwait(false);

            }
        }

        private static async Task CheckBackLeftEdge(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            var frontFaceColour = configuration.Faces[FaceType.Front].LeftCentre();
            var leftFaceColour = configuration.Faces[FaceType.Left].RightCentre();

            var rightEdgeOnFace = configuration.Faces[FaceType.Back].GetEdge(Edge.Right);
            var backFaceTop = rightEdgeOnFace[configuration.MinInnerLayerIndex()];
            var backFaceBottom = rightEdgeOnFace[configuration.MaxInnerLayerIndex()];

            var leftEdgeOnLeftFace = configuration.Faces[FaceType.Left].GetEdge(Edge.Left);
            var leftFaceTop = leftEdgeOnLeftFace[configuration.MinInnerLayerIndex()];
            var leftFaceBottom = leftEdgeOnLeftFace[configuration.MaxInnerLayerIndex()];

            if ((backFaceTop == frontFaceColour && leftFaceTop == leftFaceColour) ||
                (backFaceBottom == frontFaceColour && leftFaceBottom == leftFaceColour) ||
                (backFaceTop == leftFaceColour && leftFaceTop == frontFaceColour) ||
                (backFaceBottom == leftFaceColour && leftFaceBottom == frontFaceColour))
            {
                await CommonActions.ApplyAndAddRotation(Rotations.Back2, solution, configuration).ConfigureAwait(false);

                await CommonActions.ApplyAndAddRotation(Rotations.Right2, solution, configuration).ConfigureAwait(false);

                await CheckFrontRightEdge(configuration, solution).ConfigureAwait(false);

            }
        }

        private static async Task CheckLeftFrontEdge(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            var frontFaceColour = configuration.Faces[FaceType.Front].LeftCentre();
            var leftFaceColour = configuration.Faces[FaceType.Left].RightCentre();

            var rightEdgeOnFace = configuration.Faces[FaceType.Left].GetEdge(Edge.Right);
            var leftFaceTop = rightEdgeOnFace[configuration.MinInnerLayerIndex()];
            var leftFaceBottom = rightEdgeOnFace[configuration.MaxInnerLayerIndex()];

            var leftEdgeOnFrontFace = configuration.Faces[FaceType.Front].GetEdge(Edge.Left);
            var frontFaceTop = leftEdgeOnFrontFace[configuration.MinInnerLayerIndex()];
            var frontFaceBottom = leftEdgeOnFrontFace[configuration.MaxInnerLayerIndex()];

            if (leftFaceTop == frontFaceColour && frontFaceTop == leftFaceColour)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperAntiClockwise, solution, configuration).ConfigureAwait(false);

                await PerformFlip(solution, configuration).ConfigureAwait(false);

                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperClockwise, solution, configuration).ConfigureAwait(false);

                await CheckFrontRightEdge(configuration, solution).ConfigureAwait(false);

            }

            if (leftFaceBottom == frontFaceColour && frontFaceBottom == leftFaceColour)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerDownClockwise, solution, configuration).ConfigureAwait(false);

                await PerformFlip(solution, configuration).ConfigureAwait(false);

                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerDownAntiClockwise, solution, configuration).ConfigureAwait(false);

                await CheckFrontRightEdge(configuration, solution).ConfigureAwait(false);

            }
        }


        private static async Task PerformFlip(List<IRotation> solution, CubeConfiguration<FaceColour> configuration)
        {
            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.FrontAntiClockwise, solution, configuration).ConfigureAwait(false);

            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration).ConfigureAwait(false);

        }
    }
}
