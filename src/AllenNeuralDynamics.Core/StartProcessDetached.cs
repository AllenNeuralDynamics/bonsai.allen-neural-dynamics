using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using Bonsai;

namespace AllenNeuralDynamics.Core
{
    [DefaultProperty(nameof(FileName))]
    [Description("Starts a new system process with the specified file name and command-line arguments.")]
    public class StartProcessOnNewConsole : Source<Unit>
    {
        [Description("The name of the application or document to start.")]
        [FileNameFilter("Executable files|*.exe|All Files|*.*")]
        [Editor("Bonsai.Design.OpenFileNameEditor, Bonsai.Design", DesignTypes.UITypeEditor)]
        public string FileName { get; set; }

        [Editor(DesignTypes.MultilineStringEditor, DesignTypes.UITypeEditor)]
        [Description("The set of command-line arguments to use when starting the application.")]
        public string Arguments { get; set; }

        public IObservable<Unit> Generate<TSource>(IObservable<TSource> source)
        {
            return source.SelectMany(input => Generate());
        }

        public override IObservable<Unit> Generate()
        {
            var process = new Process();
            process.StartInfo.FileName = FileName;
            process.StartInfo.Arguments = Arguments;
            process.StartInfo.CreateNoWindow = false;
            process.StartInfo.UseShellExecute = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            process.Start();
            return Observable.Return(new Unit());
        }
    }
}
