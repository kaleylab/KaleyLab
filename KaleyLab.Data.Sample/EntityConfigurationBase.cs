using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;

namespace KaleyLab.Data.Sample
{
    internal class EntityConfigurationBase<TEntity> : EntityTypeConfiguration<TEntity> where TEntity : EntityBase
    {
        public EntityConfigurationBase()
        {
            HasKey(e => e.Id);
            Property(e => e.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
