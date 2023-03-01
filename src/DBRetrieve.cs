using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace NedStevens
{
    class DBRetrieve
    {
        public static List<string> GetAllTableNames()
        {
            DotNetEnv.Env.Load();
            string connString = Environment.GetEnvironmentVariable("AZURE_CONNECTION_STRING");
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            Console.WriteLine("Connected to database");

            SqlCommand command = new SqlCommand("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES", connection);
            SqlDataReader reader = command.ExecuteReader();
            
            List<string> tableNames = new List<string>();
            while (reader.Read())
            {
                if (reader.GetString(0) != "database_firewall_rules")
                {
                    tableNames.Add(reader.GetString(0));
                    //Console.WriteLine(reader.GetString(0));
                }
            }
            reader.Close();
            connection.Close();
            
            Console.WriteLine("Getting all database table names");
            return tableNames;
        }

        public static Dictionary<string, string> GetEmployeeByName(string fname, string lname)
        {
            DotNetEnv.Env.Load();
            string connString = Environment.GetEnvironmentVariable("AZURE_CONNECTION_STRING");
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            Console.WriteLine("Connected to database");

            SqlCommand command = new SqlCommand($"SELECT * FROM EMPLOYEE WHERE FNAME = '{fname}' AND LNAME = '{lname}'", connection);
            SqlDataReader reader = command.ExecuteReader();
            
            Dictionary<string, string> employee = new Dictionary<string, string>();
            while (reader.Read())
            {
                employee.Add("SSN", reader.GetString(0));
                employee.Add("FNAME", reader.GetString(1));
                employee.Add("LNAME", reader.GetString(2));
                employee.Add("DOB", reader.GetDateTime(3).ToString());
                employee.Add("ADDRESS", reader.GetString(4));
                employee.Add("MGRSSN", reader.GetString(5));
                employee.Add("DNO", reader.GetInt32(6).ToString());
            }
            reader.Close();

            employee.Add("MANAGER", SearchNameBySSN(employee["MGRSSN"]));
            employee.Add("DEPARTMENT", SearchDNameByDNO(employee["DNO"]));
            employee.Add("DLOCATION", SearchLocationByDNO(employee["DNO"]));

            connection.Close();
            
            Console.WriteLine("Getting employee by name");
            return employee;
        }

        public static string SearchLocationByDNO(string dno)
        {
            DotNetEnv.Env.Load();
            string connString = Environment.GetEnvironmentVariable("AZURE_CONNECTION_STRING");
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            //Console.WriteLine("Connected to database");

            SqlCommand command = new SqlCommand($"SELECT DLOCATION FROM DEPT_LOCATIONS WHERE DNUMBER = '{dno}'", connection);
            SqlDataReader reader = command.ExecuteReader();
            
            string location = "";
            while (reader.Read())
            {
                location = reader.GetString(0);
            }
            reader.Close();
            connection.Close();
            
            
            return location;
        }

        public static string SearchDNameByDNO(string dno)
        {
            DotNetEnv.Env.Load();
            string connString = Environment.GetEnvironmentVariable("AZURE_CONNECTION_STRING");
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            //Console.WriteLine("Connected to database");

            SqlCommand command = new SqlCommand($"SELECT DNAME FROM DEPARTMENT WHERE NNUMBER = '{dno}'", connection);
            SqlDataReader reader = command.ExecuteReader();
            
            string dname = "";
            while (reader.Read())
            {
                dname = reader.GetString(0);
            }
            reader.Close();
            connection.Close();
            
            
            return dname;
        }

        public static string SearchNameBySSN(string ssn)
        {
            DotNetEnv.Env.Load();
            string connString = Environment.GetEnvironmentVariable("AZURE_CONNECTION_STRING");
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            //Console.WriteLine("Connected to database");

            SqlCommand command = new SqlCommand($"SELECT FNAME, LNAME FROM EMPLOYEE WHERE SSN = '{ssn}'", connection);
            SqlDataReader reader = command.ExecuteReader();
            
            string name = "";
            while (reader.Read())
            {
                name = reader.GetString(0) + " " + reader.GetString(1);
            }
            reader.Close();
            connection.Close();
            
            //Console.WriteLine("Getting employee by name");
            return name;
        }
    }
}