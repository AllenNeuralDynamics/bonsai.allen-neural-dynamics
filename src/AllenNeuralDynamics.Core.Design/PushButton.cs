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

        private EventHandler onEnableChanged;
        public event EventHandler OnEnableChanged
        {
            add{onEnableChanged += value;}
            remove{onEnableChanged += value;}
        }

        private bool enable = true;
        public bool Enable {
            get { return enable; }
            set {
                enable = value;
                onEnableChanged?.Invoke(this, new EnableStateEventArgs {EnableState = enable }); 
            } 
        }

        readonly Subject<Unit> subject = new Subject<Unit>();

        public string Label { get; set; }

        public PushButton() { }

        public void OnNext(Unit value) { 
            subject.OnNext(value);
        }

        public virtual IObservable<Unit> Process()
        {
            return subject.ObserveOn(Scheduler.TaskPool);
        }
    }

    public class EnableStateEventArgs : EventArgs
    {
        public bool EnableState { get; set; }
    }
}

