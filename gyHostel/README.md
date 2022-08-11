# hostel-microservice

Steps to build the project

1. Open the project using visual studio
2. Visual Studion automatically restore the dependency.
3. If not, Navigate to the Tools -> Nuguat Package Manager -> Package manager console and run commnad: "dotnet restore"  after navigating to each of the project
3. Navigate to the Tools -> Nuguat Package Manager -> Package manager console
4. Run command:  cd DataAccess
5. Then Run Command: dotnet ef database update -s ../gyHostel
6. Run the starter project
7. Right click on each project (except for DataAccess) and select "Debug" -> "Start New Instance" to start each of the microservices
