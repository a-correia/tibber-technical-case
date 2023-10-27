# Technical Case

Create a new microservice that could fit into the Tibber Platform environment as described above. 
The created service will simulate a robot moving in an office space and will be cleaning the places this robot visits. 
The path of the robot's movement is described by the starting coordinates and move commands. 
After the cleaning has been done, the robot reports the number of unique places cleaned. 
The service will store the results into the database and return the created record in JSON format. 
The service listens to HTTP protocol on port 5000.

Request method: POST
Request path: `/tibber-developer-test/enter-path` 

Input criteria:
- 0 ≤ number of commmands elements ≤ 10000
- −100 000 ≤ x ≤ 100 000, x ∈ Z
- −100 000 ≤ y ≤ 100 000, y ∈ Z
- direction ∈ {north, east, south, west}
- 0 < steps < 100000, steps ∈ Z

Request body example:
```json
{
  "start": {
    "x": 10,
    "y": 22
  },
  "commmands": [
    {
      "direction": "east",
      "steps": 2
    },
    {
      "direction": "north",
      "steps": 1
    }
  ]
}
```
The resulting value will be stored in a table named executions together with a timestamp of insertion, number of command elements and duration of the calculation in seconds.
Stored record example:

| Id | Timestamp | Commands | Result | Duration |
|----|-----------|----------|--------|----------|
| 1234| 2018-05-12 12:45:10.851596 | 2 | 4 | 0.000123|


## Notes:

- You can assume, for the sake of simplicity, that the office can be viewed as a grid where the robot moves only on the vertices.
- The robot cleans at every vertex it touches, not just where it stops.
- All input should be considered well formed and syntactically correct. There is no
need, therefore, to implement elaborate input validation.
- The robot will never be sent outside the bounds of the office.
- Ensure that database connection is configurable using environment variable. 
- Think about structure, readability, maintainability, performance, re-usability and
test-ability of the code. Like the solution is going to be deployed into the
production environment. You should be proud of what you deliver.
- Use only open source dependencies if needed.
- Include Dockerfile and docker-compose configuration files in the solution.


## How to run the project:
### 1. Use Docker

    ./docker-compose up

This will start the application and the postgres database.
The database connection can be configured using the environment variable `DATABASE_CONNECTION`. If not initialized, the connection will default to the configuration in [appsettings.json](CleaningService.Api/appsettings.json):

    "DatabaseConnection":"Host=localhost;Port=5432;Username=tibber;Password=tibber;Database=tibber",


### 2. IDE/Terminal + Database in Docker
If you prefer the run the project directly in an IDE or terminal you should start the postgres database first:
    
    ./docker-compose up -d postgres

    dotnet run --project CleaningService.Api/CleaningService.Api.csproj

## API Documentation:
Swagger: http://localhost:5000/swagger/index.html

## Personal notes:

This was the first time I worked with .NET. I decided to do it as a learning exercise and tried to follow good practices and conventions.

I used the Rider IDE on an Intel Macbook. I didn't have the opportunity to test it in a different architectures/operation systems but please let me know if you have any problem or question.

## TODO
- Add integration tests using Docker.
- Improve logging
- Improve docs and error handling
- Async calls
    
    
