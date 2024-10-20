using Bonsai;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Collections.Generic;

namespace AllenNeuralDynamics.Core.Design
{
    [Combinator]
    [Description("Accumulates an incoming sequence of elements and outputs a list each time a new element arrives")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    public class AccumulateToList
    {
        public IObservable<IList<T>> Process<T>(IObservable<T> source)
        {
            var list = new List<T>();
            return source.Select(value =>
            {
                list.Add(value);
                return list;
            });
        }
    }
}
