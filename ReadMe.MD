# Currency Tracker

Currency Tracker is a .NET application that fetches and logs currency exchange rates using Hangfire for scheduling and Azure Functions for serverless execution.

## Project Structure

- `CurrencyTracker/`: Main application directory
  - `CurrencyResponse.cs`: Defines the structure of the currency response.
  - `CurrencyService.cs`: Contains the service to fetch currency rates.
  - `Program.cs`: Configures and runs the application.
  - `appsettings.json`: Application configuration file.
  - `appsettings.Development.json`: Development-specific configuration.
  - `CurrencyTracker.csproj`: Project file for the main application.
  - `CurrencyTracker.sln`: Solution file for the main application.
  - `.vscode/`: VS Code configuration files.

- `CurrencyTrackerFunction/`: Azure Functions project directory
  - `CurrencyResponse.cs`: Defines the structure of the currency response.
  - `CurrencyService.cs`: Contains the service to fetch currency rates.
  - `FetchCurrencyRate.cs`: Azure Function to fetch currency rates on a schedule.
  - `Program.cs`: Configures and runs the Azure Functions application.
  - `local.settings.json`: Local settings for Azure Functions.
  - `CurrencyTrackerFunction.csproj`: Project file for the Azure Functions application.
  - `CurrencyTrackerFunction.sln`: Solution file for the Azure Functions application.
  - `.vscode/`: VS Code configuration files.

## Getting Started

### Prerequisites

- .NET 8.0 SDK
- Azure Functions Core Tools
- Visual Studio Code or Visual Studio

### Setting Up

1. Clone the repository:
   ```sh
   git clone <repository-url>
   cd CurrencyTracker
   ```

2. Restore the dependencies:
   ```sh
   dotnet run
   ```

3. The application will be available at `https://localhost:7178` or `http://localhost:5001`

### Running Azure Functions

1. Navigate to the CurrencyTrackerFunction directory:
   ```sh
   cd CurrencyTrackerFunction
   ```

2. Build the Azure Functions project:
   ```sh
   dotnet build
   ```

3. Start the Azure Functions host:
   ```sh
   func start
   ```
