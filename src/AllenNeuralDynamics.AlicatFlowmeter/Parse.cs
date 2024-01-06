using System;
using System.ComponentModel;
using System.Reactive.Linq;
using Bonsai;


namespace AllenNeuralDynamics.AlicatFlowmeter
{
    [Description("Parses an incoming string into a FlowmeterDataFrame.")]
    public class Parse : Transform<string, FlowmeterDataframe>
    {
        public override IObservable<FlowmeterDataframe> Process(IObservable<string> source)
        {
            return source.Select(x => new FlowmeterDataframe(x));
        }
    }
}
