using System;
using System.Reactive;
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
        private Connection comm;
        private Device[] devices;
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

        public void MoveAbsolute(int? deviceIndex, int axis, double position, double velocity, double acceleration, Units unit, Units velocityUnit, Units accelerationUnit)
        {
            Axis thisAxis = devices[deviceIndex.HasValue ? deviceIndex.Value : 0].GetAxis(axis);
            thisAxis.MoveAbsoluteAsync(
                position:position,
                velocity:velocity,
                acceleration:acceleration,
                waitUntilIdle:false,
                unit: unit, velocityUnit: velocityUnit, accelerationUnit: accelerationUnit);
        }

        public void MoveRelative(int? deviceIndex, int axis, double position, double velocity, double acceleration, Units unit, Units velocityUnit, Units accelerationUnit)
        {
            Axis thisAxis = devices[deviceIndex.HasValue ? deviceIndex.Value : 0].GetAxis(axis);
            thisAxis.MoveRelativeAsync(
                position: position,
                velocity: velocity,
                acceleration: acceleration,
                waitUntilIdle: false,
                unit: unit, velocityUnit: velocityUnit, accelerationUnit: accelerationUnit);
        }

        public void MoveVelocity(int? deviceIndex, int axis, double velocity, double acceleration, Units velocityUnit, Units accelerationUnit)
        {
            Axis thisAxis = devices[deviceIndex.HasValue ? deviceIndex.Value : 0].GetAxis(axis);
            thisAxis.MoveVelocityAsync(
                velocity: velocity,
                acceleration: acceleration,
                unit: velocityUnit, accelerationUnit: accelerationUnit);
        }
        public void Stop(int? deviceIndex, int? axis)
        {
            var device = devices[deviceIndex.HasValue ? deviceIndex.Value : 0];
            _ = axis.HasValue ? device.GetAxis(axis.Value).StopAsync(false) : device.AllAxes.StopAsync(false);
        }

        public void Park(int? deviceIndex, int? axis)
        {
            var device = devices[deviceIndex.HasValue ? deviceIndex.Value : 0];
            _ = axis.HasValue ? device.GetAxis(axis.Value).ParkAsync() : device.AllAxes.ParkAsync();
        }

        public void Unpark(int? deviceIndex, int? axis)
        {
            var device = devices[deviceIndex.HasValue ? deviceIndex.Value : 0];
            _ = axis.HasValue ? device.GetAxis(axis.Value).UnparkAsync() : device.AllAxes.UnparkAsync();
        }

        public void Home(int? deviceIndex, int? axis)
        {
            var device = devices[deviceIndex.HasValue ? deviceIndex.Value : 0];
            _ = axis.HasValue ? device.GetAxis(axis.Value).HomeAsync(false) : device.AllAxes.HomeAsync(false);
        }

        public void GenericCommandNoResponse(int? deviceIndex, int? axis, string command)
        {
            comm.GenericCommandNoResponseAsync(command,
                deviceIndex.HasValue ? deviceIndex.Value : 0,
                axis.HasValue? axis.Value : 0);
        }

        public async Task<double> GetPosition(int? deviceIndex, int axis, Units unit)
        {
            Axis thisAxis = devices[deviceIndex.HasValue ? deviceIndex.Value : 0].GetAxis(axis);
            return await thisAxis.GetPositionAsync(unit:unit);
        }

        public async Task<bool> IsBusy(int? deviceIndex, int? axis)
        {
            var device = devices[deviceIndex.HasValue ? deviceIndex.Value : 0];
            if (axis.HasValue)
            {
                return await device.GetAxis(axis.Value).IsBusyAsync();
            }
            else
            {
                return await device.AllAxes.IsBusyAsync();
            }
        }

        public async Task<Unit> WaitUntilIdle(int? deviceIndex, int? axis)
        {
            var device = devices[deviceIndex.HasValue ? deviceIndex.Value : 0];
            if (axis.HasValue){
                await device.GetAxis(axis.Value).WaitUntilIdleAsync();
            }
            else
            {
                await device.AllAxes.WaitUntilIdleAsync();
            }
            return new Unit();
        }

        public async Task<Response[]> GenericCommandMultiResponse(int? deviceIndex, int? axis, string command)
        {
                return await comm.GenericCommandMultiResponseAsync(
                    command,
                    deviceIndex.HasValue ? deviceIndex.Value : 0,
                    axis.HasValue ? axis.Value : 0);
        }

        public async Task<Response> GenericCommand(int? deviceIndex, int? axis, string command)
        {
            return await comm.GenericCommandAsync(
                command,
                deviceIndex.HasValue ? deviceIndex.Value : 0,
                axis.HasValue ? axis.Value : 0);
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
