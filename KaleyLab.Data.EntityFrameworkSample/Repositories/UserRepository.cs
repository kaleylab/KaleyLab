﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using KaleyLab.Data;
using KaleyLab.Data.Specifications;
using KaleyLab.Data.EntityFrameworkSample.Models;

namespace KaleyLab.Data.EntityFrameworkSample.Repositories
{
    public class UserRepository : BaseEntityRepository<User>
    {
        public UserRepository(IRepositoryContext context)
            : base(context)
        {

        }

        public void Test()
        {
            this.Get(new AnySpecification<User>());
        }
    }
}
