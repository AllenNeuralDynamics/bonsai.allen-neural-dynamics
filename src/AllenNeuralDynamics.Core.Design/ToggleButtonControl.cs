using System;
using System.Reactive.Linq;
using System.Windows.Forms;

namespace AllenNeuralDynamics.Core.Design
{
    public partial class ToggleButtonStateControl : UserControl
    {
        public ToggleButtonState Source { get; }

        public ToggleButtonStateControl()
        {
            Source = new ToggleButtonState(); 
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
                //toggleButton.Text = $"Turn {(toggleButton.Checked ? "Off" : "On")}";
            }
        }

        private void maintenanceButton_CheckedChanged(object sender, EventArgs e)
        {
            Source.OnNext(State);
        }
    }
}
