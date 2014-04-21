using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;

namespace KaleyLab.Data.EntityFrameworkSample
{
    internal class RelationEntityConfiguration<TEntity> : EntityTypeConfiguration<TEntity> where TEntity : class
    {
        public RelationEntityConfiguration() { }
    }
}
