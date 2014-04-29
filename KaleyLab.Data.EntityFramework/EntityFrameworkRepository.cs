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

        protected abstract Expression<Func<TEntity, bool>> KeyPredicate(object keyValue);

        protected override TEntity DoGet(object keyValue)
        {
            return this.efContext.Context.Set<TEntity>().Where(KeyPredicate(keyValue)).FirstOrDefault();
        }

        protected override TEntity DoGet(ISpecification<TEntity> specification)
        {
            return this.efContext.Context.Set<TEntity>().AsExpandable().Where(specification.IsSatisfiedBy()).FirstOrDefault();
        }

        protected override TEntity DoGet(ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            var query = this.efContext.Context.Set<TEntity>().AsExpandable();

            if (eagerLoadingProperties != null && eagerLoadingProperties.Length > 0)
            {
                var eagerLoadingProperty = eagerLoadingProperties[0];
                var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                query = query.Include(eagerLoadingPath);
                for (int i = 1; i < eagerLoadingProperties.Length; i++)
                {
                    eagerLoadingProperty = eagerLoadingProperties[i];
                    eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                    query = query.Include(eagerLoadingPath);
                }
            }

            return query.Where(specification.IsSatisfiedBy()).FirstOrDefault();
        }

        protected override IEnumerable<TEntity> DoGetAll(ISpecification<TEntity> specification)
        {
            return this.efContext.Context.Set<TEntity>().AsExpandable().Where(specification.IsSatisfiedBy()).OrderBy(e => 1).ToList();
        }

        protected override IEnumerable<TEntity> DoGetAll(ISpecification<TEntity> specification, params Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties)
        {
            var query = this.efContext.Context.Set<TEntity>().AsExpandable();

            if (eagerLoadingProperties != null && eagerLoadingProperties.Length > 0)
            {
                var eagerLoadingProperty = eagerLoadingProperties[0];
                var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                query = query.Include(eagerLoadingPath);
                for (int i = 1; i < eagerLoadingProperties.Length; i++)
                {
                    eagerLoadingProperty = eagerLoadingProperties[i];
                    eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                    query = query.Include(eagerLoadingPath);
                }
            }

            return query.Where(specification.IsSatisfiedBy()).OrderBy(e => 1).ToList();
        }

        protected override IEnumerable<TEntity> DoGetAll(ISpecification<TEntity> specification, params Order<TEntity>[] orderBys)
        {
            var query = this.efContext.Context.Set<TEntity>().AsExpandable().Where(specification.IsSatisfiedBy());
            
            if (orderBys != null && orderBys.Any())
            {
                var sortOrder = orderBys.First();
                if (sortOrder.Descending)
                {
                    query = query.OrderByDescending(sortOrder);
                }
                else
                {
                    query = query.OrderBy(sortOrder);
                }

                for (int i = 1; i < orderBys.Length; i++)
                {
                    sortOrder = orderBys[i];
                    if (sortOrder.Descending)
                    {
                        query = (query as IOrderedQueryable<TEntity>).ThenByDescending(sortOrder);
                    }
                    else
                    {
                        query = (query as IOrderedQueryable<TEntity>).ThenBy(sortOrder);
                    }

                }

                return query.ToList();
            }

            return query.OrderBy(e => 1).ToList();
        }

        protected override IEnumerable<TEntity> DoGetAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties, params Order<TEntity>[] orderBys)
        {
            var query = this.efContext.Context.Set<TEntity>().AsExpandable();

            if (eagerLoadingProperties != null && eagerLoadingProperties.Length > 0)
            {
                var eagerLoadingProperty = eagerLoadingProperties[0];
                var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                query = query.Include(eagerLoadingPath);
                for (int i = 1; i < eagerLoadingProperties.Length; i++)
                {
                    eagerLoadingProperty = eagerLoadingProperties[i];
                    eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                    query = query.Include(eagerLoadingPath);
                }
            }

            query = query.Where(specification.IsSatisfiedBy());

            if (orderBys != null && orderBys.Any())
            {
                var sortOrder = orderBys.First();
                if (sortOrder.Descending)
                {
                    query = query.OrderByDescending(sortOrder);
                }
                else
                {
                    query = query.OrderBy(sortOrder);
                }

                for (int i = 1; i < orderBys.Length; i++)
                {
                    sortOrder = orderBys[i];
                    if (sortOrder.Descending)
                    {
                        query = (query as IOrderedQueryable<TEntity>).ThenByDescending(sortOrder);
                    }
                    else
                    {
                        query = (query as IOrderedQueryable<TEntity>).ThenBy(sortOrder);
                    }
                }

                return query.ToList();
            }

            return query.OrderBy(e => 1).ToList();
        }

        protected override PagedResult<TEntity> DoGetAll(ISpecification<TEntity> specification, int pageNumber, int pageSize, params Order<TEntity>[] orderBys)
        {
            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "pageSize must  greater than or equals zero.");
            }
            if (pageNumber <= 0)
            {
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "pageNumber must greater than or equals zero.");
            }

            int skip = pageSize * (pageNumber - 1), take = pageSize;
            var query = this.efContext.Context.Set<TEntity>().AsExpandable().Where(specification.IsSatisfiedBy());
            
            var totalRecords = query.Count();
            var pagedData = new List<TEntity>();

            if (orderBys != null && orderBys.Any())
            {
                var sortOrder = orderBys.First();
                if (sortOrder.Descending)
                {
                    query = query.OrderByDescending(sortOrder);
                }
                else
                {
                    query = query.OrderBy(sortOrder);
                }

                for (int i = 1; i < orderBys.Length; i++)
                {
                    sortOrder = orderBys[i];
                    if (sortOrder.Descending)
                    {
                        query = (query as IOrderedQueryable<TEntity>).ThenByDescending(sortOrder);
                    }
                    else
                    {
                        query = (query as IOrderedQueryable<TEntity>).ThenBy(sortOrder);
                    }

                }

                pagedData = query.Skip(skip).Take(take).ToList();
            }
            else
            {
                pagedData = query.OrderBy(e => 1).Skip(skip).Take(take).ToList();
            }
               
            return new PagedResult<TEntity>(totalRecords, pageSize, pageNumber, pagedData);
        }

        protected override PagedResult<TEntity> DoGetAll(ISpecification<TEntity> specification, int pageNumber, int pageSize, Expression<Func<TEntity, dynamic>>[] eagerLoadingProperties, params Order<TEntity>[] orderBys)
        {
            if (pageSize <= 0)
            {
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "pageSize must  greater than or equals zero.");
            }
            if (pageNumber <= 0)
            {
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "pageNumber must greater than or equals zero.");
            }

            int skip = pageSize * (pageNumber - 1), take = pageSize;
            var query = this.efContext.Context.Set<TEntity>().AsExpandable();

            if (eagerLoadingProperties != null && eagerLoadingProperties.Length > 0)
            {
                var eagerLoadingProperty = eagerLoadingProperties[0];
                var eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                query = query.Include(eagerLoadingPath);
                for (int i = 1; i < eagerLoadingProperties.Length; i++)
                {
                    eagerLoadingProperty = eagerLoadingProperties[i];
                    eagerLoadingPath = this.GetEagerLoadingPath(eagerLoadingProperty);
                    query = query.Include(eagerLoadingPath);
                }
            }

            query = query.Where(specification.IsSatisfiedBy());

            var totalRecords = query.Count();
            var pagedData = new List<TEntity>();

            if (orderBys != null && orderBys.Any())
            {
                var sortOrder = orderBys.First();
                if (sortOrder.Descending)
                {
                    query = query.OrderByDescending(sortOrder);
                }
                else
                {
                    query = query.OrderBy(sortOrder);
                }

                for (int i = 1; i < orderBys.Length; i++)
                {
                    sortOrder = orderBys[i];
                    if (sortOrder.Descending)
                    {
                        query = (query as IOrderedQueryable<TEntity>).ThenByDescending(sortOrder);
                    }
                    else
                    {
                        query = (query as IOrderedQueryable<TEntity>).ThenBy(sortOrder);
                    }
                }

                pagedData = query.Skip(skip).Take(take).ToList();
            }
            else
            {
                pagedData = query.OrderBy(e => 1).Skip(skip).Take(take).ToList();
            }

            return new PagedResult<TEntity>(totalRecords, pageSize, pageNumber, pagedData);
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


        private MemberExpression GetMemberInfo(LambdaExpression lambda)
        {
            if (lambda == null)
                throw new ArgumentNullException("method");

            MemberExpression memberExpr = null;

            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpr = ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            if (memberExpr == null)
                throw new ArgumentException("method");

            return memberExpr;
        }

        private string GetEagerLoadingPath(Expression<Func<TEntity, dynamic>> eagerLoadingProperty)
        {
            MemberExpression memberExpression = this.GetMemberInfo(eagerLoadingProperty);
            var parameterName = eagerLoadingProperty.Parameters.First().Name;
            var memberExpressionStr = memberExpression.ToString();
            var path = memberExpressionStr.Replace(parameterName + ".", "");

            return path;
        }

    }
}
