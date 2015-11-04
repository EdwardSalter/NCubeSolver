using System;
using System.Linq;
using NCubeSolvers.Core.Extensions;

namespace NCubeSolvers.Core
{
    public class Face<T>
    {
        private readonly int m_size;
        public string Id { get; private set; }
        public T[,] Items { get; private set; }

        // TODO: MOVE TO ANOTHER WRAPPER
        public T Centre
        {
            get
            {
                // TODO: DO SOMETHING IF THE THERE IS NO CENTRE
                var centreIndex = (m_size - 1) / 2;
                return Items[centreIndex, centreIndex];
            }
        }

        public Face(string id, int size)
            : this(id, default(T), size)
        { }

        public Face(string id, T fill, int size)
        {
            Id = id;
            m_size = size;
            Items = FillArray(fill, size);
        }

        public Face(string id, T[,] array)
        {
            Id = id;
            m_size = MathEx.Sqrt(array.Length);
            Items = array;
        }

        private static T[,] FillArray(T fill, int arraySize)
        {
            var array = new T[arraySize, arraySize];
            for (int i = 0; i < arraySize; i++)
            {
                for (int j = 0; j < arraySize; j++)
                {
                    array[i, j] = fill;
                }
            }
            return array;
        }

        public void CheckValidity()
        {
            // Throw if there are duplicate in the same face
            if (Items.AsEnumerable().Distinct().Count() != Items.Length)
            {
                throw new InvalidCubeConfigurationException();
            }
        }

        public void Rotate(RotationDirection direction)
        {
            Items = direction == RotationDirection.Clockwise ?
                ArrayRotater.RotateClockwise(Items) :
                ArrayRotater.RotateAntiClockwise(Items);
        }

        public T[] GetEdge(Edge edge)
        {
            return GetEdge(0, edge);
        }

        public T[] GetEdge(int indexFromEdge, Edge edge)
        {
            if (indexFromEdge >= m_size)
            {
                throw new ArgumentOutOfRangeException("The index from edge value must be less than the size of the face");
            }

            switch (edge)
            {
                case Edge.Top:
                    return GetTop(indexFromEdge);
                case Edge.Bottom:
                    return GetBottom(indexFromEdge);
                case Edge.Left:
                    return GetLeft(indexFromEdge);
                case Edge.Right:
                    return GetRight(indexFromEdge);
                default:
                    throw new ArgumentException("Invalid edge specified", "edge");
            }
        }

        public void SetEdge(Edge edge, T[] array)
        {
            SetEdge(0, edge, array);
        }

        public void SetEdge(int indexFromEdge, Edge edge, T[] array)
        {
            if (array.Length != m_size)
            {
                throw new ArgumentException("Array should only contain " + m_size + " elements", "array");
            }
            if (indexFromEdge >= m_size)
            {
                throw new ArgumentOutOfRangeException("The index from edge value must be less than the size of the face");
            }

            switch (edge)
            {
                case Edge.Top:
                    SetTop(array, indexFromEdge);
                    break;
                case Edge.Bottom:
                    SetBottom(array, indexFromEdge);
                    break;
                case Edge.Left:
                    SetLeft(array, indexFromEdge);
                    break;
                case Edge.Right:
                    SetRight(array, indexFromEdge);
                    break;
                default:
                    throw new ArgumentException("Invalid edge specified", "edge");
            }
        }

        private T[] GetRight(int indexFromEdge = 0)
        {
            var array = new T[m_size];
            for (int i = 0; i < m_size; i++)
            {
                array[i] = Items[i, m_size - (indexFromEdge + 1)];
            }
            return array;
        }

        private void SetRight(T[] array, int indexFromEdge = 0)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Items[i, m_size - indexFromEdge - 1] = array[i];
            }
        }

        private T[] GetLeft(int indexFromEdge = 0)
        {
            var array = new T[m_size];
            for (int i = 0; i < m_size; i++)
            {
                array[i] = Items[i, indexFromEdge];
            }
            return array;
        }

        private void SetLeft(T[] array, int indexFromEdge = 0)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Items[i, indexFromEdge] = array[i];
            }
        }

        private T[] GetTop(int indexFromEdge = 0)
        {
            var array = new T[m_size];
            for (int i = 0; i < m_size; i++)
            {
                array[i] = Items[indexFromEdge, i];
            }
            return array;
        }

        private void SetTop(T[] array, int indexFromEdge = 0)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Items[indexFromEdge, i] = array[i];
            }
        }

        private T[] GetBottom(int indexFromEdge = 0)
        {
            var array = new T[m_size];
            for (int i = 0; i < m_size; i++)
            {
                array[i] = Items[m_size - 1 - indexFromEdge, i];
            }
            return array;
        }

        private void SetBottom(T[] array, int indexFromEdge = 0)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Items[m_size - 1 - indexFromEdge, i] = array[i];
            }
        }

        public bool Contains(T item)
        {
            return Items.AsEnumerable().Contains(item);
        }

        public override string ToString()
        {
            return Id;
        }
    }
}
