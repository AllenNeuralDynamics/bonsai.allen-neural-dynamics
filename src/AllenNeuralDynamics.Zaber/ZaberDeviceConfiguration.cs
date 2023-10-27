using System.ComponentModel;
using Bonsai.IO.Ports;

namespace AllenNeuralDynamics.Zaber
{
    /// <summary>
    /// Represents configuration settings used to initialize a connection with a <see cref="ZaberDevice"/>
    /// </summary>
    public class ZaberDeviceConfiguration
    {
        internal static readonly ZaberDeviceConfiguration Default = new ZaberDeviceConfiguration();

        /// <summary>
        /// Initializes a new instance of the <see cref="ZaberDeviceConfiguration"/> class.
        /// </summary>
        public ZaberDeviceConfiguration()
        {
        }

        /// <summary>
        /// Gets or sets the name of the serial port.
        /// </summary>
        [TypeConverter(typeof(SerialPortNameConverter))]
        [Description("The name of the serial port.")]
        public string PortName { get; set; }
    }
}
