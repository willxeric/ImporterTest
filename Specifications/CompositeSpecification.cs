using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImporterTest.Specifications
{
    public class CompositeSpecification<T>
    {
        private IList<Specification<T>> specs = new List<Specification<T>>();

        public void AddSpecification(Specification<T> item)
        {
            specs.Add(item);
        }

        public bool IsSatisfiedBy(T obj)
        {
            var satisfied = true;

            foreach (Specification<T> spec in specs) 
            {
                if(!spec.IsSatisfiedBy(obj))
                {
                    satisfied = false;
                }
            }

            return satisfied;
        }
    }
}
