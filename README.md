# DietPlanner (AKA Ever-Fitter)

## Setup Instructions

1. Clone the repository from GitHub and open in Visual Studio 2022.
2. Ensure that the solution is set to open 2 startup projects, namely:
    - DietPlanner.API
    - DietPlanner.UI

## Configuration Settings

### `appsettings.json` in DietPlanner.API

No configuration changes should be needed, as this uses a SQLite database with a connection string to a local file which is created automatically on the first run of the API.

### `appsettings.json` in DietPlanner.UI

- `DefaultConnection` connection string should be the same as the `DefaultConnection` connection string in the API.
- Optionally, the `MaxCalories` entry can be updated here if desired.
- The `ApiBaseAddress` setting should be set to the HTTP address of the API; by default, this should be `http://localhost:5068`.

## Optional - Run the Database Migrations

The API project is configured to apply the database migrations on startup, but if you wish to apply the migrations manually:

1. Ensure that the API project is set as the startup project.
2. Open Package Manager Console in Visual Studio.
3. Choose "DietPlanner.Data" as the default project in the package manager dropdown menu then type:
    ```powershell
    update-database
    ```
    and press Enter.
4. (NB: Remember to set the UI and API back to multiple startup status after completing this step.)

You should now be able to build and run the solution.

## Using the UI

1. Register an account from the register link in the menu. This requires an email address and password.
2. Once logged in, there are screens for updating your food diary, updating your username and password, and logging out.
3. You will also be able to use the API’s swagger documentation to test the endpoints if desired.

## Solution Breakdown

Below is a breakdown of the layers used in the solution:

### DietPlanner.API

This is a WebAPI project containing controller methods to add, update, delete, and view data.

### DietPlanner.API.Tests

This is an integration test project for the API.

### DietPlanner.API.UnitTests

This is a unit test project for the API.

### DietPlanner.Fakes

This is a fake service layer collection used for testing the API instead of using Moq.

### DietPlanner.Contracts

This contains shared models and DTO objects common between the API and UI.

### DietPlanner.Data

This is the database and repository layer, using SQLite.

### DietPlanner.Services

This is the service layer used by the API to interact with the Data project.

### DietPlanner.Services.Test

This is a unit test project for the service layer (specifically to test the validation).

### DietPlanner.UI

This is the UI for the application, built using .NET 8, Blazor, and MudBlazor.
