using Bonsai;
using System.ComponentModel;

namespace AllenNeuralDynamics.Core.Design
{
    [TypeVisualizer(typeof(PushButtonVisualizer))]
    [Description("Generates a sequence of manual annotations or alerts about the experiment.")]
    public class PushButtonSource : PushButton
    {
    }
}
