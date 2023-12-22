using System;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Bonsai;


namespace AllenNeuralDynamics.Zaber
{
    /// <summary>
    /// Represents an operator sends a command to a  <see cref="ZaberDevice"/> without expecting a reply.
    /// </summary>
    [Description("Sends a command without expecting a result.")]
    public class GenericCommandNoResponse : Sink<string>
    {
        /// <summary>
        /// Gets or sets the COM port or alias of the target <see cref="ZaberDevice"/>
        /// </summary>
        [TypeConverter(typeof(PortNameConverter))]
        [Description("The name of the serial port used to communicate with the manipulator.")]
        public string PortName { get; set; }

        /// <summary>
        /// Gets or sets the device to be controlled. Defaults to 0.
        /// </summary>
        [Description("The axis index to be actuated.")]
        public int Device { get; set; } = 0;

        /// <summary>
        /// Gets or sets the axis of the manipulator to be controlled.
        /// </summary>
        [Description("The index of the axis of the manipulator to be controlled.")]
        public int? Axis { get; set; }

        /// <summary>
        /// Sends a command to the device wihout expecting a reply.
        /// </summary>
        /// <returns>
        /// It will propagate the original sequence.
        /// </returns>
        public override IObservable<string> Process(IObservable<string> source)
        {
            return Observable.Using(
                cancellationToken => ZaberDeviceManager.ReserveConnectionAsync(PortName),
                (connection, cancellationToken) =>
                {
                    return Task.FromResult(source.Do(value =>
                    {
                        lock (connection.Device)
                        {
                            connection.Device.GenericCommandNoResponse(Device, Axis, value);
                        }
                    }));
                });
        }
    }
}
