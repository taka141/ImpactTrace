# VSCode Configuration for ImpactTrace

This directory contains VSCode workspace configuration files for consistent development experience across the team.

## 📁 Files Overview

### `.vscode/settings.json`
Workspace settings for C# development with explicit configuration following 2025 best practices:
- **C# Dev Kit** as the primary language server (recommended over OmniSharp)
- **Format on save** enabled with organized imports
- **Inlay hints** for better code readability
- **AI/Copilot** friendly settings
- **Editor config** support enabled
- File-specific formatting rules (C#, JSON, XML, Markdown)

### `.vscode/extensions.json`
Recommended extensions for optimal development experience:
- **Required**: C# Dev Kit, OmniSharp
- **Code Quality**: EditorConfig, Error Lens
- **Productivity**: GitLens, Copilot, Path Intellisense
- **Testing**: .NET Runtime
- **Documentation**: Markdown All in One

### `.vscode/tasks.json`
Predefined tasks for common operations:
- `build` (Ctrl+Shift+B) - Build the solution
- `test` - Run tests
- `restore` - Restore NuGet packages
- `clean` - Clean build artifacts
- `watch` - Watch mode for development
- `format` - Format code using `dotnet format`

### `.vscode/launch.json`
Debug configurations:
- Console application launch
- Web application launch (with auto-open browser)
- Attach to process

### `.editorconfig`
Comprehensive EditorConfig with:
- C# coding conventions (var usage, expression bodies, pattern matching)
- Formatting rules (braces, spacing, indentation)
- Naming conventions (PascalCase, camelCase, _privateFields)
- Code analysis rules (CA, IDE analyzers)
- File-scoped namespaces preference
- Modern C# feature preferences

### `omnisharp.json`
OmniSharp configuration (for backward compatibility):
- Formatting options aligned with EditorConfig
- Roslyn analyzer support
- Import completion
- Decompilation support

## 🚀 Getting Started

### 1. Install Recommended Extensions

When you open this workspace in VSCode, you'll be prompted to install recommended extensions. Click **Install All** to get the complete development environment.

Alternatively, you can install them manually:
```bash
# View recommendations
code --list-extensions

# Install all at once (on Windows/Linux/macOS)
cat .vscode/extensions.json | jq -r '.recommendations[]' | xargs -L 1 code --install-extension
```

### 2. Verify C# Dev Kit Installation

After installing extensions:
1. Open any `.cs` file
2. Check the bottom status bar for "C# Dev Kit" or "OmniSharp"
3. Wait for the project to load (first time may take a minute)

### 3. Build and Run

Use the Command Palette (Ctrl+Shift+P / Cmd+Shift+P):
- **Tasks: Run Build Task** (Ctrl+Shift+B)
- **Tasks: Run Test Task**
- **.NET: Generate Assets for Build and Debug** (if launch.json needs updating)

Or use the terminal:
```bash
dotnet restore
dotnet build
dotnet test
```

## 🎨 Code Formatting

### Automatic Formatting
- **On Save**: Enabled by default (`editor.formatOnSave: true`)
- **On Paste**: Disabled to avoid unexpected changes
- **Organize Imports**: Automatically on save

### Manual Formatting
- **Format Document**: Shift+Alt+F (Windows/Linux) or Shift+Option+F (macOS)
- **Format Selection**: Ctrl+K Ctrl+F (Windows/Linux) or Cmd+K Cmd+F (macOS)
- **Format via Task**: Run the `format` task from Command Palette

### Using `dotnet format`
```bash
# Format all files
dotnet format

# Check formatting without changes
dotnet format --verify-no-changes

# Format specific project
dotnet format path/to/project.csproj
```

## 🔍 Code Analysis

### Real-time Analysis
- **Error Lens**: Shows diagnostics inline in the editor
- **Problems Panel**: View all diagnostics (Ctrl+Shift+M)
- **Code Actions**: Quick fixes available via lightbulb (Ctrl+.)

### Analyzer Rules
The `.editorconfig` file contains:
- **CA rules**: Code Analysis (Microsoft.CodeAnalysis.NetAnalyzers)
- **IDE rules**: Style and refactoring suggestions
- Severity levels: `error`, `warning`, `suggestion`, `silent`, `none`

### Running Analysis
```bash
# Build with full analysis
dotnet build /p:EnforceCodeStyleInBuild=true

# Analyze without building
dotnet format analyzers
```

## 🧪 Testing

### Running Tests
- **Test Explorer**: View → Test (Ctrl+; Ctrl+A)
- **Run All Tests**: Click the flask icon in the sidebar
- **Run Single Test**: Click the green arrow next to test method
- **Debug Test**: Click the bug icon next to test method

### Command Line
```bash
# Run all tests
dotnet test

# Run tests with detailed output
dotnet test --logger "console;verbosity=detailed"

# Run tests in a specific project
dotnet test path/to/test.csproj
```

## 🔧 Troubleshooting

### C# Extension Not Working
1. **Reload Window**: Command Palette → "Developer: Reload Window"
2. **Check Output**: View → Output → Select "C#" or "OmniSharp Log"
3. **Restart OmniSharp**: Command Palette → "OmniSharp: Restart OmniSharp"
4. **Clear Cache**: Delete `.vscode/obj` and `.vscode/bin` directories

### Formatting Not Working
1. **Check Default Formatter**: Command Palette → "Format Document With..."
2. **Verify EditorConfig**: Ensure `.editorconfig` is in the workspace root
3. **Check for Conflicts**: Disable conflicting formatter extensions
4. **Manual Format**: Run `dotnet format` in terminal

### IntelliSense Not Working
1. **Restore Packages**: Run `dotnet restore`
2. **Build Project**: Run `dotnet build`
3. **Check for Errors**: View Problems panel (Ctrl+Shift+M)
4. **Restart Language Server**: Command Palette → "OmniSharp: Restart OmniSharp"

### Extensions Conflicts
If you have conflicting extensions:
- **Disable**: C# for Visual Studio Code (powered by OmniSharp) - OLD version
- **Keep**: C# Dev Kit - CURRENT version (2025)

## 📚 Best Practices

### File-scoped Namespaces
```csharp
namespace MyNamespace; // Preferred (C# 10+)

public class MyClass { }
```

### Null Checking
```csharp
// Use ArgumentNullException.ThrowIfNull (C# 11+)
public void MyMethod(string parameter)
{
    ArgumentNullException.ThrowIfNull(parameter);
    // ...
}
```

### Private Fields
```csharp
// Use underscore prefix
private readonly string _name;
private int _count;
```

### Async Methods
```csharp
// Always end with Async suffix
public async Task<string> GetDataAsync(CancellationToken cancellationToken)
{
    // Accept and pass CancellationToken
    return await _service.FetchAsync(cancellationToken);
}
```

### Pattern Matching
```csharp
// Prefer pattern matching over traditional checks
if (obj is MyType { IsValid: true } myObj)
{
    // Use myObj
}
```

## 🚢 CI/CD Integration

The configuration is designed to work seamlessly with CI/CD:

### GitHub Actions Example
```yaml
- name: Format check
  run: dotnet format --verify-no-changes

- name: Build
  run: dotnet build --configuration Release

- name: Test
  run: dotnet test --no-build --configuration Release
```

### Azure DevOps Example
```yaml
- task: DotNetCoreCLI@2
  displayName: 'Format check'
  inputs:
    command: 'custom'
    custom: 'format'
    arguments: '--verify-no-changes'

- task: DotNetCoreCLI@2
  displayName: 'Build'
  inputs:
    command: 'build'
    arguments: '--configuration Release'
```

## 🔐 Security

### Code Analysis for Security
The `.editorconfig` includes security-related analyzers:
- CA2007: ConfigureAwait in library code
- CA2016: Forward CancellationToken
- CA2100: SQL injection prevention
- CA3001-3147: Security vulnerability detection

### Recommended NuGet Packages
```bash
# Add security analyzers
dotnet add package Microsoft.CodeAnalysis.NetAnalyzers
dotnet add package SecurityCodeScan.VS2019
```

## 📖 Additional Resources

- [C# Dev Kit Documentation](https://code.visualstudio.com/docs/csharp/get-started)
- [EditorConfig Documentation](https://editorconfig.org/)
- [.NET Code Style Rules](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/style-rules/)
- [Roslyn Analyzers](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/overview)

## 🤝 Contributing

When contributing to this project:
1. Ensure all recommended extensions are installed
2. Format code before committing (`dotnet format`)
3. Fix all warnings in modified files
4. Run tests before pushing (`dotnet test`)
5. Follow the naming conventions in `.editorconfig`

---

**Note**: These configurations are designed for team consistency. Personal preferences like themes, fonts, and keybindings should be kept in user settings, not workspace settings.
