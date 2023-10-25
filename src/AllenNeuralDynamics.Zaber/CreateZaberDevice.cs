using System;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using Bonsai;
using Bonsai.IO.Ports;

namespace AllenNeuralDynamics.Zaber
{

    [DefaultProperty(nameof(Name))]
    [Description("Creates a connection to an Zaber manipulator.")]
    public class CreateZaberDevice : Source<ZaberDevice>, INamedElement
    {
        [Description("The optional alias for the manipulator.")]
        public string Name { get; set; }

        [TypeConverter(typeof(SerialPortNameConverter))]
        [Description("The name of the serial port used to communicate with the manipulator.")]
        public string PortName { get; set; }

        public override IObservable<ZaberDevice> Generate()
        {
            return Observable.Using(
                () => ZaberDeviceManager.ReserveConnection(PortName),
                resource =>
                {
                    return Observable.Return(resource.Device)
                                     .Concat(Observable.Never(resource.Device));
                });
        }
    }
}
