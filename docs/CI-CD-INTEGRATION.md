# CI/CD Integration Guide

This document explains how the VSCode configuration integrates with CI/CD pipelines to ensure consistent code quality.

## Overview

The configuration files in this repository are designed to work seamlessly between:
- **Local Development**: VSCode with extensions
- **CI/CD Pipelines**: GitHub Actions, Azure DevOps, etc.

This ensures that code formatting and analysis rules are consistent across all environments.

## GitHub Actions

### Code Quality Workflow

The `.github/workflows/code-quality.yml` workflow runs on every push and pull request:

1. **Format Check**: Validates code formatting using `dotnet format`
2. **Build**: Compiles the project with warnings as errors
3. **Test**: Runs all tests and uploads results
4. **Code Analysis**: Runs Roslyn analyzers and checks for warnings

### Configuration

The workflow uses the same configuration files as VSCode:
- `.editorconfig` - Code style rules
- Project files - Analyzer settings

### Running Locally

To replicate CI checks locally:

```bash
# Format check (what CI runs)
dotnet format --verify-no-changes

# Format code (to fix issues)
dotnet format

# Build with code analysis
dotnet build /p:EnforceCodeStyleInBuild=true /p:TreatWarningsAsErrors=true

# Run tests
dotnet test --verbosity normal
```

## Azure DevOps

### Pipeline Configuration

Example `azure-pipelines.yml`:

```yaml
trigger:
  branches:
    include:
    - main
    - develop

pool:
  vmImage: 'ubuntu-latest'

variables:
  buildConfiguration: 'Release'
  dotnetVersion: '10.0.x'

stages:
- stage: Validation
  jobs:
  - job: FormatCheck
    displayName: 'Code Format Check'
    steps:
    - task: UseDotNet@2
      displayName: 'Use .NET SDK'
      inputs:
        version: $(dotnetVersion)
    
    - task: DotNetCoreCLI@2
      displayName: 'Restore packages'
      inputs:
        command: 'restore'
    
    - task: DotNetCoreCLI@2
      displayName: 'Check formatting'
      inputs:
        command: 'custom'
        custom: 'format'
        arguments: '--verify-no-changes --verbosity diagnostic'

- stage: Build
  dependsOn: Validation
  jobs:
  - job: BuildAndTest
    displayName: 'Build and Test'
    steps:
    - task: UseDotNet@2
      displayName: 'Use .NET SDK'
      inputs:
        version: $(dotnetVersion)
    
    - task: DotNetCoreCLI@2
      displayName: 'Restore packages'
      inputs:
        command: 'restore'
    
    - task: DotNetCoreCLI@2
      displayName: 'Build with analysis'
      inputs:
        command: 'build'
        arguments: '--configuration $(buildConfiguration) --no-restore /p:EnforceCodeStyleInBuild=true /p:TreatWarningsAsErrors=true'
    
    - task: DotNetCoreCLI@2
      displayName: 'Run tests'
      inputs:
        command: 'test'
        arguments: '--configuration $(buildConfiguration) --no-build --logger trx'
        publishTestResults: true
    
    - task: PublishTestResults@2
      displayName: 'Publish test results'
      condition: succeededOrFailed()
      inputs:
        testResultsFormat: 'VSTest'
        testResultsFiles: '**/*.trx'
```

## GitLab CI

### Pipeline Configuration

Example `.gitlab-ci.yml`:

```yaml
image: mcr.microsoft.com/dotnet/sdk:10.0

stages:
  - validate
  - build
  - test

variables:
  DOTNET_SKIP_FIRST_TIME_EXPERIENCE: "true"
  DOTNET_CLI_TELEMETRY_OPTOUT: "true"

format-check:
  stage: validate
  script:
    - dotnet restore
    - dotnet format --verify-no-changes --verbosity diagnostic

build:
  stage: build
  needs:
    - format-check
  script:
    - dotnet restore
    - dotnet build --configuration Release --no-restore /p:EnforceCodeStyleInBuild=true /p:TreatWarningsAsErrors=true
  artifacts:
    paths:
      - "**/bin/"
      - "**/obj/"
    expire_in: 1 hour

test:
  stage: test
  needs:
    - build
  script:
    - dotnet test --configuration Release --no-build --verbosity normal --logger "junit;LogFileName=test-results.xml"
  artifacts:
    reports:
      junit: "**/test-results.xml"
```

## Configuration Files Used by CI/CD

### .editorconfig

This file contains:
- Code style rules (naming, formatting)
- Code analysis severity settings
- IDE-agnostic configuration

**Used by**: `dotnet format`, MSBuild analyzers

### Project Files (.csproj)

Analyzer packages and settings:

```xml
<PropertyGroup>
  <!-- Enable code analysis -->
  <EnableNETAnalyzers>true</EnableNETAnalyzers>
  <AnalysisLevel>latest</AnalysisLevel>
  <EnforceCodeStyleInBuild>true</EnforceCodeStyleInBuild>
  <TreatWarningsAsErrors>true</TreatWarningsAsErrors>
  
  <!-- Nullable reference types -->
  <Nullable>enable</Nullable>
</PropertyGroup>

<ItemGroup>
  <!-- Code analyzers -->
  <PackageReference Include="Microsoft.CodeAnalysis.NetAnalyzers" Version="8.0.0">
    <PrivateAssets>all</PrivateAssets>
    <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
  </PackageReference>
</ItemGroup>
```

## Tools and Commands

### Format Code

```bash
# Check formatting (CI mode)
dotnet format --verify-no-changes

# Fix formatting issues
dotnet format

# Format specific project
dotnet format path/to/Project.csproj
```

### Code Analysis

```bash
# Build with analysis
dotnet build /p:EnforceCodeStyleInBuild=true

# Analyze without building
dotnet format analyzers --verify-no-changes

# Show all diagnostics
dotnet build /p:EnforceCodeStyleInBuild=true /p:TreatWarningsAsErrors=true
```

### Testing

```bash
# Run all tests
dotnet test

# Run with coverage
dotnet test --collect:"XPlat Code Coverage"

# Run specific test
dotnet test --filter "FullyQualifiedName~MyTest"
```

## Best Practices

### 1. Run Format Before Push

Always run `dotnet format` before pushing code:

```bash
# Quick check
dotnet format --verify-no-changes

# Fix issues
dotnet format

# Commit
git add .
git commit -m "Your message"
git push
```

### 2. Test Locally First

Before pushing, run what CI will run:

```bash
# Full CI check
dotnet restore
dotnet format --verify-no-changes
dotnet build /p:EnforceCodeStyleInBuild=true /p:TreatWarningsAsErrors=true
dotnet test
```

### 3. Keep Configuration in Sync

- **Never** disable rules only in project files
- **Always** update `.editorconfig` for team-wide changes
- **Document** any rule suppressions

## Troubleshooting

### Formatting Differences Between Local and CI

**Cause**: Different .NET SDK versions or `dotnet format` versions

**Solution**:
1. Check SDK version in CI config
2. Install same SDK locally: `dotnet --list-sdks`
3. Update `global.json` to pin SDK version:

```json
{
  "sdk": {
    "version": "10.0.100",
    "rollForward": "latestMinor"
  }
}
```

### Analyzer Warnings Only in CI

**Cause**: Different analyzer package versions or project not restored

**Solution**:
1. Run `dotnet restore` locally
2. Check `dotnet build /p:EnforceCodeStyleInBuild=true`
3. Ensure all projects restored properly

### Tests Pass Locally But Fail in CI

**Cause**: Environment-specific issues (paths, timezone, culture)

**Solution**:
1. Check test output in CI logs
2. Use invariant culture in tests
3. Avoid absolute paths
4. Mock time-dependent code

## Resources

- [dotnet format documentation](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-format)
- [.NET Code Analysis](https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/overview)
- [EditorConfig](https://editorconfig.org/)
- [GitHub Actions for .NET](https://docs.github.com/en/actions/automating-builds-and-tests/building-and-testing-net)

---

**Last Updated**: 2025-12-24
