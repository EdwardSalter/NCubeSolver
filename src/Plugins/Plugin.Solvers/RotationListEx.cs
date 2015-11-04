using System.Collections.Generic;
using System.Linq;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Solvers
{
    static class RotationListEx
    {
        public static IEnumerable<IRotation> ParseInstructionList(string s)
        {
            foreach (var s1 in s.Split(' '))
            {
                var cubeRotation = CubeRotations.ByName(s1);
                if (cubeRotation != null)
                {
                    yield return cubeRotation;
                }

                var faceRotation = Rotations.ByName(s1);
                if (faceRotation != null)
                {
                    yield return faceRotation;
                }
            } 
        }

        public static IEnumerable<IRotation> Condense(this IEnumerable<IRotation> inList)
        {
            var list = inList.ToList();
            if (!list.Any()) return list;

            var returning = new List<IRotation>(list.Count);

            IRotation previous = list[0];
            returning.Add(previous);

            for (int i = 1; i < list.Count; i++)
            {
                var current = list[i];

                if (current is FaceRotation && previous is FaceRotation)
                {
                    var condenseDoubleRotations = CondenseDoubleRotations(current as FaceRotation, previous as FaceRotation).ToList();

                    // We've condensed it so remove the last one
                    if (condenseDoubleRotations.Count <= 1 && returning.Count > 0)
                    {
                        returning.RemoveAt(returning.Count - 1);
                        returning.AddRange(condenseDoubleRotations);

                        previous = returning.Count > 0 ? returning.Last() : null;
                        continue;
                    }

                    // Cannot condense, add
                    returning.Add(current);
                }
                else if (current is CubeRotation && previous is CubeRotation)
                {
                    var condenseDoubleRotations = CondenseDoubleRotations(current as CubeRotation, previous as CubeRotation).ToList();

                    // We've condensed it so remove the last one
                    if (condenseDoubleRotations.Count <= 1 && returning.Count > 0)
                    {
                        returning.RemoveAt(returning.Count - 1);
                        returning.AddRange(condenseDoubleRotations);

                        previous = returning.Count > 0 ? returning.Last() : null;
                        continue;
                    }

                    // Cannot condense, add
                    returning.Add(current);
                }
                else
                {
                    returning.Add(current);
                }

                previous = current;
            }

            return returning;
        }

        private static IEnumerable<FaceRotation> CondenseDoubleRotations(FaceRotation current, FaceRotation previous)
        {
            if (current.Face == previous.Face && current.LayerNumberFromFace == previous.LayerNumberFromFace)
            {
                var currentMultiplier = current.Direction == RotationDirection.Clockwise ? 1 : -1;
                var previousMultiplier = previous.Direction == RotationDirection.Clockwise ? 1 : -1;

                var totalTimes = currentMultiplier * current.Count + previousMultiplier * previous.Count;

                if (totalTimes % 4 == 0) return new List<FaceRotation>();
                if (totalTimes % 2 == 0) return new List<FaceRotation> { Rotations.ByFaceTwice(current.Face, current.LayerNumberFromFace) };
                if (totalTimes % 3 == 0) return new List<FaceRotation> { Rotations.ByFace(current.Face, RotationDirection.AntiClockwise, current.LayerNumberFromFace) };
                if (totalTimes == 1) return new List<FaceRotation> { Rotations.ByFace(current.Face, RotationDirection.Clockwise, current.LayerNumberFromFace) };
            }

            return new List<FaceRotation> { previous, current };
        }

        private static IEnumerable<CubeRotation> CondenseDoubleRotations(CubeRotation current, CubeRotation previous)
        {
            if (current.Axis == previous.Axis)
            {
                var currentMultiplier = current.Direction == RotationDirection.Clockwise ? 1 : -1;
                var previousMultiplier = previous.Direction == RotationDirection.Clockwise ? 1 : -1;

                var totalTimes = currentMultiplier * current.Count + previousMultiplier * previous.Count;

                if (totalTimes % 4 == 0) return new List<CubeRotation>();
                if (totalTimes % 2 == 0) return new List<CubeRotation> { CubeRotations.ByAxisTwice(current.Axis) };
                if (totalTimes % 3 == 0) return new List<CubeRotation> { CubeRotations.ByAxis(current.Axis, RotationDirection.AntiClockwise) };
                if (totalTimes == 1) return new List<CubeRotation> { CubeRotations.ByAxis(current.Axis, RotationDirection.Clockwise) };
            }

            return new List<CubeRotation> { previous, current };
        }
    }
}
