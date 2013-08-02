using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ImporterTest.Specifications
{
    public class HasValidLastNameSpec : Specification<Dictionary<string, string>>
    {
        public override bool IsSatisfiedBy(Dictionary<string, string> subject)
        {
            bool satisfied = true;
            string lastName = subject["LastName"];

            if (string.IsNullOrEmpty(lastName) || !Regex.Match(lastName.ToLower().Trim(), @"^[a-z ]*$").Success)
            {
                satisfied = false;
            }

            return satisfied;
        }
    }
}
