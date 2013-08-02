using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using ImporterTest.Models;
using ImporterTest.Specifications;
using System.Globalization;
using System.Threading;

namespace ImporterTest
{
    public class UserImporter
    {
        public UserImporterResults Import(string path)
        {
            FileInfo fileIn = new FileInfo(path);
            StreamReader reader = fileIn.OpenText();

            string[] line;
            var results = new UserImporterResults();
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine().Split(',');

                var user = CreateUser(line);

                if (user != null)
                {
                    results.Users.Add(user);
                }
                else
                {
                    results.SkippedRows.Add(string.Join(",", line));
                }
            }
            return results;
        }

        private User CreateUser(string[] userData)
        {
            User user = null;
            if(userData.Length == 6)
            {
                var data = SanitizeData(userData);
                
                var valid = ValidateData(data);

                if (valid)
                {
                    user = new User
                    {
                        FirstName = data[0],
                        LastName = data[1],
                        Email = data[2],
                        Gender = GetGender(data[3]),
                        FavoriteNumber = Convert.ToInt32(data[4]),
                        JoinDate = Convert.ToDateTime(data[5])
                    };
                }
            }

            return user;
        }

        private Gender GetGender(string gender)
        {
            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            TextInfo textInfo = cultureInfo.TextInfo;

            var titleCaseGender = textInfo.ToTitleCase(gender);

            var correctGenderEnum = (Gender) Enum.Parse(typeof(Gender), titleCaseGender);
        
            return correctGenderEnum;
        }


        private bool ValidateData(string[] data)
        {
            Dictionary<string, string> dataToValidate = new Dictionary<string, string> 
                {
                    { "FirstName", data[0]},
                    { "LastName", data[1]},
                    { "Email" , data[2]},
                    { "Gender", data[3]},
                    { "FavoriteNumber", data[4]},
                    { "Date", data[5]} 
                };

            var compositeSpec = new CompositeSpecification<Dictionary<string, string>>();
            compositeSpec.AddSpecification(new HasValidFirstNameSpec());
            compositeSpec.AddSpecification(new HasValidLastNameSpec());
            compositeSpec.AddSpecification(new HasValidGenderSpec());
            compositeSpec.AddSpecification(new HasValidDateSpec());

            var validUserData = compositeSpec.IsSatisfiedBy(dataToValidate);

            return validUserData;
        }

        private string[] SanitizeData(string[] data)
        {
            string[] returnData = new string[6];
            for (int i = 0; i < data.Length; i++)
            {
               returnData[i] = Sanitize(data[i]);
            }

            return returnData;
        }

        private string Sanitize(string data) 
        {
            data.Trim();
            StringBuilder builder = new StringBuilder();
            foreach (char c in data)
            {
                if (c != '\'' && c != '"' && c != ' ')
                {
                    builder.Append(c);
                }
            }

            return builder.ToString();
        }
    }
}
