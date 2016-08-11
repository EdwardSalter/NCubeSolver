using System.Collections.Generic;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size7
{
    internal class Layer1EdgeSolver : IPartialSolver
    {
        public async Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration)
        {
            var solution = new List<IRotation>();
            if (IsSolved(configuration)) return solution;

            var wantedColour = configuration.Faces[FaceType.Upper].Centre;

            for (int i = 0; i < 4; i++)
            {
                await CheckFront(configuration, solution, wantedColour).ConfigureAwait(false);
                if (IsSolved(configuration)) return solution;
                await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration).ConfigureAwait(false);
            }

            await CheckBottom(configuration, solution, wantedColour).ConfigureAwait(false);

            if (!IsSolved(configuration))
            {
                throw new SolveFailureException("Failed to solve the cube");
            }

            return solution;
        }

        private static bool IsSolved(CubeConfiguration<FaceColour> configuration)
        {
            var upperFace = configuration.Faces[FaceType.Upper];
            return upperFace.Centre == upperFace.GetEdge(1, Edge.Top)[2] &&
                   upperFace.Centre == upperFace.GetEdge(1, Edge.Top)[4] &&
                   upperFace.Centre == upperFace.GetEdge(1, Edge.Bottom)[2] &&
                   upperFace.Centre == upperFace.GetEdge(1, Edge.Bottom)[4] &&
                   upperFace.Centre == upperFace.GetEdge(1, Edge.Left)[2] &&
                   upperFace.Centre == upperFace.GetEdge(1, Edge.Left)[4] &&
                   upperFace.Centre == upperFace.GetEdge(1, Edge.Right)[2] &&
                   upperFace.Centre == upperFace.GetEdge(1, Edge.Right)[4];
        }

        private class FoundResult
        {
            public Edge Edge { get; set; }
            public bool LeftOfCentre { get; set; }
        }

        private static async Task CheckFront(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution, FaceColour wantedColour)
        {
            for (int i = 0; i < 8; i ++)
            {
                var frontFace = configuration.Faces[FaceType.Front];
                var found = FindColourInFrontFace(wantedColour, frontFace);
                if (found == null) break;
                await RotateEdgeToTop(configuration, solution, found.Edge, FaceType.Front).ConfigureAwait(false);

                var freeEdge = FindFreeSlotInUpperFace(wantedColour, configuration.Faces[FaceType.Upper], found.LeftOfCentre);
                if (freeEdge == null)
                {
                    throw new SolveFailureException("Unexpected to be solved at this point");
                }
                await RotateEdgeToTop(configuration, solution, freeEdge.Value, FaceType.Upper).ConfigureAwait(false);

                await ApplyMoveFromFront(found.LeftOfCentre, configuration, solution).ConfigureAwait(false);
            }
        }

        private static async Task ApplyMoveFromFront(bool leftOfCentre, CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            FaceRotation[] rotationsToAdd;
            if (leftOfCentre)
            {
                rotationsToAdd = new[]
                {
                    Rotations.ByFace(FaceType.Left, RotationDirection.AntiClockwise, 2),
                    Rotations.UpperClockwise,
                    Rotations.SecondLayerRightClockwise,
                    Rotations.UpperAntiClockwise,
                    Rotations.ByFace(FaceType.Left, RotationDirection.Clockwise, 2),
                    Rotations.UpperClockwise,
                    Rotations.SecondLayerRightAntiClockwise
                };
            }
            else
            {
                rotationsToAdd = new[]
                {
                    Rotations.ByFace(FaceType.Right, RotationDirection.Clockwise, 2),
                    Rotations.UpperAntiClockwise,
                    Rotations.SecondLayerLeftAntiClockwise,
                    Rotations.UpperClockwise,
                    Rotations.ByFace(FaceType.Right, RotationDirection.AntiClockwise, 2),
                    Rotations.UpperAntiClockwise,
                    Rotations.SecondLayerLeftClockwise
                };
            }

            await CommonActions.ApplyAndAddRotations(rotationsToAdd, solution, configuration).ConfigureAwait(false);
        }

        private static async Task ApplyMoveFromBottom(bool leftOfCentre, CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            FaceRotation[] rotationsToAdd;
            if (leftOfCentre)
            {
                rotationsToAdd = new[]
                {
                    Rotations.ByFaceTwice(FaceType.Left, 2),
                    Rotations.UpperClockwise,
                    Rotations.SecondLayerRight2,
                    Rotations.UpperAntiClockwise,
                    Rotations.ByFaceTwice(FaceType.Left, 2),
                    Rotations.UpperClockwise,
                    Rotations.SecondLayerRight2
                };
            }
            else
            {
                rotationsToAdd = new[]
                {
                    Rotations.ByFaceTwice(FaceType.Right, 2),
                    Rotations.UpperAntiClockwise,
                    Rotations.SecondLayerLeft2,
                    Rotations.UpperClockwise,
                    Rotations.ByFaceTwice(FaceType.Right, 2),
                    Rotations.UpperAntiClockwise,
                    Rotations.SecondLayerLeft2
                };
            }

            await CommonActions.ApplyAndAddRotations(rotationsToAdd, solution, configuration).ConfigureAwait(false);
        }

        private static async Task RotateEdgeToTop(IRotatable configuration, ICollection<IRotation> solution, Edge edge, FaceType face)
        {
            switch (edge)
            {
                case Edge.Left:
                    await CommonActions.ApplyAndAddRotation(Rotations.ByFace(face, RotationDirection.Clockwise), solution, configuration).ConfigureAwait(false);
                    break;
                case Edge.Bottom:
                    await CommonActions.ApplyAndAddRotation(Rotations.ByFaceTwice(face), solution, configuration).ConfigureAwait(false);
                    break;
                case Edge.Right:
                    await CommonActions.ApplyAndAddRotation(Rotations.ByFace(face, RotationDirection.AntiClockwise), solution, configuration).ConfigureAwait(false);
                    break;
            }
        }

        private static Edge? FindFreeSlotInUpperFace(FaceColour wantedColour, Face<FaceColour> upperFace, bool leftOfCentre)
        {
            if ((leftOfCentre && wantedColour != upperFace.GetEdge(1, Edge.Top)[2]) || (!leftOfCentre && wantedColour != upperFace.GetEdge(1, Edge.Top)[4]))
            {
                return Edge.Top;
            }
            if ((!leftOfCentre && wantedColour != upperFace.GetEdge(1, Edge.Left)[2]) || (leftOfCentre && wantedColour != upperFace.GetEdge(1, Edge.Left)[4]))
            {
                return Edge.Left;
            }
            if ((!leftOfCentre && wantedColour != upperFace.GetEdge(1, Edge.Bottom)[2]) || (leftOfCentre && wantedColour != upperFace.GetEdge(1, Edge.Bottom)[4]))
            {
                return Edge.Bottom;
            }
            if ((leftOfCentre && wantedColour != upperFace.GetEdge(1, Edge.Right)[2]) || (!leftOfCentre && wantedColour != upperFace.GetEdge(1, Edge.Right)[4]))
            {
                return Edge.Right;
            }

            return null;
        }

        private static FoundResult FindColourInFrontFace(FaceColour wantedColour, Face<FaceColour> frontFace)
        {
            if (wantedColour == frontFace.GetEdge(1, Edge.Top)[2])
            {
                return new FoundResult { Edge = Edge.Top, LeftOfCentre = true };
            }
            if (wantedColour == frontFace.GetEdge(1, Edge.Top)[4])
            {
                return new FoundResult { Edge = Edge.Top, LeftOfCentre = false };
            }
            if (wantedColour == frontFace.GetEdge(1, Edge.Left)[2])
            {
                return new FoundResult { Edge = Edge.Left, LeftOfCentre = false };
            }
            if (wantedColour == frontFace.GetEdge(1, Edge.Left)[4])
            {
                return new FoundResult { Edge = Edge.Left, LeftOfCentre = true };
            }
            if (wantedColour == frontFace.GetEdge(1, Edge.Bottom)[2])
            {
                return new FoundResult { Edge = Edge.Bottom, LeftOfCentre = false };
            }
            if (wantedColour == frontFace.GetEdge(1, Edge.Bottom)[4])
            {
                return new FoundResult { Edge = Edge.Bottom, LeftOfCentre = true };
            }
            if (wantedColour == frontFace.GetEdge(1, Edge.Right)[2])
            {
                return new FoundResult { Edge = Edge.Right, LeftOfCentre = true };
            }
            if (wantedColour == frontFace.GetEdge(1, Edge.Right)[4])
            {
                return new FoundResult { Edge = Edge.Right, LeftOfCentre = false };
            }

            return null;
        }

        private static async Task CheckBottom(CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution, FaceColour wantedColour)
        {
            for (int i = 0; i < 8; i++)
            {
                var found = FindColourInFrontFace(wantedColour, configuration.Faces[FaceType.Down]);
                if (found == null) break;
                await RotateEdgeToTop(configuration, solution, found.Edge, FaceType.Down).ConfigureAwait(false);

                var freeEdge = FindFreeSlotInUpperFace(wantedColour, configuration.Faces[FaceType.Upper], found.LeftOfCentre);
                if (freeEdge == null)
                {
                    throw new SolveFailureException("Unexpected to be solved at this point");
                }
                await RotateEdgeToTop(configuration, solution, freeEdge.Value, FaceType.Upper).ConfigureAwait(false);

                await ApplyMoveFromBottom(found.LeftOfCentre, configuration, solution).ConfigureAwait(false);
            }
        }
    }
}
