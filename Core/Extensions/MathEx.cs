using System;

namespace Core.Extensions
{
    public class MathEx
    {
        public const float Pi = (float) Math.PI;
        public const float PiOver2 = Pi / 2.0f;

        public static float DegToRad(float degrees)
        {
            return degrees * (Pi / 180.0f);
        }

        public static float Sin(float angle)
        {
            return (float)Math.Sin(angle);
        }

        public static float Cos(float angle)
        {
            return (float)Math.Cos(angle);
        }

        public static int Sqrt(int val)
        {
            return (int) Math.Sqrt(val);
        }

        public static double CubicRoot(double x)
        {
            return Math.Pow(x, (1.0 / 3.0));
        }
    }
}