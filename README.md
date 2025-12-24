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

## 📚 Documentation

- **[Architecture Guide](docs/ARCHITECTURE.md)** - Detailed DDD patterns, design decisions, and architecture overview
- **[Platform Notes](docs/PLATFORM_NOTES.md)** - Windows/Mac requirements, build instructions, and platform-specific information

## 📄 License

MIT License
# ImpactTrace

A .NET project configured with modern development tools and best practices for 2025.

## 🚀 Quick Start

1. **Clone the repository**
   ```bash
   git clone https://github.com/taka141/ImpactTrace.git
   cd ImpactTrace
   ```

2. **Open in VSCode**
   ```bash
   code .
   ```

3. **Install recommended extensions**
   - When prompted, click "Install All"
   - Or press `Ctrl+Shift+P` and run: `Extensions: Show Recommended Extensions`

4. **Start coding!**
   - Open or create a `.cs` file
   - Format on save is enabled
   - IntelliSense and code analysis work automatically

📖 **New to this project?** Check out the [Quick Start Guide](docs/QUICK-START.md)

## 📁 Project Structure

```
ImpactTrace/
├── .vscode/                    # VSCode workspace configuration
│   ├── settings.json          # Editor and C# settings
│   ├── extensions.json        # Recommended extensions
│   ├── tasks.json             # Build, test, and format tasks
│   ├── launch.json            # Debug configurations
│   ├── impactTrace.code-snippets  # Code snippets
│   └── README.md              # Detailed VSCode setup guide
├── .github/
│   └── workflows/
│       └── code-quality.yml   # CI/CD workflow
├── docs/
│   ├── QUICK-START.md         # Quick start guide
│   └── CI-CD-INTEGRATION.md   # CI/CD integration guide
├── .editorconfig              # Code style and formatting rules
├── .gitignore                 # Git ignore patterns
├── omnisharp.json             # OmniSharp configuration
└── global.json                # .NET SDK version
```

## 🛠️ Development Setup

### Prerequisites

- [.NET SDK 10.0](https://dotnet.microsoft.com/download/dotnet/10.0) or later
- [Visual Studio Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/)

### Recommended Extensions

The following extensions will be suggested when you open the project:

- **C# Dev Kit** - Modern C# development experience
- **C# (OmniSharp)** - IntelliSense and debugging
- **EditorConfig** - Enforce code style
- **Error Lens** - Inline diagnostics
- **GitLens** - Git integration
- **GitHub Copilot** - AI pair programming
- **Path Intellisense** - File path autocomplete
- **Markdown All in One** - Markdown support

See [.vscode/extensions.json](.vscode/extensions.json) for the complete list.

## ⚙️ Configuration

### Code Style

This project uses a comprehensive `.editorconfig` file that defines:

- **Naming conventions**: PascalCase for types, camelCase with `_` prefix for private fields
- **Formatting rules**: Brace placement, spacing, indentation
- **C# conventions**: var usage, expression bodies, pattern matching
- **Code analysis**: CA and IDE rules with appropriate severity levels

### Code Analysis

Enabled analyzers:
- **Microsoft.CodeAnalysis.NetAnalyzers** - Built-in .NET analyzers
- **StyleCop Analyzers** - Code style enforcement
- **Security analyzers** - Vulnerability detection

### Formatting

- **Format on save**: Enabled by default
- **Organize imports**: Automatically on save
- **Trailing whitespace**: Trimmed on save
- **Final newline**: Added automatically

## 🔨 Common Commands

### Building

```bash
# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Clean build artifacts
dotnet clean
```

### Testing

```bash
# Run all tests
dotnet test

# Run tests with detailed output
dotnet test --verbosity detailed

# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"
```

### Formatting

```bash
# Check formatting (what CI runs)
dotnet format --verify-no-changes

# Fix formatting issues
dotnet format

# Format with diagnostics
dotnet format --verbosity diagnostic
```

### Code Analysis

```bash
# Build with code analysis
dotnet build /p:EnforceCodeStyleInBuild=true

# Treat warnings as errors
dotnet build /p:TreatWarningsAsErrors=true
```

## 🧪 Testing

### Running Tests in VSCode

1. Open the Test Explorer (flask icon in sidebar)
2. Click "Run All Tests" or run individual tests
3. Debug tests by clicking the bug icon

### Command Line

```bash
# Run all tests
dotnet test

# Run specific test
dotnet test --filter "FullyQualifiedName~MyTestMethod"

# Run tests in a specific project
dotnet test path/to/TestProject.csproj
```

## 🚢 CI/CD

This project includes a GitHub Actions workflow that:

1. ✅ Validates code formatting
2. ✅ Builds the project with warnings as errors
3. ✅ Runs all tests
4. ✅ Performs code analysis

See [docs/CI-CD-INTEGRATION.md](docs/CI-CD-INTEGRATION.md) for details on:
- GitHub Actions configuration
- Azure DevOps pipelines
- GitLab CI setup
- Pre-commit hooks

## 📚 Documentation

- [Quick Start Guide](docs/QUICK-START.md) - Get started in 5 minutes
- [VSCode Setup](.vscode/README.md) - Detailed VSCode configuration
- [CI/CD Integration](docs/CI-CD-INTEGRATION.md) - Pipeline setup and best practices

## 🎯 Code Style Guidelines

### Naming Conventions

```csharp
// Types: PascalCase
public class MyClass { }
public interface IMyInterface { }
public enum MyEnum { }

// Public members: PascalCase
public string PropertyName { get; set; }
public void MethodName() { }

// Private fields: camelCase with underscore prefix
private readonly string _fieldName;
private int _count;

// Local variables and parameters: camelCase
public void Method(string parameterName)
{
    var localVariable = parameterName;
}
```

### Modern C# Features

```csharp
// File-scoped namespaces (C# 10+)
namespace MyNamespace;

// Primary constructors (C# 12+)
public class MyClass(string name)
{
    private readonly string _name = name;
}

// Pattern matching
if (obj is MyType { IsValid: true } myObj)
{
    // Use myObj
}

// Null checking (C# 11+)
ArgumentNullException.ThrowIfNull(parameter);
```

## 🔐 Security

Security analyzers are enabled to detect:
- SQL injection vulnerabilities
- Cross-site scripting (XSS)
- Insecure deserialization
- Weak cryptography
- Other security issues

## 🤝 Contributing

1. **Fork the repository**
2. **Create a feature branch**: `git checkout -b feature/my-feature`
3. **Make your changes** (format code before committing)
4. **Run tests**: `dotnet test`
5. **Commit**: `git commit -m "Add my feature"`
6. **Push**: `git push origin feature/my-feature`
7. **Create a Pull Request**

### Before Committing

```bash
# Format code
dotnet format

# Run tests
dotnet test

# Check for issues
dotnet build /p:EnforceCodeStyleInBuild=true
```

## 📝 License

[Specify your license here]

## 📮 Contact

[Specify contact information here]

---

**Built with ❤️ using .NET 10.0 and modern development practices**
