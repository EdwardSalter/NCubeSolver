using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NCubeSolver.Plugins.Solvers.Common;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size7
{
    internal class AllLayer1CrossesSolver : IPartialSolver
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
                var stepsToSolveCross = await new SingleFaceCrossSolver(faceColour, 1).Solve(configuration).ConfigureAwait(false);
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
