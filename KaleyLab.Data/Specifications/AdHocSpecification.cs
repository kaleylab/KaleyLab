using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace KaleyLab.Data.Specifications
{
    public class AdHocSpecification<T> : Specification<T>
    {
        private readonly Expression<Func<T, bool>> predicate;

        public AdHocSpecification(Expression<Func<T, bool>> predicate)
        {
            this.predicate = predicate;
        }

        public override System.Linq.Expressions.Expression<Func<T, bool>> IsSatisfiedBy()
        {
            return this.predicate;
        }
    }
}
