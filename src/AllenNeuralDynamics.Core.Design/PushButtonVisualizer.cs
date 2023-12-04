using Bonsai.Design;
using Bonsai.Expressions;
using System;
using System.Drawing;
using System.Reactive;
using System.Windows.Forms;

namespace AllenNeuralDynamics.Core.Design
{
    public class PushButtonVisualizer : DialogTypeVisualizer
    {
        PushButtonControl control;

        public override void Load(IServiceProvider provider)
        {
            var context = (ITypeVisualizerContext)provider.GetService(typeof(ITypeVisualizerContext));
            var visualizerElement = ExpressionBuilder.GetVisualizerElement(context.Source);
            var source = (PushButton)ExpressionBuilder.GetWorkflowElement(visualizerElement.Builder);

            control = new PushButtonControl(source);
            control.Dock = DockStyle.Fill;
            control.ButtonLabel = source.Label;

            var visualizerService = (IDialogTypeVisualizerService)provider.GetService(typeof(IDialogTypeVisualizerService));
            if (visualizerService != null)
            {
                visualizerService.AddControl(control);
            }
        }

        public override void Show(object value)
        {
        }

        public override void Unload()
        {
            if (control != null)
            {
                control.Dispose();
                control = null;
            }
        }
    }
}
