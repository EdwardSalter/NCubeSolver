using System;
using System.Collections.Generic;
using System.Windows.Media;
using NCubeSolvers.Core.Extensions;
using SharpGL.VertexBuffers;

namespace NCubeSolver.Plugins.Display.OpenGL.Models.Primitives
{
    class Cylinder : IModel
    {
        private readonly VertexBufferArray m_vertexBuffer = new VertexBufferArray();


        private static readonly List<float> Vertices = new List<float>();
        private readonly float m_radius;
        private readonly int m_slices;
        private readonly float m_length;
        private readonly Color m_colour = Colors.White;

        private float[] Colours
        {
            get
            {
                var colours = new List<float>();
                int vertexCount = Vertices.Count/3;
                for (int i = 0; i < vertexCount; i++)
                {
                    colours.Add(m_colour.ScR);
                    colours.Add(m_colour.ScG);
                    colours.Add(m_colour.ScB);
                }
                return colours.ToArray();
            }
        }

        public Cylinder(float radius, int slices, float length, Color colour)
        {
            m_radius = radius;
            m_slices = slices;
            m_length = length;
            m_colour = colour;
        }

        private void GenerateVertices(float radius, int slices)
        {
            float anglePerSlice = 2 * (float)Math.PI / slices;
            for (int i = 0; i < slices; i++)
            {
                float angle = anglePerSlice * i;
                float nextAngle = anglePerSlice * (i + 1);
                Vertices.Add(radius * MathEx.Cos(angle));      //x1
                Vertices.Add(radius * MathEx.Sin(angle));      //y1
                Vertices.Add(0);                                    //z1

                Vertices.Add(radius * MathEx.Cos(nextAngle));  //x2
                Vertices.Add(radius * MathEx.Sin(nextAngle));  //y2
                Vertices.Add(0);                                    //z2


                Vertices.Add(radius * MathEx.Cos(nextAngle));  //x3
                Vertices.Add(radius * MathEx.Sin(nextAngle));  //y3
                Vertices.Add(m_length);                             //z3

                Vertices.Add(radius * MathEx.Cos(angle));      //x4
                Vertices.Add(radius * MathEx.Sin(angle));      //y4
                Vertices.Add(m_length);                             //z4
            }
        }

        public void GenerateGeometry(SharpGL.OpenGL gl)
        {
            m_vertexBuffer.Create(gl);
            m_vertexBuffer.Bind(gl);

            GenerateVertexBuffer(gl);
            GenerateColourBuffer(gl);

            m_vertexBuffer.Unbind(gl);
        }

        private void GenerateColourBuffer(SharpGL.OpenGL gl)
        {
            var vertexDataBuffer = new VertexBuffer();
            vertexDataBuffer.Create(gl);
            vertexDataBuffer.Bind(gl);
            vertexDataBuffer.SetData(gl, 1, Colours, false, 3);
        }

        private void GenerateVertexBuffer(SharpGL.OpenGL gl)
        {
            GenerateVertices(m_radius, m_slices);
            var vertexDataBuffer = new VertexBuffer();
            vertexDataBuffer.Create(gl);
            vertexDataBuffer.Bind(gl);
            vertexDataBuffer.SetData(gl, 0, Vertices.ToArray(), false, 3);
        }


        public void Render(SharpGL.OpenGL gl, ShaderWrapper shader)
        {
            m_vertexBuffer.Bind(gl);

            gl.DrawArrays(SharpGL.OpenGL.GL_QUADS, 0, Vertices.Count);

            m_vertexBuffer.Unbind(gl);

        }
    }
}
