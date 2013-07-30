using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ImporterTest.Models;

namespace ImporterTest
{
    public class UserImporterResults
    {
        public UserImporterResults()
        {
            Users = new List<User>();
            SkippedRows = new List<string>();
        }

        public List<User> Users { get; set; }
        public List<string> SkippedRows { get; set; }
    }
}
