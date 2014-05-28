using System.Windows.Media;
using Core;
using Core.Extensions;
using Display.OpenGL.Models.Primitives;
using OpenTK;
using SharpGL.VertexBuffers;

namespace Display.OpenGL.Models
{
    class Axes : IModel
    {
        private const float CylinderRadius = 0.05f;
        private const int CylinderSlices = 10;
        private const int CylinderLength = 5;
        private readonly VertexBufferArray m_vertexBuffer = new VertexBufferArray();
        private readonly Cylinder m_xCylinder = new Cylinder(CylinderRadius, CylinderSlices, CylinderLength, Colors.Red);
        private readonly Cylinder m_yCylinder = new Cylinder(CylinderRadius, CylinderSlices, CylinderLength, Colors.Green);
        private readonly Cylinder m_zCylinder = new Cylinder(CylinderRadius, CylinderSlices, CylinderLength, Colors.Blue);

        public void GenerateGeometry(SharpGL.OpenGL gl)
        {
            m_xCylinder.GenerateGeometry(gl);
            m_yCylinder.GenerateGeometry(gl);
            m_zCylinder.GenerateGeometry(gl);
        }

        public void Render(SharpGL.OpenGL gl, ShaderWrapper shader)
        {
            m_vertexBuffer.Bind(gl);
            // TODO: CENTRE TUBES

            var rotation = Matrix4.CreateRotationY(MathEx.PiOver2);
            shader.SetModelMatrix(rotation);
            m_xCylinder.Render(gl, shader);

            rotation = Matrix4.CreateRotationX(-MathEx.PiOver2);
            shader.SetModelMatrix(rotation);
            m_yCylinder.Render(gl, shader);

            rotation = Matrix4.CreateRotationZ(MathEx.PiOver2);
            shader.SetModelMatrix(rotation);
            m_zCylinder.Render(gl, shader);

            m_vertexBuffer.Unbind(gl);

        }
    }
}
