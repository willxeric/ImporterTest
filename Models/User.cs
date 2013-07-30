using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImporterTest.Models
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public Gender Gender { get; set; }
        public int FavoriteNumber { get; set; }

        public DateTime JoinDate { get; set; }
    }
}