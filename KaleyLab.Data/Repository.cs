using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;



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

        protected abstract TEntity DoGet(Specifications.ISpecification<TEntity> specification);

        protected virtual IEnumerable<TEntity> DoAll()
        {
            return this.DoFindAll(new Specifications.AnySpecification<TEntity>());
        }

        protected virtual IEnumerable<TEntity> DoAll(int pageNumber, int pageSize)
        {
            return this.DoFindAll(new Specifications.AnySpecification<TEntity>(), pageNumber, pageSize);
        }

        protected abstract IEnumerable<TEntity> DoAll(System.Linq.Expressions.Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder);

        protected abstract IEnumerable<TEntity> DoAll(System.Linq.Expressions.Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder,int pageNumber,int pageSize);

        protected abstract IEnumerable<TEntity> DoFindAll(Specifications.ISpecification<TEntity> specification);

        protected abstract IEnumerable<TEntity> DoFindAll(Specifications.ISpecification<TEntity> specification, int pageNumber, int pageSize);

        protected abstract IEnumerable<TEntity> DoFindAll(Specifications.ISpecification<TEntity> specification, System.Linq.Expressions.Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder);

        protected abstract IEnumerable<TEntity> DoFindAll(Specifications.ISpecification<TEntity> specification, System.Linq.Expressions.Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder,int pageNumber,int pageSize);

        protected abstract bool DoExists(Specifications.ISpecification<TEntity> specification);

        protected abstract void DoAdd(TEntity entity);

        protected abstract void DoUpdate(TEntity entity);

        protected abstract void DoRemove(TEntity entity);

        #endregion

        #region IRepository<TEntity>

        public IRepositoryContext Context
        {
            get { return this.context; }
        }

        public TEntity Get(object key)
        {
            return this.DoGet(key);
        }

        public TEntity Get(Specifications.ISpecification<TEntity> specification)
        {
            return this.DoGet(specification);
        }

        public IEnumerable<TEntity> All()
        {
            return this.DoAll();
        }

        public IEnumerable<TEntity> All(int pageNumber, int pageSize)
        {
            return this.DoAll(pageNumber, pageSize);
        }

        public IEnumerable<TEntity> All(System.Linq.Expressions.Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return this.DoAll(sortPredicate, sortOrder);
        }

        public IEnumerable<TEntity> All(System.Linq.Expressions.Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return this.DoAll(sortPredicate, sortOrder, pageNumber, pageSize);
        }

        public IEnumerable<TEntity> FindAll(Specifications.ISpecification<TEntity> specification, System.Linq.Expressions.Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            return this.DoFindAll(specification, sortPredicate, sortOrder);
        }

        public IEnumerable<TEntity> FindAll(Specifications.ISpecification<TEntity> specification, System.Linq.Expressions.Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            return this.DoFindAll(specification, sortPredicate, sortOrder,pageNumber,pageSize);
        }

        public IEnumerable<TEntity> FindAll(Specifications.ISpecification<TEntity> specification)
        {
            return this.DoFindAll(specification);
        }

        public IEnumerable<TEntity> FindAll(Specifications.ISpecification<TEntity> specification, int pageNumber, int pageSize)
        {
            return this.DoFindAll(specification, pageNumber, pageSize);
        }

        public bool Exists(Specifications.ISpecification<TEntity> specification)
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
