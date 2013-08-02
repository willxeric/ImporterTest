using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Should;
using Xunit;

namespace ImporterTest.Tests
{
    public class VerificationTests
    {
        [Fact]
        public void Importer_should_work()
        {
            var importer = new UserImporter();
            var results = importer.Import(@"..\..\Data\Verification.csv");
            Assert.NotNull(results);

            var users = results.Users;
            var skippedRows = results.SkippedRows;

            Assert.NotNull(users);
            Assert.NotNull(skippedRows);

            Assert.Equal(4, users.Count);
            Assert.True(users.Any(u => u.FirstName == "Teal"));
            Assert.True(users.Any(u => u.FirstName == "Dan"));
            Assert.True(users.Any(u => u.FirstName == "Pat"));
            Assert.True(users.Any(u => u.FirstName == "John"));

            Assert.Equal(4, skippedRows.Count);
            Assert.True(skippedRows.Any(row => row.Contains("BadRow")));
            Assert.True(skippedRows.Any(row => row.Contains("missingfirstname")));
            Assert.True(skippedRows.Any(row => row.Contains("invaliddate")));
            Assert.True(skippedRows.Any(row => row.Contains("badgender")));
        }
    }
}