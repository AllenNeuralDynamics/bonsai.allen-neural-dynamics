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
        private bool ignoreUntracked = false;
        [Editor("Bonsai.Design.FolderNameEditor, Bonsai.Design", DesignTypes.UITypeEditor)]
        [Description("The relative or absolute path of the selected repository root.")]

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

/*
This file was adapted from https://github.com/SainsburyWellcomeCentre/aeon_acquisition under the following license:
BSD 3-Clause License

Copyright (c) 2023 University College London
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer.

2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution.

3. Neither the name of the copyright holder nor the names of its
   contributors may be used to endorse or promote products derived from
   this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE
FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
