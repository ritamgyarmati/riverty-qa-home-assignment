# Home Assignment
Following repo contains the solution of Rita Medgyesi-Gyarmati for Riverty QA Engineer Home Assignment.

# Application information 
It’s an small microservice that validates provided Credit Card data and returns either an error or type of credit card application. 

# Running the  application 
There are two ways to run the application.
1. Clone this repository.
2. Open terminal and navigate to the project root.
3. To run the application in Docker container, run command: docker compose up --build and once finished open the app in http://localhost:5135/swagger/index.html to see the Swagger documentation of the endpoint.
4. To run the application without Docker open CardValidation.Web and run command: dotnet run

# Trigger unit tests with coverage
1. In case needed, please install code coverage tool:
	a. dotnet add CardValidation.UnitTests package coverlet.collector
	b. dotnet build CardValidation.UnitTests
1. In terminal in the project root run: dotnet test CardValidation.UnitTests --collect:"XPlat Code Coverage"
2. This will run the unit tests and create coverage report under CardValidation.UnitTests/TestResults

# Trigger integration tests
1. In terminal navigate to CardValidation.IntegrationTests folder
2. Run command: dotnet test
3. This will trigger the integration tests and write test results on the command line

# Author
- Rita Medgyesi-Gyarmati - rita.m.gyarmati@gmail.com
