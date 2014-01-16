using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaleyLab.Data.Sample.Models
{
    public class Order : EntityBase
    {
        public string OrderNo { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
        public string Comment { get; set; }
        public virtual IList<OrderItem> Items { get; set; }
    }
}
