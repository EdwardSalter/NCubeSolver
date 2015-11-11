﻿using System.Collections.Generic;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size5
{
    internal class MiddleLayerTredgeSolver : IPartialSolver
    {
        public async Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration)
        {
            var solution = new List<IRotation>();
            List<IRotation> previousSolution;
            do
            {
                previousSolution = new List<IRotation>(solution);

                await CheckFrontRightEdge(configuration, solution);
                await CheckRightBackEdge(configuration, solution);
                await CheckBackLeftEdge(configuration, solution);
                await CheckLeftFrontEdge(configuration, solution);
            } while (previousSolution.Count != solution.Count);

            await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration);

            do
            {
                previousSolution = new List<IRotation>(solution);

                await CheckFrontRightEdge(configuration, solution);
                await CheckRightBackEdge(configuration, solution);
                await CheckLeftFrontEdge(configuration, solution);
            } while (previousSolution.Count != solution.Count);

            await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration);

            do
            {
                previousSolution = new List<IRotation>(solution);

                await CheckFrontRightEdge(configuration, solution);
                await CheckLeftFrontEdge(configuration, solution);
            } while (previousSolution.Count != solution.Count);

            await SolveLastTredge(configuration, solution);

            return solution;
        }

        private async Task SolveLastTredge(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            if (Utilities.IsInnerEdgeComplete(FaceType.Front, FaceType.Right, configuration))
            {
                return;
            }

            await CommonActions.ApplyAndAddRotation(CubeRotations.ZAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRight2, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.Back2, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeftClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRightAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRightClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.Front2, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRightClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.Front2, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerLeftAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.Back2, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerRight2, solution, configuration);
            await CommonActions.ApplyAndAddRotation(CubeRotations.ZClockwise, solution, configuration); // OPTIONAL
        }

        private static async Task CheckFrontRightEdge(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            var frontFaceColour = configuration.Faces[FaceType.Front].LeftCentre();
            var leftFaceColour = configuration.Faces[FaceType.Left].RightCentre();

            var rightEdgeOnFace = configuration.Faces[FaceType.Front].GetEdge(Edge.Right);
            var frontFaceTop = rightEdgeOnFace[1];
            var frontFaceBottom = rightEdgeOnFace[3];

            var leftEdgeOnRightFace = configuration.Faces[FaceType.Right].GetEdge(Edge.Left);
            var rightFaceTop = leftEdgeOnRightFace[1];
            var rightFaceBottom = leftEdgeOnRightFace[3];

            if (frontFaceTop == frontFaceColour && rightFaceTop == leftFaceColour)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerDownClockwise, solution, configuration);
                await PerformFlip(solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerDownAntiClockwise, solution, configuration);
            }

            if (frontFaceBottom == frontFaceColour && rightFaceBottom == leftFaceColour)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperAntiClockwise, solution, configuration);
                await PerformFlip(solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperClockwise, solution, configuration);
            }

            if (frontFaceTop == leftFaceColour && rightFaceTop == frontFaceColour)
            {
                await PerformFlip(solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperAntiClockwise, solution, configuration);
                await PerformFlip(solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperClockwise, solution, configuration);
            }

            if (frontFaceBottom == leftFaceColour && rightFaceBottom == frontFaceColour)
            {
                await PerformFlip(solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperAntiClockwise, solution, configuration);
                await PerformFlip(solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperClockwise, solution, configuration);
            }
        }

        private static async Task CheckRightBackEdge(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            var frontFaceColour = configuration.Faces[FaceType.Front].LeftCentre();
            var leftFaceColour = configuration.Faces[FaceType.Left].RightCentre();

            var rightEdgeOnFace = configuration.Faces[FaceType.Right].GetEdge(Edge.Right);
            var rightFaceTop = rightEdgeOnFace[1];
            var rightFaceBottom = rightEdgeOnFace[3];

            var leftEdgeOnBackFace = configuration.Faces[FaceType.Back].GetEdge(Edge.Left);
            var backFaceTop = leftEdgeOnBackFace[1];
            var backFaceBottom = leftEdgeOnBackFace[3];

            if ((rightFaceTop == frontFaceColour && backFaceTop == leftFaceColour) ||
                (rightFaceBottom == frontFaceColour && backFaceBottom == leftFaceColour) ||
                (rightFaceTop == leftFaceColour && backFaceTop == frontFaceColour) ||
                (rightFaceBottom == leftFaceColour && backFaceBottom == frontFaceColour))
            {
                await CommonActions.ApplyAndAddRotation(Rotations.Right2, solution, configuration);
                await CheckFrontRightEdge(configuration, solution);
            }
        }

        private static async Task CheckBackLeftEdge(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            var frontFaceColour = configuration.Faces[FaceType.Front].LeftCentre();
            var leftFaceColour = configuration.Faces[FaceType.Left].RightCentre();

            var rightEdgeOnFace = configuration.Faces[FaceType.Back].GetEdge(Edge.Right);
            var backFaceTop = rightEdgeOnFace[1];
            var backFaceBottom = rightEdgeOnFace[3];

            var leftEdgeOnLeftFace = configuration.Faces[FaceType.Left].GetEdge(Edge.Left);
            var leftFaceTop = leftEdgeOnLeftFace[1];
            var leftFaceBottom = leftEdgeOnLeftFace[3];

            if ((backFaceTop == frontFaceColour && leftFaceTop == leftFaceColour) ||
                (backFaceBottom == frontFaceColour && leftFaceBottom == leftFaceColour) ||
                (backFaceTop == leftFaceColour && leftFaceTop == frontFaceColour) ||
                (backFaceBottom == leftFaceColour && leftFaceBottom == frontFaceColour))
            {
                await CommonActions.ApplyAndAddRotation(Rotations.Back2, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.Right2, solution, configuration);
                await CheckFrontRightEdge(configuration, solution);
            }
        }

        private static async Task CheckLeftFrontEdge(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            var frontFaceColour = configuration.Faces[FaceType.Front].LeftCentre();
            var leftFaceColour = configuration.Faces[FaceType.Left].RightCentre();

            var rightEdgeOnFace = configuration.Faces[FaceType.Left].GetEdge(Edge.Right);
            var leftFaceTop = rightEdgeOnFace[1];
            var leftFaceBottom = rightEdgeOnFace[3];

            var leftEdgeOnFrontFace = configuration.Faces[FaceType.Front].GetEdge(Edge.Left);
            var frontFaceTop = leftEdgeOnFrontFace[1];
            var frontFaceBottom = leftEdgeOnFrontFace[3];

            if (leftFaceTop == frontFaceColour && frontFaceTop == leftFaceColour)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperAntiClockwise, solution, configuration);
                await PerformFlip(solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperClockwise, solution, configuration);
                await CheckFrontRightEdge(configuration, solution);
            }

            if (leftFaceBottom == frontFaceColour && frontFaceBottom == leftFaceColour)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerDownClockwise, solution, configuration);
                await PerformFlip(solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerDownAntiClockwise, solution, configuration);
                await CheckFrontRightEdge(configuration, solution);
            }
        }


        private static async Task PerformFlip(List<IRotation> solution, CubeConfiguration<FaceColour> configuration)
        {
            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.FrontAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration);
        }
    }
}