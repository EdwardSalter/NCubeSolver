using NCubeSolvers.Core.Extensions;

namespace NCubeSolvers.Core
{
    public static class ArrayRotater
    {
        public static T[,] RotateClockwise<T>(T[,] array)
        {
            var size = MathEx.Sqrt(array.Length);
            var newArray = new T[size, size];

            for (int i = size - 1; i >= 0; --i)
            {
                for (int j = 0; j < size; ++j)
                {
                    newArray[j, size - 1 - i] = array[i, j];
                }
            }

            return newArray;
        }

        public static T[,] RotateAntiClockwise<T>(T[,] array)
        {
            var size = MathEx.Sqrt(array.Length);
            var newArray = new T[size, size];

            for (int i = size - 1; i >= 0; --i)
            {
                for (int j = 0; j < size; ++j)
                {
                    newArray[size - 1 - j, i] = array[i, j];
                }
            }

            return newArray;
        }

        public static T[,] Flip<T>(T[,] array)
        {
            var size = MathEx.Sqrt(array.Length);
            var newArray = new T[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    newArray[j, i] = array[size - 1 - j, size - 1 - i];
                }
            }

            return newArray;
        }
    }
}
