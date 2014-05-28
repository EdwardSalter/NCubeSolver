using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public static class Rotations
    {
        public static FaceRotation RightClockwise = new FaceRotation { Face = FaceType.Right, Name = "R" };
        public static FaceRotation Right2 = new FaceRotation { Face = FaceType.Right, Name = "R2", Count = 2 };
        public static FaceRotation RightAntiClockwise = new FaceRotation { Face = FaceType.Right, Name = "R'", Direction = RotationDirection.AntiClockwise };

        public static FaceRotation LeftClockwise = new FaceRotation { Face = FaceType.Left, Name = "L" };
        public static FaceRotation Left2 = new FaceRotation { Face = FaceType.Left, Name = "L2", Count = 2 };
        public static FaceRotation LeftAntiClockwise = new FaceRotation { Face = FaceType.Left, Name = "L'", Direction = RotationDirection.AntiClockwise };

        public static FaceRotation UpperClockwise = new FaceRotation { Face = FaceType.Upper, Name = "U" };
        public static FaceRotation Upper2 = new FaceRotation { Face = FaceType.Upper, Name = "U2", Count = 2 };
        public static FaceRotation UpperAntiClockwise = new FaceRotation { Face = FaceType.Upper, Name = "U'", Direction = RotationDirection.AntiClockwise };

        public static FaceRotation DownClockwise = new FaceRotation { Face = FaceType.Down, Name = "D" };
        public static FaceRotation Down2 = new FaceRotation { Face = FaceType.Down, Name = "D2", Count = 2 };
        public static FaceRotation DownAntiClockwise = new FaceRotation { Face = FaceType.Down, Name = "D'", Direction = RotationDirection.AntiClockwise };

        public static FaceRotation FrontClockwise = new FaceRotation { Face = FaceType.Front, Name = "F" };
        public static FaceRotation Front2 = new FaceRotation { Face = FaceType.Front, Name = "F2", Count = 2 };
        public static FaceRotation FrontAntiClockwise = new FaceRotation { Face = FaceType.Front, Name = "F'", Direction = RotationDirection.AntiClockwise };

        public static FaceRotation BackClockwise = new FaceRotation { Face = FaceType.Back, Name = "B" };
        public static FaceRotation Back2 = new FaceRotation { Face = FaceType.Back, Name = "B2", Count = 2 };
        public static FaceRotation BackAntiClockwise = new FaceRotation { Face = FaceType.Back, Name = "B'", Direction = RotationDirection.AntiClockwise };

        private static readonly List<FaceRotation> AllRotations = new List<FaceRotation>
        {
            LeftClockwise, Left2, LeftAntiClockwise,
            RightClockwise, Right2, RightAntiClockwise,
            BackClockwise, Back2, BackAntiClockwise,
            FrontClockwise, Front2, FrontAntiClockwise,
            UpperClockwise, Upper2, UpperAntiClockwise,
            DownClockwise, Down2, DownAntiClockwise,
        };
        private static readonly Random RandomGenerator = RandomFactory.CreateRandom();

        public static FaceRotation Random()
        {
            var index = RandomGenerator.Next(0, AllRotations.Count);
            return AllRotations[index];
        }

        public static FaceRotation ByFace(FaceType face, RotationDirection direction)
        {
            return AllRotations.First(r => r.Face == face && r.Direction == direction);
        }

        public static FaceRotation ByFaceTwice(FaceType face)
        {
            return AllRotations.First(r => r.Face == face && r.Count == 2);
        }
    }
}
