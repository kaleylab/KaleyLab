using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Data.Entity;

using KaleyLab.Data.EntityFrameworkSample.Models;
using KaleyLab.Data.EntityFrameworkSample.ModelConfigurations;

namespace KaleyLab.Data.EntityFrameworkSample
{
    public class EntityFameworkDbContext : DbContext
    {
        public EntityFameworkDbContext()
            : base("EntityFrameworkSample")
        {
            Database.SetInitializer<EntityFameworkDbContext>(null);
        }


        public DbSet<User> Users { get; set; }
        public DbSet<UserField> UserFields { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Entity Configurations

            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new UserFieldConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
            modelBuilder.Configurations.Add(new UserRoleConfiguration());

            //Conventions
            
        
            base.OnModelCreating(modelBuilder);
        }

    }
}
