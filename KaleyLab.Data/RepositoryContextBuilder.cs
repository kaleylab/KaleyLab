using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaleyLab.Data
{
    public static class RepositoryContextBuilder
    {
        public static IRepositoryContext Build<TRepositoryContext>() where TRepositoryContext : IRepositoryContext,new()
        {
            return new TRepositoryContext();
        }
    }
}
