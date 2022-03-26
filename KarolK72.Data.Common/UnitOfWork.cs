using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarolK72.Data.Common
{
    public class UnitOfWork<T> : IUnitOfWork<T> where T : IDisposable
    {
        private bool disposedValue;

        private T _work;
        public T Work => _work;
        private IDisposable _disposable;
        private Action<IDisposable> _saveDisposable;
        private Func<IDisposable, Task> _saveAsyncDisposable;
        public UnitOfWork(T workObj, IDisposable disposable, Action<IDisposable> saveDisposable, Func<IDisposable, Task> saveAsyncDisposable)
        {
            _work = workObj;
            _disposable = disposable;
            _saveDisposable = saveDisposable;
            _saveAsyncDisposable = saveAsyncDisposable;
        }
        public void Save()
        {
            _saveDisposable.Invoke(_disposable);
        }

        public async Task SaveAsync()
        {
            await _saveAsyncDisposable.Invoke(_disposable);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _disposable?.Dispose();
                    _work?.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
