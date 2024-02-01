using System;
using System.Windows.Forms;

namespace AllenNeuralDynamics.Core.Design
{
    public partial class ToggleButtonStateControl : UserControl
    {
        public ToggleButton Source { get; }

        public string CheckedLabel { get; set; }
        public string UncheckedLabel { get; set; }

        private void HandleEnabledChanges(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(HandleEnabledChanges), sender, e);
                return;
            }
            Enabled = ((ToggleEnabledStateEventArgs)e).Enabled;
        }

        public ToggleButtonStateControl(ToggleButton source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Source.OnEnabledChanged += HandleEnabledChanges;
            InitializeComponent();
            State = Source.IsInitiallyChecked;
            Enabled = Source.Enabled;
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
