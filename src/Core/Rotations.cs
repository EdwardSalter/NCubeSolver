using System;
using System.Collections.Generic;
using System.Linq;

namespace NCubeSolvers.Core
{
    public static class Rotations
    {
        public static FaceRotation RightClockwise = new FaceRotation { Face = FaceType.Right};
        public static FaceRotation Right2 = new FaceRotation { Face = FaceType.Right, Count = 2 };
        public static FaceRotation RightAntiClockwise = new FaceRotation { Face = FaceType.Right, Direction = RotationDirection.AntiClockwise };
        public static FaceRotation SecondLayerRightClockwise = new FaceRotation { Face = FaceType.Right, LayerNumberFromFace = 1};
        public static FaceRotation SecondLayerRight2 = new FaceRotation { Face = FaceType.Right, Count = 2, LayerNumberFromFace = 1 };
        public static FaceRotation SecondLayerRightAntiClockwise = new FaceRotation { Face = FaceType.Right, Direction = RotationDirection.AntiClockwise, LayerNumberFromFace = 1 };

        public static FaceRotation LeftClockwise = new FaceRotation { Face = FaceType.Left};
        public static FaceRotation Left2 = new FaceRotation { Face = FaceType.Left, Count = 2 };
        public static FaceRotation LeftAntiClockwise = new FaceRotation { Face = FaceType.Left, Direction = RotationDirection.AntiClockwise };
        public static FaceRotation SecondLayerLeftClockwise = new FaceRotation { Face = FaceType.Left, LayerNumberFromFace = 1 };
        public static FaceRotation SecondLayerLeft2 = new FaceRotation { Face = FaceType.Left, Count = 2, LayerNumberFromFace = 1 };
        public static FaceRotation SecondLayerLeftAntiClockwise = new FaceRotation { Face = FaceType.Left, Direction = RotationDirection.AntiClockwise, LayerNumberFromFace = 1 };

        public static FaceRotation UpperClockwise = new FaceRotation { Face = FaceType.Upper};
        public static FaceRotation Upper2 = new FaceRotation { Face = FaceType.Upper, Count = 2 };
        public static FaceRotation UpperAntiClockwise = new FaceRotation { Face = FaceType.Upper, Direction = RotationDirection.AntiClockwise };
        public static FaceRotation SecondLayerUpperClockwise = new FaceRotation { Face = FaceType.Upper, LayerNumberFromFace = 1 };
        public static FaceRotation SecondLayerUpper2 = new FaceRotation { Face = FaceType.Upper, Count = 2, LayerNumberFromFace = 1 };
        public static FaceRotation SecondLayerUpperAntiClockwise = new FaceRotation { Face = FaceType.Upper, Direction = RotationDirection.AntiClockwise, LayerNumberFromFace = 1 };

        public static FaceRotation DownClockwise = new FaceRotation { Face = FaceType.Down};
        public static FaceRotation Down2 = new FaceRotation { Face = FaceType.Down, Count = 2 };
        public static FaceRotation DownAntiClockwise = new FaceRotation { Face = FaceType.Down, Direction = RotationDirection.AntiClockwise };
        public static FaceRotation SecondLayerDownClockwise = new FaceRotation { Face = FaceType.Down, LayerNumberFromFace = 1 };
        public static FaceRotation SecondLayerDown2 = new FaceRotation { Face = FaceType.Down, Count = 2, LayerNumberFromFace = 1 };
        public static FaceRotation SecondLayerDownAntiClockwise = new FaceRotation { Face = FaceType.Down, Direction = RotationDirection.AntiClockwise, LayerNumberFromFace = 1 };

        public static FaceRotation FrontClockwise = new FaceRotation { Face = FaceType.Front};
        public static FaceRotation Front2 = new FaceRotation { Face = FaceType.Front, Count = 2 };
        public static FaceRotation FrontAntiClockwise = new FaceRotation { Face = FaceType.Front, Direction = RotationDirection.AntiClockwise };
        public static FaceRotation SecondLayerFrontClockwise = new FaceRotation { Face = FaceType.Front, LayerNumberFromFace = 1 };
        public static FaceRotation SecondLayerFront2 = new FaceRotation { Face = FaceType.Front, Count = 2, LayerNumberFromFace = 1 };
        public static FaceRotation SecondLayerFrontAntiClockwise = new FaceRotation { Face = FaceType.Front, Direction = RotationDirection.AntiClockwise, LayerNumberFromFace = 1 };

        public static FaceRotation BackClockwise = new FaceRotation { Face = FaceType.Back};
        public static FaceRotation Back2 = new FaceRotation { Face = FaceType.Back, Count = 2 };
        public static FaceRotation BackAntiClockwise = new FaceRotation { Face = FaceType.Back, Direction = RotationDirection.AntiClockwise };
        public static FaceRotation SecondLayerBackClockwise = new FaceRotation { Face = FaceType.Back, LayerNumberFromFace = 1 };
        public static FaceRotation SecondLayerBack2 = new FaceRotation { Face = FaceType.Back, Count = 2, LayerNumberFromFace = 1 };
        public static FaceRotation SecondLayerBackAntiClockwise = new FaceRotation { Face = FaceType.Back, Direction = RotationDirection.AntiClockwise, LayerNumberFromFace = 1 };

        private static readonly List<FaceRotation> AllRotations = new List<FaceRotation>
        {
            LeftClockwise, Left2, LeftAntiClockwise,
            SecondLayerLeftClockwise, SecondLayerLeft2, SecondLayerLeftAntiClockwise,
            RightClockwise, Right2, RightAntiClockwise,
            SecondLayerRightClockwise, SecondLayerRight2, SecondLayerRightAntiClockwise,
            BackClockwise, Back2, BackAntiClockwise,
            SecondLayerBackClockwise, SecondLayerBack2, SecondLayerBackAntiClockwise,
            FrontClockwise, Front2, FrontAntiClockwise,
            SecondLayerFrontClockwise, SecondLayerFront2, SecondLayerFrontAntiClockwise,
            UpperClockwise, Upper2, UpperAntiClockwise,
            SecondLayerUpperClockwise, SecondLayerUpper2, SecondLayerUpperAntiClockwise,
            DownClockwise, Down2, DownAntiClockwise,
            SecondLayerDownClockwise, SecondLayerDown2, SecondLayerDownAntiClockwise
        };
        private static readonly Random RandomGenerator = RandomFactory.CreateRandom();

        public static FaceRotation Random()
        {
            var index = RandomGenerator.Next(0, AllRotations.Count);
            return AllRotations[index];
        }

        public static FaceRotation ByName(string name)
        {
            return AllRotations.SingleOrDefault(r => r.GetName() == name);
        }

        public static FaceRotation ByFace(FaceType face, RotationDirection direction, int layerNumber = 0)
        {
            return AllRotations.Single(r => r.Face == face && r.Direction == direction && r.LayerNumberFromFace == layerNumber && r.Count == 1);
        }

        public static FaceRotation ByFaceTwice(FaceType face, int layerNumber = 0)
        {
            return AllRotations.Single(r => r.Face == face && r.Count == 2 && r.LayerNumberFromFace == layerNumber);
        }
    }
}
