using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImporterTest.Specifications
{
    public class HasValidDateSpec : Specification<Dictionary<string,string>>
    {
        public override bool IsSatisfiedBy(Dictionary<string,string> subject)
        {
            var satisfied = true;
            DateTime date;
            string userDate = subject["Date"];
            if (!DateTime.TryParse(userDate, out date))
            {
                satisfied = false;
            }

            return satisfied;
        }
    }
}
