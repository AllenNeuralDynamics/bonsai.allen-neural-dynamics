using Bonsai.Harp;
using Newtonsoft.Json;
using System.ComponentModel;

namespace AllenNeuralDynamics.HarpUtils
{
    [Description("A data structure representing metadata associated with a Harp Device.")]

    public class HarpDeviceMetadata
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

        public FirmwareMetadata ToFirmwareMetadata()
        {
            return new FirmwareMetadata(
                        DeviceName,
                        new HarpVersion(MajorFirmwareVersion, MinorFirmwareVersion),
                        new HarpVersion(MajorCoreVersion, MinorCoreVersion),
                        new HarpVersion(MajorHardwareVersion, MinorHardwareVersion),
                        AssemblyVersion,
                        PrereleaseVersion
                );
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }


}