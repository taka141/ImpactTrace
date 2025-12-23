# ImpactTrace - SQL Recording and Tracing Tool

ImpactTrace is an ASP.NET Core MVC application that records and traces SQL operations (INSERT, UPDATE, DELETE) performed during a recording session. It provides a comprehensive interface for starting/stopping recordings, viewing captured SQL operations, filtering by various criteria, and exporting data to Excel or CSV formats.

## Features

### 1. Recording Operation Screen (記録操作画面)
- **Start/Stop Recording**: Control recording sessions with a simple button interface
- **Recording Name Input**: Assign meaningful names to each recording session
- **Auto Recording ID**: Automatically generates unique IDs for each recording
- **Time-based Recording**: Captures all SQL operations between start and end times

### 2. SQL Interception Module (SQL傍受モジュール)
- **SQL Capture**: Intercepts and captures SQL operations
- **Operation Type Detection**: Identifies INSERT, UPDATE, and DELETE operations
- **Table Name Extraction**: Automatically extracts table names from SQL statements
- **Recording Association**: Links captured SQL with the active recording ID

### 3. Recording Verification Screen (記録確認画面)
- **Operation List Display**: Shows all operations grouped by recording name
- **Detailed Information**: Displays table name, operation type, and SQL text for each operation
- **Advanced Filtering**: Filter operations by:
  - Operation type (INSERT, UPDATE, DELETE)
  - Table name
  - Time period (start and end time)
- **Export Functionality**: Export recordings to Excel (ClosedXML) or CSV format
- **Summary Statistics**: View operation summaries by type and table

## Technology Stack

- **Framework**: ASP.NET Core 10.0 MVC
- **Database**: SQLite (Entity Framework Core)
- **UI**: Bootstrap 5 with Bootstrap Icons
- **Excel Export**: ClosedXML
- **Architecture**: Model-View-Controller (MVC) pattern

## Getting Started

### Prerequisites

- .NET 10.0 SDK or later
- A modern web browser

### Installation

1. Clone the repository:
```bash
git clone https://github.com/taka141/ImpactTrace.git
cd ImpactTrace
```

2. Restore dependencies:
```bash
cd ImpactTrace.Web
dotnet restore
```

3. Build the application:
```bash
dotnet build
```

4. Run the application:
```bash
dotnet run
```

5. Open your browser and navigate to `http://localhost:5000` (or the URL shown in the console)

## Usage

### Recording SQL Operations

1. **Start a Recording**:
   - Enter a recording name in the "Recording Name" field
   - Click the "Start Recording" button
   - The system will begin capturing SQL operations

2. **Generate Test Data** (for testing):
   - While recording is active, click "Generate Test SQL" button
   - This creates sample INSERT, UPDATE, and DELETE operations

3. **Stop Recording**:
   - Click the "Stop Recording" button when done
   - The recording will be saved with an end timestamp

### Viewing and Filtering Operations

1. Navigate to the "Verification" page from the top menu
2. Use the filter options to narrow down operations:
   - **Operation Type**: Filter by INSERT, UPDATE, or DELETE
   - **Table Name**: Filter by specific table names
   - **Time Period**: Set start and/or end time filters
3. Click "Apply Filter" to apply the filters
4. Click "Clear Filters" to reset all filters

### Exporting Data

From the Verification or Details page:
- Click **"Excel"** button to download an Excel (.xlsx) file
- Click **"CSV"** button to download a CSV file

Both formats include:
- Operation ID
- Table Name
- Operation Type
- Full SQL Text
- Execution Timestamp

## Project Structure

```
ImpactTrace.Web/
├── Controllers/
│   ├── RecordingController.cs      # Recording start/stop operations
│   └── VerificationController.cs   # Viewing and exporting operations
├── Data/
│   └── ApplicationDbContext.cs     # EF Core database context
├── Models/
│   ├── Recording.cs                # Recording entity
│   ├── SqlOperation.cs             # SQL operation entity
│   └── VerificationViewModel.cs    # View models for verification
├── Services/
│   └── SqlInterceptorService.cs    # SQL interception service
├── Views/
│   ├── Recording/
│   │   └── Index.cshtml           # Recording control interface
│   └── Verification/
│       ├── Index.cshtml           # Operations list and filtering
│       └── Details.cshtml         # Detailed recording view
└── Program.cs                      # Application entry point
```

## Database Schema

### Recordings Table
- `Id` (INT, Primary Key)
- `Name` (VARCHAR(200))
- `StartTime` (DATETIME)
- `EndTime` (DATETIME, nullable)
- `IsRecording` (BOOLEAN)

### SqlOperations Table
- `Id` (INT, Primary Key)
- `RecordingId` (INT, Foreign Key)
- `TableName` (VARCHAR(100))
- `OperationType` (VARCHAR(50))
- `SqlText` (TEXT)
- `ExecutedAt` (DATETIME)

## License

This project is licensed under the MIT License.

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.