using Bonsai;
using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;

namespace AllenNeuralDynamics.AindManipulator
{
    [Combinator]
    [Description("Returns true if the dictionary contains the specified key.")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [DesignTimeVisible(false)]

    public class ContainsKey
    {
        public IObservable<bool> Process<TKey, TValue>(IObservable<Tuple<TKey, IDictionary<TKey, TValue>>> source)
        {
            return source.Select(value => { return value.Item2.ContainsKey(value.Item1); });
        }

        public IObservable<bool> Process<TKey, TValue>(IObservable<Tuple<IDictionary<TKey, TValue>, TKey>> source)
        {
            return source.Select(value => { return value.Item1.ContainsKey(value.Item2); });
        }
    }
}