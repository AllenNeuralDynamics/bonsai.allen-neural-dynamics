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
        private Connection comm;
        private Device[] devices;
        private Device device;
        bool disposed;
        string portName;

        public ZaberDevice(string portName)
        {
            this.portName = portName;
        }

        public bool IsOpen
        {
            get { return comm.IsConnected; }
        }

        Task RunAsync(CancellationToken cancellationToken)
        {
            comm = Connection.OpenSerialPort(portName);
            devices = comm.DetectDevices();
            device = devices[deviceIdx];
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

        public void MoveAxis(int axis, double position, double velocity, double acceleration)
        {
            var thisAxis = device.GetAxis(axis);
            thisAxis.MoveAbsoluteAsync(
                position:position,
                velocity:velocity,
                acceleration:acceleration,
                waitUntilIdle:false);
        }

        public void MoveRelativeAxis(int axis, double position, double velocity, double acceleration)
        {
            var thisAxis = device.GetAxis(axis);
            thisAxis.MoveRelativeAsync(
                position: position,
                velocity: velocity,
                acceleration: acceleration,
                waitUntilIdle: false);
        }

        public void StopAxis(int axis)
        {
            var thisAxis = device.GetAxis(axis);
            thisAxis.StopAsync(false);
        }

        public void HomeAxis(int axis)
        {
            var thisAxis = device.GetAxis(axis);
            thisAxis.HomeAsync(waitUntilIdle: false);
        }

        public void MoveVelocityAxis(int axis, double velocity, double acceleration)
        {
            var thisAxis = device.GetAxis(axis);
            thisAxis.MoveVelocityAsync(
                velocity: velocity,
                acceleration: acceleration);
        }

        public async Task<double> GetPosition(int axis)
        {
            var thisAxis = device.GetAxis(axis);
            return await thisAxis.GetPositionAsync();
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
