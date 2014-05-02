using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace KaleyLab.Data
{
    public abstract class RepositoryContext : DisposableObj, IRepositoryContext
    {
        private readonly ThreadLocal<bool> localCommitted;

        public RepositoryContext()
        {
            this.localCommitted =  new ThreadLocal<bool>(() => true);
        }

        #region IRepositoryContext

        public virtual void RegisterNew<TEntity>(TEntity entity) where TEntity : class
        {
            //DON'T NEED TO DO ANYTHING
        }

        public virtual void RegisterModified<TEntity>(TEntity entity) where TEntity : class
        {
            //DON'T NEED TO DO ANYTHING
        }

        public virtual void RegisterDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            //DON'T NEED TO DO ANYTHING
        }

        #endregion

        #region IUnitOfWork

        public bool Committed
        {
            get { return this.localCommitted.Value; }
            protected set { this.localCommitted.Value = value; }
        }

        public abstract void Commit();

        public abstract void Rollback();

        public override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.localCommitted.Dispose();
            }
        }

        #endregion

    }
}
