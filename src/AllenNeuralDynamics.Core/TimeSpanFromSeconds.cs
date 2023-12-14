using Bonsai;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;

namespace AllenNeuralDynamics.Core
{
    [Combinator]
    [Description("Converts a numerical value in seconds to a TimeSpan object.")]
    [WorkflowElementCategory(ElementCategory.Transform)]

    public class TimeSpanFromSeconds
    {
        public IObservable<TimeSpan> Process(IObservable<double> source)
        {
            return source.Select(value => TimeSpan.FromSeconds(value));
        }
    }
}
