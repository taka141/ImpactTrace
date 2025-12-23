# Architecture Documentation

## Overview

ImpactTrace has been refactored from an ASP.NET Core MVC web application to a **.NET MAUI Blazor Hybrid desktop application** following Domain-Driven Design (DDD) and Modular Monolith principles.

## Key Changes

### 1. Platform Change
- **From**: ASP.NET Core MVC Web Application
- **To**: .NET MAUI Blazor Hybrid Desktop Application
- **Platforms**: Windows 10/11, macOS

### 2. UI Framework Change
- **From**: Bootstrap 5
- **To**: Tailwind CSS
- **Reason**: Modern utility-first CSS framework, better customization

### 3. Architecture Change
- **From**: Traditional MVC with service layer
- **To**: DDD + Modular Monolith with CQRS
- **Benefits**: Better maintainability, testability, scalability

## Project Structure

### Core Layer (Domain + Application)
**ImpactTrace.Core** - Contains pure domain logic with no external dependencies

#### Domain Layer
- `Entities/` - Aggregate roots and entities
  - `Entity.cs` - Base entity with identity
  - `Recording.cs` - Recording aggregate root
  - `SqlOperation.cs` - SQL operation entity
  
- `ValueObjects/` - Immutable value objects with validation
  - `RecordingName.cs` - Recording name (max 200 chars)
  - `TableName.cs` - SQL table name (max 100 chars)
  - `SqlText.cs` - SQL query text
  - `OperationType.cs` - INSERT/UPDATE/DELETE enum
  - `RecordingStatus.cs` - Active/Completed enum

- `Repositories/` - Repository interfaces (persistence abstraction)
  - `IRecordingRepository.cs` - Recording aggregate repository

#### Application Layer
- `DTOs/` - Data Transfer Objects for communication
  - `RecordingDto.cs` - Recording summary
  - `SqlOperationDto.cs` - SQL operation data
  - `RecordingDetailDto.cs` - Full recording details

- `Interfaces/` - Application service interfaces
  - `IRecordingService.cs` - Recording management
  - `ISqlInterceptorService.cs` - SQL interception
  - `IExportService.cs` - Excel/CSV export

### Infrastructure Layer
**ImpactTrace.Infrastructure** - Implements Core interfaces

- `Data/ApplicationDbContext.cs` - EF Core context with value converters
- `Repositories/RecordingRepository.cs` - Repository implementation
- `Services/` - Service implementations
  - `RecordingService.cs` - CQRS command/query handlers
  - `SqlInterceptorService.cs` - SQL parsing and capture
  - `ExportService.cs` - ClosedXML and CSV export

### Presentation Layer
**ImpactTrace.Maui** - MAUI Blazor Hybrid UI

- `Components/Pages/` - Blazor page components
  - `Recording.razor` - Main recording interface
  - `Verification.razor` - (to be created) Verification interface

- `Components/Layout/` - Layout components
  - `MainLayout.razor` - Application shell with navigation

- `MauiProgram.cs` - Dependency injection configuration
- `App.xaml` / `MainPage.xaml` - MAUI application setup

## DDD Patterns Used

### 1. Aggregate Pattern
- `Recording` is the aggregate root
- Controls the lifecycle of `SqlOperation` entities
- Enforces business rules (e.g., can't add operations to inactive recording)

### 2. Value Object Pattern
- `RecordingName`, `TableName`, `SqlText` are value objects
- Immutable and self-validating
- Converted to/from primitives at persistence boundary

### 3. Repository Pattern
- `IRecordingRepository` abstracts data access
- Domain doesn't know about EF Core or database
- Easy to swap implementations or add caching

### 4. Domain Events (Future)
- Can add events like `RecordingStarted`, `RecordingCompleted`
- Enable loose coupling between aggregates
- Support eventual consistency patterns

## CQRS Implementation

### Commands (Write Operations)
- `StartRecordingAsync(name)` - Creates new recording
- `StopRecordingAsync()` - Completes active recording
- `CaptureTestSqlAsync()` - Generates test data

### Queries (Read Operations)
- `GetActiveRecordingAsync()` - Returns active recording if any
- `GetAllRecordingsAsync()` - Returns all recordings summary
- `GetRecordingDetailAsync(id)` - Returns full recording details

## Dependency Flow

```
Presentation (MAUI)
       ↓ (depends on)
   Application Interfaces (IRecordingService, etc.)
       ↑ (implements)
  Infrastructure
       ↓ (depends on)
    Domain (Core)
```

**Key Principle**: Dependencies point inward toward the domain.
- Domain has no dependencies
- Infrastructure depends on Domain
- Presentation depends on Application interfaces (not implementations)

## UX Psychology Principles Applied

### 1. Gestalt Principles
- **Proximity**: Related elements grouped in cards
- **Similarity**: Consistent button styles indicate similar actions
- **Continuation**: Navigation flows naturally left to right

### 2. Visual Hierarchy
- **Size**: Primary action button is larger and prominent
- **Color**: Green (start) stands out more than gray (secondary)
- **Position**: Most important controls at the top

### 3. Feedback & Affordance
- **Immediate Feedback**: Success/error messages appear instantly
- **Loading States**: Buttons show "⏳ Starting..." during async operations
- **Visual Affordance**: Buttons have shadows, hover states signal clickability

### 4. Color Psychology
- **Green (#10b981)**: Growth, start, positive actions
- **Red (#ef4444)**: Stop, danger, important warnings
- **Blue (#3b82f6)**: Information, trust, navigation
- **Gray**: Neutral, secondary actions

### 5. Hick's Law
- Limited choices at each step
- Clear primary action reduces decision time
- Progressive disclosure (details on demand)

## Benefits of This Architecture

### Maintainability
✅ Clear separation of concerns
✅ Each layer has single responsibility
✅ Easy to locate and fix bugs

### Testability
✅ Domain logic testable without database
✅ Mock interfaces for unit tests
✅ Integration tests can use in-memory database

### Scalability
✅ Can extract modules to separate services
✅ Repository pattern supports different data stores
✅ CQRS enables read/write separation

### Evolvability
✅ Add new features without changing existing code
✅ Swap implementations without changing interfaces
✅ Domain stays stable as infrastructure changes

## Migration Notes

### What Changed
- Removed Controllers (replaced with Blazor components)
- Removed Views (.cshtml files replaced with .razor files)
- Removed Bootstrap (replaced with Tailwind CSS)
- Added Value Objects for type safety
- Split service layer into Application and Infrastructure
- Added Repository abstraction

### What Stayed the Same
- Core business logic (SQL interception, recording lifecycle)
- Database schema (SQLite with EF Core)
- Export functionality (ClosedXML for Excel, CSV)

## Running the Application

### Development Environment Requirements
- Windows 10/11 with Visual Studio 2022, OR
- macOS with Visual Studio for Mac or VS Code
- .NET 10 SDK
- .NET MAUI workload installed

### Build and Run
```bash
# Install MAUI workload (first time only)
dotnet workload install maui

# Build the solution
dotnet build

# Run on Windows
cd src/ImpactTrace.Maui
dotnet run -f net10.0-windows10.0.19041.0

# Run on Mac
cd src/ImpactTrace.Maui
dotnet run -f net10.0-maccatalyst
```

### Note on Linux
MAUI is not supported on Linux. This is a Windows/Mac desktop application.

## Future Enhancements

### Phase 2 Considerations
1. **Domain Events**: Implement event-driven architecture
2. **CQRS Materialized Views**: Optimize read models
3. **Real SQL Interception**: Integrate with actual SQL proxy
4. **Unit Tests**: Add comprehensive test coverage
5. **API Layer**: Add REST API for external integrations
6. **Microservices**: Extract modules if needed for scale

## References

- [UX Psychology Principles](https://www.shokasonjuku.com/ux-psychology)
- [Domain-Driven Design](https://martinfowler.com/bliki/DomainDrivenDesign.html)
- [Modular Monolith](https://www.kamilgrzybek.com/blog/posts/modular-monolith-primer)
- [CQRS Pattern](https://martinfowler.com/bliki/CQRS.html)
- [.NET MAUI Documentation](https://learn.microsoft.com/en-us/dotnet/maui/)
- [Tailwind CSS](https://tailwindcss.com/)
