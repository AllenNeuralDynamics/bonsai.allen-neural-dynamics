using Bonsai;
using System;
using Bonsai.Vision.Design;
using OpenTK.Graphics.OpenGL;
using OpenCV.Net;
using AllenNeuralDynamics.Core.Design;

[assembly: TypeVisualizer(typeof(IplImageSaturationVisualizer), Target = typeof(IplImage))]

namespace AllenNeuralDynamics.Core.Design
{

    public class IplImageSaturationVisualizer : IplImageVisualizer
    {
        int _shaderProgram;
        int _textureLocation;

        public override void Load(IServiceProvider provider)
        {
            base.Load(provider);
            VisualizerCanvas.Load += (sender, e) =>
            {
                GL.Enable(EnableCap.Blend);
                GL.Enable(EnableCap.PointSmooth);
                GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

                _shaderProgram = CompileShader(vertexShaderCode, fragShaderCode);
                _textureLocation = GL.GetUniformLocation(_shaderProgram, "texture");
            };
        }

        protected override void RenderFrame()
        {

            GL.PushMatrix();
            GL.UseProgram(_shaderProgram);

            base.RenderFrame();

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, 1);

            GL.Uniform1(_textureLocation, 0);

            GL.PopMatrix();
            GL.UseProgram(0);

        }

        int CompileShader(string vertexCode, string fragmentCode)
        {
            // Compile vertex shader
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexCode);
            GL.CompileShader(vertexShader);
            GL.GetShader(vertexShader, ShaderParameter.CompileStatus, out int vertexCompiled);

            if (vertexCompiled == (int)All.False)
            {
                string vertexLog = GL.GetShaderInfoLog(vertexShader);
                Console.WriteLine($"Vertex shader compilation failed:\n{vertexLog}");
                GL.DeleteShader(vertexShader);
                return -1;
            }

            // Compile fragment shader
            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentCode);
            GL.CompileShader(fragmentShader);
            GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out int fragmentCompiled);

            if (fragmentCompiled == (int)All.False)
            {
                string fragmentLog = GL.GetShaderInfoLog(fragmentShader);
                Console.WriteLine($"Fragment shader compilation failed:\n{fragmentLog}");
                GL.DeleteShader(vertexShader);
                GL.DeleteShader(fragmentShader);
                return -1;
            }

            // Link shaders to program
            int shaderProgram = GL.CreateProgram();
            GL.AttachShader(shaderProgram, vertexShader);
            GL.AttachShader(shaderProgram, fragmentShader);
            GL.LinkProgram(shaderProgram);
            GL.GetProgram(shaderProgram, GetProgramParameterName.LinkStatus, out int programLinked);

            if (programLinked == (int)All.False)
            {
                string programLog = GL.GetProgramInfoLog(shaderProgram);
                Console.WriteLine($"Shader program linking failed:\n{programLog}");
                GL.DeleteShader(vertexShader);
                GL.DeleteShader(fragmentShader);
                GL.DeleteProgram(shaderProgram);
                return -1;
            }

            // Clean up shaders
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            return shaderProgram;
        }
        const string fragShaderCode2 = @"
#version 330 core

in vec2 TexCoords;
uniform sampler2D imageTexture;

out vec4 FragColor;

void main()
{
    FragColor = vec4(TexCoords.x, TexCoords.y, 0, 1.0);
}
";
        const string fragShaderCode = @"
#version 330 core

in vec2 TexCoords;
uniform sampler2D imageTexture;

out vec4 FragColor;

void main()
{
    vec4 color = texture(imageTexture, TexCoords);
	
    if (color.r == 1.0){
        FragColor = vec4(1.0, 0.0, 0.0, 1.0);
    }
    else if (color.r <= 1.0 / 255){
        FragColor = vec4(0, 0.0, 1.0, 1.0);
    }
    else{
        FragColor = color;
    }
}
";
        const string vertexShaderCode = @"#version 330 core
layout(location = 0) in vec2 vertexPosition;
layout(location = 1) in vec2 vertexTexCoords;

out vec2 TexCoords;

void main()
{
    TexCoords = vec2(.5, -.5) * (vertexPosition.xy + 1);
    gl_Position = vec4(vertexPosition.xy, 0.0, 1.0);
}
";
    }
}
