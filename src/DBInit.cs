using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace NedStevens
{
    class DBInit
    {
        public static void Init()
        {
            DotNetEnv.Env.Load();
            string connString = Environment.GetEnvironmentVariable("AZURE_CONNECTION_STRING");
            SqlConnection connection = new SqlConnection(connString);
            connection.Open();
            Console.WriteLine("Connected to database");

            // Delete previous database tables
            List<string> tableNames = DBRetrieve.GetAllTableNames();
            foreach (string tableName in tableNames)
            {
                SqlCommand deleteCommand = new SqlCommand($"DROP TABLE {tableName}", connection);
                deleteCommand.ExecuteNonQuery();
            }
            Console.WriteLine("Old Database has been successfully deleted");

            try
            {
                    //string createTableQuery = "CREATE TABLE EMPLOYEE (SSN INT PRIMARY KEY, FNAME VARCHAR(50), LNAME VARCHAR(50), DOB DATE, ADDRESS VARCHAR(225), MGRSSN INT, DNO INT )";
                    //SqlCommand createTableCommand = new SqlCommand(createTableQuery, connection);
                    // Create EMPLOYEE table
                SqlCommand createEmployeeTable = new SqlCommand(
                    "CREATE TABLE EMPLOYEE (" +
                    "SSN CHAR(9) PRIMARY KEY," +
                    "FNAME VARCHAR(20)," +
                    "LNAME VARCHAR(20)," +
                    "DOB DATE," +
                    "ADDRESS VARCHAR(50)," +
                    "MGRSSN CHAR(9)," +
                    "DNO INT" +
                    ")", connection);
                createEmployeeTable.ExecuteNonQuery();

                // Create DEPARTMENT table
                SqlCommand createDepartmentTable = new SqlCommand(
                    "CREATE TABLE DEPARTMENT (" +
                    "DNAME VARCHAR(30) PRIMARY KEY," +
                    "NNUMBER INT," +
                    "MGRSSN CHAR(9)," +
                    "STARTDATE DATE" +
                    ")", connection);
                createDepartmentTable.ExecuteNonQuery();

                // Create DEPT_LOCATIONS table
                SqlCommand createDeptLocationsTable = new SqlCommand(
                    "CREATE TABLE DEPT_LOCATIONS (" +
                    "DNUMBER INT PRIMARY KEY," +
                    "DLOCATION VARCHAR(30)" +
                    ")", connection);
                createDeptLocationsTable.ExecuteNonQuery();

                Console.WriteLine("Tables created successfully.");

                // Insert data into EMPLOYEE table
                SqlCommand insertEmployee = new SqlCommand(
                    "INSERT INTO EMPLOYEE VALUES" +
                    "('123233232','David','Yang','01/01/1990','19 Brady Rd, Shrewsbury, NJ','123788888',1)," +
                    "('123788888','Joe','Smo','02/02/1991','12 Broad St, Eatontown, NJ','199973829',1)," +
                    "('199973829','John','Doe','03/03/1992','1 Reading Rd, Little Silver, NJ','412332222',1)," +
                    "('547822938','Susan','Dale','04/04/1993','5 Victory Rd, Lawrenceville, NJ','123333124',2)", connection);
                insertEmployee.ExecuteNonQuery();

                // Insert data into DEPARTMENT table
                SqlCommand insertDepartment = new SqlCommand(
                    "INSERT INTO DEPARTMENT VALUES" +
                    "('IT',1,'199973829','12/12/2012')," +
                    "('Operations',2,'547822938','01/02/2015')", connection);
                insertDepartment.ExecuteNonQuery();

                // Insert data into DEPT_LOCATIONS table
                SqlCommand insertDeptLocations = new SqlCommand(
                    "INSERT INTO DEPT_LOCATIONS VALUES" +
                    "(1,'Fairfield, NJ')," +
                    "(2,'Princeton, NJ')", connection);
                insertDeptLocations.ExecuteNonQuery();

                Console.WriteLine("Data inserted successfully.");
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            finally
            {
                connection.Close();
            }

            return;
        }
    }
}