using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaleyLab.Data
{
    public interface IUnitOfWork
    {
        bool Committed { get; }

        void Commit();

        void Rollback();
    }
}
