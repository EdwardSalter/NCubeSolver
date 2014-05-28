using OpenTK;

namespace Display.OpenGL.Extensions
{
    static class QuaternionEx
    {
        public static Matrix4 ToRotationMatrix(this Quaternion quaternion)
        {
            quaternion.Normalize();
            var qx = quaternion.X;
            var qy = quaternion.Y;
            var qz = quaternion.Z;
            var qw = quaternion.W;


            var matrix = new Matrix4(1.0f - 2.0f * qy * qy - 2.0f * qz * qz, 2.0f * qx * qy - 2.0f * qz * qw, 2.0f * qx * qz + 2.0f * qy * qw, 0.0f,
    2.0f * qx * qy + 2.0f * qz * qw, 1.0f - 2.0f * qx * qx - 2.0f * qz * qz, 2.0f * qy * qz - 2.0f * qx * qw, 0.0f,
    2.0f * qx * qz - 2.0f * qy * qw, 2.0f * qy * qz + 2.0f * qx * qw, 1.0f - 2.0f * qx * qx - 2.0f * qy * qy, 0.0f,
    0.0f, 0.0f, 0.0f, 1.0f);

            return matrix;
        }
    }
}
