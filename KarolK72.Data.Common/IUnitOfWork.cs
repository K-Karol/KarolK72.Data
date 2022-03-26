﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KarolK72.Data.Common
{
    public interface IUnitOfWork<T> : IDisposable where T : IDisposable
    {
        T Work { get; }
        /// <summary>
        /// Synchronous save operation on the DbContext.
        /// </summary>
        void Save();
        /// <summary>
        /// Asynchronous save operation on the DbContext.
        /// </summary>
        Task SaveAsync();
    }
}
