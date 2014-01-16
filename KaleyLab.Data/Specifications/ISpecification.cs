using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace KaleyLab.Data.Specifications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> IsSatisfiedBy();
        ISpecification<T> And(ISpecification<T> other);
        ISpecification<T> Or(ISpecification<T> other);
        ISpecification<T> Not();
    }
}
