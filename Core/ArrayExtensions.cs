using System;
using System.Collections.Generic;
using NCubeSolvers.Core.Extensions;

namespace NCubeSolvers.Core
{
    public static class ArrayExtensions
    {
        public static IEnumerable<T> AsEnumerable<T>(this T[,] array)
        {
            int length = MathEx.Sqrt(array.Length);
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    yield return array[j, i];
                }
            }
        }

        public static T Centre<T>(this T[] array)
        {
            if (array.Length % 2 == 0)
            {
                throw new ArgumentException("Centre method can only work on arrays with an odd number of elements");
            }

            var index = (array.Length - 1) / 2;
            return array[index];
        }
    }
}
