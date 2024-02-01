using System;
using System.ComponentModel;
using System.Drawing;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Bonsai;

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

        public string Label { get; set; }

        private EventHandler onEnableChanged;
        public event EventHandler OnEnableChanged
        {
            add { onEnableChanged += value; }
            remove { onEnableChanged += value; }
        }

        private bool enabled = true;
        public bool Enabled
        {
            get { return enabled; }
            set
            {
                enabled = value;
                onEnableChanged?.Invoke(
                    this,
                    new EnabledChangedEventArgs { Enabled = enabled });
            }
        }


        public void OnNext(Unit value) { 
            subject.OnNext(value);
        }

        public virtual IObservable<Unit> Process()
        {   
            return subject.ObserveOn(Scheduler.TaskPool);
        }

        public class EnabledChangedEventArgs : EventArgs
        {
            public bool Enabled { get; set; }
        }
    }
}

