using System;
using System.ComponentModel;
using System.Reactive.Linq;
using Bonsai;
using Zaber.Motion.Ascii;


namespace AllenNeuralDynamics.Zaber
{
    /// <summary>
    /// Represents an operator that sends a command to a <see cref="ZaberDevice"/> expecting multiple <see cref="Response[]"/>.
    /// </summary>
    [Description("Returns the result of a command.")]
    public class GenericCommandMultiResponse : Combinator<string, Response[]>
    {
        /// <summary>
        /// Gets or sets the COM port or alias of the target <see cref="ZaberDevice"/>
        /// </summary>
        [TypeConverter(typeof(PortNameConverter))]
        [Description("The name of the serial port used to communicate with the manipulator.")]
        public string PortName { get; set; }

        /// <summary>
        /// Gets or sets the axis of the manipulator to be controlled.
        /// </summary>
        [Description("The axis index to be actuated.")]
        public int? Axis { get; set; }

        /// <summary>
        /// Sends a command to the device expecting multiple replies.
        /// </summary>
        /// <returns>
        /// On each event, it will emit the response of the device.
        /// </returns>
        public override IObservable<Response[]> Process(IObservable<string> source)
        {
            return Observable.Using(
                async token => await ZaberDeviceManager.ReserveConnectionAsync(PortName),
                async (connection, cancellationToken) => source.Select(value =>
                    Observable.FromAsync( async token =>
                        await connection.Device.GenericCommandMultiResponse(Axis, value)
                    )).Concat());
        }
    }
}
