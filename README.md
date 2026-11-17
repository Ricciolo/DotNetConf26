# Repository .NET agent-ready | .NET Conference Italia 2026

Repository dimostrativo per la sessione **GitHub Copilot e .NET: progettare un repository per lo sviluppo agentico**.

Mostra come contesto, ambiente riproducibile e controlli eseguibili aiutino gli agenti AI a implementare funzionalità rispettando architettura e regole di business.

## Tecnologie

- SDK .NET 11 (tramite `global.json`) e ASP.NET Core Minimal APIs
- Progetti Domain, Application, Infrastructure e API
- Test unitari, di integrazione e architetturali con xUnit
- Central Package Management e configurazioni MSBuild condivise
- CSharpier 1.4.0 come tool locale versionato
- Dev Container per VS Code, Agent Skills e agente specializzato nella revisione

## Avvio rapido

Installa l'SDK .NET 11.0.100 oppure apri il repository nel Dev Container.

```bash
dotnet tool restore
dotnet csharpier --version
dotnet restore DotNetConf26.slnx
dotnet build DotNetConf26.slnx
dotnet test DotNetConf26.slnx --no-build
dotnet run --project src/Orders.Api --urls http://localhost:5080
```

Da un secondo terminale:

```bash
curl http://localhost:5080/health
curl -X POST http://localhost:5080/orders \
  -H 'Content-Type: application/json' \
  -d '{"customer":"Ada","lines":[{"sku":"LIBRO","quantity":2,"unitPrice":12.50}]}'
curl http://localhost:5080/orders
```

I dati sono conservati soltanto in memoria e vengono persi al riavvio. Non sono necessari database, segreti o sottoscrizioni cloud.

## Tool CLI condivisi

`.config/dotnet-tools.json` fissa **CSharpier 1.4.0**, in modo che sviluppatori, agenti e CI possano utilizzare lo stesso formatter senza installazione globale:

```bash
dotnet tool restore
dotnet csharpier format src tests
dotnet csharpier check src tests
```

Gli script `scripts/validate.sh` e `scripts/validate.ps1` ripristinano il formatter ed eseguono build e test. Il controllo del formato è attualmente separato dalla CI: sul campione iniziale potrebbe essere necessario eseguire `format` prima di `check`.

## Guida per la conferenza

Consulta **[demo.md](demo.md)** per tutti i passaggi, i prompt in italiano, i comandi e i piani alternativi per la dimostrazione dal vivo.

## Contratto del repository

- `AGENTS.md`: punto di ingresso e regole comuni per gli agenti.
- `.github/copilot-instructions.md` e `.github/instructions/`: istruzioni per Copilot.
- `.github/skills/`: procedure riutilizzabili, documenti e script.
- `.github/agents/`: definizioni di agenti specializzati.
- `.devcontainer/`: ambiente di sviluppo riproducibile.
- `Directory.Build.props`, `Directory.Build.targets`, `Directory.Packages.props`, `.editorconfig`: regole di compilazione e convenzioni.
- `tests/Orders.ArchitectureTests/`: dipendenze architetturali verificabili.

Non approfondiamo Spec Kit o .NET Aspire: sono argomenti di altre sessioni della conferenza.
