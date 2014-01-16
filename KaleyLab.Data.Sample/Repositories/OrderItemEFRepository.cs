using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KaleyLab.Data;
using KaleyLab.Data.Sample.Models;

namespace KaleyLab.Data.Sample.Repositories
{
    public class OrderItemEFRepository : SampleEFRepository<OrderItem>
    {
        public OrderItemEFRepository(IRepositoryContext context)
            : base(context)
        {
        }
    }
}
