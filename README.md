# ImpactTrace - Desktop SQL Recording and Tracing Tool

ImpactTrace is a cross-platform **desktop application** built with **.NET MAUI Blazor Hybrid** that records and traces SQL operations (INSERT, UPDATE, DELETE) during recording sessions. The application follows **Domain-Driven Design (DDD)** and **Modular Monolith** architecture principles for maintainability and scalability.

## 🏗️ Architecture

### Project Structure (Modular Monolith + DDD)

```
ImpactTrace/
├── src/
│   ├── ImpactTrace.Core/              # Domain & Application Layer
│   │   ├── Domain/
│   │   │   ├── Entities/              # Aggregate roots and entities
│   │   │   ├── ValueObjects/          # Value objects (immutable)
│   │   │   └── Repositories/          # Repository interfaces
│   │   └── Application/
│   │       ├── DTOs/                  # Data Transfer Objects
│   │       └── Interfaces/            # Application service interfaces
│   ├── ImpactTrace.Infrastructure/    # Infrastructure Layer
│   │   ├── Data/                      # EF Core DbContext
│   │   ├── Repositories/              # Repository implementations
│   │   └── Services/                  # Service implementations
│   └── ImpactTrace.Maui/              # Presentation Layer (MAUI Blazor)
│       ├── Components/                # Blazor pages and layouts
│       ├── Resources/                 # Platform resources
│       └── wwwroot/css/               # Tailwind CSS
```

### Design Principles

#### 1. **Domain-Driven Design (DDD)**
- **Entities**: `Recording` (aggregate root), `SqlOperation`
- **Value Objects**: `RecordingName`, `TableName`, `SqlText`, `OperationType`
- **Repositories**: Abstraction for data access
- **Domain Logic**: Encapsulated in entities

#### 2. **Modular Monolith**
- **Core Layer**: Pure domain logic, no dependencies
- **Infrastructure Layer**: Implements interfaces from Core
- **Presentation Layer**: MAUI Blazor UI

#### 3. **CQRS Pattern**
- Commands: `StartRecording`, `StopRecording`
- Queries: `GetActiveRecording`, `GetAllRecordings`

## 🎨 UI/UX Design (Following UX Psychology Principles)

### UX Psychology Principles Applied

Based on [UX Psychology principles](https://www.shokasonjuku.com/ux-psychology):

1. **Visual Hierarchy** - Primary actions prominently displayed
2. **Feedback Principle** - Real-time status indicators
3. **Affordance** - Buttons look clickable with clear states
4. **Consistency** - Uniform design throughout
5. **Color Psychology** - Green for start, Red for stop, Blue for info

### Tailwind CSS Implementation

All UI components use **Tailwind CSS** with custom utility classes.

## 📱 Platform Support

- ✅ **Windows 10/11** (x64, ARM64)
- ✅ **macOS** (Intel, Apple Silicon)

## 🚀 Getting Started

### Prerequisites

- .NET 10 SDK or later
- .NET MAUI workload: `dotnet workload install maui`

### Running the Application

**On Windows**:
```bash
cd src/ImpactTrace.Maui
dotnet run -f net10.0-windows10.0.19041.0
```

**On Mac**:
```bash
cd src/ImpactTrace.Maui
dotnet run -f net10.0-maccatalyst
```

## 📋 Features

### 1. Recording Operation Screen (記録操作画面)
- Start/Stop recording with prominent buttons
- Recording name input
- Real-time status with pulsing animation
- Test SQL generation

### 2. SQL Interception Module (SQL傍受モジュール)
- Captures INSERT, UPDATE, DELETE operations
- Regex-based table name extraction
- Links operations to active recording

### 3. Recording Verification Screen (記録確認画面)
- List recordings with operation counts
- Filter by operation type, table name, time
- Export to Excel (ClosedXML) or CSV

## 🔧 Technical Stack

- **Framework**: .NET 10 + MAUI Blazor Hybrid
- **UI**: Blazor + Tailwind CSS
- **Database**: SQLite + EF Core
- **Architecture**: DDD + Modular Monolith
- **Export**: ClosedXML (Excel), CSV

## 🎯 Architecture Benefits

- **Maintainability**: Clear separation of concerns
- **Testability**: Domain logic isolated
- **Scalability**: Can extract to microservices
- **Cross-Platform**: Windows & Mac from single codebase

## 📝 Usage

1. **Start Recording**: Enter name, click "▶️ Start Recording"
2. **Capture SQL**: Use "🧪 Generate Test SQL" for testing
3. **Stop Recording**: Click "⏹️ Stop Recording"
4. **View Results**: Navigate to "🔍 Verification"
5. **Export**: Download as Excel or CSV

## 📄 License

MIT License
