# ASP.NET Core 5
## Tech Stack
- ASP.NET Core 5
- Entity Framework Core 3.1
https://www.entityframeworktutorial.net/efcore/entity-framework-core.aspx
## Install Packages
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.EntityFrameworkCore.Design
###
1. Configuration DBCONTEXT
 - Fluent API
 https://docs.microsoft.com/en-us/ef/core/modeling/
 - Migration database
 https://docs.microsoft.com/en-us/ef/core/cli/dbcontext-creation?tabs=dotnet-core-cli
<PackageReference Include="Microsoft.Extensions.Configuration.FileExtensions" Version="5.0.0" />
<PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="5.0.0" />
2. Data Seeding
https://docs.microsoft.com/en-us/ef/core/modeling/data-seeding
- Seeding data OnModelCreating
https://www.learnentityframeworkcore.com/migrations/seeding
