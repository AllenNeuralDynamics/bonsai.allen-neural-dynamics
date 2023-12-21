using System;
using System.ComponentModel;
using System.Reactive.Linq;
using Bonsai;
using Zaber.Motion;


namespace AllenNeuralDynamics.Zaber
{
    /// <summary>
    /// Represents an operator queries the current position of a <see cref="ZaberDevice"/> axis.
    /// </summary>
    [Description("Returns the current position of a selected axis.")]
    public class GetPosition : Source<double>
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
        public int Axis { get; set; }

        /// <summary>
        /// Gets or sets the Units the manipulator instruction is operating on.
        /// </summary>
        [Description("The axis index to be actuated.")]
        public Units Units { get; set; } = Units.Native;

        /// <summary>
        /// Queries the current position of an axis from the manipulator.
        /// </summary>
        /// <returns>
        /// When subscribed, it will emit a sequence with a single value of the most recent axis position.
        /// </returns>
        public override IObservable<double> Generate()
        {
            return Observable.Using(
                async token => await ZaberDeviceManager.ReserveConnectionAsync(PortName),
                async (connection, cancellationToken) => Observable.Return(
                    await connection.Device.GetPosition(Axis, Units)
                    ));
        }

        /// <summary>
        /// Queries the current position of an axis from the manipulator on each event.
        /// </summary>
        /// <returns>
        /// On each event, it will emit a value with a single value of the most recent axis position.
        /// </returns>
        public IObservable<double> Generate<TSource>(IObservable<TSource> source)
        {
            return Observable.Using(
                async token => await ZaberDeviceManager.ReserveConnectionAsync(PortName),
                async (connection, cancellationToken) => source.Select( _ => 
                    Observable.FromAsync( async token =>
                        await connection.Device.GetPosition(Axis, Units)
                    )).Concat());
        }
    }
}
