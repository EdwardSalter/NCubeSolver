namespace Display.OpenGL.Models
{
    internal interface IModel
    {
        void GenerateGeometry(SharpGL.OpenGL gl);
        void Render(SharpGL.OpenGL gl, ShaderWrapper shader);
    }
}