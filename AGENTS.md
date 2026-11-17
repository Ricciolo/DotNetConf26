# Contratto del repository per gli agenti di sviluppo

Leggi questo file prima di modificare la solution. È il punto di ingresso essenziale, valido per agenti diversi. I dettagli sono nei documenti e nelle skill collegate.

## Scopo del repository

Una piccola API ASP.NET Core per la gestione degli ordini, utilizzata alla .NET Conference Italia 2026 per mostrare come progettare repository adatti allo sviluppo agentico.

## Responsabilità dei progetti

- `src/Orders.Domain`: entità, righe d'ordine, regole e invarianti di business. Nessuna dipendenza dagli altri progetti Orders.
- `src/Orders.Application`: casi d'uso e interfacce necessarie all'applicazione. Dipende solo da Domain.
- `src/Orders.Infrastructure`: implementazioni delle interfacce applicative, come l'archivio in memoria. Dipende da Application e Domain.
- `src/Orders.Api`: contratti HTTP, dependency injection e definizione degli endpoint.
- `tests/`: test unitari, di integrazione e architetturali.

Approfondimenti: `docs/architecture.md`, `docs/domain.md`, `docs/testing.md`.

## Procedura obbligatoria

1. Leggi solo i documenti e i sorgenti pertinenti alla richiesta.
2. Identifica i layer coinvolti e motiva le modifiche.
3. Mantieni le regole di business nel Domain, non negli endpoint HTTP né in Infrastructure.
4. Aggiungi o aggiorna i test pertinenti.
5. Esegui `./scripts/validate.sh` (Linux/macOS/Dev Container) oppure `./scripts/validate.ps1` (PowerShell).
6. Comunica i comandi effettivamente eseguiti, i risultati ottenuti e gli eventuali controlli non eseguiti.

## Build e dipendenze

- SDK fissato da `global.json`; solution `DotNetConf26.slnx`.
- Regole comuni in `Directory.Build.props` e `Directory.Build.targets`.
- Versioni NuGet centralizzate in `Directory.Packages.props`; non inserire l'attributo `Version` nei `PackageReference` dei progetti.
- Convenzioni C# e formattazione in `.editorconfig`.
- CSharpier 1.4.0 è fissato da `.config/dotnet-tools.json`. Ripristinalo con `dotnet tool restore`; usa `dotnet csharpier format src tests` per formattare e `dotnet csharpier check src tests` per controllare. Non richiedere un'installazione globale.
- Non disabilitare analyzer, modificare SDK/TFM o indebolire i test soltanto per ottenere un esito positivo.

## Skill e agenti specializzati

- Per sviluppare funzionalità usa `.github/skills/implement-dotnet-feature/SKILL.md`.
- Per verificare l'architettura usa `.github/skills/validate-dotnet-architecture/SKILL.md`.
- `.github/agents/dotnet-reviewer.agent.md` definisce un agente specializzato nella revisione.

## Lingua e sicurezza

Scrivi in **italiano** analisi, piani, spiegazioni, commenti di business e riepiloghi. Mantieni invariati identificatori tecnici C#, nomi dei progetti, endpoint e comandi esistenti.

Non inserire segreti, credenziali o dati reali di produzione. Il Dev Container rende riproducibile l'ambiente, ma non costituisce da solo una barriera di sicurezza.

Non introdurre Spec Kit, flussi di gestione delle issue GitHub o .NET Aspire in questo esempio.
