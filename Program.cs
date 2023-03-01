using System;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NedStevens
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;
            Regex lettersRegex = new Regex("^[a-zA-Z]+$");
            while (exit == false)
            {
                Console.WriteLine("Look up employee by name or add new employee");
                Console.WriteLine("1: Look up employee");
                Console.WriteLine("2: Add new employee");
                Console.WriteLine("3: Exit");
                string input = Console.ReadLine().Trim();
                Console.WriteLine("==============================================");
                if (input == "1")
                {
                    Console.WriteLine("What is the employees first name?");
                    string fname = Console.ReadLine().Trim();
                    if (!lettersRegex.IsMatch(fname))
                    {
                        Console.WriteLine("Invalid input");
                        continue;
                    }
                    Console.WriteLine("What is the employees last name?");
                    string lname = Console.ReadLine().Trim();
                    if (!lettersRegex.IsMatch(lname))
                    {
                        Console.WriteLine("Invalid input");
                        continue;
                    }
                    try
                    {
                        Dictionary<string, string> employee = DBRetrieve.GetEmployeeByName(fname, lname);
                        if (employee == null)
                        {
                            Console.WriteLine("Employee not found");
                        }
                        else
                        {
                            Console.WriteLine($"Name: {employee["FNAME"]} {employee["LNAME"]}, DOB: {employee["DOB"]}, Address: {employee["ADDRESS"]}");
                            Console.WriteLine($"Manager: {employee["MANAGER"]} Department: {employee["DEPARTMENT"]}, Department Location: {employee["DLOCATION"]}");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("No record of such employee please try again");
                    }
                    Console.WriteLine("==============================================");
                    Console.WriteLine("Would you like to look up another employee? (y/n)");
                    string answer = Console.ReadLine().Trim();
                    if (answer == "n")
                    {
                        exit = true;
                    }
                }
                else if (input == "2")
                {
                    Dictionary<string, string> employee = new Dictionary<string, string>();
                    Console.WriteLine("What is the employees first name?");
                    employee.Add("FNAME", Console.ReadLine().Trim());
                    while (!lettersRegex.IsMatch(employee["FNAME"]) || string.IsNullOrEmpty(employee["FNAME"]))
                    {
                        Console.WriteLine("Please enter a valid first name");
                        employee["FNAME"] = Console.ReadLine().Trim();
                    }
                    // Console.WriteLine(employee["FNAME"]);
                    Console.WriteLine("What is the employees last name?");
                    employee.Add("LNAME", Console.ReadLine().Trim());
                    while (!lettersRegex.IsMatch(employee["LNAME"]) || string.IsNullOrEmpty(employee["LNAME"]))
                    {
                        Console.WriteLine("Please enter a valid last name");
                        employee["LNAME"] = Console.ReadLine().Trim();
                    }
                    Console.WriteLine("What is the employees department number?");
                    employee.Add("DNO", Console.ReadLine().Trim());
                    while (!Validation.IsValidDNO(employee["DNO"]))
                    {
                        Console.WriteLine("Please enter a valid department number");
                        employee["DNO"] = Console.ReadLine().Trim();
                    }
                    Console.WriteLine("What is the employees date of birth? (MM/DD/YYYY)");
                    employee.Add("DOB", Console.ReadLine().Trim());
                    while (!Validation.IsValidDOB(employee["DOB"]))
                    {
                        Console.WriteLine("Please enter a valid date of birth (MM/DD/YYYY)");
                        employee["DOB"] = Console.ReadLine().Trim();
                    }
                    Console.WriteLine("What is the employees address?");
                    employee.Add("ADDRESS", Console.ReadLine().Trim());
                    while (string.IsNullOrEmpty(employee["ADDRESS"]))
                    {
                        Console.WriteLine("Please enter a valid address");
                        employee["ADDRESS"] = Console.ReadLine().Trim();
                    }
                    Console.WriteLine("What is the employees SSN?");
                    employee.Add("SSN", Console.ReadLine().Trim());
                    while (!Validation.IsValidSSN(employee["SSN"]))
                    {
                        Console.WriteLine("Please enter a valid SSN");
                        employee["SSN"] = Console.ReadLine().Trim();
                    }
                    Console.WriteLine("What is their managers SSN?");
                    employee.Add("MGRSSN", Console.ReadLine().Trim());
                    while (!Validation.IsValidSSN(employee["SSN"]))
                    {
                        Console.WriteLine("Please enter a valid SSN");
                        employee["SSN"] = Console.ReadLine().Trim();
                    }

                    DBInsert.AddEmployee(employee);
                    Console.WriteLine("==============================================");
                    Console.WriteLine("Would you like to add another employee? (y/n)");
                    string answer = Console.ReadLine().Trim();
                    if (answer == "n")
                    {
                        exit = true;
                    }
                }
                else if (input == "3")
                {
                    exit = true;
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
                
            }


        }
    }
}

