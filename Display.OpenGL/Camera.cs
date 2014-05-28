using Core.Extensions;
using OpenTK;

namespace NCubeSolver.Plugins.Display.OpenGL
{
    class Camera
    {
        public float VerticalAngle { get; private set; }
        public float HorizontalAngle { get; private set; }
        public float Zoom { get; private set; }
        public float ZoomSpeed { get; set; }
        public float AngleSpeed { get; private set; }

        public Vector3 Position
        {
            get
            {
                var sineTheta = MathEx.Sin(MathEx.DegToRad(VerticalAngle));
                var position = new Vector3
                {
                    X = Zoom * sineTheta * MathEx.Sin(MathEx.DegToRad(HorizontalAngle)),
                    Y = Zoom * MathEx.Cos(MathEx.DegToRad(VerticalAngle)),
                    Z = Zoom * sineTheta * MathEx.Cos(MathEx.DegToRad(HorizontalAngle)),
                };

                return position;
            }
        }

        public Camera()
        {
            Zoom = 8;
            ZoomSpeed = 0.015f;
            HorizontalAngle = 20;
            VerticalAngle = 65;
            AngleSpeed = 1;
        }


        public void IncreaseHorizontalAngle(float delta)
        {
            HorizontalAngle += delta * AngleSpeed;
        }


        public void IncreaseVerticalAngle(float delta)
        {
            VerticalAngle += delta * AngleSpeed;
        }

        public void ZoomIn(float delta)
        {
            Zoom -= delta * ZoomSpeed;
        }
    }
}
