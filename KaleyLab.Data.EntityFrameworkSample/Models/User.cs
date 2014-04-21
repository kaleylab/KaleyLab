using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaleyLab.Data.EntityFrameworkSample.Models
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Comment { get; set; }

        public virtual IList<UserRole> Roles { get; set; }
        public virtual IList<UserField> Fields { get; set; }
        
    }
}
