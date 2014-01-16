using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using KaleyLab.Data;
using KaleyLab.Data.LinqKit;
using KaleyLab.Data.Specifications;


namespace KaleyLab.Data.EntityFramework
{
    public abstract class EntityFrameworkRepository<TEntity,TDbContext> : Repository<TEntity> 
        where TEntity : class 
        where TDbContext : DbContext,new()
    {
        private readonly EntityFrameworkRepositoryContext<TDbContext> efContext;

        public EntityFrameworkRepository(IRepositoryContext context)
            : base(context)
        {
            if (context is EntityFrameworkRepositoryContext<TDbContext> )
            {
                this.efContext = context as EntityFrameworkRepositoryContext<TDbContext>;
            }
        }

        protected IEntityFrameworkRepositoryContext EfContext
        {
            get { return this.efContext; }
        }

        protected abstract Expression<Func<TEntity, bool>> IsSatisfiedKey(object key);

        protected override TEntity DoGet(object key)
        {
            return this.efContext.Context.Set<TEntity>().Where(IsSatisfiedKey(key)).FirstOrDefault();
        }

        protected override TEntity DoGet(ISpecification<TEntity> specification)
        {
            return this.efContext.Context.Set<TEntity>().AsExpandable().Where(specification.IsSatisfiedBy()).FirstOrDefault();
        }

        protected override IEnumerable<TEntity> DoAll(System.Linq.Expressions.Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            var query = this.efContext.Context.Set<TEntity>().AsExpandable().Where(new AnySpecification<TEntity>().IsSatisfiedBy());
            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        return query.SortBy(sortPredicate);
                    case SortOrder.Descending:
                        return query.SortByDescending(sortPredicate);
                    default:
                        break;
                }
            }

            return query;
        }

        protected override IEnumerable<TEntity> DoAll(System.Linq.Expressions.Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
            {
                throw new ArgumentException("page number must greater than zero.");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentException("page size must greater than zero.");
            }

            int skip = pageSize * (pageNumber - 1),take = pageSize;
            var query = this.efContext.Context.Set<TEntity>().AsExpandable().Where(new AnySpecification<TEntity>().IsSatisfiedBy());
            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        return query.SortBy(sortPredicate).Skip(skip).Take(take);
                    case SortOrder.Descending:
                        return query.SortByDescending(sortPredicate).Skip(skip).Take(take);
                    default:
                        break;
                }
            }

            return query.Skip(skip).Take(take);

        }

        protected override IEnumerable<TEntity> DoFindAll(ISpecification<TEntity> specification)
        {
            return this.efContext.Context.Set<TEntity>().AsExpandable().Where(specification.IsSatisfiedBy());
        }

        protected override IEnumerable<TEntity> DoFindAll(ISpecification<TEntity> specification, int pageNumber, int pageSize)
        {
            return this.efContext.Context.Set<TEntity>().AsExpandable().Where(specification.IsSatisfiedBy()).Skip(pageSize * (pageNumber - 1)).Take(pageSize);
        }

        protected override IEnumerable<TEntity> DoFindAll(ISpecification<TEntity> specification, System.Linq.Expressions.Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder)
        {
            var query = this.efContext.Context.Set<TEntity>().AsExpandable().Where(specification.IsSatisfiedBy());
            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        return query.SortBy(sortPredicate);
                    case SortOrder.Descending:
                        return query.SortByDescending(sortPredicate);
                    default:
                        break;
                }
            }

            return query;
        }

        protected override IEnumerable<TEntity> DoFindAll(ISpecification<TEntity> specification, System.Linq.Expressions.Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
            {
                throw new ArgumentException("page number must greater than zero.");
            }

            if (pageSize <= 0)
            {
                throw new ArgumentException("page size must greater than zero.");
            }

            int skip = pageSize * (pageNumber - 1), take = pageSize;
            var query = this.efContext.Context.Set<TEntity>().AsExpandable().Where(specification.IsSatisfiedBy());
            if (sortPredicate != null)
            {
                switch (sortOrder)
                {
                    case SortOrder.Ascending:
                        return query.SortBy(sortPredicate).Skip(skip).Take(take);
                    case SortOrder.Descending:
                        return query.SortByDescending(sortPredicate).Skip(skip).Take(take);
                    default:
                        break;
                }
            }

            return query.Skip(skip).Take(take);
        }

        protected override bool DoExists(ISpecification<TEntity> specification)
        {
            return this.efContext.Context.Set<TEntity>().AsExpandable().Any(specification.IsSatisfiedBy()); 
        }

        protected override void DoAdd(TEntity entity)
        {
            this.efContext.RegisterNew<TEntity>(entity);
        }

        protected override void DoUpdate(TEntity entity)
        {
            this.efContext.RegisterModified<TEntity>(entity);
        }

        protected override void DoRemove(TEntity entity)
        {
            this.efContext.RegisterDeleted<TEntity>(entity);
        }


        private object[] GetKeyValues(TEntity entity)
        {
            List<object> keyValues = new List<object>();

            if (this.efContext.Context.Entry(entity).State == System.Data.EntityState.Detached)
            {
                this.efContext.Context.Entry(entity).State = System.Data.EntityState.Unchanged;
            }

            var objectStateEntry = ((IObjectContextAdapter)this.efContext.Context).ObjectContext.ObjectStateManager.GetObjectStateEntry(entity);
            foreach (var item in objectStateEntry.EntityKey.EntityKeyValues)
            {
                keyValues.Add(item.Value);
            }

            //Detach the current object after read the KeyValues,
            //if not it will effect the Apply(T entity) to replace old object.
            this.efContext.Context.Entry(entity).State = System.Data.EntityState.Detached;

            return keyValues.ToArray();
        }

    }
}
