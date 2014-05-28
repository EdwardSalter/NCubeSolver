using Display.OpenGL.Models;
using OpenTK;

namespace Display.OpenGL.Animation
{
    class CubieAnimator
    {
        public Cubie Cubie { get; private set; }
        private int m_currentFrame;
        private Quaternion m_originalRotation;
        private Quaternion m_newRotation;
        private int m_animationLength;

        public bool IsRunning { get; private set; }

        public CubieAnimator(Cubie cubie)
        {
            Cubie = cubie;
        }

        public void RotateBy(Quaternion rotation, int animationLengthInFrames)
        {
            IsRunning = true;
            m_animationLength = animationLengthInFrames;
            m_currentFrame = 0;
            m_originalRotation = Cubie.WorldRotation;
            m_newRotation = (rotation * Cubie.WorldRotation);
        }

        public void Animate()
        {
            if (!IsRunning) return;

            m_currentFrame++;

            float blend = (float)m_currentFrame / m_animationLength;
            Cubie.WorldRotation = Quaternion.Slerp(m_originalRotation, m_newRotation, blend);

            if (m_currentFrame >= m_animationLength)
            {
                AnimationComplete();
            }
        }

        private void AnimationComplete()
        {
            Cubie.Position = Cubie.GenerateModelMatrix().ExtractTranslation();
            Cubie.ModelRotation *= Cubie.WorldRotation;
            Cubie.WorldRotation = Quaternion.Identity;
            IsRunning = false;
        }
    }
}
