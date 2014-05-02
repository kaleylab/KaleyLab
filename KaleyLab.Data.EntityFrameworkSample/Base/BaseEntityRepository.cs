using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

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

        protected virtual void ApplyCurrentValues(TEntity entity)
        {
            if (entity == null) { throw new ArgumentNullException("entity"); }
            if (entity.Id == Guid.Empty) { throw new ArgumentException("Entity Id should not be empty."); }

            if (this.EFContext.Context.Entry<TEntity>(entity).State == System.Data.EntityState.Detached)
            {
                TEntity attachedEntity = this.EFContext.Context.Set<TEntity>().Find(entity.Id);
                if (attachedEntity == null) { throw new InvalidOperationException("Can't found the specified entity."); }

                var attachedEntry = this.EFContext.Context.Entry<TEntity>(attachedEntity);
                attachedEntry.CurrentValues.SetValues(entity);
                this.EFContext.RegisterUnCommittedState();
            }//For inner function testing
            else if (this.EFContext.Context.Entry<TEntity>(entity).State == System.Data.EntityState.Modified)
            {
                this.Update(entity);
            }
        }

        protected override Expression<Func<TEntity, bool>> KeyPredicate(object keyValue)
        {
            return e => e.Id == (Guid)keyValue;
        }
    }
}
