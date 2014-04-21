using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaleyLab.Data.EntityFrameworkSample.Models
{
    public class UserField : BaseEntity
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
