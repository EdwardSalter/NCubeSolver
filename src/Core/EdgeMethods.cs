using System;

namespace NCubeSolvers.Core
{
    public static class EdgeMethods
    {
        public static Edge GetReverseEdge(Edge edge)
        {
            switch (edge)
            {
                case Edge.Top: return Edge.Top;
                case Edge.Bottom: return Edge.Bottom;
                case Edge.Left: return Edge.Right;
                case Edge.Right: return Edge.Left;
                default:
                    throw new ArgumentException("Invalid edge specified", "edge");
            }
        }
    }
}
