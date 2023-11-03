using System;
using System.ComponentModel;
using System.Reactive.Linq;
using Bonsai;


namespace AllenNeuralDynamics.Zaber
{
    /// <summary>
    /// Represents an operator queries the current state of a <see cref="ZaberDevice"/> axis.
    /// </summary>
    [Description("Reports the movement state of the manipulator")]
    public class IsBusy : Source<bool>
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
        /// Queries the current state of an axis from the manipulator.
        /// </summary>
        /// <returns>
        /// When subscribed, it will emit a sequence with a single value of the most axis state.
        /// </returns>
        public override IObservable<bool> Generate()
        {
            return Observable.Using(
                async token => await ZaberDeviceManager.ReserveConnectionAsync(PortName),
                async (connection, cancellationToken) => Observable.Return(
                    await connection.Device.IsBusy(Axis)
                    ));
        }

        /// <summary>
        /// Queries the current state of an axis from the manipulator on each event.
        /// </summary>
        /// <returns>
        /// On each event, it will emit a value with a single value of the most recent axis state.
        /// </returns>
        public IObservable<bool> Generate<TSource>(IObservable<TSource> source)
        {
            return Observable.Using(
                async token => await ZaberDeviceManager.ReserveConnectionAsync(PortName),
                async (connection, cancellationToken) => source.Select( _ =>
                    Observable.FromAsync( async token =>
                        await connection.Device.IsBusy(Axis)
                    )).Concat());
        }
    }
}
