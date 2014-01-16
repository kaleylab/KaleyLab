using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaleyLab.Data.Specifications
{
    public class AnySpecification<T> : Specification<T>
    {
        public override System.Linq.Expressions.Expression<Func<T, bool>> IsSatisfiedBy()
        {
            return t => true;
        }
    }
}
