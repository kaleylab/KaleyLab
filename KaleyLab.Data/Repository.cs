using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using KaleyLab.Data.Specifications;

namespace KaleyLab.Data
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly IRepositoryContext context;

        public Repository(IRepositoryContext context)
        {
            this.context = context;
        }

        #region Protected Methods

        protected abstract TEntity DoGet(object key);

        protected abstract TEntity DoGet(ISpecification<TEntity> specification);

        protected abstract TEntity DoGet(ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

        protected abstract IEnumerable<TEntity> DoGetAll(ISpecification<TEntity> specification);

        protected abstract IEnumerable<TEntity> DoGetAll(ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties);

        protected abstract IEnumerable<TEntity> DoGetAll(ISpecification<TEntity> specification, params Order<TEntity>[] orderBys);

        protected abstract IEnumerable<TEntity> DoGetAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties, params Order<TEntity>[] orderBys);

        protected abstract PagedResult<TEntity> DoGetAll(ISpecification<TEntity> specification, int pageNumber, int pageSize, params Order<TEntity>[] orderBys);

        protected abstract PagedResult<TEntity> DoGetAll(ISpecification<TEntity> specification, int pageNumber, int pageSize,  Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties,params Order<TEntity>[] orderBys);

        protected abstract bool DoExists(ISpecification<TEntity> specification);

        protected abstract void DoAdd(TEntity entity);

        protected abstract void DoUpdate(TEntity entity);

        protected abstract void DoRemove(TEntity entity);

        #endregion

        #region IRepository<TEntity>

        public IRepositoryContext Context
        {
            get { return this.context; }
        }

        public TEntity Get(object keyValue)
        {
            return this.DoGet(keyValue);
        }

        public TEntity Get(ISpecification<TEntity> specification)
        {
            return this.DoGet(specification);
        }

        public TEntity Get(ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
           return  this.DoGet(specification, eagerLoadingProperties);
        }

        public IEnumerable<TEntity> GetAll(ISpecification<TEntity> specification)
        {
            return this.DoGetAll(specification);
        }

        public IEnumerable<TEntity> GetAll(ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            return this.DoGetAll(specification, eagerLoadingProperties);
        }

        public IEnumerable<TEntity> GetAll(ISpecification<TEntity> specification, params Order<TEntity>[] orderBys)
        {
            return this.DoGetAll(specification,orderBys);
        }

        public IEnumerable<TEntity> GetAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties, params Order<TEntity>[] orderBys)
        {
            return this.DoGetAll(specification, eagerLoadingProperties, orderBys);
        }

        public PagedResult<TEntity> GetAll(ISpecification<TEntity> specification, int pageNumber, int pageSize, params Order<TEntity>[] orderBys)
        {
            return this.DoGetAll(specification, pageNumber,pageSize,orderBys);
        }

        public PagedResult<TEntity> GetAll(ISpecification<TEntity> specification, int pageNumber, int pageSize, Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties, params Order<TEntity>[] orderBys)
        {
            return this.DoGetAll(specification, pageNumber, pageSize, eagerLoadingProperties, orderBys);
        }

        public bool Exists(ISpecification<TEntity> specification)
        {
            return this.DoExists(specification);
        }

        public void Add(TEntity entity)
        {
            this.DoAdd(entity);
        }

        public void Update(TEntity entity)
        {
            this.DoUpdate(entity);
        }

        public void Remove(TEntity entity)
        {
            this.DoRemove(entity);
        }

        #endregion

    }
}
