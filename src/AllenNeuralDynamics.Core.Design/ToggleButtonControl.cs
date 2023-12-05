using System;
using System.Windows.Forms;

namespace AllenNeuralDynamics.Core.Design
{
    public partial class ToggleButtonStateControl : UserControl
    {
        public ToggleButton Source { get; }

        public string CheckedLabel { get; set; }
        public string UncheckedLabel { get; set; }

        public ToggleButtonStateControl(ToggleButton source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            InitializeComponent();
        }

        public bool State
        {
            get
            {
                return toggleButton.Checked;
            }
            set
            {
                toggleButton.Checked = value == true;
                toggleButton.Text = toggleButton.Checked ? CheckedLabel : UncheckedLabel;
            }
        }

        private void toggleButton_CheckedChanged(object sender, EventArgs e)
        {
            Source.OnNext(State);
        }
    }
}
