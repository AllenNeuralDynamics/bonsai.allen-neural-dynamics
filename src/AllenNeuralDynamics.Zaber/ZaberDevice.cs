using System;
using System.Threading;
using System.Threading.Tasks;
using Zaber.Motion;
using Zaber.Motion.Ascii;

namespace AllenNeuralDynamics.Zaber
{
    /// <summary>
    /// Represents an Zaber manipulator communicating with the host computer using the Zaber.Motion SDK.
    /// </summary>
    public sealed class ZaberDevice : IDisposable
    {
        const int deviceIdx = 0;
        private Connection comm;
        private Device[] devices;
        private Device device;
        bool disposed;
        string portName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZaberDevice"/> class using the
        /// specified port name.
        /// </summary>
        /// <param name="portName">The port to use (for example, COM1).</param>
        public ZaberDevice(string portName)
        {
            this.portName = portName;
        }

        public bool IsOpen
        {
            get { return comm.IsConnected; }
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

        public void StopAllAxes()
        {
            device.AllAxes.StopAsync(false);
        }
        
        public void StopAxis(int axis)
        {
            var thisAxis = device.GetAxis(axis);
            thisAxis.StopAsync(false);
        }

        public void HomeAllAxes()
        {
            device.AllAxes.HomeAsync(false);
        }

        public void HomeAxis(int axis)
        {
            var thisAxis = device.GetAxis(axis);
            thisAxis.HomeAsync(false);
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

        public async Task<bool> IsBusy()
        {
            return await device.AllAxes.IsBusyAsync();
        }

        public async Task<System.Reactive.Unit> WaitUntilIdle ()
        {
            await device.AllAxes.WaitUntilIdleAsync();
            return new System.Reactive.Unit();
        }
        /// <summary>
        /// Opens a new connection to a <see cref="ZaberDevice"/>.
        /// </summary>
        /// <param name="cancellationToken">
        /// A <see cref="CancellationToken"/> which can be used to cancel the operation.
        /// </param>
        public void Open()
        {
            comm = Connection.OpenSerialPort(portName);
            devices = comm.DetectDevices();
            device = devices[deviceIdx];
        }

        /// <summary>
        /// Closes the port connection, sets the <see cref="IsOpen"/>
        /// property to <see langword="false"/> and disposes of the
        /// internal <see cref="Connection"/> object.
        /// </summary>
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
                    comm.Dispose();
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
