using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaleyLab.Data.Sample.Models
{
    public class OrderItem : EntityBase
    {
        public string GoodName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Comment { get; set; }
        public virtual Order Order { get; set; }
    }
}
