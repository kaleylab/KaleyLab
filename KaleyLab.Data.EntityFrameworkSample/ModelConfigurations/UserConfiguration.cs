using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KaleyLab.Data.EntityFrameworkSample.Models;

namespace KaleyLab.Data.EntityFrameworkSample.ModelConfigurations
{
    internal class UserConfiguration : BaseEntityConfiguration<User>
    {
        public UserConfiguration()
            : base()
        {
            this.ToTable("User");

            this.HasMany(u => u.Fields)
                .WithRequired(f => f.User)
                .HasForeignKey(f => f.UserId)
                .WillCascadeOnDelete();

        }
    }
}
