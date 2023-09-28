using Bonsai;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using NuGet.Versioning;


namespace AllenNeuralDynamics.Git
{
    [Combinator]
    [Description("Parses a string into a compatible NugetVersion object. The input string should follow semantic versioning rules format.")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    public class ParseVersion
    {
        public IObservable<NuGetVersion> Process(IObservable<string> source)
        {
            return source.Select(value => {
                return new NuGetVersion(value);
            });
        }
    }
}
