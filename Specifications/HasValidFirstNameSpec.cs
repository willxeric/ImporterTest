﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ImporterTest.Specifications
{
    public class HasValidFirstNameSpec : Specification<Dictionary<string, string>>
    {
        public override bool IsSatisfiedBy(Dictionary<string, string> subject)
        {
            bool satisfied = true;
            string firstName = subject["FirstName"];

            if (string.IsNullOrEmpty(firstName) || !Regex.Match(firstName.ToLower().Trim(), @"^[a-z ]*$").Success)
            {
                satisfied = false;
            }

            return satisfied;
        }
    }
}
