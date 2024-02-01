using System;
using System.ComponentModel;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Bonsai;

namespace AllenNeuralDynamics.Core.Design
{
    [Combinator]
    [WorkflowElementCategory(ElementCategory.Source)]
    [TypeVisualizer(typeof(ToggleButtonVisualizer))]
    [Description("Generates a sequence of commands to tare a weight scale.")]
    public class ToggleButton
    {
        private EventHandler onEnabledChanged;
        public event EventHandler OnEnabledChanged
        {
            add { onEnabledChanged += value; }
            remove { onEnabledChanged += value; }
        }

        private bool enabled = true;
        public bool Enabled
        {
            get { return enabled; }
            set
            {
                enabled = value;
                onEnabledChanged?.Invoke(this, new ToggleEnabledStateEventArgs { Enabled = enabled });
            }
        }

        readonly Subject<bool> subject = new Subject<bool>();
        
        [DesignOnly(true)]
        public bool IsInitiallyChecked { get; set; } = false;

        public string CheckedLabel { get; set; } = "Turn Off.";
        public string UncheckedLabel { get; set; } = "Turn On.";

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

    public class ToggleEnabledStateEventArgs : EventArgs
    {
        public bool Enabled { get; set; }
    }
}
