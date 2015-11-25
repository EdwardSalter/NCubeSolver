using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using NCubeSolvers.Core;
using NCubeSolvers.Core.Extensions;
using NCubeSolvers.Core.Plugins;
using OpenTK;
using SharpGL.Enumerations;
using SharpGL.SceneGraph;
using SharpGL.Shaders;

namespace NCubeSolver.Plugins.Display.OpenGL
{
    /// <summary>
    /// Interaction logic for Display.xaml
    /// </summary>
    public partial class DisplayControl : IDisplay
    {
        public DisplayControl()
        {
            InitializeComponent();

            if (!IsInDesignMode)
            {
                GlControl.DrawFPS = true;
                GlControl.OpenGLDraw += OpenGLControl_OpenGLDraw;
                GlControl.OpenGLInitialized += OpenGLControl_OpenGLInitialized;
                GlControl.Resized += OpenGlControl_OnResized;

                Loaded += OnLoaded;
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            Window parentWindow = Window.GetWindow(this);
            // Hack: Assumes that the control is taking up the entire window
            parentWindow.MouseDown += OpenGLControl_OnMouseDown;
            parentWindow.MouseUp += OpenGLControl_OnMouseUp;
            parentWindow.MouseWheel += OpenGLControl_OnMouseWheel;
            parentWindow.MouseMove += OpenGLControl_OnMouseMove;
        }

        private Point m_dragOrigin;
        private bool m_dragging;
        private Matrix4 m_projectionMatrix;
        private Matrix4 m_viewMatrix;
        private ShaderWrapper m_shader;
        private ShaderProgram m_shaderProgram;
        public readonly Scene Scene = new Scene();
        private readonly Camera m_camera = new Camera();
        private bool m_firstRun = true;
        private readonly Queue<string> m_displayText = new Queue<string>(DisplayTextCapacity);

        // TODO: MOVE THESE
        //  Constants that specify the attribute indexes.
        const uint AttributeIndexPosition = 0;
        const uint AttributeIndexColour = 1;
        private const int DisplayTextCapacity = 100;

        public bool IsInDesignMode
        {
            get { return System.ComponentModel.DesignerProperties.GetIsInDesignMode(this); }
        }

        public bool ShowFPS
        {
            get { return GlControl.DrawFPS; }
            set { GlControl.DrawFPS = value; }
        }

        /// <summary>
        /// The DrawFPS property.
        /// </summary>
        private static readonly DependencyProperty ShowConsoleTextProperty =
          DependencyProperty.Register("ShowConsoleText", typeof(bool), typeof(DisplayControl),
          new PropertyMetadata(false, null));

        /// <summary>
        /// Gets or sets a value indicating whether to draw FPS.
        /// </summary>
        /// <value>
        ///   <c>true</c> if draw FPS; otherwise, <c>false</c>.
        /// </value>
        public bool ShowConsoleText
        {
            get { return (bool)GetValue(ShowConsoleTextProperty); }
            set { SetValue(ShowConsoleTextProperty, value); }
        }

        private void OpenGLControl_OpenGLDraw(object sender, OpenGLEventArgs args)
        {
            SharpGL.OpenGL gl = args.OpenGL;

            // Clear The Screen And The Depth Buffer
            gl.Clear(SharpGL.OpenGL.GL_COLOR_BUFFER_BIT | SharpGL.OpenGL.GL_DEPTH_BUFFER_BIT);

            if (ShowConsoleText)
            {
                RenderText(gl);
            }
                
            //  Bind the shader, set the matrices.
            m_shader.Bind();
            m_shader.SetProjectionMatrix(m_projectionMatrix);
            m_shader.SetViewMatrix(m_viewMatrix);
            m_shader.SetModelMatrix(Matrix4.Identity);

            Scene.RegenerateRubiksCube(gl);
            Scene.Render(gl, m_shader);

            m_shader.Unbind();

            // Bug: This is not actually fixing the bug where the cube rotates apart
            if (m_firstRun)
            {
                m_firstRun = false;
                if (Ready != null)
                {
                    Ready.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void RenderText(SharpGL.OpenGL gl)
        {
            var numFit = ((int)ActualHeight - 30) / 15;

            int y = (int)ActualHeight - 15;
            var textToDisplay = m_displayText.Reverse().Take(numFit).Reverse();
            foreach (var text in textToDisplay)
            {
                gl.DrawText(5, y, 1.0f, 1.0f, 1.0f, "Consolas", 12.0f, text);
                y -= 15;
            }
        }

        private void OpenGLControl_OpenGLInitialized(object sender, OpenGLEventArgs args)
        {
            SharpGL.OpenGL gl = args.OpenGL;

            gl.Enable(SharpGL.OpenGL.GL_CULL_FACE);
            gl.Enable(SharpGL.OpenGL.GL_DEPTH_TEST);
            gl.DepthFunc(DepthFunction.LessThanOrEqual);

            CreateShader(gl);
            UpdateViewMatrix();

            Scene.GenerateGeometry(gl);
        }

        private void CreateProjectionMatrix(float screenWidth, float screenHeight)
        {
            m_projectionMatrix = Matrix4.CreatePerspectiveFieldOfView(MathEx.DegToRad(60), screenWidth / screenHeight, 0.1f, 100.0f);
        }

        private void UpdateViewMatrix()
        {
            m_viewMatrix = Matrix4.LookAt(m_camera.Position, Vector3.Zero, Vector3.UnitY);
        }

        private void CreateShader(SharpGL.OpenGL gl)
        {
            //  Create the shader program.
            var vertexShaderSource = ManifestResourceLoader.LoadTextFile("Shader.vert");
            var fragmentShaderSource = ManifestResourceLoader.LoadTextFile("Shader.frag");
            m_shaderProgram = new ShaderProgram();
            m_shaderProgram.Create(gl, vertexShaderSource, fragmentShaderSource, null);
            m_shaderProgram.BindAttributeLocation(gl, AttributeIndexPosition, "in_Position");
            m_shaderProgram.BindAttributeLocation(gl, AttributeIndexColour, "in_Color");
            m_shaderProgram.AssertValid(gl);

            m_shader = new ShaderWrapper(m_shaderProgram, gl);
        }

        private void OpenGLControl_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            switch (e.ChangedButton)
            {
                case MouseButton.Left:
                    m_dragOrigin = e.GetPosition(this);
                    m_dragging = true;
                    break;
            }
        }

        private void OpenGLControl_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (m_dragging)
            {
                var currentPos = e.GetPosition(this);
                var moved = currentPos - m_dragOrigin;

                m_camera.IncreaseHorizontalAngle(-(float)moved.X);
                m_camera.IncreaseVerticalAngle((float)moved.Y);
                m_dragOrigin = currentPos;

                UpdateViewMatrix();
            }
        }

        private void OpenGLControl_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                m_dragging = false;
        }

        private void OpenGLControl_OnMouseWheel(object sender, MouseWheelEventArgs e)
        {
            m_camera.ZoomIn(e.Delta);
            UpdateViewMatrix();
        }

        private void OpenGlControl_OnResized(object sender, OpenGLEventArgs args)
        {
            CreateProjectionMatrix((float)GlControl.ActualWidth, (float)GlControl.ActualHeight);
        }

        public void ToggleConsoleText()
        {
            ShowConsoleText = !ShowConsoleText;
        }

        #region IDisplay
        public Task Rotate(FaceRotation rotation)
        {
            return Scene.Rotate(rotation);
        }

        public Task RotateCube(CubeRotation rotation)
        {
            return Scene.RotateCube(rotation);
        }

        public event EventHandler Closed;
        public Task Initialise()
        {
            return TaskEx.Completed;
        }

        public Task SetCubeConfiguration(CubeConfiguration<FaceColour> configuration)
        {
            m_camera.SetZoomForConfigurationSize(configuration.Size);
            UpdateViewMatrix();
            return Scene.SetCubeConfiguration(configuration);
        }

        public void SetCancellation(CancellationTokenSource cancellationToken)
        {
            Scene.CancellationTokenSource = cancellationToken;
        }

        public void WriteText(string text)
        {
            if (m_displayText.Count >= DisplayTextCapacity)
            {
                m_displayText.Dequeue();
            }
            m_displayText.Enqueue(text);
        }

        public string PluginName
        {
            get { return "OpenGL"; }
        }
        #endregion

        public event EventHandler Ready;
    }
}
