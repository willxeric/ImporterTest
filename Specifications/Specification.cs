using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImporterTest.Specifications
{
    public abstract class Specification<T>
    {
        public abstract bool IsSatisfiedBy(T subject);
    }
}
