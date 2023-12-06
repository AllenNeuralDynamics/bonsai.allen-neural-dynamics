using System;
using System.Windows.Forms;

namespace AllenNeuralDynamics.Core.Design
{
    public partial class ToggleButtonStateControl : UserControl
    {
        public ToggleButton Source { get; }

        public ToggleButtonStateControl(ToggleButton source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            InitializeComponent();
            State = Source.IsInitiallyChecked;
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
                toggleButton.Text = toggleButton.Checked ? Source.CheckedLabel : Source.UncheckedLabel;
            }
        }

        private void toggleButton_CheckedChanged(object sender, EventArgs e)
        {
            Source.OnNext(State);
        }
    }
}
