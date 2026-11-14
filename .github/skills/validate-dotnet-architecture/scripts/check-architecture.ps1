$ErrorActionPreference = "Stop"
$root = (Resolve-Path (Join-Path $PSScriptRoot "../../../..")).Path
Push-Location $root
try {
    dotnet restore tests/Orders.ArchitectureTests/Orders.ArchitectureTests.csproj
    if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
    dotnet test tests/Orders.ArchitectureTests/Orders.ArchitectureTests.csproj --no-restore
    if ($LASTEXITCODE -ne 0) { exit $LASTEXITCODE }
}
finally {
    Pop-Location
}
