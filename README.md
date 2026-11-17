# Repository .NET agent-ready | .NET Conference Italia 2026

Repository dimostrativo per la sessione **GitHub Copilot e .NET: progettare un repository per lo sviluppo agentico**.

Mostra come contesto, ambiente riproducibile e controlli eseguibili aiutino gli agenti AI a implementare funzionalità rispettando architettura e regole di business.

## Tecnologie

- SDK .NET 11 (tramite `global.json`) e ASP.NET Core Minimal APIs
- Progetti Domain, Application, Infrastructure e API
- Test unitari, di integrazione e architetturali con xUnit
- Central Package Management e configurazioni MSBuild condivise
- CSharpier 1.4.0 e dotnet-ef 11.0.0 come strumenti CLI locali versionati
- Dev Container per VS Code, Agent Skills e agente specializzato nella revisione

## Avvio rapido

Installa l'SDK .NET 11.0.100 oppure apri il repository nel Dev Container.

```bash
dotnet tool restore
dotnet csharpier --version
dotnet ef --version
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

`.config/dotnet-tools.json` fissa **CSharpier 1.4.0** e **dotnet-ef 11.0.0**, così sviluppatori, agenti e CI possono utilizzare le stesse versioni senza installazioni globali:

```bash
dotnet tool restore
dotnet ef --version
dotnet csharpier format src tests
dotnet csharpier check src tests
```

Gli script `scripts/validate.sh` e `scripts/validate.ps1` ripristinano entrambi i tool, ne verificano l'esecuzione ed effettuano build e test.

**Nota:** l'esempio usa un archivio in memoria e non contiene un `DbContext` EF Core. `dotnet ef` è disponibile come strumento CLI, ma per usare migrazioni e comandi sui database in un progetto occorre configurare Entity Framework Core e il relativo pacchetto `Microsoft.EntityFrameworkCore.Design`. Il controllo del formato è attualmente separato dalla CI: sul campione iniziale potrebbe essere necessario eseguire `format` prima di `check`.

## Guida per la conferenza

Consulta **[demo.md](demo.md)** per tutti i passaggi, i prompt in italiano, i comandi e i piani alternativi per la dimostrazione dal vivo.

## Agent Skills ufficiali Microsoft

Sono incluse come **copie locali versionate** cinque skill tratte da [dotnet/skills](https://github.com/dotnet/skills), repository ufficiale del team .NET. Le copie originali, mantenute in inglese, sono disponibili in `.github/skills/` e possono essere individuate da Copilot nel contesto del progetto.

| Skill | Utilizzo nel progetto |
| --- | --- |
| `dotnet-webapi` | Convenzioni e semantica HTTP per le Minimal API Orders |
| `directory-build-organization` | `Directory.Build.props`, `Directory.Build.targets`, Central Package Management |
| `property-patterns` | Condizioni e proprietà MSBuild |
| `run-tests` | Comandi e modalità corrette per eseguire i test .NET |
| `test-gap-analysis` | Individuazione di comportamenti non coperti dai test |

**Provenienza:** [dotnet/skills, commit `2877c712`](https://github.com/dotnet/skills/tree/2877c712edc20643b72f64e2983b168e6661d607), licenza MIT conservata in [`.github/skills/LICENSE.dotnet-skills`](.github/skills/LICENSE.dotnet-skills). Queste copie non si aggiornano automaticamente. Le due skill `implement-dotnet-feature` e `validate-dotnet-architecture` rimangono personalizzate per Orders e non sono state sostituite.

La copia nel repository è distinta dall'installazione dei plugin tramite marketplace a livello di utente: qui vogliamo che le skill siano disponibili e versionate insieme al codice, senza configurazioni personali.

## Contratto del repository

- `AGENTS.md`: punto di ingresso e regole comuni per gli agenti.
- `.github/copilot-instructions.md` e `.github/instructions/`: istruzioni per Copilot.
- `.github/skills/`: procedure riutilizzabili, documenti e script.
- `.github/agents/`: definizioni di agenti specializzati.
- `.devcontainer/`: ambiente di sviluppo riproducibile.
- `Directory.Build.props`, `Directory.Build.targets`, `Directory.Packages.props`, `.editorconfig`: regole di compilazione e convenzioni.
- `tests/Orders.ArchitectureTests/`: dipendenze architetturali verificabili.

Non approfondiamo Spec Kit o .NET Aspire: sono argomenti di altre sessioni della conferenza.
