# Agent-ready .NET Repository | .NET Conference Italia 2026

Companion repository for **GitHub Copilot e .NET: progettare un repository per lo sviluppo agentico**.

The sample shows how repository context, reproducible environments and executable guardrails help AI agents implement and validate features consistently.

## Stack

- .NET 11 SDK (`global.json`), ASP.NET Core Minimal APIs
- Domain, Application, Infrastructure and API projects
- xUnit unit, integration and architecture tests
- Central Package Management and shared MSBuild configuration
- Pinned local CSharpier 1.4.0 tool for reproducible C# formatting
- VS Code Dev Container, Agent Skills and custom reviewer

## Quick start

Install .NET SDK 11.0.100 or open the repository in the Dev Container.

```bash
dotnet tool restore
dotnet csharpier --version
dotnet restore DotNetConf26.slnx
dotnet build DotNetConf26.slnx
dotnet test DotNetConf26.slnx --no-build
dotnet run --project src/Orders.Api --urls http://localhost:5080
```

In a second terminal:

```bash
curl http://localhost:5080/health
curl -X POST http://localhost:5080/orders \
  -H 'Content-Type: application/json' \
  -d '{"customer":"Ada","lines":[{"sku":"BOOK","quantity":2,"unitPrice":12.50}]}'
curl http://localhost:5080/orders
```

Data is intentionally in-memory and resets when the process stops. No database, secrets or cloud subscription required.

## Local tooling (pinned in the repository)

`.config/dotnet-tools.json` pins **CSharpier 1.4.0**, so developers and coding agents can use exactly the same C# formatter on any machine. It does not require a global installation.

```bash
dotnet tool restore
dotnet csharpier format src tests  # Apply canonical C# formatting
dotnet csharpier check src tests   # Validate formatting without changes
```

`scripts/validate.sh` / `scripts/validate.ps1` restore and verify the formatter version alongside building and testing. Formatting is a separate explicit check, not yet a blocking CI rule; run `format` before `check` on the initial sample.

## Conference walkthrough

See **[demo.md](demo.md)** for the speaker's full timed walkthrough, prompts, commands and fallbacks.

## Repository contract

- `AGENTS.md`: cross-agent entrypoint and project navigation.
- `.github/copilot-instructions.md` + scoped instructions: Copilot repository conventions.
- `.github/skills/`: reusable procedures and scripts.
- `.github/agents/`: specialist agent definitions.
- `.devcontainer/`: reproducible .NET 11 environment.
- `Directory.Build.props`, `Directory.Build.targets`, `Directory.Packages.props`, `.editorconfig`: deterministic tooling and guardrails.
- `tests/Orders.ArchitectureTests/`: executable dependency constraints.

This repository deliberately avoids Spec Kit and .NET Aspire because those topics have dedicated sessions at the conference.
