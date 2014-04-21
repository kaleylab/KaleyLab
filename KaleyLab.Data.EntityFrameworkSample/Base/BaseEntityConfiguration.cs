using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Entity.ModelConfiguration;

namespace KaleyLab.Data.EntityFrameworkSample
{
    internal class BaseEntityConfiguration<TEntity> : EntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public BaseEntityConfiguration()
        {
            this.HasKey(e => e.Id);
        }
    }
}
