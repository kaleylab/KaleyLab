﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaleyLab.Data.EntityFrameworkSample.Models
{
    public class UserRole
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }

        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
    }
}
