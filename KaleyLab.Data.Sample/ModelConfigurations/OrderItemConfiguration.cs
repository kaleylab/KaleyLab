using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KaleyLab.Data.Sample.Models;

namespace KaleyLab.Data.Sample.ModelConfigurations
{
    internal class OrderItemConfiguration : EntityConfigurationBase<OrderItem>
    {
        public OrderItemConfiguration()
            : base()
        {
            ToTable("OrderItem");
            HasRequired(i => i.Order).WithMany(o => o.Items).WillCascadeOnDelete();
        }
    }
}
