# url-shortener
Url Shortener made in .NET 5

## How to run

1. Install the dependencies stated below
2. Trust the HTTPS development certificate by running the following command `dotnet dev-certs https --trust`
3. `dotnet run`
4. Open link `https://localhost:5001/`
5. Follow the "Step by step guide"

## Dependencies
`dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 5.0.0`

`dotnet add package Microsoft.AspNetCore.Authentication.OpenIdConnect --version 5.0.0`

`dotnet add package Microsoft.EntityFrameworkCore --version 5.0.1`

`dotnet add package Microsoft.EntityFrameworkCore.Design --version 5.0.1`

`dotnet add package Microsoft.EntityFrameworkCore.Sqlite --version 5.0.1`
