# Repository contract for coding agents

Read this file before modifying the solution. This is the portable, short entrypoint; details live in linked files and skills.

## What this repository does

A small ASP.NET Core Orders API used to demonstrate agent-ready .NET repositories at .NET Conference Italia 2026.

## Where things belong

- `src/Orders.Domain`: entities, value objects and business invariants. No references to other Orders projects.
- `src/Orders.Application`: use cases and ports; references Domain only.
- `src/Orders.Infrastructure`: adapters such as persistence; references Application and Domain.
- `src/Orders.Api`: HTTP contracts, dependency injection and endpoint mapping.
- `tests`: domain, API integration and architecture tests.

See `docs/architecture.md`, `docs/domain.md` and `docs/testing.md` for detail.

## Required workflow

1. Read only the relevant documentation and source files, not the entire repository.
2. State which layers the change affects and why.
3. Keep domain rules out of HTTP endpoints and Infrastructure.
4. Add or update tests at the appropriate level.
5. Run `./scripts/validate.sh` (Linux/macOS/Dev Container) or `./scripts/validate.ps1` (PowerShell).
6. Report actual commands, their results and any checks you could not execute.

## Build and dependency conventions

- SDK fixed by `global.json`; solution `DotNetConf26.slnx`.
- Shared build conventions in `Directory.Build.props` and `Directory.Build.targets`.
- Central package versions in `Directory.Packages.props`; do not put Version attributes on project PackageReference elements.
- Formatting and C# conventions in `.editorconfig`.
- CSharpier 1.4.0 is pinned in `.config/dotnet-tools.json`. Run `dotnet tool restore`, then `dotnet csharpier format src tests` for formatting or `dotnet csharpier check src tests` to validate formatting. Do not add global tool installation instructions.
- Do not disable analyzers, change SDK/TFM or weaken test assertions to make a change pass.

## Skills and specialists

- Use `.github/skills/implement-dotnet-feature/SKILL.md` for feature implementation.
- Use `.github/skills/validate-dotnet-architecture/SKILL.md` for architecture review and validation.
- The optional `.github/agents/dotnet-reviewer.agent.md` is a review-focused custom agent.

## Security and scope

No secrets, production data or external service credentials belong in this repository.
The development container is a reproducible environment, not a security boundary.
Do not introduce Spec Kit, GitHub issue workflows or .NET Aspire into this sample.
