using Bonsai;
using System;
using System.ComponentModel;
using System.Reactive.Linq;
using LibGit2Sharp;

namespace AllenNeuralDynamics.Git
{
    /// <summary>
    /// Represents an operator that populates a Repository object from a target folder path
    /// </summary>
    [DefaultProperty("Path")]
    [Description("Returns a new Repository object (LibGit2Sharp) for the specified repository root path. Accepts relative or absolute paths.")]
    public class CreateRepository : Source<Repository>
    {
        /// <summary>
        /// Gets or sets path for the targetted repository.
        /// </summary>
        [Editor("Bonsai.Design.FolderNameEditor, Bonsai.Design", DesignTypes.UITypeEditor)]
        [Description("The relative or absolute path of the selected repository root.")]
        public string Path { get; set; } = "../.";

        /// <summary>
        /// Generates an observable with a single Repository object from a given root path.
        /// </summary>
        /// <returns>
        /// A sequence of <see cref="Repository"/> objects representing a git repository.
        /// </returns>
        public override IObservable<Repository> Generate()
        {
            return Observable.Defer(() => {
                return Observable.Return(new Repository(Path));
                });
        }
    }
}
