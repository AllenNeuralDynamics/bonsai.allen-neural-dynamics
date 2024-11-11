using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Bonsai;
using System.ComponentModel;
using Bonsai.Harp;
using System.Xml.Serialization;

namespace AllenNeuralDynamics.HarpUtils
{
    [Combinator]
    [WorkflowElementCategory(ElementCategory.Transform)]
    [Description("From a dictionary of expected channels connected to a clock synchronizer, outputs a structure with diagnosis information.")]
    public class ValidateClkOutputChannels
    {
        [XmlIgnore]
        [Description("Paired elements of the channel number and channel name.")]

        public Dictionary<int, string> ExpectedChannels { get; set; } = new Dictionary<int, string>();


        public IObservable<ValidateClkOutputChannelsDiagnosis> Process<T>(IObservable<T> source) where T : struct, IConvertible{
            return Process(source.Select(value => Convert.ToInt32(value)));
        }

        public IObservable<ValidateClkOutputChannelsDiagnosis> Process(IObservable<int> source){

            return source.Select(value => {
                var expectedChannelsMask = ExpectedChannelsMask(ExpectedChannels);
                var foundChannels = expectedChannelsMask & value;

                return new ValidateClkOutputChannelsDiagnosis{
                    ExtraChannels = Enumerable.Range(0, 32).Where(bit => ((~expectedChannelsMask & value) & (1 << bit)) != 0).ToArray(),
                    FoundChannels = FilterDictionaryOnMask(ExpectedChannels, foundChannels),
                    MissingChannels = FilterDictionaryOnMask(ExpectedChannels, ~foundChannels),
                };
            });
        }

        private static Dictionary<int, string> FilterDictionaryOnMask(Dictionary<int, string> dictionary, int mask) {
            return dictionary.Where(channel => (mask & (1 << channel.Key)) != 0).ToDictionary(channel => channel.Key, channel => channel.Value);
        }

        private static int ExpectedChannelsMask(Dictionary<int, string> expectedChannels) {
            int mask = 0;
            foreach (var channel in expectedChannels) {
                mask |= 1 << channel.Key;
            }
            return mask;
        }
    }

    public class ValidateClkOutputChannelsDiagnosis
    {
        public Dictionary<int, string> FoundChannels;
        public int[] ExtraChannels;
        public Dictionary<int, string> MissingChannels;


        public override string ToString()
        {
            return $"Found channels: {string.Join(", ", FoundChannels.Select(channel => $"{channel.Key} ({channel.Value})"))}\n" +
                   $"Extra channels: {string.Join(", ", ExtraChannels)}\n" +
                   $"Missing channels: {string.Join(", ", MissingChannels.Select(channel => $"{channel.Key} ({channel.Value})"))}";
        }
    }


}
