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
        TEntity Get(object keyValue);
        TEntity Get(ISpecification<TEntity> specification);
        IEnumerable<TEntity> GetAll(ISpecification<TEntity> specification);
        IEnumerable<TEntity> GetAll(ISpecification<TEntity> specification, params Order<TEntity>[] orderBys);
        PagedResult<TEntity> GetAll(ISpecification<TEntity> specification, int pageNumber, int pageSize, params Order<TEntity>[] orderBys);
        bool Exists(ISpecification<TEntity> specification);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
