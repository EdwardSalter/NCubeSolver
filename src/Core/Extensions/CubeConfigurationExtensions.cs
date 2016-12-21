using System;

namespace NCubeSolvers.Core.Extensions
{
    public static class CubeConfigurationExtensions
    {
        private static readonly object PrintLock = new object();

        public static void PrintConfiguration(this CubeConfiguration<FaceColour> configuration)
        {
            lock (PrintLock)
            {
                foreach (var face in configuration.Faces)
                {
                    PrintFace(face.Value, face.Key);
                }
            }
        }

        private static void PrintFace(Face<FaceColour> face, FaceType faceType)
        {
            Console.WriteLine("Face " + faceType);

            Console.ForegroundColor = ConsoleColor.Black;
            var length = face.GetEdge(Edge.Top).Length;
            for (int j = 0; j < length; j++)
            {
                for (int i = 0; i < length - 1; i++)
                {
                    var item = face.Items[j, i];
                    var colour = GetColour(item);
                    Console.BackgroundColor = colour;
                    Console.Write(item.ToString()[0]);
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.Write(" ");
                }
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine();
            }
        }

        private static ConsoleColor GetColour(FaceColour colour)
        {
            switch (colour)
            {
                case FaceColour.Blue:
                    return ConsoleColor.Blue;
                case FaceColour.Green:
                    return ConsoleColor.Green;
                case FaceColour.Orange:
                    return ConsoleColor.Magenta;
                case FaceColour.Red:
                    return ConsoleColor.Red;
                case FaceColour.White:
                    return ConsoleColor.White;
                case FaceColour.Yellow:
                    return ConsoleColor.Yellow;
            }

            throw new ArgumentOutOfRangeException("Unspoorted face colour " + colour);
        }
    }
}
