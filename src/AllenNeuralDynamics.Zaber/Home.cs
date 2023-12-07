using System;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Bonsai;


namespace AllenNeuralDynamics.Zaber
{
    /// <summary>
    /// Represents an operator that instructs a <see cref="ZaberDevice"/> to perform a homing routine.
    /// </summary>
    [Description("Homes the target axis of the manipulator.")]
    public class Home : Sink
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
        [Description("The index of the axis of the manipulator to be controlled. If left null, it will home all axes.")]
        public int? Axis { get; set; } = null;

        /// <summary>
        /// Homes the target axis when an event is received.
        /// </summary>
        /// <returns>
        /// Returns the original input sequence.
        /// </returns>
        public override IObservable<TSource> Process<TSource>(IObservable<TSource> source)
        {
            return Observable.Using(
                cancellationToken => ZaberDeviceManager.ReserveConnectionAsync(PortName),
                (connection, cancellationToken) =>
                {
                    var axis = Axis;
                    return Task.FromResult(source.Do(_ =>
                    {
                        lock (connection.Device)
                        {
                            connection.Device.Home(axis);
                        }
                    }));
                });
        }
    }
}
