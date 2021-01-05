# dotnet-core-promocodes-api
.NET Core 5.0 Web API and Tests.

[This image was taken as inspiration.](https://github.com/NEOLPAR/dotnet-core-promocodes-api/blob/master/reference_layout.PNG)

[x]	Use EF Core.

[x]	Use in memory database. 

[x] Use of Identity for User database.

[x] Authentization with JwtBearer.

[x] Swagger for documentation and manual testing.

[x] Setup of Cors.

[x] Ability to search services by name.

[x] Search by name with contains.

[x] Ability to Activate bonus for a Service for the current user.

[x] List services with the actived bonus.

[x] Infinite scroll for the Services list.

[x] Infinite scroll with name filtered.

[x] All services getters are filtered returning the services and the code related if the user is the current.

[x] All code service user getters are filtering by current user.

[x] Code splat in services using DI.

## Getting Started

**You’ll need to have .NET Core 5.0.1 or later version on your local development machine.** 
[You can download it here.](https://dotnet.microsoft.com/download/dotnet/5.0)

This project was developed with Visual Studio 2019 Community which can be downloaded for free [here](https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=Community&rel=16). If you want to use VS for this application, you just need to follow the [Installation](#Installing) and run the application. VS will download all the dependencies and build the project automatically.

The following steps are instructions to instal, build, run and test the application from PowerShell.

## Installing

First, we need to clone the project from Github, we will install it in a folder called `promocodes` or a different one at your choice. If you don't give it a different name it will be downloaded in the folder `dotnet-core-promocodes-api`.
```sh
git clone https://github.com/NEOLPAR/dotnet-core-promocodes-api.git promocodes
```

Once it is installed we need to go to the project folder for the next steps.
```sh
cd promocodes
```

## Building

We need to build and install all the dependencies to start using this project, this can be done from the PowerShell:
```sh
dotnet build
```

## Running the application

With the project installed and built, we can execute the following command to run the application:
```sh
dotnet run --project .\PromocodesApp\PromocodesApp.csproj
```

This will start the application in the urls:

https://localhost:5001

http://localhost:5000


**If you are using these ports you will need to change them in the configuration file `PromocodesApp/Services/launchSettings.json`.**

Now we are ready to use Swagger/OpenApi if we are in dev environment, or other application like Postman.

## Documentation and Manual testing with Swagger/OpenApi

Swagger has been setup as root of the project, in the url address `http://localhost:5000` or `http://localhost:5001`, when we are running the project in dev environment. We can use it to check the API specs or to do manual tests.

**All routes, but login and register, require authorization.**
For testing purposes, we need to follow the next steps: 
1.  Authenticate using `/api/Authenticate/login`, the test user is test/Test1234*. This will return a token.
2.  Right click button `Authorize`.
3.  Set `Bearer [TOKEN]` in Value field.
4.  Click in Authorize. 

With this steps we are authorized to all the API without restrictions white the token is not expired.

## Running the tests

We can run the tests with the command:
```sh
dotnet test
```