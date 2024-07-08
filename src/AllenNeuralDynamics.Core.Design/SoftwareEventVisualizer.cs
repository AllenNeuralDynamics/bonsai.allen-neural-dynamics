using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Bonsai.Design;
using System.Drawing;
using System.Reactive;
using System.Text.RegularExpressions;
using System.Linq;
using Bonsai;
using AllenNeuralDynamics.Core.Design;
using AllenNeuralDynamics.AindBehaviorServices.DataTypes;

[assembly: TypeVisualizer(typeof(SoftwareEventVisualizer), Target = typeof(SoftwareEvent))]

namespace AllenNeuralDynamics.Core.Design
{
    public class SoftwareEventVisualizer : BufferedVisualizer
    {
        const int AutoScaleHeight = 13;
        const float DefaultDpi = 96f;

        SoftwareEvent softwareEvent;
        StatusStrip statusStrip;
        ToolStripTextBox filterTextBox;
        RichTextBox textBox;
        UserControl textPanel;
        Queue<string> buffer;
        int bufferSize;
        string[] filter;

        protected override void ShowBuffer(IList<Timestamped<object>> values)
        {
            if (values.Count > 0)
            {
                base.ShowBuffer(values);
                textBox.Text = string.Join(Environment.NewLine, buffer);
                textPanel.Invalidate();
            }
        }

        public override void Show(object value)
        {

            softwareEvent = (SoftwareEvent)value;
            if (filter == null || filter.Length == 0 || filter.Contains(softwareEvent.Name))
            {
                var text = value.ToString();
                text = Regex.Replace(text, @"\r|\n", string.Empty);
                buffer.Enqueue(text);

                while (buffer.Count > bufferSize)
                {
                    buffer.Dequeue();
                }
            }
        }

        public override void Load(IServiceProvider provider)
        {
            filterTextBox = new ToolStripTextBox();
            filterTextBox.LostFocus += (sender, e) =>
            {
                var input = filterTextBox.Text;
                if (input != null)
                {
                    filter = ProcessInput(input);
                    filterTextBox.Text = string.Join(", ", filter);
                }
            };

            filterTextBox.KeyDown += (sender, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    var input = filterTextBox.Text;
                    if (input != null)
                    {
                        filter = ProcessInput(input);
                        filterTextBox.Text = string.Join(", ", filter);
                    }
                }
            };

            statusStrip = new StatusStrip();
            statusStrip.Items.Add(new ToolStripLabel("Filter:"));
            statusStrip.Items.Add(filterTextBox);
            statusStrip.Dock = DockStyle.Bottom;

            buffer = new Queue<string>();
            textBox = new RichTextBox { Dock = DockStyle.Fill };
            textBox.Multiline = true;
            textBox.WordWrap = false;
            textBox.ScrollBars = RichTextBoxScrollBars.Horizontal;
            textBox.MouseDoubleClick += (sender, e) =>
            {
                if (e.Button == MouseButtons.Right)
                {
                    buffer.Clear();
                    textBox.Text = string.Empty;
                    textPanel.Invalidate();
                }
            };

            textPanel = new UserControl();
            textPanel.SuspendLayout();
            textPanel.Dock = DockStyle.Fill;
            textPanel.MinimumSize = textPanel.Size = new Size(320, 2 * AutoScaleHeight);
            textPanel.AutoScaleDimensions = new SizeF(6F, AutoScaleHeight);
            textPanel.AutoScaleMode = AutoScaleMode.Font;
            textPanel.Paint += textPanel_Paint;
            textPanel.Controls.Add(textBox);
            textPanel.Controls.Add(statusStrip);

            textPanel.ResumeLayout(false);

            var visualizerService = (IDialogTypeVisualizerService)provider.GetService(typeof(IDialogTypeVisualizerService));
            if (visualizerService != null)
            {
                visualizerService.AddControl(textPanel);
            }
        }

        void textPanel_Paint(object sender, PaintEventArgs e)
        {
            var lineHeight = AutoScaleHeight * e.Graphics.DpiY / DefaultDpi;
            bufferSize = (int)((textBox.ClientSize.Height - 2) / lineHeight);
            var textSize = TextRenderer.MeasureText(textBox.Text, textBox.Font);
            if (textBox.ClientSize.Width < textSize.Width)
            {
                var offset = 2 * lineHeight + SystemInformation.HorizontalScrollBarHeight - textPanel.Height;
                if (offset > 0)
                {
                    textPanel.Parent.Height += (int)offset;
                }
            }
        }

        public override void Unload()
        {
            bufferSize = 0;
            textBox.Dispose();
            textBox = null;
            buffer = null;
        }

        public static string[] ProcessInput(string input)
        {
            return input.Split(',').Select(x => x.Trim()).Where(x => !string.IsNullOrEmpty(x)).ToArray();
        }
    }
}
