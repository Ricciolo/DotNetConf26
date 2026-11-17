$ErrorActionPreference = "Stop"
$root = (Resolve-Path (Join-Path $PSScriptRoot "..")).Path
Push-Location $root
try {
    dotnet tool restore
    if ($LASTEXITCODE -ne 0) { throw "Local tool restore failed." }
    dotnet csharpier --version
    if ($LASTEXITCODE -ne 0) { throw "CSharpier tool verification failed." }
    dotnet restore DotNetConf26.slnx
    if ($LASTEXITCODE -ne 0) { throw "Restore failed." }
    dotnet build DotNetConf26.slnx --no-restore
    if ($LASTEXITCODE -ne 0) { throw "Build failed." }
    dotnet test DotNetConf26.slnx --no-restore --no-build
    if ($LASTEXITCODE -ne 0) { throw "Tests failed." }
    Write-Host "Validation succeeded."
}
finally {
    Pop-Location
}
