using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaleyLab.Data
{
    public abstract class UnitOfWorkService
    {
        private readonly IRepositoryContext context;

        public UnitOfWorkService(IRepositoryContext context)
        {
            this.context = context;
        }

        protected IRepositoryContext Context
        {
            get { return this.context; }
        }
    }
}
