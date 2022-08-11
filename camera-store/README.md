# angular-learning-camera-store

### Preparing the project to run

#### Inside Client APP

1. Open command Prompt (cmd).
2. Write "npm i" and hit enter.

#### Inside Server App

1. Check the database connection string in the "appsettings.Development.json" file.
2. Open command Prompt (cmd) and run the following command
   1. dotnet restore
   2. dotnet ef database drop --force --context DataContext
   3. dotnet ef database drop --force --context IdentityDataContext
   4. dotnet ef database update --context DataContext
   5. dotnet ef database update --context IdentityDataContext
   3. dotnet add package Microsoft.Extensions.Caching.SqlServer --version 3.0.0
   4. dotnet tool uninstall --global dotnet-sql-cache
   5. dotnet tool install --global dotnet-sql-cache --version 3.0.0
   6. dotnet sql-cache create "Server=(localdb)\MSSQLLocalDB;Database=EssentialApp3; MultipleActiveResultSets=true" "dbo" "SessionData"
3. Run the project by "dotnet run"
