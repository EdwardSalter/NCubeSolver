using System.Threading.Tasks;
using NCubeSolver.Plugins.Display.OpenGL.Animation;
using NCubeSolver.Plugins.Display.OpenGL.Models;
using NCubeSolvers.Core;

namespace NCubeSolver.Plugins.Display.OpenGL
{
    public class Scene : IModel
    {
        private const int DefaultCubeSize = 3;
        private const int DefaultAnimationLength = 30;
        
        private readonly Axes m_axes = new Axes();
        internal RubiksCube RubiksCube = new RubiksCube(DefaultCubeSize);
        private RubiksCubeAnimator m_cubeAnimator;
        private bool m_regenerateRubiksCube;
        private TaskCompletionSource<object> m_cubeResetTask;

        public int AnimationLength
        {
            get { return m_cubeAnimator.AnimationLength; }
            set { m_cubeAnimator.AnimationLength = value; }
        }

        public bool ShowAxes { get; set; }

        public Scene()
        {
            m_cubeAnimator = new RubiksCubeAnimator(RubiksCube, DefaultAnimationLength);
            ShowAxes = true;
        }

        public void GenerateGeometry(SharpGL.OpenGL gl)
        {
            // TODO: CREATE CORRECT GEOMETRY UPFRONT - MIROR CUBE IS NOT RIGHT
            m_axes.GenerateGeometry(gl);
            RubiksCube.GenerateGeometry(gl);
            m_cubeAnimator.Setup();
        }

        public void Render(SharpGL.OpenGL gl, ShaderWrapper shader)
        {
            if (ShowAxes)
                m_axes.Render(gl, shader);

            m_cubeAnimator.Animate();
            RubiksCube.Render(gl, shader);
        }

        public void ToggleAnimation()
        {
            m_cubeAnimator.AnimationEnabled = !m_cubeAnimator.AnimationEnabled;
        }

        public void ToggleAnimationHighlights()
        {
            m_cubeAnimator.HighlightAnimating = !m_cubeAnimator.HighlightAnimating;
        }

        public void RegenerateRubiksCube(SharpGL.OpenGL gl)
        {
            if (!m_regenerateRubiksCube) return;
            m_regenerateRubiksCube = false;

            RubiksCube.GenerateGeometry(gl);
            m_cubeAnimator.Setup();

            m_cubeResetTask.SetResult(null);
        }

        public Task SetCubeConfiguration(CubeConfiguration<FaceColour> configuration)
        {
            m_cubeResetTask = new TaskCompletionSource<object>();
            RubiksCube = new RubiksCube(configuration);
            m_cubeAnimator = new RubiksCubeAnimator(RubiksCube, AnimationLength);

            m_regenerateRubiksCube = true;

            return m_cubeResetTask.Task;
        }

        public Task Rotate(FaceRotation faceRotation)
        {
            return m_cubeAnimator.Rotate(faceRotation);
        }

        public Task RotateCube(CubeRotation rotation)
        {
            return m_cubeAnimator.RotateCube(rotation);
        }

        public void SpeedUpAnimation()
        {
            m_cubeAnimator.SpeedUp();
        }

        public void SlowDownAnimation()
        {
            m_cubeAnimator.SlowDown();
        }
    }
}
