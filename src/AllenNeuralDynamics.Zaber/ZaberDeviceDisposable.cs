using System;
using System.Threading;
using System.Reactive.Disposables;


namespace AllenNeuralDynamics.Zaber
{
    internal sealed class ZaberDeviceDisposable : ICancelable, IDisposable
    {
        IDisposable resource;

        public ZaberDeviceDisposable(ZaberDevice device, IDisposable disposable)
        {
            Device = device ?? throw new ArgumentNullException(nameof(device));
            resource = disposable ?? throw new ArgumentNullException(nameof(disposable));
        }

        public ZaberDevice Device { get; private set; }

        public bool IsDisposed
        {
            get { return resource == null; }
        }


        public void Dispose()
        {
            var disposable = Interlocked.Exchange(ref resource, null);
            if (disposable != null)
            {
                lock (ZaberDeviceManager.SyncRoot)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}
