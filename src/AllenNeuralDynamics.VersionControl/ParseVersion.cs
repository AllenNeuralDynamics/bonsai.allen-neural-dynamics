using Bonsai;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using NuGet.Versioning;


namespace AllenNeuralDynamics.VersionControl
{
    /// <summary>
    /// Represents an operator that parses an compatible incoming <see cref="string"/> a <see cref="NuGetVersion"/> object.
    /// </summary>
    [Combinator]
    [Description("Parses a string into a compatible NugetVersion object. The input string should follow semantic versioning rules format.")]
    [WorkflowElementCategory(ElementCategory.Transform)]
    public class ParseVersion
    {
        /// <summary>
        /// Emits a sequence of values with the parsed <see cref="NuGetVersion"/>.
        /// </summary>
        /// <returns>
        /// A sequence of <see cref="NuGetVersion"/> objects.
        /// </returns>
        public IObservable<NuGetVersion> Process(IObservable<string> source)
        {
            return source.Select(value => {
                return new NuGetVersion(value);
            });
        }
    }
}
