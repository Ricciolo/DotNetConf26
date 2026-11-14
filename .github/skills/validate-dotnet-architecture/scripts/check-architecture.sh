#!/usr/bin/env bash
set -euo pipefail
ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/../../../.." && pwd)"
cd "$ROOT"
dotnet restore tests/Orders.ArchitectureTests/Orders.ArchitectureTests.csproj
dotnet test tests/Orders.ArchitectureTests/Orders.ArchitectureTests.csproj --no-restore
