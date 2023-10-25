using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Reactive.Disposables;
using System.Threading.Tasks;
using System.Threading;

namespace AllenNeuralDynamics.Zaber
{
    internal static class ZaberDeviceManager
    {
        static readonly Dictionary<string, Tuple<ZaberDevice, RefCountDisposable>> openConnections = new();
        internal static readonly object SyncRoot = new();

        public static ZaberDeviceDisposable ReserveConnection(string portName)
        {
            return ReserveConnection(portName, ZaberDeviceConfiguration.Default);
        }

        public static async Task<ZaberDeviceDisposable> ReserveConnectionAsync(string portName)
        {
            return await Task.Run(() => ReserveConnection(portName, ZaberDeviceConfiguration.Default));
        }

        internal static ZaberDeviceDisposable ReserveConnection(string portName, ZaberDeviceConfiguration deviceConfiguration)
        {
            Tuple<ZaberDevice, RefCountDisposable> connection = default;
            lock (SyncRoot)
            {
                if (string.IsNullOrEmpty(portName))
                {
                    if (!string.IsNullOrEmpty(deviceConfiguration.PortName)) portName = deviceConfiguration.PortName;
                    else if (openConnections.Count == 1) connection = openConnections.Values.Single();
                    else throw new ArgumentException("An alias or serial port name must be specified.", nameof(portName));
                }

                if (connection == null && !openConnections.TryGetValue(portName, out connection))
                {
                    var serialPortName = deviceConfiguration.PortName;
                    if (string.IsNullOrEmpty(serialPortName)) serialPortName = portName;

                    var cancellation = new CancellationTokenSource();
                    var device = new ZaberDevice(serialPortName);
                    device.Open(cancellation.Token);
                    var dispose = Disposable.Create(() =>
                    {
                        cancellation.Cancel();
                        openConnections.Remove(portName);
                    });

                    var refCount = new RefCountDisposable(dispose);
                    connection = Tuple.Create(device, refCount);
                    openConnections.Add(portName, connection);
                    return new ZaberDeviceDisposable(device, refCount);
                }

                return new ZaberDeviceDisposable(connection.Item1, connection.Item2.GetDisposable());
            }
        }
    }
}
