using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace NedStevens
{
    class DBInsert
    {
        public static void AddEmployee(Dictionary<string, string> employee)
        {
            DotNetEnv.Env.Load();
            string connString = Environment.GetEnvironmentVariable("AZURE_CONNECTION_STRING");
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            // Console.WriteLine("Connected to database");

            SqlCommand command = new SqlCommand($"INSERT INTO EMPLOYEE VALUES ('{employee["SSN"]}', '{employee["FNAME"]}', '{employee["LNAME"]}', '{employee["DOB"]}', '{employee["ADDRESS"]}', '{employee["MGRSSN"]}', '{employee["DNO"]}')", connection);
            command.ExecuteNonQuery();
            connection.Close();
            Console.WriteLine("Added employee to database");
        }
    }
}