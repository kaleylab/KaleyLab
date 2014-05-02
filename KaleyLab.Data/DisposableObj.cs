using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaleyLab.Data
{
    public abstract class DisposableObj : IDisposable
    {
        ~DisposableObj()
        {
            this.Dispose(false);
        }

        public abstract void Dispose(bool disposing);

        public void ExplicitDispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose()
        {
            this.ExplicitDispose();
        }
    }
}
