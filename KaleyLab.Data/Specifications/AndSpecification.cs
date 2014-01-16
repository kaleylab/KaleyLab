using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KaleyLab.Data.Specifications
{
    public class AndSpecification<T> : Specification<T>
    {
 
        private readonly ISpecification<T> left;
        private readonly ISpecification<T> right;

        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            this.left = left;
            this.right = right;
        }

        public override System.Linq.Expressions.Expression<Func<T, bool>> IsSatisfiedBy()
        {
            return this.left.IsSatisfiedBy().AndAlso(this.right.IsSatisfiedBy());
        }
    }
}
