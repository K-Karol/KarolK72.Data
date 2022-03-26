using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarolK72.Data.Common
{
    public class UnitOfWorkFactory<T> : IUnitOfWorkFactory<T> where T : IDisposable
    {
        /// <summary>
        /// Implementation factory
        /// </summary>
        private Func<T> _factory;
        private Func<T, IDisposable> _workDisposable;
        private Action<IDisposable> _saveDisposable;
        private Func<IDisposable, Task> _saveAsyncDisposable;

        public UnitOfWorkFactory(Func<T> implementationFactory, Func<T, IDisposable> disposableFactory, Action<IDisposable> saveDisposable = null, Func<IDisposable, Task> saveAsyncDisposable = null)
        {
            _factory = implementationFactory;
            _workDisposable = disposableFactory;
            _saveDisposable = saveDisposable;
            _saveAsyncDisposable = saveAsyncDisposable;

            if(_saveDisposable == null && _saveAsyncDisposable == null)
            {
                throw new ArgumentNullException("At least 1 save disposable action needs to be provided");
            }
        }

        public IUnitOfWork<T> CreateNew()
        {
            T t = _factory.Invoke();
            return new UnitOfWork<T>(t, _workDisposable.Invoke(t),_saveDisposable,_saveAsyncDisposable);
        }
    }
}
