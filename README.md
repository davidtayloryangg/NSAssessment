# NedStevens C# Application
This C# application allows the user to look up employees by name or add new employees to the database.

## Usage
The application provides the following options:

- Look up employee
- Add new employee
- Exit
### Look up employee
When the user selects the "Look up employee" option, the application prompts the user to enter the employee's first and last name. If the entered names are valid, the application retrieves the employee's information from the database and displays it to the user.

### Add new employee
When the user selects the "Add new employee" option, the application prompts the user to enter the employee's information, including first name, last name, department number, date of birth, address, and social security number. The application validates the input and adds the employee to the database.

## Database

The SQL database used for this application is through Azure. There is a DBInit file inbedded in the project under the src folder for DB initial poppulation.
The connection string will be provided.

## Dependencies
This application uses the following libraries:

- System.Data.SqlClient
- System.Collections.Generic
- System.Text.RegularExpressions

## How to run
To run this application, make sure you have the above-mentioned libraries installed and the MySQL server running. Then, compile and run the Program.cs file using the C# compiler.
You will also need to great a .env file which will contain the connection string to connect to the database.
```
dotnet run
```
