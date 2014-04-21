using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KaleyLab.Data;
using KaleyLab.Data.EntityFramework;

namespace KaleyLab.Data.Sample
{
    public class SampleEFRepository<TEntity> : EntityFrameworkRepository<TEntity,SampleEFDbContext> where TEntity : EntityBase
    {
        public SampleEFRepository(IRepositoryContext context)
            : base(context)
        {

        }

        protected override System.Linq.Expressions.Expression<Func<TEntity, bool>> KeyPredicate(object keyValue)
        {
            return e => e.Id == (Guid)keyValue;
        }
    }
}
