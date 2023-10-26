using System;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Bonsai;


namespace AllenNeuralDynamics.Zaber
{
    [Description("Moves an axis of a Zaber manipulator to a supplied position.")]
    public class MoveAxis : Sink<double>
    {
        [TypeConverter(typeof(PortNameConverter))]
        [Description("The name of the serial port used to communicate with the manipulator.")]
        public string PortName { get; set; }

        [Description("The axis index to be actuated.")]
        public int Axis { get; set; }

        [Description("Optional velocity used to generate the movement.")]
        public double? Velocity { get; set; } = null;

        [Description("Optional acceleration used to generate the movement.")]
        public double? Acceleration { get; set; } = null;
                
        public override IObservable<double> Process(IObservable<double> source)
        {
            return Observable.Using(
                cancellationToken => ZaberDeviceManager.ReserveConnectionAsync(PortName),
                (connection, cancellationToken) =>
                {
                    var axis = Axis;
                    return Task.FromResult(source.Do(value =>
                    {
                        lock (connection.Device)
                        {
                            connection.Device.MoveAxis(axis, value,
                                Velocity.HasValue ? Velocity.Value : 0,
                                Acceleration.HasValue ? Acceleration.Value : 0);
                        }
                    }));
                });
        }
    }
}
