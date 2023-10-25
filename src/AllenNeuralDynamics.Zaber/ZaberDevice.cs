using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using Zaber.Motion;
using Zaber.Motion.Ascii;

namespace AllenNeuralDynamics.Zaber
{
    public sealed class ZaberDevice : IDisposable
    {
        const int deviceIdx = 0;
        readonly Connection comm;
        readonly Device[] devices;
        bool disposed;

        public ZaberDevice(string portName)
        {
            comm = Connection.OpenSerialPort(portName);
            devices = comm.DetectDevices();
        }

        public bool IsOpen
        {
            get { return comm.IsConnected; }
        }

        Task RunAsync(CancellationToken cancellationToken)
        {
            var devices = comm.DetectDevices();
            Thread.Sleep(2000);
            return Task.Factory.StartNew(() =>
            {
                using var cancellation = cancellationToken.Register(comm.Dispose);
                while (!cancellationToken.IsCancellationRequested)
                {
                }
            },
            cancellationToken,
            TaskCreationOptions.LongRunning,
            TaskScheduler.Default);
        }

        public void MoveAxis(int axis, int value)
        {
            var thisDevice = devices[deviceIdx];
            var thisAxis = thisDevice.GetAxis(axis);
            thisAxis.MoveAbsoluteAsync(value, waitUntilIdle:false);
        }

        public void HomeAxis(int axis)
        {
            var thisDevice = devices[deviceIdx];
            var thisAxis = thisDevice.GetAxis(axis);
            thisAxis.HomeAsync(false);
        }


        public void Open(CancellationToken cancellationToken = default)
        {
            RunAsync(cancellationToken);
        }

        public void Close()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    comm.Close();
                    disposed = true;
                }
            }
        }

        void IDisposable.Dispose()
        {
            Close();
        }
    }
}
