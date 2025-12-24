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