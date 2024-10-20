using Bonsai;
using System;
using Bonsai.Vision.Design;
using OpenTK.Graphics.OpenGL;
using OpenCV.Net;
using System.Windows.Forms;
using AllenNeuralDynamics.Core.Design;
using Bonsai.Design;

[assembly: TypeVisualizer(typeof(IplImageSaturationVisualizer), Target = typeof(IplImage))]

namespace AllenNeuralDynamics.Core.Design
{

    public class IplImageSaturationVisualizer : IplImageVisualizer
    {
        int shaderProgram;
        int textureLocation;
        int minSaturationLocation;
        int maxSaturationLocation;

        NumericUpDown minSaturationInput;
        NumericUpDown maxSaturationInput;
        public byte minSaturation { get; set; } = 0;
        public byte maxSaturation { get; set; } = 255;

        private byte? SanitizeInput(string value)
        {
            return byte.TryParse(value, out byte parsed) ? parsed : null;
        }

        private NumericUpDown NumericUpDownFactory(byte val, byte min = byte.MinValue, byte max = byte.MaxValue)
        {
            var numericUpDown = new NumericUpDown();
            numericUpDown.Maximum = max;
            numericUpDown.Minimum = min;
            numericUpDown.Value = val;
            return numericUpDown;
        }

        public override void Load(IServiceProvider provider)
        {
            base.Load(provider);

            minSaturationInput = NumericUpDownFactory(minSaturation);
            minSaturationInput.ValueChanged += (sender, e) =>
            {
                minSaturation = (byte)minSaturationInput.Value;
            };

            maxSaturationInput = NumericUpDownFactory(maxSaturation);
            maxSaturationInput.ValueChanged += (sender, e) =>
            {
                maxSaturation = (byte)maxSaturationInput.Value;
            };

            ToolStripControlHost minSaturationInputHost = new ToolStripControlHost(minSaturationInput);
            ToolStripControlHost maxSaturationInputHost = new ToolStripControlHost(maxSaturationInput);

            StatusStrip.Items.Add(new ToolStripLabel("Min:"));
            StatusStrip.Items.Add(minSaturationInputHost);
            StatusStrip.Items.Add(new ToolStripLabel("Max:"));
            StatusStrip.Items.Add(maxSaturationInputHost);

            VisualizerCanvas.Load += (sender, e) =>
            {
                GL.Enable(EnableCap.Blend);
                GL.Enable(EnableCap.PointSmooth);
                GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

                shaderProgram = CompileShader(vertexShaderCode, fragShaderCode);
                textureLocation = GL.GetUniformLocation(shaderProgram, "texture");
                minSaturationLocation = GL.GetUniformLocation(shaderProgram, "minSaturation");
                maxSaturationLocation = GL.GetUniformLocation(shaderProgram, "maxSaturation");
            };
        }

        protected override void RenderFrame()
        {

            GL.PushMatrix();
            GL.UseProgram(shaderProgram);

            base.RenderFrame();

            GL.ActiveTexture(TextureUnit.Texture0);
            GL.BindTexture(TextureTarget.Texture2D, 1);

            GL.Uniform1(textureLocation, 0);
            GL.Uniform1(minSaturationLocation, (float) minSaturation / byte.MaxValue);
            GL.Uniform1(maxSaturationLocation, (float) maxSaturation / byte.MaxValue);

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
uniform float minSaturation = 1.0;
uniform float maxSaturation = 0.0;

out vec4 FragColor;

void main()
{
    vec4 color = texture(imageTexture, TexCoords);
	
    if (color.r >= maxSaturation){
        FragColor = vec4(1.0, 0.0, 0.0, 1.0);
    }
    else if (color.r <= minSaturation){
        FragColor = vec4(0.0, 0.0, 1.0, 1.0);
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
