using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reactive;
using System.Reactive.Linq;
using Bonsai;

namespace AllenNeuralDynamics.Core
{
    [DefaultProperty(nameof(Source))]
    [Description("Copies a source folder, and all its contents, to a destination directory.")]
    public class StartRobocopy : Source<Unit>
    {
        readonly string executable = "robocopy";

        [Description("The relative or absolute path of the directory to copy.")]
        [Editor("Bonsai.Design.FolderNameEditor, Bonsai.Design", DesignTypes.UITypeEditor)]
        public string Source { get; set; }

        [Description("The relative or absolute path of the directory to copy the Source folder to.")]
        [Editor("Bonsai.Design.FolderNameEditor, Bonsai.Design", DesignTypes.UITypeEditor)]
        public string Destination { get; set; }

        [Description("The relative or absolute path of the log file to be created")]
        public bool CreateLog { get; set; } = true;

        [Editor(DesignTypes.MultilineStringEditor, DesignTypes.UITypeEditor)]
        [Description("The set of command-line arguments to use when starting the application.")]
        public string Arguments { get; set; } = "/E /DCOPY:DAT /R:100 /W:3 /tee";

        public ProcessWindowStyle ProcessWindowStyle { get; set; } = ProcessWindowStyle.Normal;

        private string FormatCommand()
        {

            if (!Directory.Exists(Source)){
                throw new DirectoryNotFoundException("Source directory does not exist.");
            }

            if (!Directory.Exists(Destination))
            {
                Directory.CreateDirectory(Destination);
            }

            string log_arg = "";
            if (CreateLog)
            {
                var log = Path.Combine(Destination, "robocopy.log");
                log_arg = $"/LOG:{log}";
            }

            var command = $"\"{Source}\" \"{Destination}\" {Arguments} {log_arg}";
            return command;
        }
        public override IObservable<Unit> Generate()
        {
            var process = new Process();
            process.StartInfo.FileName = executable;
            process.StartInfo.Arguments = FormatCommand();
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle;
            process.Start();
            return Observable.Return(new Unit());
        }
    }
}
