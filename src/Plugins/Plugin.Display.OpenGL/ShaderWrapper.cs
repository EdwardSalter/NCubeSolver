using NCubeSolver.Plugins.Display.OpenGL.Extensions;
using OpenTK;
using SharpGL.Shaders;

namespace NCubeSolver.Plugins.Display.OpenGL
{
    public class ShaderWrapper
    {
        private readonly ShaderProgram m_shaderProgram;
        private readonly SharpGL.OpenGL m_gl;

        public ShaderWrapper(ShaderProgram shaderProgram, SharpGL.OpenGL gl)
        {
            m_gl = gl;
            m_shaderProgram = shaderProgram;
        }

        public void Bind()
        {
            m_shaderProgram.Bind(m_gl);
        }

        public void Unbind()
        {
            m_shaderProgram.Unbind(m_gl);
        }

        public void SetModelMatrix(Matrix4 matrix)
        {
            SetUniformMatrix("modelMatrix", matrix);
        }

        public void SetProjectionMatrix(Matrix4 matrix)
        {
            SetUniformMatrix("projectionMatrix", matrix);
        }

        public void SetViewMatrix(Matrix4 matrix)
        {
            SetUniformMatrix("viewMatrix", matrix);
        }

        private void SetUniformMatrix(string key, Matrix4 matrix)
        {
            m_shaderProgram.SetUniformMatrix4(m_gl, key, matrix.ToArray());
        }
    }
}
