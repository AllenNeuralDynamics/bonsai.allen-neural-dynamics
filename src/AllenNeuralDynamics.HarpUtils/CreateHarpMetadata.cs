using System;
using System.Reactive.Linq;
using Bonsai;

namespace AllenNeuralDynamics.HarpUtils
{
    public class CreateHarpMetadata : Source<HarpDeviceMetadata>
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

        public int? WhoAmI { get; set; }

        public int? SerialNumber { get; set; }

        public int? FirmwareTag { get; set; }

        public override IObservable<HarpDeviceMetadata> Generate() { 
        
            return Observable.Return(new HarpDeviceMetadata
            {
                DeviceName = DeviceName,
                MajorFirmwareVersion = MajorFirmwareVersion,
                MinorFirmwareVersion = MinorFirmwareVersion,
                MajorCoreVersion = MajorCoreVersion,
                MinorCoreVersion = MinorCoreVersion,
                MajorHardwareVersion = MajorHardwareVersion,
                MinorHardwareVersion = MinorHardwareVersion,
                AssemblyVersion = AssemblyVersion,
                PrereleaseVersion = PrereleaseVersion,
                WhoAmI = WhoAmI,
                SerialNumber = SerialNumber,
                FirmwareTag = FirmwareTag
            });
        }

        public IObservable<HarpDeviceMetadata> Generate<TSource>(IObservable<TSource> source)
        {

            return source.Select(x => new HarpDeviceMetadata
            {
                DeviceName = DeviceName,
                MajorFirmwareVersion = MajorFirmwareVersion,
                MinorFirmwareVersion = MinorFirmwareVersion,
                MajorCoreVersion = MajorCoreVersion,
                MinorCoreVersion = MinorCoreVersion,
                MajorHardwareVersion = MajorHardwareVersion,
                MinorHardwareVersion = MinorHardwareVersion,
                AssemblyVersion = AssemblyVersion,
                PrereleaseVersion = PrereleaseVersion,
                WhoAmI = WhoAmI,
                SerialNumber = SerialNumber,
                FirmwareTag = FirmwareTag
            });
        }
    }
}
