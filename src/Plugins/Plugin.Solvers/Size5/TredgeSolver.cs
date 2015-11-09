using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size5
{
    internal class TredgeSolver : IPartialSolver
    {
        public async Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration)
        {
            var solution = new List<IRotation>();
            solution.Add(await CommonActions.PositionOnBottom(configuration, FaceColour.Yellow));

            var frontColour = configuration.Faces[FaceType.Front].LeftCentre();
            var leftColour = configuration.Faces[FaceType.Right].RightCentre();

            var topLeft = configuration.Faces[FaceType.Front].GetEdge(Edge.Top)[1];
            var upperBottomLeft = configuration.Faces[FaceType.Upper].GetEdge(Edge.Bottom)[1];
            if (topLeft == frontColour || upperBottomLeft == leftColour)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerDownClockwise, solution, configuration);
            }
            // TODO: CHECK THIS
            if (topLeft == leftColour && upperBottomLeft == leftColour)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.FrontAntiClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration);
                await CommonActions.ApplyAndAddRotation(Rotations.SecondLayerUpperClockwise, solution, configuration);
            }

            return solution;
        }
    }
}
