using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KaleyLab.Data.EntityFrameworkSample.Models;

namespace KaleyLab.Data.EntityFrameworkSample.ModelConfigurations
{
    internal class UserRoleConfiguration : RelationEntityConfiguration<UserRole>
    {
        public UserRoleConfiguration()
        {
            this.ToTable("UserRoles");

            this.HasKey(ur => new { ur.UserId, ur.RoleId });

            this.HasRequired(ur => ur.User)
                .WithMany(u => u.Roles)
                .HasForeignKey(ur => ur.UserId)
                .WillCascadeOnDelete();
                
            this.HasRequired(ur => ur.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(ur => ur.RoleId)
                .WillCascadeOnDelete();
        }
    }
}
