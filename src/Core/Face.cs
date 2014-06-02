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
            switch (edge)
            {
                case Edge.Top:
                    return GetTop();
                case Edge.Bottom:
                    return GetBottom();
                case Edge.Left:
                    return GetLeft();
                case Edge.Right:
                    return GetRight();
                default:
                    throw new ArgumentException("Invalid edge specified", "edge");
            }
        }

        public void SetEdge(Edge edge, T[] array)
        {
            switch (edge)
            {
                case Edge.Top:
                    SetTop(array);
                    break;
                case Edge.Bottom:
                    SetBottom(array);
                    break;
                case Edge.Left:
                    SetLeft(array);
                    break;
                case Edge.Right:
                    SetRight(array);
                    break;
                default:
                    throw new ArgumentException("Invalid edge specified", "edge");
            }
        }

        private T[] GetRight()
        {
            var array = new T[m_size];
            for (int i = 0; i < m_size; i++)
            {
                array[i] = Items[i, m_size - 1];
            }
            return array;
        }

        private void SetRight(T[] array)
        {
            if (array.Length != m_size)
            {
                throw new ArgumentException("Array should only contain " + m_size + " elements", "array");
            }

            for (int i = 0; i < array.Length; i++)
            {
                Items[i, m_size - 1] = array[i];
            }
        }

        private T[] GetLeft()
        {
            var array = new T[m_size];
            for (int i = 0; i < m_size; i++)
            {
                array[i] = Items[i, 0];
            }
            return array;
        }

        private void SetLeft(T[] array)
        {
            if (array.Length != m_size)
            {
                throw new ArgumentException("Array should only contain " + m_size + " elements", "array");
            }

            for (int i = 0; i < array.Length; i++)
            {
                Items[i, 0] = array[i];
            }
        }

        private T[] GetTop()
        {
            var array = new T[m_size];
            for (int i = 0; i < m_size; i++)
            {
                array[i] = Items[0, i];
            }
            return array;
        }

        private void SetTop(T[] array)
        {
            if (array.Length != m_size)
            {
                throw new ArgumentException("Array should only contain " + m_size + " elements", "array");
            }

            for (int i = 0; i < array.Length; i++)
            {
                Items[0, i] = array[i];
            }
        }

        private T[] GetBottom()
        {
            var array = new T[m_size];
            for (int i = 0; i < m_size; i++)
            {
                array[i] = Items[m_size - 1, i];
            }
            return array;
        }

        private void SetBottom(T[] array)
        {
            if (array.Length != m_size)
            {
                throw new ArgumentException("Array should only contain " + m_size + " elements", "array");
            }

            for (int i = 0; i < array.Length; i++)
            {
                Items[m_size - 1, i] = array[i];
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
