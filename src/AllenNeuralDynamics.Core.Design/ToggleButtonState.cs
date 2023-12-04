using Bonsai;
using System;
using System.ComponentModel;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace AllenNeuralDynamics.Core.Design
{
    [TypeVisualizer(typeof(ToggleButtonStateVisualizer))]
    [Description("Generates a sequence of commands to tare a weight scale.")]
    public class ToggleButtonState
    {
        readonly Subject<bool> subject = new Subject<bool>();

        public string OnLabel { get; set; }
        public string OffLabel { get; set; }

        public ToggleButtonState() { }

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
