using Bonsai;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Collections.Generic;

namespace AllenNeuralDynamics.Core.Logging
{
    [Description("Outputs a known data modality string from a dropdown list.")]
    public class AppendModalitySuffix : Transform<string, string>
    {
        public Modality? Modality { get; set; } = null;

        public override IObservable<string> Process(IObservable<string> source)
        {
            return source.Select(value =>
            {
                return Path.Combine(value, Modality.HasValue ? modalityDirectories[Modality.Value] : "");
            });
        }

        private readonly Dictionary<Modality, string> modalityDirectories = new Dictionary<Modality, string>()
        {
                {Logging.Modality.Other, "other"},
                {Logging.Modality.Behavior, "behavior"},
                {Logging.Modality.Confocal, "confocal"},
                {Logging.Modality.Ecephys, "ecephys"},
                {Logging.Modality.BehaviorVideos, "behavior-videos"},
                {Logging.Modality.Electromyography, "EMG"},
                {Logging.Modality.Fib, "fib"},
                {Logging.Modality.Fmost, "fMOST"},
                {Logging.Modality.Icephys, "icephys"},
                {Logging.Modality.Isi, "ISI"},
                {Logging.Modality.Merfish, "merfish"},
                {Logging.Modality.Mri, "MRI"},
                {Logging.Modality.POphys, "ophys"},
                {Logging.Modality.Slap, "slap"},
                {Logging.Modality.Spim, "SPIM"}
        };
    }

    public enum Modality
    {
        Other = 0,
        Behavior = 1,
        Confocal = 2,
        Ecephys = 3,
        BehaviorVideos = 4,
        Electromyography = 5,
        Fib = 6,
        Fmost = 7,
        Icephys = 8,
        Isi = 9,
        Merfish = 10,
        Mri = 11,
        POphys = 12,
        Slap = 13,
        Spim = 14,
    }
}
