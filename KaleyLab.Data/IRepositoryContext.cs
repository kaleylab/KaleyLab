using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaleyLab.Data
{
    public interface IRepositoryContext : IUnitOfWork,IDisposable
    {
        void RegisterNew<TEntity>(TEntity entity) where TEntity : class;
        void RegisterModified<TEntity>(TEntity entity) where TEntity : class;
        void RegisterDeleted<TEntity>(TEntity entity) where TEntity : class;
    }
}
