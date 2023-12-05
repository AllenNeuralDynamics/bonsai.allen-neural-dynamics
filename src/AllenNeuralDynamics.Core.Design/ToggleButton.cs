using Bonsai;
using System;
using System.ComponentModel;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace AllenNeuralDynamics.Core.Design
{
    [Combinator]
    [WorkflowElementCategory(ElementCategory.Source)]
    [TypeVisualizer(typeof(ToggleButtonVisualizer))]
    [Description("Generates a sequence of commands to tare a weight scale.")]
    public class ToggleButton
    {
        readonly Subject<bool> subject = new Subject<bool>();

        public string CheckedLabel { get; set; }
        public string UncheckedLabel { get; set; }

        public ToggleButton() { }

        public void OnNext(bool value)
        {
            subject.OnNext(value);
        }

        public virtual IObservable<bool> Process()
        {
            return subject.ObserveOn(Scheduler.TaskPool);
        }
    }
}
