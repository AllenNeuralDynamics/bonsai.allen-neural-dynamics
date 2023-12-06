using Bonsai;
using System;
using System.ComponentModel;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace AllenNeuralDynamics.Core.Design
{
    [Combinator]
    [WorkflowElementCategory(ElementCategory.Source)]
    [TypeVisualizer(typeof(PushButtonVisualizer))]
    [Description("Generates a sequence of Unit events.")]
    public class PushButton
    {
        readonly Subject<Unit> subject = new Subject<Unit>();

        public PushButton() { }

        [DesignOnly(true)]
        public string Label { get; set; }

        public void OnNext(Unit value) { 
            subject.OnNext(value);
        }

        public virtual IObservable<Unit> Process()
        {
            return subject.ObserveOn(Scheduler.TaskPool);
        }
    }
}

