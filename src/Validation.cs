using System;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NedStevens
{
    class Validation
    {
        public static bool IsValidDNO(string dno)
        {
            DotNetEnv.Env.Load();
            string connString = Environment.GetEnvironmentVariable("AZURE_CONNECTION_STRING");
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();

            SqlCommand command = new SqlCommand($"SELECT * FROM DEPT_LOCATIONS WHERE DNUMBER = '{dno}'", connection);
            SqlDataReader reader = command.ExecuteReader();
            
            bool isValid = false;
            while (reader.Read())
            {
                isValid = true;
            }
            reader.Close();
            connection.Close();
            return isValid;
        }

        public static bool IsValidDOB(string dob)
        {
            DateTime temp;
            return DateTime.TryParse(dob, out temp);
        }

        public static bool IsValidSSN(string ssn)
        {
            if (!Regex.IsMatch(ssn, @"^\d{3}-?\d{2}-?\d{4}$"))
            {
                return false;
            }

            ssn = ssn.Replace("-", "");
            if (new string(ssn[0], ssn.Length) == ssn)
            {
                return false;
            }
            return true;
        }
    }
}