﻿using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using NCubeSolver.Plugins.Display.OpenGL.Animation;
using NCubeSolver.Plugins.Display.OpenGL.Models.Primitives;
using NCubeSolvers.Core;
using OpenTK;

namespace NCubeSolver.Plugins.Display.OpenGL.Models
{
    internal class RubiksCube : IModel
    {
        private const float CubeSpacing = 0.05f;
        private static readonly Cube HighlightCube = new Cube(Colors.Pink);

        private readonly List<CubieAnimator> m_animators = new List<CubieAnimator>();
        private readonly Vector3 m_cubieCentre;
        private readonly CubeConfiguration<FaceColour> m_initialConfiguration;
        private readonly Dictionary<FaceColour, Color> m_faceColours = new Dictionary<FaceColour, Color>
        {
            {FaceColour.Yellow, Colors.Yellow},
            {FaceColour.Red, Colors.Red},
            {FaceColour.White, Colors.White},
            {FaceColour.Green, Colors.Green},
            {FaceColour.Blue, Colors.Blue},
            {FaceColour.Orange, Colors.Orange}
        };

        public CubeConfiguration<Cubie> CubeConfiguration { get; private set; }
        public int Size { get; private set; }

        public RubiksCube(int cubeSize)
        {
            Size = cubeSize;
            CubeConfiguration = new CubeConfiguration<Cubie>(cubeSize);

            float halfSize = (Size + (Size - 1) * CubeSpacing) / 2f;
            halfSize -= 0.5f;
            m_cubieCentre = new Vector3(-halfSize);
        }

        public RubiksCube(CubeConfiguration<FaceColour> configuration)
            : this(configuration.Size)
        {
            m_initialConfiguration = configuration;
        }

        public void GenerateGeometry(SharpGL.OpenGL gl)
        {
            HighlightCube.GenerateGeometry(gl);

            const float spacing = 1 + CubeSpacing;

            for (int z = 0; z < Size; z++)
            {
                for (int y = 0; y < Size; y++)
                {
                    for (int x = 0; x < Size; x++)
                    {
                        var cube = new Cube(Colors.Black);
                        ColourCubeFromConfiguration(cube, x, y, z);
                        cube.GenerateGeometry(gl);

                        var position = new Vector3(x * spacing, y * spacing, z * spacing);
                        position += m_cubieCentre;  // Centre
                        string id = string.Format("{0},{1},{2}", x - 1, y - 1, z - 1);
                        var cubie = new Cubie(id, cube, position);
                        AddToConfiguration(cubie, x, y, z);
                        m_animators.Add(new CubieAnimator(cubie));
                    }
                }
            }

            CubeConfiguration.CheckValid();
        }

        private void AddToConfiguration(Cubie cubie, int x, int y, int z)
        {
            var lastIndex = Size - 1;
            var debugString = new StringBuilder();

            if (x == 0)
            {
                // TODO: BETTER WAY OF CREATING FACE SO EXTERNAL DOES NOT CARE ABOUT INDEX ORDER
                CubeConfiguration.Faces[FaceType.Left].Items[lastIndex - y, z] = cubie;
                debugString.Append("L");
            }
            if (x == lastIndex)
            {
                CubeConfiguration.Faces[FaceType.Right].Items[lastIndex - y, lastIndex - z] = cubie;
                debugString.Append("R");
            }
            if (y == 0)
            {
                CubeConfiguration.Faces[FaceType.Down].Items[lastIndex - z, x] = cubie;
                debugString.Append("D");
            }
            if (y == lastIndex)
            {
                CubeConfiguration.Faces[FaceType.Upper].Items[z, x] = cubie;
                debugString.Append("U");
            }
            if (z == 0)
            {
                CubeConfiguration.Faces[FaceType.Back].Items[lastIndex - y, lastIndex - x] = cubie;
                debugString.Append("B");
            }
            if (z == lastIndex)
            {
                CubeConfiguration.Faces[FaceType.Front].Items[lastIndex - y, x] = cubie;
                debugString.Append("F");
            }

            //Console.WriteLine(debugString.ToString());
        }

        public void Render(SharpGL.OpenGL gl, ShaderWrapper shader)
        {
            foreach (var animator in m_animators)
            {
                var cubie = animator.Cubie;
                shader.SetModelMatrix(cubie.GenerateModelMatrix());

                var cube = cubie.Highlighted ? HighlightCube : cubie.Cube;
                cube.Render(gl, shader);
            }
        }

        private void ColourCubeFromConfiguration(Cube cube, int x, int y, int z)
        {
            var config = m_initialConfiguration ?? CubeConfiguration<FaceColour>.CreateStandardCubeConfiguration(Size);

            var lastIndex = Size - 1;

            if (x == 0)
            {
                // TODO: BETTER WAY OF CREATING FACE SO EXTERNAL DOES NOT CARE ABOUT INDEX ORDER
                var colour = config.Faces[FaceType.Left].Items[lastIndex - y, z];
                cube.SetFaceColour(FaceType.Left, m_faceColours[colour]);
            }
            if (x == lastIndex)
            {
                var colour = config.Faces[FaceType.Right].Items[lastIndex - y, lastIndex - z];
                cube.SetFaceColour(FaceType.Right, m_faceColours[colour]);
            }
            if (y == 0)
            {
                var colour = config.Faces[FaceType.Down].Items[lastIndex - z, x];
                cube.SetFaceColour(FaceType.Down, m_faceColours[colour]);
            }
            if (y == lastIndex)
            {
                var colour = config.Faces[FaceType.Upper].Items[z, x];
                cube.SetFaceColour(FaceType.Upper, m_faceColours[colour]);
            }
            if (z == 0)
            {
                var colour = config.Faces[FaceType.Back].Items[lastIndex - y, lastIndex - x];
                cube.SetFaceColour(FaceType.Back, m_faceColours[colour]);
            }
            if (z == lastIndex)
            {
                var colour = config.Faces[FaceType.Front].Items[lastIndex - y, x];
                cube.SetFaceColour(FaceType.Front, m_faceColours[colour]);
            }
        }
    }
}
