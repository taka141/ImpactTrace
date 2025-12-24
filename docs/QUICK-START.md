# VSCode Setup - Quick Start Guide

## 🚀 Quick Setup (5 minutes)

### 1. Install VSCode
Download from [code.visualstudio.com](https://code.visualstudio.com/)

### 2. Open the Project
```bash
cd ImpactTrace
code .
```

### 3. Install Recommended Extensions
When prompted, click **"Install All"** or press `Ctrl+Shift+P` and run:
```
Extensions: Show Recommended Extensions
```

### 4. Wait for Language Server
Check the bottom-right status bar for "C# Dev Kit" or "OmniSharp" to finish loading.

### 5. Verify Setup
Open any `.cs` file and check:
- ✅ Syntax highlighting works
- ✅ IntelliSense shows suggestions (type `Ctrl+Space`)
- ✅ Code formatting works (`Shift+Alt+F`)

## 📝 Common Tasks

### Format Code
- **Auto**: Save file (`Ctrl+S`) - formats automatically
- **Manual**: `Shift+Alt+F` (Windows/Linux) or `Shift+Option+F` (macOS)
- **Terminal**: `dotnet format`

### Build Project
- **Task**: `Ctrl+Shift+B`
- **Terminal**: `dotnet build`

### Run Tests
- **Test Explorer**: Click flask icon in sidebar
- **Terminal**: `dotnet test`

### Debug
1. Set breakpoint: Click left of line number
2. Press `F5` to start debugging
3. Use debug controls in top toolbar

## ⚡ Keyboard Shortcuts

| Action | Windows/Linux | macOS |
|--------|---------------|-------|
| Command Palette | `Ctrl+Shift+P` | `Cmd+Shift+P` |
| Quick Open File | `Ctrl+P` | `Cmd+P` |
| Format Document | `Shift+Alt+F` | `Shift+Option+F` |
| Build | `Ctrl+Shift+B` | `Cmd+Shift+B` |
| Go to Definition | `F12` | `F12` |
| Find References | `Shift+F12` | `Shift+F12` |
| Rename Symbol | `F2` | `F2` |
| Code Actions | `Ctrl+.` | `Cmd+.` |
| Toggle Terminal | `` Ctrl+` `` | `` Cmd+` `` |
| Problems Panel | `Ctrl+Shift+M` | `Cmd+Shift+M` |

## 🔧 Troubleshooting

### IntelliSense Not Working
1. **Reload**: `Ctrl+Shift+P` → "Developer: Reload Window"
2. **Restart**: `Ctrl+Shift+P` → "OmniSharp: Restart OmniSharp"
3. **Restore**: Run `dotnet restore` in terminal

### Formatting Not Working
1. Check default formatter: Right-click → "Format Document With..."
2. Select "C# (ms-dotnettools.csharp)"
3. Or run `dotnet format` in terminal

### Build Errors
1. Restore packages: `dotnet restore`
2. Clean build: `dotnet clean`
3. Rebuild: `dotnet build`

### Extensions Not Installing
1. Check internet connection
2. Reload VSCode
3. Install manually: `Ctrl+Shift+X` → Search → Install

## 📚 Learn More

- [VSCode README](.vscode/README.md) - Detailed configuration docs
- [CI/CD Integration](docs/CI-CD-INTEGRATION.md) - Pipeline setup
- [EditorConfig](.editorconfig) - Code style rules

## 🎯 Best Practices

✅ **DO**:
- Format code before committing (`Ctrl+S` or `dotnet format`)
- Run tests before pushing (`dotnet test`)
- Use code snippets (type `class-ctor` + `Tab`)
- Check Problems panel regularly (`Ctrl+Shift+M`)

❌ **DON'T**:
- Disable format-on-save
- Ignore code analysis warnings
- Commit without testing
- Push without formatting

## 🆘 Get Help

- VSCode Issues: Check Output panel → "C#"
- Build Issues: Run `dotnet build --verbosity detailed`
- Test Issues: Run `dotnet test --verbosity detailed`

---

**Ready to code!** Open a `.cs` file and start coding. The editor will help you with IntelliSense, formatting, and code analysis.
