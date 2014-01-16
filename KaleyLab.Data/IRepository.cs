using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using KaleyLab.Data.Specifications;

namespace KaleyLab.Data
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(object key);
        TEntity Get(ISpecification<TEntity> specification);
        IEnumerable<TEntity> All();
        IEnumerable<TEntity> All(int pageNumber, int pageSize);
        IEnumerable<TEntity> All(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder);
        IEnumerable<TEntity> All(Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder,int pageNumber,int pageSize);
        IEnumerable<TEntity> FindAll(ISpecification<TEntity> specification);
        IEnumerable<TEntity> FindAll(ISpecification<TEntity> specification, int pageNumber, int pageSize);
        IEnumerable<TEntity> FindAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder);
        IEnumerable<TEntity> FindAll(ISpecification<TEntity> specification, Expression<Func<TEntity, dynamic>> sortPredicate, SortOrder sortOrder, int pageNumber, int pageSize);
        bool Exists(ISpecification<TEntity> specification);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
