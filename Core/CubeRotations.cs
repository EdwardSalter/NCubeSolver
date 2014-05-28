using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public static class CubeRotations
    {
        public static CubeRotation XClockwise = new CubeRotation { Axis = Axis.X, Name = "x" };
        public static CubeRotation XAntiClockwise = new CubeRotation { Axis = Axis.X, Name = "x'", Direction = RotationDirection.AntiClockwise };
        public static CubeRotation X2 = new CubeRotation { Axis = Axis.X, Name = "x2", Count = 2};

        public static CubeRotation YClockwise = new CubeRotation { Axis = Axis.Y, Name = "y" };
        public static CubeRotation YAntiClockwise = new CubeRotation { Axis = Axis.Y, Name = "y'", Direction = RotationDirection.AntiClockwise };
        public static CubeRotation Y2 = new CubeRotation { Axis = Axis.Y, Name = "y2", Count = 2 };

        public static CubeRotation ZClockwise = new CubeRotation { Axis = Axis.Z, Name = "z" };
        public static CubeRotation ZAntiClockwise = new CubeRotation { Axis = Axis.Z, Name = "z'", Direction = RotationDirection.AntiClockwise };
        public static CubeRotation Z2 = new CubeRotation { Axis = Axis.Z, Name = "z2", Count = 2 };

        private static readonly List<CubeRotation> AllRotations = new List<CubeRotation>
        {
            XClockwise, XAntiClockwise, X2,
            YClockwise, YAntiClockwise, Y2,
            ZClockwise, ZAntiClockwise, Z2
        };
        private static readonly Random RandomGenerator = RandomFactory.CreateRandom();

        public static CubeRotation Random()
        {
            var index = RandomGenerator.Next(0, AllRotations.Count);
            return AllRotations[index];
        }

        public static CubeRotation ByAxis(Axis axis, RotationDirection direction)
        {
            return AllRotations.First(r => r.Axis == axis && r.Direction == direction);
        }

        public static CubeRotation ByAxisTwice(Axis axis)
        {
            return AllRotations.First(r => r.Axis == axis && r.Count == 2);
        }
    }
}