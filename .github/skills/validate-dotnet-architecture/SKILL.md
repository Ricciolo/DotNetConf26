---
name: validate-dotnet-architecture
description: Validate project dependency directions, repository build conventions and .NET tests. Use after modifying projects, package references, architecture or when reviewing a pull request.
license: MIT
compatibility: Requires .NET SDK 11
metadata:
  author: dotnet-conf-italia
  version: "1.0"
---

# Validate .NET architecture

1. Read the dependency table in [architecture rules](references/dependency-rules.md).
2. Run the architecture test project using `scripts/check-architecture.sh` (or `scripts/check-architecture.ps1` on Windows).
3. If the change affects runtime behavior, run `scripts/validate.sh` at the repository root.
4. Identify actual dependency violations rather than proposing to disable checks.
5. Report each executed command, the result and any limitations.

Do not silently change `Directory.Build.props`, `Directory.Build.targets`, `global.json` or `Directory.Packages.props` to obtain a passing build.
