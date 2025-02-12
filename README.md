# DotLeague
DotLeague is a simple REST API for registering soccer teams, leagues, and their matches. The API also provides an endpoint to fetch the league ranking information, to determine the winner :D

<img src="https://raw.githubusercontent.com/andre-luis33/dot-league/refs/heads/main/assets/DotLeague.png" width="600" height="259" />

## Getting Started
To configure, run, and contribute with DotLeague, follow these steps:

### 1 - System Requirements
Make sure that your environment contains:
- .NET 8.0 Sdk

### 2 - Cloning the project
Clone the project from GitHub to your preferred directory and then navigate into it:
```bash
git clone https://github.com/andre-luis33/DotLeague.git
cd DotLeague
```

### 3 - Installing dependencies
Once you’ve confirmed that .NET is correctly installed, install the project dependencies:
```bash
dotnet restore
```

### 4 - Running migrations
After installing the project dependencies, run the database migrations to create the SQLite database:
```bash
dotnet ef database update
```

### 5 - Starting application
Now, start the application:
```bash
dotnet run --project=DotLeague/DotLeague.csproj
```

_On the first start in a development environment, some data will be seeded to the database to help you get started._

### 6 - Testing the API
Once the application is running, you can test the API in two ways:

### 1. Using Swagger UI
Swagger is automatically enabled for the API, allowing you to interact with the endpoints through a web interface. To access the Swagger UI, simply navigate to the following URL in your browser:

```bash
http://localhost:5183/swagger
```
This will open the Swagger UI, where you can see all available API endpoints, along with the ability to make requests directly from the interface.

### 2. Using cURL or API Testing tools
You can also test the endpoints by making requests directly. For example, to fetch all teams, you can use cURL:
```bash
curl http://localhost:5183/api/teams
```
This will return a list of teams in the response.
