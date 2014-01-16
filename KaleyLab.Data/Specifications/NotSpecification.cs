using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace KaleyLab.Data.Specifications
{
    public class NotSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> other;

        public NotSpecification(ISpecification<T> other)
        {
            this.other = other;
        }

        public override System.Linq.Expressions.Expression<Func<T, bool>> IsSatisfiedBy()
        {
            return Expression.Lambda<Func<T, bool>>(this.other.IsSatisfiedBy().Body, this.other.IsSatisfiedBy().Parameters);
        }
    }
}
