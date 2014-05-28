using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers.Size3
{
    class TopCrossSolver : IPartialSolver
    {
        public async Task<IEnumerable<IRotation>> Solve(CubeConfiguration<FaceColour> configuration)
        {
            var solution = new List<IRotation>();

            await SolveCross(configuration, solution);
            await PermuteCorrectColours(configuration, solution);

            return solution;
        }

        internal async Task SolveCross(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            var solved = await LineOnTop(configuration, solution);
            if (!solved)
            {
                solved = await LShapeOnTop(configuration, solution);
                if (!solved)
                {
                    await DotOnTop(configuration, solution);
                }
            }
        }

        internal async Task PermuteCorrectColours(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            var relations = CrossRelativePositions(configuration);

            // All the same, just needs rotating
            var solved = await PermuteAllSame(relations, configuration, solution);
            // 1 set of opposite sides are in the wrong place or both sets of diagonal sides
            if (!solved)
            {
                solved = await PermuteDiagonal(relations, configuration, solution);

                if (!solved)
                {
                    await PermuteSingleDiagonal(relations, configuration, solution);
                }
            }
        }

        private static Dictionary<FaceType, RelativePosition> CrossRelativePositions(CubeConfiguration<FaceColour> configuration)
        {
            var faces = new List<FaceType> {FaceType.Front, FaceType.Right, FaceType.Back, FaceType.Left};
            var relations = faces.ToDictionary(face => face, face => GetRelativePositionForColourInTopCentreOfFace(configuration, face));
            return relations;
        }

        private static async Task<bool> PermuteDiagonal(Dictionary<FaceType, RelativePosition> relations, CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            if (!(relations[FaceType.Front] == relations[FaceType.Back] && relations[FaceType.Right] == relations[FaceType.Left]))
                return false;

            await PermuteClockwise(configuration, solution);
            // TODO: STOP PASSING ROUND RELATIONS. USE AS A PROPERTY
            relations = CrossRelativePositions(configuration);
            await PermuteSingleDiagonal(relations, configuration, solution);

            return true;
        }

        private static async Task PermuteAntiClockwise(IRotatable configuration, ICollection<IRotation> solution)
        {
            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration);
        }

        private static async Task PermuteClockwise(IRotatable configuration, ICollection<IRotation> solution)
        {
            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration);
        }

        private static async Task PermuteSingleDiagonal(Dictionary<FaceType, RelativePosition> relations, CubeConfiguration<FaceColour> configuration, ICollection<IRotation> solution)
        {
            var groups = relations.GroupBy(kvp => kvp.Value).ToList();
            var oppositeRelation = groups.FirstOrDefault(group => group.Key == RelativePosition.Opposite);
            if (oppositeRelation != null)
            {
                switch (oppositeRelation.Count())
                {
                    case 2:
                    case 4:
                        await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
                        relations = CrossRelativePositions(configuration);
                        await PermuteSingleDiagonal(relations, configuration, solution);
                        return;
                }
            }

            // TODO: TIDY
            var sameRelation = groups.FirstOrDefault(group => group.Key == RelativePosition.Same);
            if (sameRelation == null) return;
            if (sameRelation.Count() > 1)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
                relations = CrossRelativePositions(configuration);
                await PermuteSingleDiagonal(relations, configuration, solution);
                return;
            }

            var faceType = sameRelation.First().Key;

            var faceColour = configuration.Faces[faceType].TopCentre();
            var cubeRotation = await CommonActions.PositionOnFront(configuration, faceColour);
            if (cubeRotation != null)
                solution.Add(cubeRotation);

            if (oppositeRelation.First().Key == FaceRules.FaceAtRelativePositionTo(faceType, RelativePosition.Right))
            {
                await PermuteClockwise(configuration, solution);
            }
            else
            {
                await PermuteAntiClockwise(configuration, solution);
            }
        }

        private static FaceType ColourToPutAtFront(IReadOnlyList<FaceType> faces)
        {
            return FaceRules.FaceAtRelativePositionTo(faces[0], RelativePosition.Right) == faces[1] ? faces[1] : faces[0];
        }

        private static async Task<bool> PermuteAllSame(Dictionary<FaceType, RelativePosition> relativePositions, IRotatable configuration, ICollection<IRotation> solution)
        {
            if (relativePositions == null || !relativePositions.Any()) return false;

            var first = relativePositions.First().Value;

            if (relativePositions.Values.Skip(1).Any(p => p != first)) return false;

            switch (first)
            {
                case RelativePosition.Right:
                    await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
                    break;

                case RelativePosition.Left:
                    await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
                    break;

                case RelativePosition.Opposite:
                    await CommonActions.ApplyAndAddRotation(Rotations.Upper2, solution, configuration);
                    break;
            }

            return true;
        }

        private static RelativePosition GetRelativePositionForColourInTopCentreOfFace(CubeConfiguration<FaceColour> configuration, FaceType face)
        {
            var colour = configuration.Faces[face].TopCentre();
            var faceOfColour = FaceRules.GetFaceOfColour(colour, configuration);
            return FaceRules.RelativePositionBetweenFaces(face, faceOfColour);
        }

        internal async Task DotOnTop(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            await HorizontalLineToCross(configuration, solution);
            await LShapeOnTop(configuration, solution);
        }

        internal async Task<bool> LShapeOnTop(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            // Back left
            if (configuration.Faces[FaceType.Upper].LeftCentre() == FaceColour.Yellow &&
                configuration.Faces[FaceType.Upper].RightCentre() != FaceColour.Yellow &&
                configuration.Faces[FaceType.Upper].TopCentre() == FaceColour.Yellow &&
                configuration.Faces[FaceType.Upper].BottomCentre() != FaceColour.Yellow)
            {
                await LShapeToCross(configuration, solution);
                return true;
            }

            // Back Right
            if (configuration.Faces[FaceType.Upper].LeftCentre() != FaceColour.Yellow &&
                configuration.Faces[FaceType.Upper].RightCentre() == FaceColour.Yellow &&
                configuration.Faces[FaceType.Upper].TopCentre() == FaceColour.Yellow &&
                configuration.Faces[FaceType.Upper].BottomCentre() != FaceColour.Yellow)
            {
                await CommonActions.ApplyAndAddRotation(CubeRotations.YAntiClockwise, solution, configuration);
                await LShapeToCross(configuration, solution);
                return true;
            }

            // Front Right
            if (configuration.Faces[FaceType.Upper].LeftCentre() != FaceColour.Yellow &&
                configuration.Faces[FaceType.Upper].RightCentre() == FaceColour.Yellow &&
                configuration.Faces[FaceType.Upper].TopCentre() != FaceColour.Yellow &&
                configuration.Faces[FaceType.Upper].BottomCentre() == FaceColour.Yellow)
            {
                await CommonActions.ApplyAndAddRotation(CubeRotations.Y2, solution, configuration);
                await LShapeToCross(configuration, solution);
                return true;
            }

            // Front Left
            if (configuration.Faces[FaceType.Upper].LeftCentre() == FaceColour.Yellow &&
                configuration.Faces[FaceType.Upper].RightCentre() != FaceColour.Yellow &&
                configuration.Faces[FaceType.Upper].TopCentre() != FaceColour.Yellow &&
                configuration.Faces[FaceType.Upper].BottomCentre() == FaceColour.Yellow)
            {
                await CommonActions.ApplyAndAddRotation(CubeRotations.YClockwise, solution, configuration);
                await LShapeToCross(configuration, solution);
                return true;
            }

            return false;
        }

        private static async Task LShapeToCross(IRotatable configuration, ICollection<IRotation> solution)
        {
            await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.FrontAntiClockwise, solution, configuration);
        }

        internal async Task<bool> LineOnTop(CubeConfiguration<FaceColour> configuration, List<IRotation> solution)
        {
            // Horizontal
            if (configuration.Faces[FaceType.Upper].LeftCentre() == FaceColour.Yellow &&
                configuration.Faces[FaceType.Upper].RightCentre() == FaceColour.Yellow &&
                configuration.Faces[FaceType.Upper].TopCentre() != FaceColour.Yellow &&
                configuration.Faces[FaceType.Upper].BottomCentre() != FaceColour.Yellow)
            {
                await HorizontalLineToCross(configuration, solution);
                return true;
            }

            // Vertical
            if (configuration.Faces[FaceType.Upper].LeftCentre() != FaceColour.Yellow &&
                configuration.Faces[FaceType.Upper].RightCentre() != FaceColour.Yellow &&
                configuration.Faces[FaceType.Upper].TopCentre() == FaceColour.Yellow &&
                configuration.Faces[FaceType.Upper].BottomCentre() == FaceColour.Yellow)
            {
                await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
                await HorizontalLineToCross(configuration, solution);
                return true;
            }

            return false;
        }

        private static async Task HorizontalLineToCross(IRotatable configuration, ICollection<IRotation> solution)
        {
            await CommonActions.ApplyAndAddRotation(Rotations.FrontClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.RightClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.UpperClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.RightAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.UpperAntiClockwise, solution, configuration);
            await CommonActions.ApplyAndAddRotation(Rotations.FrontAntiClockwise, solution, configuration);
        }
    }
}
