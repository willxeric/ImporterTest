using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImporterTest.Specifications
{
    public class HasValidGenderSpec : Specification<Dictionary<string, string>>
    {
        public override bool IsSatisfiedBy(Dictionary<string, string> subject)
        {
            var satisfied = false;
            var gender = subject["Gender"];

            var validGenders = Enum.GetValues(typeof(Gender));
            foreach (Gender g in validGenders)
            {
                if (g.ToString().ToLower() == gender.ToLower())
                {
                    satisfied = true;
                }
            }

            return satisfied;
        }
    }
}
