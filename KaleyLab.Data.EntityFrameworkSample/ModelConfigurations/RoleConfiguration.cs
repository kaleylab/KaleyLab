using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KaleyLab.Data.EntityFrameworkSample.Models;

namespace KaleyLab.Data.EntityFrameworkSample.ModelConfigurations
{
    internal class RoleConfiguration : BaseEntityConfiguration<Role>
    {
        public RoleConfiguration()
            : base()
        {
            this.ToTable("Role");
        }
    }
}
