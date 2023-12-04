using System;
using Bonsai.Design;
using Bonsai.Expressions;
using System.Windows.Forms;

namespace AllenNeuralDynamics.Core.Design
{
    public class ToggleButtonStateVisualizer : DialogTypeVisualizer
    {
        ToggleButtonStateControl control;

        public override void Load(IServiceProvider provider)
        {
            var context = (ITypeVisualizerContext)provider.GetService(typeof(ITypeVisualizerContext));
            var visualizerElement = ExpressionBuilder.GetVisualizerElement(context.Source);
            control = new ToggleButtonStateControl();
            control.Dock = DockStyle.Fill;

            var visualizerService = (IDialogTypeVisualizerService)provider.GetService(typeof(IDialogTypeVisualizerService));
            if (visualizerService != null)
            {
                visualizerService.AddControl(control);
            }
        }

        public override void Show(object value)
        {
            control.State = (bool) value;
        }

        public override void Unload()
        {
            control.Dispose();
            control = null;
        }
    }
}
