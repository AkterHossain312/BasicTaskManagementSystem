# BasicTaskManagementSystem
Basic Task Management System

***First, please switch to the 'btm_develop' branch and pull the latest changes.

Change Connection String:
•	Clone the project.
•	Navigate to the "webapi" project and open appsettings.json.
•	Modify the connection string with your SQL Server information:
"DefaultConnectionString": "Data Source=<ServerName>; Database=BasicTaskManagement; User Id=<userId>; Password=<Password>"
Example: 
"DefaultConnectionString": "Data Source=192.168.0.1; Database=BasicTaskManagement; User Id=sa; Password=Pass@123"
Ensure you replace <ServerName>, <userId>, and <Password> with your actual SQL Server information.

Build and Update Database:
•	Build the project.
•	Open the Package Manager Console and execute:
--- Update-Database
This command creates the database and migrates all tables.

Run the Project:
•	After updating the database, run the project.
Create Role:
•	Before registration, create a role by calling the following API:
(POST)  https://localhost:<yourportnumber>/api/Role/Create

Request Body:
{
  "Id": 0,
  "RoleName": "Admin",
  "PermissionList": [  ],
  "Permissions": [  ]
}


Register User:
•	Register a user by calling the following API:
(POST) https://localhost:7265/api/Auth/Register
Request Body:
{
  "FullName": "string",
  "Email": "string",
  "Password": "string",
  "RoleName": "Admin"
}
Add a body parameter as per your preference, but note that you have to specify the RoleName that you created earlier. All properties are required except for Id."
Token and Authorization:
•	After successful registration, you will receive a token.
•	Use this token for authorization in subsequent requests.
Check Tasks:
•	Now, you can check tasks using the appropriate API.

Additionally, ensure that your SQL Server is configured properly, and the necessary packages are installed before running the project.


“Note that I haven't conducted any unit tests”
