using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KaleyLab.Data.Sample.Models;

namespace KaleyLab.Data.Sample.ModelConfigurations
{
    internal class OrderConfiguration : EntityConfigurationBase<Order>
    {
        public OrderConfiguration()
            : base()
        {
            ToTable("Order");
        }
    }
}
