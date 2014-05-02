using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//TODO ITEMS:
//1. Use operator to instead of the And,Or,Not logic.

namespace KaleyLab.Data.Specifications
{
    
    public abstract class Specification<T> : ISpecification<T>
    {
        public ISpecification<T> And(ISpecification<T> other)
        {
            return new AndSpecification<T>(this, other);
        }

        public ISpecification<T> Or(ISpecification<T> other)
        {
            return new OrSpecification<T>(this, other);
        }

        public ISpecification<T> Not()
        {
            return new NotSpecification<T>(this);
        }

        public abstract System.Linq.Expressions.Expression<Func<T, bool>> IsSatisfiedBy();
    }
}
