# URL Shortener

URL shortener made in .NET 5 using SQLite.

***Disclaimer: This project is a simple url shortener made for learning purposes and thus it is in a pre-release stage. It is not advised and made to be deployed in production environments. Use it at your own risk.***

## How to run in dev environment

1. Install the dependencies stated under the "Dependencies" header below
2. Trust the HTTPS development certificate by running the following command `dotnet dev-certs https --trust`
3. Run command `dotnet run`
4. Open link `https://localhost:5001/`
5. Follow the "Step by step guide"

## How to run migrations

1. Create a migration by running the following command `dotnet ef migrations add <Name of Migration>`
2. Run the migrations by running the following command `dotnet ef database update`
3. To remove the last migration run the following command `dotnet ef migrations remove`

## How to publish project

1. Change the PRIVATE_KEY in `Utils/TokenService.cs` and keep it a secret
2. Open terminal in project root and run the following command `dotnet publish -c Release`
3. The package will be created in `bin\Release\net5.0\win-x64\publish`

## Dependencies

1. .NET 5
2. (Migrations only) Entity Framework Core .NET Command-line Tools 5.0.1
3. Packages:

```
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 5.0.0
dotnet add package Microsoft.AspNetCore.Authentication.OpenIdConnect --version 5.0.0
dotnet add package Microsoft.EntityFrameworkCore --version 5.0.1
dotnet add package Microsoft.EntityFrameworkCore.Design --version 5.0.1
dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 5.0.1
```
