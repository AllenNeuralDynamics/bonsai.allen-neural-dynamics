using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using Bonsai;
using Bonsai.IO.Ports;

namespace AllenNeuralDynamics.Zaber
{
    /// <summary>
    /// Represents an operator that establishes a connection with a <see cref="ZaberDevice"/>.
    /// </summary>
    [DefaultProperty(nameof(Name))]
    [Description("Creates a connection to an Zaber manipulator.")]
    public class CreateZaberDevice : Source<ZaberDevice>, INamedElement
    {
        readonly ZaberDeviceConfiguration configuration = new ZaberDeviceConfiguration();

        /// <summary>
        /// Gets or sets the optional alias to be used for the manipulator.
        /// </summary>
        [Description("The optional alias for the manipulator.")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the COM port where the manipulator is connected to.
        /// </summary>
        [TypeConverter(typeof(SerialPortNameConverter))]
        [Description("The name of the serial port used to communicate with the manipulator.")]
        public string PortName
        {
            get { return configuration.PortName; }
            set { configuration.PortName = value; }
        }

        /// <summary>
        /// Generates an observable with a single <see cref="ZaberDevice"/> resource object.
        /// </summary>
        /// <returns>
        /// A sequence of a single <see cref="ZaberDevice"/> resource object.
        /// </returns>
        public override IObservable<ZaberDevice> Generate()
        {
            return Observable.Using(
                () => ZaberDeviceManager.ReserveConnection(Name, configuration),
                resource =>
                {
                    return Observable.Return(resource.Device)
                                     .Concat(Observable.Never(resource.Device));
                });
        }
    }
}
