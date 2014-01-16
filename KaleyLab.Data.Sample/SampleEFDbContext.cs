using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

using KaleyLab.Data.Sample.Models;
using KaleyLab.Data.Sample.ModelConfigurations;

namespace KaleyLab.Data.Sample
{
    public class SampleEFDbContext : DbContext
    {
        public SampleEFDbContext()
            : base("SampleEFDbContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new OrderConfiguration());
            modelBuilder.Configurations.Add(new OrderItemConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
    }
}
