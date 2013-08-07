using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Should;
using Xunit;
using ImporterTest.Specifications;


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

        [Fact]
        public void Should_pass_with_valid_first_name()
        {
            var spec = new HasValidFirstNameSpec();
            var name = new Dictionary<string,string>{{"FirstName","Will"}};
            Assert.True(spec.IsSatisfiedBy(name));
        }

        [Fact]
        public void Should_fail_if_first_name_contains_intergers()
        {
            var spec = new HasValidFirstNameSpec();
            var name = new Dictionary<string, string> { { "FirstName", "1Will2" } };
            Assert.False(spec.IsSatisfiedBy(name));
        }

        [Fact]
        public void Should_fail_if_first_name_contains_special_characters()
        {
            var spec = new HasValidFirstNameSpec();
            var name = new Dictionary<string, string> { { "FirstName", "@Will" } };
            Assert.False(spec.IsSatisfiedBy(name));
        }

        [Fact]
        public void Should_fail_if_first_name_is_empty()
        {
            var spec = new HasValidFirstNameSpec();
            var name = new Dictionary<string, string> { { "FirstName", "" } };
            Assert.False(spec.IsSatisfiedBy(name));
        }

        [Fact]
        public void Should_pass_with_valid_date()
        {
            var spec = new HasValidDateSpec();
            var date = new Dictionary<string, string> { { "Date", "01/12/2012" } };
            Assert.True(spec.IsSatisfiedBy(date));
        }

        [Fact]
        public void Should_fail_for_invalid_month()
        {
            var spec = new HasValidDateSpec();
            var date = new Dictionary<string, string> { { "Date", "13/12/2012" } };
            Assert.False(spec.IsSatisfiedBy(date));
        }

        [Fact]
        public void Should_fail_for_invalid_day()
        {
            var spec = new HasValidDateSpec();
            var date = new Dictionary<string, string> { { "Date", "02/30/2012" } };
            Assert.False(spec.IsSatisfiedBy(date));
        }

        [Fact]
        public void Should_fail_for_invalid_format()
        {
            var spec = new HasValidDateSpec();
            var date = new Dictionary<string, string> { { "Date", "01_2012" } };
            Assert.False(spec.IsSatisfiedBy(date));
        }

        [Fact]
        public void Should_pass_for_male()
        {
            var spec = new HasValidGenderSpec();
            var gender = new Dictionary<string, string> { { "Gender", "male" } };
            Assert.True(spec.IsSatisfiedBy(gender));
        }

        [Fact]
        public void Should_pass_for_all_caps_male()
        {
            var spec = new HasValidGenderSpec();
            var gender = new Dictionary<string, string> { { "Gender", "MALE" } };
            Assert.True(spec.IsSatisfiedBy(gender));
        }

        [Fact]
        public void Should_pass_for_female()
        {
            var spec = new HasValidGenderSpec();
            var gender = new Dictionary<string, string> { { "Gender", "FEMALE" } };
            Assert.True(spec.IsSatisfiedBy(gender));
        }

        [Fact]
        public void Should_pass_for_other()
        {
            var spec = new HasValidGenderSpec();
            var gender = new Dictionary<string, string> { { "Gender", "oThEr" } };
            Assert.True(spec.IsSatisfiedBy(gender));
        }

        [Fact]
        public void Should_pass_for_unknown()
        {
            var spec = new HasValidGenderSpec();
            var gender = new Dictionary<string, string> { { "Gender", "Unknown" } };
            Assert.True(spec.IsSatisfiedBy(gender));
        }

        [Fact]
        public void Should_fail_for_invalid_gender()
        {
            //valid genders are {male, female, other, unknown}
            var spec = new HasValidGenderSpec();
            var gender = new Dictionary<string, string> { { "Gender", "green" } };
            Assert.False(spec.IsSatisfiedBy(gender));
        }
    }
}