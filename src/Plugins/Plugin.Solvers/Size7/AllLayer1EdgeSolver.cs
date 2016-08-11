using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size7
{
    internal class AllLayer1EdgeSolver : IPartialSolver
    {
        public async Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration)
        {
            var allColours = new[] { FaceColour.Red, FaceColour.Blue, FaceColour.Yellow, FaceColour.Orange, FaceColour.Green, FaceColour.White, };
            var colourIndex = Array.IndexOf(allColours, configuration.Faces[FaceType.Front].Centre);

            var solution = new List<IRotation>();

            var count = allColours.Length;
            for (; count > 0; count--)
            {
                var faceColour = allColours[colourIndex];
                var positionToFront = await CommonActions.PositionOnFront(configuration, faceColour).ConfigureAwait(false);
                if (positionToFront != null) solution.Add(positionToFront);
                await CommonActions.ApplyAndAddRotation(CubeRotations.XClockwise, solution, configuration).ConfigureAwait(false);
                var stepsToSolveCross = await new Layer1EdgeSolver().Solve(configuration).ConfigureAwait(false);
                solution.AddRange(stepsToSolveCross);

                colourIndex++;
                if (colourIndex >= allColours.Length)
                {
                    colourIndex = 0;
                }
            }

            return solution;
        }
    }
}
