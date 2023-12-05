using Bonsai;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;


namespace AllenNeuralDynamics.Core.Design
{
    [Combinator]
    [WorkflowElementCategory(ElementCategory.Source)]
    [TypeVisualizer(typeof(AnnotationSourceVisualizer))]
    public abstract class Annotation<TMetadata>
    {
        readonly Subject<TMetadata> subject = new Subject<TMetadata>();

        public void OnNext(TMetadata value)
        {
            subject.OnNext(value);
        }

        public virtual IObservable<TMetadata> Process()
        {
            return subject.ObserveOn(Scheduler.TaskPool);
        }
    }
}
