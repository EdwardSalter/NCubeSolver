using NCubeSolver.Plugins.Display.OpenGL.Extensions;
using NCubeSolver.Plugins.Display.OpenGL.Models.Primitives;
using OpenTK;

namespace NCubeSolver.Plugins.Display.OpenGL.Models
{
    class Cubie
    {
        private readonly Vector3? m_scale;
        public string Id { get; set; }
        public bool Highlighted { get; set; }
        public Cube Cube { get; private set; }
        public Vector3 Position { get; set; }
        public Quaternion ModelRotation { get; set; }
        public Quaternion WorldRotation { get; set; }

        public Cubie(string id, Cube cube, Vector3 position, Vector3? scale = null)
        {
            m_scale = scale;
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

            // Scaling
            if (m_scale.HasValue)
            {
                modelMatrix *= Matrix4.CreateScale(m_scale.Value);
            }

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
