using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KaleyLab.Data;
using KaleyLab.Data.EntityFramework;

namespace KaleyLab.Data.EntityFrameworkSample
{
    public class BaseEntityRepository<TEntity> : EntityFrameworkRepository<TEntity,EntityFameworkDbContext> where TEntity : BaseEntity
    {

        public BaseEntityRepository(IRepositoryContext context)
            : base(context)
        {
        }

        protected override System.Linq.Expressions.Expression<Func<TEntity, bool>> KeyPredicate(object keyValue)
        {
            return e => e.Id == (Guid)keyValue;
        }

        protected virtual void ApplyCurrentValues(TEntity entity)
        {
            if (entity == null) { throw new ArgumentNullException("entity"); }
            if (entity.Id == Guid.Empty) { throw new ArgumentException("Entity Id should not be empty."); }

            if (this.EfContext.Context.Entry<TEntity>(entity).State == System.Data.EntityState.Detached)
            {
                TEntity attachedEntity = this.EfContext.Context.Set<TEntity>().Find(entity.Id);
                if (attachedEntity == null) { throw new InvalidOperationException("Can't found the specified entity."); }

                var attachedEntry = this.EfContext.Context.Entry<TEntity>(attachedEntity);
                attachedEntry.CurrentValues.SetValues(entity);
                this.EfContext.RegisterUnCommittedState();
            }//For inner function testing
            else if (this.EfContext.Context.Entry<TEntity>(entity).State == System.Data.EntityState.Modified)
            {
                this.Update(entity);
            }
        }
    }
}
