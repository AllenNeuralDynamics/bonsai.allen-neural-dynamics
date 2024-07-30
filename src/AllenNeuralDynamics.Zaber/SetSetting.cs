using System;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Bonsai;
using Zaber.Motion;


namespace AllenNeuralDynamics.Zaber
{
    /// <summary>
    /// Represents an operator that sets a setting for a specified axis.
    /// </summary>
    [Description("Sets an axis setting.")]
    public class SetSetting : Sink<double>
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
        public int? Device { get; set; }

        /// <summary>
        /// Gets or sets the axis of the manipulator to be controlled.
        /// </summary>
        [Description("The axis index to be actuated.")]
        public int Axis { get; set; }

        /// <summary>
        /// Gets or sets the axis setting to be set.
        /// </summary>
        [Description("The setting to get.")]
        public string Setting { get; set; }


        /// <summary>
        /// Gets or sets the Units of the transaction.
        /// </summary>
        [Description("The setting to get.")]
        public Units Units { get; set; } = Units.Native;

        /// <summary>
        /// Moves to the target absolute position when a valid value is received.
        /// </summary>
        /// <returns>
        /// Returns the original input sequence.
        /// </returns>
        public override IObservable<double> Process(IObservable<double> source)
        {
            return Observable.Using(
                cancellationToken => ZaberDeviceManager.ReserveConnectionAsync(PortName),
                (connection, cancellationToken) =>
                {
                    return Task.FromResult(source.Do(value =>
                    {
                        lock (connection.Device)
                        {
                            connection.Device.SetSetting(
                                Device,
                                Axis,
                                Setting,
                                value,
                                Units
                                );
                        }
                    }));
                });
        }
    }
}
