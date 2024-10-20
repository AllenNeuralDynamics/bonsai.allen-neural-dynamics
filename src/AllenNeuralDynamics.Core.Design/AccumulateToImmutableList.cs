using Bonsai;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Collections.Immutable;

namespace AllenNeuralDynamics.Core.Design
{
    [Combinator]
    [Description("Accumulates an incoming sequence of elements and outputs a new immutable list each time a new element arrives")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    public class AccumulateToImmutableList
    {
        public IObservable<ImmutableList<T>> Process<T>(IObservable<T> source)
        {
            var list = ImmutableList<T>.Empty;
            return source.Select(value =>
            {
                return list.Add(value);
            });
        }
    }
}
