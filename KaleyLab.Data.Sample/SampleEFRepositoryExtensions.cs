using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KaleyLab.Data.EntityFramework;

namespace KaleyLab.Data.Sample
{
    public static class SampleEFRepositoryExtensions 
    {
        public static void ApplyCurrentValues<TEntity>(this IRepository<TEntity> repository, TEntity entity) where TEntity : EntityBase
        {
            if (entity == null) { throw new ArgumentException("entity instance is not assigned."); }
            if (entity.Id == Guid.Empty) { throw new ArgumentException("entity id is not assigned."); }

            EntityFrameworkRepository<TEntity, SampleEFDbContext> efRepository = repository as EntityFrameworkRepository<TEntity, SampleEFDbContext>;
            EntityFrameworkRepositoryContext<SampleEFDbContext> efContext = efRepository.Context as EntityFrameworkRepositoryContext<SampleEFDbContext>;
            if (efContext.Context.Entry<TEntity>(entity).State == System.Data.EntityState.Detached)
            {
                TEntity attachedEntity = efContext.Context.Set<TEntity>().Find(entity.Id);
                if (attachedEntity == null) { throw new InvalidOperationException("can not found the specified entity."); }

                var attachedEntry = efContext.Context.Entry<TEntity>(attachedEntity);
                attachedEntry.CurrentValues.SetValues(entity);
                efContext.RegisterUnCommittedState();
            }
            else if (efContext.Context.Entry<TEntity>(entity).State == System.Data.EntityState.Modified)
            {
                efRepository.Update(entity);
            }
        }
    }
}
