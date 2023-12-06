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
        [DesignOnly(true)]
        public string CheckedLabel { get; set; } = "Turn Off.";
        
        [DesignOnly(true)]
        public string UncheckedLabel { get; set; } = "Turn On.";

        [DesignOnly(true)]
        public bool IsInitiallyChecked { get; set; } = false;
        
        readonly Subject<bool> subject = new Subject<bool>();

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

    public class ToggleStateChangedEventArgs : EventArgs
    {
        public bool State { get; set; }
    }
}
