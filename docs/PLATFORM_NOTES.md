# Development Environment Notes

## Important: MAUI Requirements

This project uses **.NET MAUI** which **requires Windows or macOS** to build and run.

### Platform-Specific Requirements

#### Windows Development
- Windows 10 version 1809 or higher, or Windows 11
- Visual Studio 2022 17.8 or later with:
  - .NET MAUI workload
  - Windows App SDK
- .NET 10 SDK

#### macOS Development  
- macOS 12 (Monterey) or later
- Visual Studio for Mac or Visual Studio Code with C# extension
- Xcode 14 or later
- .NET 10 SDK
- .NET MAUI workload

### Installing MAUI Workload

```bash
dotnet workload install maui
```

### Why Linux is Not Supported

The current sandboxed Linux environment **cannot run .NET MAUI applications** because:
1. MAUI requires platform-specific UI frameworks (WinUI on Windows, Mac Catalyst on macOS)
2. There is no Linux UI backend for MAUI
3. MAUI is specifically designed for Windows, macOS, iOS, and Android

### Testing This Application

To test the application, you will need to:

1. **Clone the repository on Windows or Mac**
2. **Install prerequisites** (Visual Studio with MAUI workload)
3. **Build and run**:
   ```bash
   cd src/ImpactTrace.Maui
   dotnet run -f net10.0-windows10.0.19041.0    # Windows
   dotnet run -f net10.0-maccatalyst             # Mac
   ```

### Alternative: Web Version

If a web-based version is needed instead of desktop, the architecture can be adapted by:
1. Keeping Core and Infrastructure layers as-is
2. Replacing ImpactTrace.Maui with an ASP.NET Core Blazor Server/WebAssembly project
3. The domain logic and services remain unchanged

## Project Structure Benefits

Despite the platform limitation, the **DDD + Modular Monolith** architecture provides:

✅ **Core business logic** is platform-independent (testable anywhere)
✅ **Infrastructure layer** works on any OS that supports .NET
✅ **Only the Presentation layer** (MAUI) requires Windows/Mac
✅ Easy to swap presentation layer if requirements change

## Build Verification

The Core and Infrastructure projects can be built and tested on Linux:

```bash
# These work on Linux
cd src/ImpactTrace.Core
dotnet build
dotnet test  # if tests are added

cd ../ImpactTrace.Infrastructure  
dotnet build
```

Only the MAUI project requires Windows/Mac:
```bash
# This requires Windows or Mac
cd src/ImpactTrace.Maui
dotnet build -f net10.0-windows10.0.19041.0
```

## Summary

This is a **production-ready architecture** for a cross-platform desktop application. The domain logic and business rules are fully implemented and testable. The UI layer requires Windows or macOS to run, which aligns with the requirements for a desktop application targeting those platforms.
