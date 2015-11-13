using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NCubeSolvers.Core.Extensions;

namespace NCubeSolvers.Core
{
    public class CubeConfiguration<T> : IRotatable
    {
        public int Size { get; private set; }
        public Dictionary<FaceType, Face<T>> Faces { get; private set; }

        public CubeConfiguration(int cubeSize)
            : this(cubeSize, default(T), default(T), default(T), default(T), default(T), default(T))
        { }

        public CubeConfiguration(int cubeSize, T fillTop, T fillBottom, T fillLeft, T fillRight, T fillFront, T fillBack)
        {
            Size = cubeSize;
            Faces = new Dictionary<FaceType, Face<T>>(6)
            {
                {FaceType.Upper, new Face<T>("U", fillTop, cubeSize)},
                {FaceType.Down, new Face<T>("D", fillBottom, cubeSize)},
                {FaceType.Left, new Face<T>("L", fillLeft, cubeSize)},
                {FaceType.Right, new Face<T>("R", fillRight, cubeSize)},
                {FaceType.Front, new Face<T>("F", fillFront, cubeSize)},
                {FaceType.Back, new Face<T>("B", fillBack, cubeSize)}
            };
        }

        public CubeConfiguration(T[,] faceTop, T[,] faceBottom, T[,] faceLeft, T[,] faceRight, T[,] faceFront, T[,] faceBack)
        {
            Size = MathEx.Sqrt(faceTop.Length);
            Faces = new Dictionary<FaceType, Face<T>>(6)
            {
                {FaceType.Upper, new Face<T>("U", faceTop)},
                {FaceType.Down, new Face<T>("D", faceBottom)},
                {FaceType.Left, new Face<T>("L", faceLeft)},
                {FaceType.Right, new Face<T>("R", faceRight)},
                {FaceType.Front, new Face<T>("F", faceFront)},
                {FaceType.Back, new Face<T>("B", faceBack)}
            };
        }

        public static CubeConfiguration<FaceColour> CreateStandardCubeConfiguration(int size)
        {
            return new CubeConfiguration<FaceColour>(size, FaceColour.Yellow, FaceColour.White,
                FaceColour.Blue, FaceColour.Green, FaceColour.Red, FaceColour.Orange);
        }

        public int GetCentreLayer()
        {
            if (Size % 2 == 0) throw new InvalidOperationException("There is not a centre layer for an even number sized cube");

            return (Size + 1) / 2 - 1;
        }

        public IEnumerable<T> AllItems
        {
            get { return Faces.Values.SelectMany(face => face.Items.AsEnumerable()).Distinct(); }
        }

        public Task Rotate(FaceRotation rotation)
        {
            var faceType = rotation.Face;
            var direction = rotation.Direction;

            for (int i = 0; i < rotation.Count; i++)
            {
                if (rotation.LayerNumberFromFace == 0)
                {
                    Faces[faceType].Rotate(direction);
                }

                switch (faceType)
                {
                    case FaceType.Upper:
                        RotateTop(direction, rotation.LayerNumberFromFace);
                        break;
                    case FaceType.Down:
                        RotateBottom(direction, rotation.LayerNumberFromFace);
                        break;
                    case FaceType.Left:
                        RotateLeft(direction, rotation.LayerNumberFromFace);
                        break;
                    case FaceType.Right:
                        RotateRight(direction, rotation.LayerNumberFromFace);
                        break;
                    case FaceType.Front:
                        RotateFront(direction, rotation.LayerNumberFromFace);
                        break;
                    case FaceType.Back:
                        RotateBack(direction, rotation.LayerNumberFromFace);
                        break;
                }
            }

            return TaskEx.Completed;
        }

        public Task RotateCube(CubeRotation rotation)
        {
            FaceType[] centralFaces;
            for (int i = 0; i < rotation.Count; i++)
            {
                switch (rotation.Axis)
                {
                    case Axis.X:
                        centralFaces = new[] { FaceType.Upper, FaceType.Front, FaceType.Down, FaceType.Back };
                        RotateCube(centralFaces, FaceType.Left, FaceType.Right, rotation.Direction);
                        break;

                    case Axis.Y:
                        centralFaces = new[] { FaceType.Front, FaceType.Right, FaceType.Back, FaceType.Left };
                        RotateCube(centralFaces, FaceType.Down, FaceType.Upper, rotation.Direction, false);
                        break;

                    case Axis.Z:
                        if (rotation.Direction == RotationDirection.Clockwise)
                        {
                            Faces[FaceType.Front].Rotate(RotationDirection.Clockwise);
                            Faces[FaceType.Back].Rotate(RotationDirection.AntiClockwise);

                            var temp = Faces[FaceType.Upper];
                            Faces[FaceType.Upper] = new Face<T>(Faces[FaceType.Upper].Id,
                                ArrayRotater.RotateClockwise(Faces[FaceType.Left].Items));
                            Faces[FaceType.Left] = new Face<T>(Faces[FaceType.Left].Id,
                                ArrayRotater.RotateClockwise(Faces[FaceType.Down].Items));
                            Faces[FaceType.Down] = new Face<T>(Faces[FaceType.Down].Id,
                                ArrayRotater.RotateClockwise(Faces[FaceType.Right].Items));
                            Faces[FaceType.Right] = new Face<T>(Faces[FaceType.Down].Id,
                                ArrayRotater.RotateClockwise(temp.Items));
                        }
                        else
                        {
                            Faces[FaceType.Front].Rotate(RotationDirection.AntiClockwise);
                            Faces[FaceType.Back].Rotate(RotationDirection.Clockwise);

                            var temp = Faces[FaceType.Upper];
                            Faces[FaceType.Upper] = new Face<T>(Faces[FaceType.Upper].Id,
                                ArrayRotater.RotateAntiClockwise(Faces[FaceType.Right].Items));
                            Faces[FaceType.Right] = new Face<T>(Faces[FaceType.Right].Id,
                                ArrayRotater.RotateAntiClockwise(Faces[FaceType.Down].Items));
                            Faces[FaceType.Down] = new Face<T>(Faces[FaceType.Down].Id,
                                ArrayRotater.RotateAntiClockwise(Faces[FaceType.Left].Items));
                            Faces[FaceType.Left] = new Face<T>(Faces[FaceType.Left].Id,
                                ArrayRotater.RotateAntiClockwise(temp.Items));
                        }
                        break;
                }
            }
            return TaskEx.Completed;
        }

        private void RotateCube(IList<FaceType> centralRotationFaces, FaceType clockwiseFace, FaceType antiClockwiseFace, RotationDirection direction, bool flipBack = true)
        {
            if (direction == RotationDirection.AntiClockwise)
            {
                centralRotationFaces = centralRotationFaces.Reverse().ToList();
                var tempFace = clockwiseFace;
                clockwiseFace = antiClockwiseFace;
                antiClockwiseFace = tempFace;
            }
            Faces[clockwiseFace].Rotate(RotationDirection.AntiClockwise);
            Faces[antiClockwiseFace].Rotate(RotationDirection.Clockwise);

            var temp = Faces[centralRotationFaces[0]];
            for (int i = 0; i < centralRotationFaces.Count - 1; i++)
            {
                var thisFace = centralRotationFaces[i];
                var nextFace = centralRotationFaces[i + 1];

                var nextItems = flipBack && (thisFace == FaceType.Back || nextFace == FaceType.Back)
                    ? ArrayRotater.Flip(Faces[nextFace].Items)
                    : Faces[nextFace].Items;

                Faces[thisFace] = new Face<T>(Faces[thisFace].Id, nextItems);
            }

            var curFace = centralRotationFaces.Last();
            var firstFace = centralRotationFaces[0];
            var items = flipBack && (curFace == FaceType.Back || firstFace == FaceType.Back)
                ? ArrayRotater.Flip(temp.Items)
                : temp.Items;

            Faces[curFace] = new Face<T>(Faces[curFace].Id, items);
        }

        private void RotateBack(RotationDirection direction, int layerNumber)
        {
            var faces = new[] { Faces[FaceType.Left], Faces[FaceType.Upper], Faces[FaceType.Right], Faces[FaceType.Down] };

            if (direction == RotationDirection.Clockwise)
            {
                var temp = faces[0].GetEdge(layerNumber, Edge.Left);
                faces[0].SetEdge(layerNumber, Edge.Left, faces[1].GetEdge(layerNumber, Edge.Top).Reverse().ToArray());
                faces[1].SetEdge(layerNumber, Edge.Top, faces[2].GetEdge(layerNumber, Edge.Right));
                faces[2].SetEdge(layerNumber, Edge.Right, faces[3].GetEdge(layerNumber, Edge.Bottom).Reverse().ToArray());
                faces[3].SetEdge(layerNumber, Edge.Bottom, temp);
            }
            else
            {
                faces = faces.Reverse().ToArray();

                var temp = faces[0].GetEdge(layerNumber, Edge.Bottom);
                faces[0].SetEdge(layerNumber, Edge.Bottom, faces[1].GetEdge(layerNumber, Edge.Right).Reverse().ToArray());
                faces[1].SetEdge(layerNumber, Edge.Right, faces[2].GetEdge(layerNumber, Edge.Top));
                faces[2].SetEdge(layerNumber, Edge.Top, faces[3].GetEdge(layerNumber, Edge.Left).Reverse().ToArray());
                faces[3].SetEdge(layerNumber, Edge.Left, temp);
            }
        }

        private void RotateFront(RotationDirection direction, int layerNumber)
        {
            var faces = new[] { Faces[FaceType.Left], Faces[FaceType.Down], Faces[FaceType.Right], Faces[FaceType.Upper] };

            if (direction == RotationDirection.Clockwise)
            {
                var temp = faces[0].GetEdge(layerNumber, Edge.Right).Reverse().ToArray();
                faces[0].SetEdge(layerNumber, Edge.Right, faces[1].GetEdge(layerNumber, Edge.Top));
                faces[1].SetEdge(layerNumber, Edge.Top, faces[2].GetEdge(layerNumber, Edge.Left).Reverse().ToArray());
                faces[2].SetEdge(layerNumber, Edge.Left, faces[3].GetEdge(layerNumber, Edge.Bottom));
                faces[3].SetEdge(layerNumber, Edge.Bottom, temp);
            }
            else
            {
                faces = faces.Reverse().ToArray();

                var temp = faces[0].GetEdge(layerNumber, Edge.Bottom).Reverse().ToArray();
                faces[0].SetEdge(layerNumber, Edge.Bottom, faces[1].GetEdge(layerNumber, Edge.Left));
                faces[1].SetEdge(layerNumber, Edge.Left, faces[2].GetEdge(layerNumber, Edge.Top).Reverse().ToArray());
                faces[2].SetEdge(layerNumber, Edge.Top, faces[3].GetEdge(layerNumber, Edge.Right));
                faces[3].SetEdge(layerNumber, Edge.Right, temp);
            }
        }

        private void RotateLeft(RotationDirection direction, int layerNumber)
        {
            var faces = new[] { Faces[FaceType.Front], Faces[FaceType.Upper], Faces[FaceType.Back], Faces[FaceType.Down] };

            const Edge edge = Edge.Left;
            Edge reverseEdge = EdgeMethods.GetReverseEdge(edge);


            if (direction == RotationDirection.AntiClockwise)
            {
                faces = faces.Reverse().ToArray();

                var reverseEdgeSet = reverseEdge == edge ?
                    faces[1].GetEdge(layerNumber, reverseEdge)
                    : faces[1].GetEdge(layerNumber, reverseEdge).Reverse().ToArray();
                var reverseEdgeSet2 = reverseEdge == edge ?
                    faces[2].GetEdge(layerNumber, edge)
                    : faces[2].GetEdge(layerNumber, edge).Reverse().ToArray();

                var temp = faces[0].GetEdge(layerNumber, edge);
                faces[0].SetEdge(layerNumber, edge, reverseEdgeSet);
                faces[1].SetEdge(layerNumber, reverseEdge, reverseEdgeSet2);
                faces[2].SetEdge(layerNumber, edge, faces[3].GetEdge(layerNumber, edge));
                faces[3].SetEdge(layerNumber, edge, temp);
            }
            else
            {
                var reverseEdgeSet = reverseEdge == edge
                    ? faces[3].GetEdge(layerNumber, edge)
                    : faces[3].GetEdge(layerNumber, edge).Reverse().ToArray();

                var temp = faces[0].GetEdge(layerNumber, edge);
                faces[0].SetEdge(layerNumber, edge, faces[1].GetEdge(layerNumber, edge));
                faces[1].SetEdge(layerNumber, edge, faces[2].GetEdge(layerNumber, reverseEdge).Reverse().ToArray());
                faces[2].SetEdge(layerNumber, reverseEdge, reverseEdgeSet);
                faces[3].SetEdge(layerNumber, edge, temp);
            }
        }

        private void RotateBottom(RotationDirection direction, int layerNumber)
        {
            var faces = new[] { Faces[FaceType.Front], Faces[FaceType.Left], Faces[FaceType.Back], Faces[FaceType.Right] };

            RotateFaces(faces, Edge.Bottom, direction, layerNumber);
        }

        private void RotateTop(RotationDirection direction, int layerNumber)
        {
            var faces = new[] { Faces[FaceType.Front], Faces[FaceType.Right], Faces[FaceType.Back], Faces[FaceType.Left] };

            RotateFaces(faces, Edge.Top, direction, layerNumber);
        }

        private void RotateRight(RotationDirection direction, int layerNumber)
        {
            var faces = new[] { Faces[FaceType.Upper], Faces[FaceType.Front], Faces[FaceType.Down], Faces[FaceType.Back] };

            const Edge edge = Edge.Right;
            Edge reverseEdge = EdgeMethods.GetReverseEdge(edge);

            if (direction == RotationDirection.AntiClockwise)
            {
                faces = faces.Reverse().ToArray();

                var reverseEdgeSet = reverseEdge == edge ?
                    faces[1].GetEdge(layerNumber, edge)
                    : faces[1].GetEdge(layerNumber, edge).Reverse().ToArray();

                var temp = faces[0].GetEdge(layerNumber, reverseEdge).Reverse().ToArray();
                faces[0].SetEdge(layerNumber, reverseEdge, reverseEdgeSet);
                faces[1].SetEdge(layerNumber, edge, faces[2].GetEdge(layerNumber, edge));
                faces[2].SetEdge(layerNumber, edge, faces[3].GetEdge(layerNumber, edge));
                faces[3].SetEdge(layerNumber, edge, temp);
            }
            else
            {
                var temp = reverseEdge == edge
                    ? faces[0].GetEdge(layerNumber, edge)
                    : faces[0].GetEdge(layerNumber, edge).Reverse().ToArray();

                faces[0].SetEdge(layerNumber, edge, faces[1].GetEdge(layerNumber, edge));
                faces[1].SetEdge(layerNumber, edge, faces[2].GetEdge(layerNumber, edge));
                faces[2].SetEdge(layerNumber, edge, faces[3].GetEdge(layerNumber, reverseEdge).Reverse().ToArray());
                faces[3].SetEdge(layerNumber, reverseEdge, temp);
            }
        }

        private static void RotateFaces(IList<Face<T>> faces, Edge edge, RotationDirection direction, int layerNumber)
        {
            if (faces.Count != 4)
            {
                throw new ArgumentException("Face list must contain 4 faces in clockwise order", "faces");
            }

            Edge reverseEdge = EdgeMethods.GetReverseEdge(edge);


            if (direction == RotationDirection.AntiClockwise)
            {
                faces = faces.Reverse().ToList();

                var reverseEdgeSet = reverseEdge == edge ?
                    faces[1].GetEdge(layerNumber, reverseEdge)
                    : faces[1].GetEdge(layerNumber, reverseEdge).Reverse().ToArray();
                var reverseEdgeSet2 = reverseEdge == edge ?
                    faces[2].GetEdge(layerNumber, edge)
                    : faces[2].GetEdge(layerNumber, edge).Reverse().ToArray();

                var temp = faces[0].GetEdge(layerNumber, edge);
                faces[0].SetEdge(layerNumber, edge, reverseEdgeSet);
                faces[1].SetEdge(layerNumber, reverseEdge, reverseEdgeSet2);
                faces[2].SetEdge(layerNumber, edge, faces[3].GetEdge(layerNumber, edge));
                faces[3].SetEdge(layerNumber, edge, temp);
            }
            else
            {
                var reverseEdgeSet = reverseEdge == edge
                    ? faces[3].GetEdge(layerNumber, edge)
                    : faces[3].GetEdge(layerNumber, edge).Reverse().ToArray();

                var temp = faces[0].GetEdge(layerNumber, edge);
                faces[0].SetEdge(layerNumber, edge, faces[1].GetEdge(layerNumber, edge));
                faces[1].SetEdge(layerNumber, edge, faces[2].GetEdge(layerNumber, reverseEdge));
                faces[2].SetEdge(layerNumber, reverseEdge, reverseEdgeSet);
                faces[3].SetEdge(layerNumber, edge, temp);
            }
        }

        public void CheckValid()
        {
            if (Faces.Values.SelectMany(face => face.Items.AsEnumerable()).Any(t => t.Equals(default(T))))
            {
                throw new InvalidCubeConfigurationException();
            }

            foreach (var face in Faces.Values)
            {
                face.CheckValidity();
            }
        }

        public IEnumerable<T> GetEdgesByLayer(FaceType faceType, int layerNumber)
        {
            List<T> returning = new List<T>();

            if (layerNumber == 0)
            {
                var items = Faces[faceType].Items;
                var length = items.GetLength(0);
                for (int y = 0; y < length; y++)
                {
                    for (int x = 0; x < length; x++)
                    {
                        returning.Add(items[y, x]);
                    }
                }
                return returning;
            }

            Face<T>[] faces;

            switch (faceType)
            {
                case FaceType.Upper:
                    faces = new[] { Faces[FaceType.Front], Faces[FaceType.Right], Faces[FaceType.Back], Faces[FaceType.Left] };
                    returning = faces.SelectMany(f => f.GetEdge(layerNumber, Edge.Top)).ToList();
                    break;

                case FaceType.Down:
                    faces = new[] { Faces[FaceType.Front], Faces[FaceType.Right], Faces[FaceType.Back], Faces[FaceType.Left] };
                    returning = faces.SelectMany(f => f.GetEdge(layerNumber, Edge.Bottom)).ToList();
                    break;

                case FaceType.Back:
                    returning.Clear();
                    returning.AddRange(Faces[FaceType.Left].GetEdge(layerNumber, Edge.Left));
                    returning.AddRange(Faces[FaceType.Upper].GetEdge(layerNumber, Edge.Top));
                    returning.AddRange(Faces[FaceType.Right].GetEdge(layerNumber, Edge.Right));
                    returning.AddRange(Faces[FaceType.Down].GetEdge(layerNumber, Edge.Bottom));
                    break;

                case FaceType.Front:
                    returning.Clear();
                    returning.AddRange(Faces[FaceType.Left].GetEdge(layerNumber, Edge.Right));
                    returning.AddRange(Faces[FaceType.Upper].GetEdge(layerNumber, Edge.Bottom));
                    returning.AddRange(Faces[FaceType.Right].GetEdge(layerNumber, Edge.Left));
                    returning.AddRange(Faces[FaceType.Down].GetEdge(layerNumber, Edge.Top));
                    break;

                case FaceType.Left:
                    returning.Clear();
                    returning.AddRange(Faces[FaceType.Front].GetEdge(layerNumber, Edge.Left));
                    returning.AddRange(Faces[FaceType.Upper].GetEdge(layerNumber, Edge.Left));
                    returning.AddRange(Faces[FaceType.Back].GetEdge(layerNumber, Edge.Right));
                    returning.AddRange(Faces[FaceType.Down].GetEdge(layerNumber, Edge.Left));
                    break;

                case FaceType.Right:
                    returning.Clear();
                    returning.AddRange(Faces[FaceType.Front].GetEdge(layerNumber, Edge.Right));
                    returning.AddRange(Faces[FaceType.Upper].GetEdge(layerNumber, Edge.Right));
                    returning.AddRange(Faces[FaceType.Back].GetEdge(layerNumber, Edge.Left));
                    returning.AddRange(Faces[FaceType.Down].GetEdge(layerNumber, Edge.Right));
                    break;

                default:
                    throw new InvalidOperationException("Unknown face type given");
            }

            return returning;
        }
    }
}
