using System;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Bonsai;

namespace AllenNeuralDynamics.Zaber
{
    [Description("Stops an axis of a Zaber manipulator.")]
    public class StopAxis : Sink
    {
        [TypeConverter(typeof(PortNameConverter))]
        [Description("The name of the serial port used to communicate with the manipulator.")]
        public string PortName { get; set; }

        [Description("The axis index to be actuated.")]
        public int Axis { get; set; }

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
                            connection.Device.StopAxis(axis);
                        }
                    }));
                });
        }
    }
}
