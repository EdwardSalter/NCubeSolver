using System.Linq;

namespace NCubeSolvers.Core
{
    public static class Utilities
    {
        public static bool IsInnerEdgeComplete<T>(FaceType face1, FaceType face2, CubeConfiguration<T> configuration)
        {
            var face1EdgeType = FaceRules.EdgeJoiningFaceToFace(face2, face1);
            var face2EdgeType = FaceRules.EdgeJoiningFaceToFace(face1, face2);

            var innerEdgeLength = configuration.Size - 2;
            var face1Edge = configuration.Faces[face1].GetEdge(face1EdgeType).Skip(1).Take(innerEdgeLength);
            var face2Edge = configuration.Faces[face2].GetEdge(face2EdgeType).Skip(1).Take(innerEdgeLength);

            return face1Edge.Distinct().Count() == 1 && face2Edge.Distinct().Count() == 1;
        }
    }
}