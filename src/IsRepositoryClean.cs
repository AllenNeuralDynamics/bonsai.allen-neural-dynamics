using Bonsai;
using System;
using System.ComponentModel;
using System.Reactive.Linq;
using LibGit2Sharp;

namespace AllenNeuralDynamics.Git{
    [DefaultProperty("IgnoreUntracked")]
    [Description("Determines whether a specified repository is clean or if uncommitted or untracked changes exist")]
    public class IsRepositoryClean : Transform<Repository, bool>
    {
        [Editor("Bonsai.Design.FolderNameEditor, Bonsai.Design", DesignTypes.UITypeEditor)]
        [Description("The relative or absolute path of the selected repository root.")]

        private bool ignoreUntracked = false;
        public bool IgnoreUntracked
        {
            get { return ignoreUntracked; }
            set { ignoreUntracked = value; }
        }

        public override IObservable<bool> Process(IObservable<Repository> source)
        {
            return source.Select(value =>
            {
                var status = value.RetrieveStatus();
                var untrackedChanges = IgnoreUntracked ? false : value.Diff.Compare<TreeChanges>().Count > 0;
                return !(status.IsDirty || untrackedChanges);
            });
        }
    }
}
