#!/usr/bin/env bash
set -euo pipefail
ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$ROOT"
echo "[1/4] Restore local .NET tools (pinned in .config/dotnet-tools.json)"
dotnet tool restore
dotnet csharpier --version
echo "[2/4] Restore solution"
dotnet restore DotNetConf26.slnx
echo "[3/4] Build (warnings as errors)"
dotnet build DotNetConf26.slnx --no-restore
echo "[4/4] Unit, integration and architecture tests"
dotnet test DotNetConf26.slnx --no-restore --no-build
echo "Validation succeeded."
