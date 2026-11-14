#!/usr/bin/env bash
set -euo pipefail
ROOT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)"
cd "$ROOT"
echo "[1/3] Restore"
dotnet restore DotNetConf26.slnx
echo "[2/3] Build (warnings as errors)"
dotnet build DotNetConf26.slnx --no-restore
echo "[3/3] Unit, integration and architecture tests"
dotnet test DotNetConf26.slnx --no-restore --no-build
echo "Validation succeeded."
