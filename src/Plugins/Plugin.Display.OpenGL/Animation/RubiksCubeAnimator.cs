using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NCubeSolver.Plugins.Display.OpenGL.Models;
using NCubeSolvers.Core;
using NCubeSolvers.Core.Extensions;
using OpenTK;

namespace NCubeSolver.Plugins.Display.OpenGL.Animation
{
    class RubiksCubeAnimator : IRotatable
    {
        private const int AnimationSpeedChange = 2;

        private int m_animationLength;
        private readonly RubiksCube m_rubiksCube;
        private List<CubieAnimator> m_animators;
        private bool m_highlightAnimating;
        private IRotation m_currentRotation;
        private TaskCompletionSource<object> m_rotationTask;
        private int m_timesRun;
        private readonly IEnumerable<IRotation> m_initialRotations;

        public bool AnimationEnabled { get; set; }

        public bool HighlightAnimating
        {
            get { return m_highlightAnimating; }
            set
            {
                if (m_highlightAnimating == value) return;
                m_highlightAnimating = value;

                foreach (var animator in CurrentlyRotatingAnimators)
                {
                    animator.Cubie.Highlighted = m_highlightAnimating;
                }
            }
        }

        private IEnumerable<CubieAnimator> CurrentlyRotatingAnimators
        {
            get
            {
                var faceRotation = m_currentRotation as FaceRotation;
                var cubeRotation = m_currentRotation as CubeRotation;

                if (faceRotation != null)
                {
                    return GetAnimatorsForFace(faceRotation.Face);
                }
                if (cubeRotation != null)
                {
                    return m_animators;
                }

                return new List<CubieAnimator>();
            }
        }

        private bool AnimationRunning
        {
            get { return CurrentlyRotatingAnimators.Any(animator => animator.IsRunning); }
        }

        public int AnimationLength
        {
            get { return m_animationLength; }
            set
            {
                m_animationLength = value;
                AnimationEnabled = m_animationLength > 0;
            }
        }

        public RubiksCubeAnimator(RubiksCube cube, int animationLength, IEnumerable<IRotation> initialRotations = null)
        {
            m_initialRotations = initialRotations;
            m_rubiksCube = cube;
            AnimationLength = animationLength;
        }

        private void RunInitialRotations(IEnumerable<IRotation> rotations)
        {
            var tempLength = m_animationLength;
            m_animationLength = 1;
            Task t = null;
            foreach (var rotation in rotations)
            {
                var faceRotation = rotation as FaceRotation;
                var cubeRotation = rotation as CubeRotation;
                if (faceRotation != null)
                {
                    t = Rotate(faceRotation);
                }
                else if (cubeRotation != null)
                {
                    t= RotateCube(cubeRotation);
                }

                while (AnimationRunning)
                {
                    NextFrame();
                }
                //t.Wait();
            }
            m_animationLength = tempLength;
        }

        public void Setup()
        {
            m_animators = new List<CubieAnimator>(m_rubiksCube.CubeConfiguration.AllItems.Count());
            foreach (var cubie in m_rubiksCube.CubeConfiguration.AllItems)
            {
                m_animators.Add(new CubieAnimator(cubie));
            }

            if (m_initialRotations != null)
            {
                RunInitialRotations(m_initialRotations);
            }
        }

        public void NextFrame()
        {
            if (!AnimationEnabled) return;

            foreach (var animator in CurrentlyRotatingAnimators)
            {
                animator.NextFrame();
            }

            if (!AnimationRunning)
            {
                AnimationFinished();
            }
        }

        private void AnimationFinished()
        {
            if (m_currentRotation == null) return;

            if (HighlightAnimating)
            {
                foreach (var animator in CurrentlyRotatingAnimators)
                {
                    animator.Cubie.Highlighted = false;
                }
            }

            if (++m_timesRun < m_currentRotation.Count)
            {
                // Need to go again
                SetNextRotation();
            }
            else
            {
                // Animation complete
                // TODO: LOOK INTO COMBINING ROTATE METHODS
                var faceRotation = m_currentRotation as FaceRotation;
                var cubeRotation = m_currentRotation as CubeRotation;
                if (faceRotation != null)
                    m_rubiksCube.CubeConfiguration.Rotate(faceRotation);
                if (cubeRotation != null)
                    m_rubiksCube.CubeConfiguration.RotateCube(cubeRotation);

                m_rotationTask.TrySetResult(null);
            }
        }

        public Task Rotate(FaceRotation rotation)
        {
            return StartRotating(rotation);
        }

        private Task StartRotating(IRotation rotation)
        {
            m_rotationTask = new TaskCompletionSource<object>();
            m_timesRun = 0;

            m_currentRotation = rotation;
            SetNextRotation();

            return m_rotationTask.Task;
        }

        private void SetNextRotation()
        {
            foreach (var animator in CurrentlyRotatingAnimators)
            {
                if (HighlightAnimating)
                {
                    animator.Cubie.Highlighted = true;
                }

                var faceRotation = m_currentRotation as FaceRotation;
                var cubeRotation = m_currentRotation as CubeRotation;
                Quaternion quaternion = faceRotation != null
                    ? GetRotation(faceRotation.Face, faceRotation.Direction)
                    : GetRotation(cubeRotation.Axis, cubeRotation.Direction);

                animator.RotateBy(quaternion, m_animationLength);
            }
        }

        public Task RotateCube(CubeRotation rotation)
        {
            return StartRotating(rotation);
        }

        private IEnumerable<CubieAnimator> GetAnimatorsForFace(FaceType faceType)
        {
            var face = m_rubiksCube.CubeConfiguration.Faces[faceType];
            var animators = m_animators.Where(animator => face.Contains(animator.Cubie)).ToList();
            if (animators.Count != (int)Math.Pow(m_rubiksCube.Size, 2))
            {
                throw new Exception("Invalid number of rotating cubes");
            }

            return animators;
        }

        private static Quaternion GetRotation(Axis axis, RotationDirection direction)
        {
            var multiplier = direction == RotationDirection.Clockwise ? 1 : -1;
            float angle = MathEx.DegToRad(90) * multiplier;
            Matrix4 rotationMatrix;

            switch (axis)
            {
                case Axis.X:
                    rotationMatrix = Matrix4.CreateRotationX(angle);
                    break;

                case Axis.Y:
                    rotationMatrix = Matrix4.CreateRotationY(angle);
                    break;

                case Axis.Z:
                    rotationMatrix = Matrix4.CreateRotationZ(angle);
                    break;

                default:
                    throw new Exception("Invalid axis type");
            }

            return rotationMatrix.ExtractRotation();
        }

        private static Quaternion GetRotation(FaceType face, RotationDirection direction)
        {
            float angle = MathEx.DegToRad(90);
            Matrix4 rotationMatrix;

            switch (face)
            {
                case FaceType.Upper:
                    rotationMatrix = Matrix4.CreateRotationY(angle);
                    break;

                case FaceType.Down:
                    rotationMatrix = Matrix4.CreateRotationY(-angle);
                    break;

                case FaceType.Right:
                    rotationMatrix = Matrix4.CreateRotationX(angle);
                    break;

                case FaceType.Left:
                    rotationMatrix = Matrix4.CreateRotationX(-angle);
                    break;

                case FaceType.Front:
                    rotationMatrix = Matrix4.CreateRotationZ(angle);
                    break;

                case FaceType.Back:
                    rotationMatrix = Matrix4.CreateRotationZ(-angle);
                    break;

                default:
                    throw new Exception("Invalid face type");
            }

            var rotation = rotationMatrix.ExtractRotation();
            if (direction == RotationDirection.AntiClockwise)
            {
                rotation.Conjugate();
            }
            return rotation;
        }

        public void SpeedUp()
        {
            m_animationLength -= AnimationSpeedChange;
            if (m_animationLength < AnimationSpeedChange)
            {
                m_animationLength = AnimationSpeedChange;
            }
        }

        public void SlowDown()
        {
            m_animationLength += AnimationSpeedChange;
        }
    }
}
