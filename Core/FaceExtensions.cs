using System.Linq;

namespace NCubeSolvers.Core
{
    public static class FaceExtensions
    {
        public static T TopRight<T>(this Face<T> configuration)
        {
            return configuration.GetEdge(Edge.Top).Last();
        }

        public static T TopLeft<T>(this Face<T> configuration)
        {
            return configuration.GetEdge(Edge.Top).First();
        }

        public static T BottomRight<T>(this Face<T> configuration)
        {
            return configuration.GetEdge(Edge.Bottom).Last();
        }

        public static T BottomLeft<T>(this Face<T> configuration)
        {
            return configuration.GetEdge(Edge.Bottom).First();
        }

        public static T BottomCentre<T>(this Face<T> configuration)
        {
            return configuration.GetEdge(Edge.Bottom).Centre();
        }

        public static T TopCentre<T>(this Face<T> configuration)
        {
            return configuration.GetEdge(Edge.Top).Centre();
        }

        public static T LeftCentre<T>(this Face<T> configuration)
        {
            return configuration.GetEdge(Edge.Left).Centre();
        }

        public static T RightCentre<T>(this Face<T> configuration)
        {
            return configuration.GetEdge(Edge.Right).Centre();
        }
    }
}
