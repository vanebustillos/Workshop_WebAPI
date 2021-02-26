# Workshop Web API
  The following implementation is a web-api project developed in .NET Core to let the user to manage past and incoming workshops. The user is capable to:

- Retrieve all registered workshops
- ADD, UPDATE or DELETE a workshop
- Cancel or Postpone a workshop

This CRM project has been developed modularizing the implementation into a:

- Middleware layer: To manage error handling with general and own exceptions.
- Controllers layer: Where the enpoints routes are specified. 
-  BusinessLogic layer : Where all the main implementation with the project's logic is located.
- Database layer: To store all the registered workshops.

in order to follow the accurate request processing pipeline for ASP.NET Core MVC pipeline.

## Project setup
Since Workshop-WebAPI is the main project that reference to previous layers, before to execute the following commands, the user should be located in the folder Workshop_WebAPI.

```
cd Workshop_WebAPI
```
### Install dependencies
```
dotnet restore
```
### Compiles and minifies for production
```
dotnet build
```
### Compiles and hot-reloads for development
```
dotnet run
```