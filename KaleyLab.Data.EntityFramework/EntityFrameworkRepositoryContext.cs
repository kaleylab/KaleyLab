using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data.Entity;

using KaleyLab.Data;

namespace KaleyLab.Data.EntityFramework
{
    public class EntityFrameworkRepositoryContext<TDbContext>: RepositoryContext, IEntityFrameworkRepositoryContext where TDbContext : DbContext,new()
    {
        private readonly ThreadLocal<DbContext> localContext;

        public EntityFrameworkRepositoryContext()
        {
            this.localContext = new ThreadLocal<DbContext>(() => { return new TDbContext(); });
        }

        #region IRepositoryContext

        public override void RegisterNew<TEntity>(TEntity entity)
        {
            this.localContext.Value.Entry<TEntity>(entity).State = System.Data.EntityState.Added;
            this.Committed = false;
        }

        public override void RegisterModified<TEntity>(TEntity entity)
        {
            this.localContext.Value.Entry<TEntity>(entity).State = System.Data.EntityState.Modified;
            this.Committed = false;
        }

        public override void RegisterDeleted<TEntity>(TEntity entity)
        {
            this.localContext.Value.Entry<TEntity>(entity).State = System.Data.EntityState.Deleted;
            this.Committed = false;
        }     
 
        #endregion

        #region IUnitOfWork

        public override void Commit()
        {
            if (!Committed)
            {
                localContext.Value.SaveChanges();
                Committed = true;
            }
        }

        public override void Rollback()
        {
            this.Committed = false;
        }

        public override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!this.Committed)
                {
                    this.Commit();
                }
                this.localContext.Value.Dispose();
                this.localContext.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion

        #region IEntityFrameworkRepositoryContext

        public System.Data.Entity.DbContext Context
        {
            get { return this.localContext.Value; }
        }

        public void RegisterUnCommittedState()
        {
            this.Committed = false;
        }

        #endregion
    }
}
