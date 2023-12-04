using Bonsai;
using System.ComponentModel;

namespace AllenNeuralDynamics.Core.Design
{
    [TypeVisualizer(typeof(AnnotationSourceVisualizer))]
    [Description("Generates a sequence of manual annotations or alerts about the experiment.")]
    public class AnnotationSource : LogMessage<string>
    {
    }
}
