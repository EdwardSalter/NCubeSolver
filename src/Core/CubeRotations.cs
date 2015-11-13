using System;
using System.Collections.Generic;
using System.Linq;

namespace NCubeSolvers.Core
{
    public static class CubeRotations
    {
        public static CubeRotation XClockwise = new CubeRotation { Axis = Axis.X};
        public static CubeRotation XAntiClockwise = new CubeRotation { Axis = Axis.X, Direction = RotationDirection.AntiClockwise };
        public static CubeRotation X2 = new CubeRotation { Axis = Axis.X, Count = 2};

        public static CubeRotation YClockwise = new CubeRotation { Axis = Axis.Y};
        public static CubeRotation YAntiClockwise = new CubeRotation { Axis = Axis.Y, Direction = RotationDirection.AntiClockwise };
        public static CubeRotation Y2 = new CubeRotation { Axis = Axis.Y, Count = 2 };

        public static CubeRotation ZClockwise = new CubeRotation { Axis = Axis.Z};
        public static CubeRotation ZAntiClockwise = new CubeRotation { Axis = Axis.Z, Direction = RotationDirection.AntiClockwise };
        public static CubeRotation Z2 = new CubeRotation { Axis = Axis.Z, Count = 2 };

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

        public static CubeRotation ByName(string name)
        {
            return AllRotations.SingleOrDefault(r => r.GetName() == name);
        }

        public static CubeRotation ByAxis(Axis axis, RotationDirection direction)
        {
            return AllRotations.Single(r => r.Axis == axis && r.Direction == direction && r.Count == 1);
        }

        public static CubeRotation ByAxisTwice(Axis axis)
        {
            return AllRotations.Single(r => r.Axis == axis && r.Count == 2);
        }
    }
}