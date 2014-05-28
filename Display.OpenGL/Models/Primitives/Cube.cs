using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using NCubeSolvers.Core;
using SharpGL.VertexBuffers;

namespace NCubeSolver.Plugins.Display.OpenGL.Models.Primitives
{
    class Cube : IModel
    {
        private readonly Color m_defaultFaceColor = Colors.White;

        private readonly VertexBufferArray m_vertexBuffer = new VertexBufferArray();
        private readonly Dictionary<FaceType, Color> m_colours = new Dictionary<FaceType, Color>(6);
        private readonly bool m_centre;

        public Cube(Color? colour = null, bool centre = true)
        {
            if (colour.HasValue)
            {
                m_defaultFaceColor = colour.Value;
            }

            foreach (var face in Enum.GetValues(typeof (FaceType)).Cast<FaceType>())
            {
                m_colours.Add(face, m_defaultFaceColor);
            }
            m_centre = centre;
        }

        public void SetFaceColour(FaceType face, Color colour)
        {
            m_colours[face] = colour;
        }

        private float[] FaceColours
        {
            get
            {
                return m_colours.Values.SelectMany(GenerateQuadFaceColours).ToArray();
            }
        }

        private static IEnumerable<float> GenerateQuadFaceColours(Color colour)
        {
            const int colourLength = 3;
            const int arraySize = 4 * colourLength;
            var colourList = new float[arraySize];
            for (int i = 0; i < arraySize; i += colourLength)
            {
                colourList[i] = colour.ScR;
                colourList[i + 1] = colour.ScG;
                colourList[i + 2] = colour.ScB;
            }
            return colourList;
        }

        private static readonly float[] Vertices =
        {
            0,0,0,      0,1,0,      1,1,0,      1,0,0,
            0,0,0,      0,0,1,      0,1,1,      0,1,0,
            0,0,1,      1,0,1,      1,1,1,      0,1,1,
            1,0,1,      1,0,0,      1,1,0,      1,1,1,
            0,1,0,      0,1,1,      1,1,1,      1,1,0,
            0,0,0,      1,0,0,      1,0,1,      0,0,1
        };


        private static readonly float[] CentreVertices =
        {
            -0.5f,-0.5f,-0.5f,      -0.5f,0.5f,-0.5f,      0.5f,0.5f,-0.5f,      0.5f,-0.5f,-0.5f,
            -0.5f,-0.5f,-0.5f,      -0.5f,-0.5f,0.5f,      -0.5f,0.5f,0.5f,      -0.5f,0.5f,-0.5f,
            -0.5f,-0.5f,0.5f,      0.5f,-0.5f,0.5f,      0.5f,0.5f,0.5f,      -0.5f,0.5f,0.5f,
            0.5f,-0.5f,0.5f,      0.5f,-0.5f,-0.5f,      0.5f,0.5f,-0.5f,      0.5f,0.5f,0.5f,
            -0.5f,0.5f,-0.5f,      -0.5f,0.5f,0.5f,      0.5f,0.5f,0.5f,      0.5f,0.5f,-0.5f,
            -0.5f,-0.5f,-0.5f,      0.5f,-0.5f,-0.5f,      0.5f,-0.5f,0.5f,      -0.5f,-0.5f,0.5f
        };

        public void GenerateGeometry(SharpGL.OpenGL gl)
        {
            m_vertexBuffer.Create(gl);
            m_vertexBuffer.Bind(gl);

            GenerateVertexBuffer(gl);
            GenerateColourBuffer(gl);

            m_vertexBuffer.Unbind(gl);
        }

        private void GenerateVertexBuffer(SharpGL.OpenGL gl)
        {
            var vertexDataBuffer = new VertexBuffer();
            vertexDataBuffer.Create(gl);
            vertexDataBuffer.Bind(gl);

            var vertices = m_centre ? CentreVertices : Vertices;
            // TODO: CONST SOMEWHERE
            vertexDataBuffer.SetData(gl, 0, vertices, false, 3);
        }

        private void GenerateColourBuffer(SharpGL.OpenGL gl)
        {
            var colourDataBuffer = new VertexBuffer();
            colourDataBuffer.Create(gl);
            colourDataBuffer.Bind(gl);
            colourDataBuffer.SetData(gl, 1, FaceColours, false, 3);
        }

        public void Render(SharpGL.OpenGL gl, ShaderWrapper shader)
        {
            m_vertexBuffer.Bind(gl);

            gl.DrawArrays(SharpGL.OpenGL.GL_QUADS, 0, Vertices.Length);

            m_vertexBuffer.Unbind(gl);
        }
    }
}
