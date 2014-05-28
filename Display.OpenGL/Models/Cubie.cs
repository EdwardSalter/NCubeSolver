using Display.OpenGL.Extensions;
using Display.OpenGL.Models.Primitives;
using OpenTK;

namespace Display.OpenGL.Models
{
    class Cubie
    {
        public string Id { get; set; }
        public bool Highlighted { get; set; }
        public Cube Cube { get; private set; }
        public Vector3 Position { get; set; }
        public Quaternion ModelRotation { get; set; }
        public Quaternion WorldRotation { get; set; }

        public Cubie(string id, Cube cube, Vector3 position)
        {
            Id = id;
            ModelRotation = Quaternion.Identity;
            WorldRotation = Quaternion.Identity;
            Cube = cube;
            Position = position;
        }

        public override string ToString()
        {
            return Id;
        }

        public Matrix4 GenerateModelMatrix()
        {
            var modelMatrix = Matrix4.Identity;

            // Model rotation
            //TODO: MOVE CONSTANTS
            //modelMatrix *= Matrix4.CreateTranslation(new Vector3(-0.5f));
            modelMatrix *= ModelRotation.ToRotationMatrix();
            //modelMatrix *= Matrix4.CreateTranslation(new Vector3(0.5f));

            // World translation
            modelMatrix *= Matrix4.CreateTranslation(Position);

            // World rotation
            modelMatrix *= WorldRotation.ToRotationMatrix();

            return modelMatrix;
        }
    }
}
