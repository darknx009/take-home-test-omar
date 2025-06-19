## **Instructions**. 
  
1.   Execute Docker Compose to create DB and Expose API in a Docker Container
2.   Navigate to backend/src/Fundo.Applications.WebApi and execute below command on the terminal. 
       docker-compose up --build  
       Docker containers will be created for SQL Server instance and the We API ready-to-use on Postman (or similar tool).
       Swagger it is available at http://localhost:5050/swagger/index.html
       The Docker container can be used for testing the API and Angular app, locally. Therefore, it is not required to Install MS SQL Server, on the development machine.
2a. If a database management tool needs to be used, use "localhost" as server name and credentials are available in the docker compose file.
3.    Execute UI Angular project locally.
        It is hardcoded the url to the Docker container Web API at
        frontend/src/app/app.service.ts
4.    In order to execute API using tools like Postman, it is required to get a valid token. It can be obtained executing Login API with valid credentials.
        Due restrictions of time, for educational purposes and simplicity of this test, credentials can be found at
        backend/src/Fundo.Applications.WebApi/Controllers/AuthControllers.cs

**NOTE**
If Web API needs to be executed locally, from development machine, it is required to update the URLs for the Angular UI.

**History of development**
MacOs was required to be configured.
I faced several issues when configuring macos
XCode needed to be installed
**Tools**
Below tools were used to complete this test:
Docker desktop
VS Code
.Net sdk 9 (6 was not longer supported by MS)
*Development organization and configuration*
DB was designed and init.sql file created
Docker compose, to create a container with an SQL Server instance in order to not use a local instance, was created. Later it was added the API
DBeaver was used as database management tool.
Executed init.sql from backend/src/init folder, in order to seed data for the project.
This will create the DB which will be required to generate the repositories, AppDbContext,  Entities and value objects (scaffolding), using EF 
Once Db was ready, I started to modify the project. I started with the backend one.
I added some Clean architecture structure.
Applications was kept, Domain and Infrastructure folders were added. As this is a small project, I decided to keep the API within the Applications folder.
Added nuget packages to each folder
Once EF was added to  project, below terminal command was executed to generate AppDbContext and related objects
dotnet ef dbcontext scaffold "Server=localhost;Database=LoanDB;User Id=sa;Password=Your_password123;TrustServerCertificate=true"
Once objects were created, some classes were reorganized to be part of the proper Clean Architecture structure.
Startup and Program files were into Program.cs, to follow .Net 9 standard.
Relocated most of "using" statements to GlobalUsing, to complain with .Net 9 Standard.
Coding for the backend.
Then, I moved to Unit testing.
I installed xunit
All packages were download from nuget.org
test sdk, xunit, and runner
It was installed the package that can be used in both the console and VS
It was necessary to re-add all nuggets packages for xunit. It was not working the unit test (was not finding the url)
MVC.testing was re-added , and it worked as expected the testing
dotnet add package Microsoft.NET.Test.Sdk
dotnet add package FluentAssertions
dotnet add package Microsoft.AspNetCore.TestHost
dotnet add package Microsoft.AspNetCore.Mvc.Testing
dotnet add package Moq
dotnet add package xunit
dotnet add package xunit.runner.visualstudio
dotnet test

Once backend and unit tests were, mostly completed, I started to modify the Angular UI.
Added node, nvm and angular modules to frontend workspace
Installed npm, node
brew install node
npm install --save-dev @angular-devkit/build-angular
Installed npm install -g @angular/cli
Execute ng serve
localhost:4200 was available

When frontend first draft was ready, I modified the docker files to include Web API and test the Angular UI using the API container version.
As soon as this was done, I committed the code to Github.

When all the testing was working as expected. I added authentication and Security API handling. 
JWT was implemented in order to protect the APIs.
Angular UI was modified to use the bearer token.
When these modifications were ready in the development machine, I proceeded to update the Docker container to test it.
After testing was completed, I committed all the code to Github.
Once coding was completed, I created this ReadMe.md file.
And I created a githubgactions file, too. But it was not tested.