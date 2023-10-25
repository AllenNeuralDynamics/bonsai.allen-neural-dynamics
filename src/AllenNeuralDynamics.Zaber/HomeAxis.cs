using System;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Bonsai;
using Zaber.Motion;
using Zaber.Motion.Ascii;

namespace AllenNeuralDynamics.Zaber
{

    [Description("Writes the sequence of digital state transitions to the specified Zaber output pin.")]
    public class HomeAxis : Sink<int>
    {

        [TypeConverter(typeof(PortNameConverter))]
        [Description("The name of the serial port used to communicate with the manipulator.")]
        public string PortName { get; set; }


        [Description("The digital output pin number on which to write the state values.")]
        public int Axis { get; set; }
                
  
        public override IObservable<int> Process(IObservable<int> source)
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
                            connection.Device.HomeAxis(axis);
                        }
                    }));
                });
        }
    }
}
