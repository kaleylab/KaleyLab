using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;

using KaleyLab.Data;

namespace KaleyLab.Data.EntityFramework
{
    public interface IEntityFrameworkRepositoryContext : IRepositoryContext
    {
        DbContext Context { get; }

        void RegisterUnCommittedState();
    }
}
