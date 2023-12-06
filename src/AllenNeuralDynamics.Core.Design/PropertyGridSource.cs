using System.ComponentModel;
using System.Reactive;
using System;
using Bonsai;
using System.Reactive.Linq;

namespace AllenNeuralDynamics.Core.Design
{
    [TypeVisualizer(typeof(ExternalizedPropertiesVisualizer))]
    [Description("Generates a sequence of string events.")]
    public class PropertyGridSource : Source<Unit> { 
        public PropertyGridSource() { }

        public override IObservable<Unit> Generate()
        {
            return(Observable.Never(new Unit()));
        }
    }
}
