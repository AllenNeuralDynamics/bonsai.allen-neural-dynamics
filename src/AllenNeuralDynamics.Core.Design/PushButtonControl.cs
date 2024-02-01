using System;
using System.Reactive;
using System.Windows.Forms;
using static AllenNeuralDynamics.Core.Design.PushButton;

namespace AllenNeuralDynamics.Core.Design
{
    public partial class PushButtonControl : UserControl
    {
        public PushButton Source { get; }

        public string ButtonLabel {
            get{return button.Text;}
            set { button.Text = value;} 
        }

        private void HandleEnableChanges(object sender, EventArgs e)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new EventHandler(HandleEnableChanges), sender, e);
                return;
            }
            Enabled = ((EnabledChangedEventArgs)e).Enabled;
        }

        public PushButtonControl(PushButton source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            Source.OnEnableChanged += HandleEnableChanges;
            InitializeComponent();
        }

        private void button_click(object sender, EventArgs e)
        {
            Source.OnNext(Unit.Default);
        }
    }
}
