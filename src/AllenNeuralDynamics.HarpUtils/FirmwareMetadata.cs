using System;
using System.Reactive.Linq;
using Bonsai;
using Bonsai.Harp;

namespace AllenNeuralDynamics.HarpUtils
{
    public class CreateFirmwareMetadata : Source<FirmwareMetadata>
    {
        public string DeviceName { get; set; }
        
        public int? MajorFirmwareVersion { get; set; }

        public int? MinorFirmwareVersion { get; set; }

        public int? MajorCoreVersion { get; set; }

        public int? MinorCoreVersion { get; set; }

        public int? MajorHardwareVersion { get; set; }

        public int? MinorHardwareVersion { get; set; }

        public int? AssemblyVersion { get; set; }

        public int? PrereleaseVersion { get; set; }

        public override IObservable<FirmwareMetadata> Generate()
        {
            

            return Observable.Return(
                new FirmwareMetadata(
                    DeviceName,
                    new HarpVersion(MajorFirmwareVersion, MinorFirmwareVersion),
                    new HarpVersion(MajorCoreVersion, MinorCoreVersion),
                    new HarpVersion(MajorHardwareVersion, MinorHardwareVersion),
                    AssemblyVersion,
                    PrereleaseVersion
                    )
                );
        }
    }
}
