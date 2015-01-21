using NCubeSolver.Plugins.Display.OpenGL.Models.Primitives;
using NCubeSolvers.Core;
using OpenTK;

namespace NCubeSolver.Plugins.Display.OpenGL.Models
{
    class MirrorCube : RubiksCube
    {
        public MirrorCube(int cubeSize)
            : base(cubeSize)
        {
        }

        public MirrorCube(CubeConfiguration<FaceColour> configuration)
            : base(configuration)
        {
        }

        public override void GenerateGeometry(SharpGL.OpenGL gl)
        {
            float halfSize = (Size + (Size - 1) * CubeSpacing) / 2f;
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            halfSize -= 0.5f;
            var centre = new Vector3(-halfSize);


            //HighlightCube.GenerateGeometry(gl);
            var currentPos = Vector3.Zero;
            var size = new Vector3(1);
            const float scalePerThing = 0.5f;   // TODO: CALCULATE THIS BASED ON CUBE SIZE

            for (int z = 0; z < Size; z++)
            {
                currentPos.Y = 0;
                for (int y = 0; y < Size; y++)
                {
                    currentPos.X = 0;
                    for (int x = 0; x < Size; x++)
                    {
                        var cube = new Cube(InsideColour, false);
                        ColourCubeFromConfiguration(cube, x, y, z);
                        cube.GenerateGeometry(gl);

                        size = new Vector3((x + 1) * scalePerThing, (1 + y) * scalePerThing, (1 + z) * scalePerThing);

                        var position = new Vector3(currentPos);

                        position += centre;  // Centre
                        string id = string.Format("{0},{1},{2}", x, y, z);

                        var cubie = new Cubie(id, cube, position, size);
                        AddToConfiguration(cubie, x, y, z);
                        AddAnimator(cubie);

                        currentPos.X += size.X + CubeSpacing;
                    }
                    currentPos.Y += size.Y + CubeSpacing;
                }
                currentPos.Z += size.Z + CubeSpacing;
            }

            CubeConfiguration.CheckValid();
        }
    }
}
