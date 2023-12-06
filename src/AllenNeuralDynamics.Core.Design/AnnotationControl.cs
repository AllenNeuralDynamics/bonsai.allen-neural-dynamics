using System;
using System.Windows.Forms;

namespace AllenNeuralDynamics.Core.Design
{
    public partial class AnnotationControl : UserControl
    {
        public AnnotationControl(AnnotationSource source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            InitializeComponent();
        }

        public AnnotationSource Source { get; }

        private void OnAnnotation()
        {
            if (string.IsNullOrEmpty(annotationsTextBox.Text))
            {
                return;
            }

            var message = annotationsTextBox.Text
                .Replace(',', '\t')
                .Replace(System.Environment.NewLine, "\t");
            annotationsTextBox.Text = string.Empty;
            Source.OnNext(message);
        }

        private void annotationButton_Click(object sender, EventArgs e)
        {
            OnAnnotation();
        }

        private void annotationBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Enter)
            {
                OnAnnotation();
                e.SuppressKeyPress = true;
            }
        }
    }

}
