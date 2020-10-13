# HomeWork_ToDos: ToDo App web API (.Net Core)

A rest api project to do CRUD operations for labels, todoitems and lists via HTTP Verbs (GET, POST, PUT, DELETE, PATCH).
It includes functionality for assigning labels to items and lists.
It also includes authorization via JWT Token.
It also logs each and every request/response or error if any.

DB Setup -

1. Database is configured and migration is already present. It will be created when the application runs for the first time automatically. Only the connection string in appsettings.json needs to be changed accordingly.
2. If database is to be updated with changes "Update-database" command will update the database.

Pre Requisite:

Microsoft dot net core 3.1 sdk package/ dot net core runtime 3.1 version should be installed on machine.

How to run application:

Step 1: Clone repo in destination folder: git clone https://github.com/onkarsngh11/HomeWork_ToDos.git

Step 2: Go to the project folder and run “dotnet restore” in cmd.

Step 3: Go the folder “Homework_ToDos\HomeWork_ToDos.API” and run “dotnet run” in cmd.

Navigate to http://localhost:5000/PlayGround to play with GraphQl UI.


To play with the functionality, RegisterUser and authenticateuser methods are provided both Swagger and Graphql.

For Swagger:

Navigate to http://localhost:5000/ in a browser to play with the Swagger UI.

Step 1: Click RegisterUser and register user with valid values.

Step 2:	After registration, click Login and enter your credentials. On Success, it will generate jwt token in result. Copy it and keep it for next step.

Step 3: Click Authorize and set token in "Authorization" header as "Bearer (copied token from step 4)" without quotes.

For GraphQL:

Step 1: clear httpheaders if any.

Step 2: Use below mutation:

mutation{
  authenticateUser(userName:"registered UserName",password: "registered password"){
    isSuccess
    result
    message
  }
}

Above mutation will generate token in result field.

Set HttpHeaders as below for accessing api resources:
{
  "Authorization":"Bearer (copied token)"
}

Note - 
1. A user has to create todo list first in order to add todo item. 
2. One username can not be registered again.
3. Base64 password encoding algorithm is used.
4. For response, a custom apiresponse class is setup for details.
5. Serilog estensions is used for logging, logs can be checked in api logs folder.
6. For seeding data, only one user in "Admin" role is added. 
   Also, Admin and User authorization policies are added for customization wherever required.
