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
        private EventHandler onToggleStateChanged;
        public event EventHandler OnToggleStateChanged
        {
            add { onToggleStateChanged += value; }
            remove { onToggleStateChanged += value; }
        }

        private bool toggleState = true;
        public bool ToggleState
        {
            get { return toggleState; }
            set
            {
                toggleState = value;
                onToggleStateChanged?.Invoke(this, new ToggleStateChangedEventArgs { State = toggleState});
            }
        }

        private EventHandler onEnableChanged;
        public event EventHandler OnEnableChanged
        {
            add { onEnableChanged += value; }
            remove { onEnableChanged += value; }
        }

        private bool enable = true;
        public bool Enable
        {
            get { return enable; }
            set
            {
                enable = value;
                onEnableChanged?.Invoke(this, new ToggleEnableStateEventArgs { EnableState = enable });
            }
        }

        readonly Subject<bool> subject = new Subject<bool>();

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

    public class ToggleStateChangedEventArgs : EventArgs
    {
        public bool State { get; set; }
    }

    public class ToggleEnableStateEventArgs : EventArgs
    {
        public bool EnableState { get; set; }
    }
}
