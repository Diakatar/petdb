# Employee Pets Application

The project was created as demo for EF6, RestAPI and Angular 10 CRUD operations as SPA

## Deployment
Prerequisites
- [Node.js](https://nodejs.org/en/).
- SQL Server (any) should be installed locally as for first run (sqllocaldb tool is needed).  

All other libraries will imported at the first run and database instance created and database created.

## Development server
Run `dotnet run --project ./EmployeePets/EmployeePets.csproj` in solution root for a dev server. Navigate to `https://localhost:5001/`. 
The app will automatically reload if you change any of the source files.

## Running integration tests
Not implemented yet

## Running unit tests
Not implemented yet.
Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests
Not implemented yet.
Run `ng e2e` to execute the end-to-end tests via [Protractor](http://www.protractortest.org/).

## Known issues
- The AnimalType combo in edit mode not supplies teh current value
- The Pet list left filled (visually, not db) when the last Person is deleted  
