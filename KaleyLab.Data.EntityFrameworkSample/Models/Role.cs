using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaleyLab.Data.EntityFrameworkSample.Models
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public string Comment { get; set; }

        public virtual IList<UserRole> Users { get; set; }
    }
}
