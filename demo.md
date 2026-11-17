# Demo dal vivo: repository .NET agent-ready

**Evento:** .NET Conference Italia 2026, 18 novembre 2026  
**Sessione:** GitHub Copilot e .NET: progettare un repository per lo sviluppo agentico  
**Durata:** 45 minuti, inclusi circa 14 minuti di demo.

Questo file è il copione dello speaker. Il `README.md` è invece il punto di ingresso per chi scarica il repository.

## 0. Preparazione prima di salire sul palco (fuori dai 45 minuti)

1. Verifica la disponibilità di .NET SDK 11.0.100 (o patch successive della banda 11.0.1xx), Git, VS Code e GitHub Copilot.
2. Apri il progetto con **Dev Containers: Reopen in Container**, oppure usa il terminale locale con SDK 11 già installato.
3. Controlla `dotnet --info` e `dotnet --list-sdks`.
4. Esegui `dotnet tool restore`, `dotnet csharpier --version` e `dotnet ef --version` per mostrare le versioni riproducibili degli strumenti CLI. Poi esegui `bash scripts/validate.sh` (oppure `./scripts/validate.ps1` in PowerShell).
5. In un terminale dedicato avvia `dotnet run --project src/Orders.Api --urls http://localhost:5080`.
6. Verifica `curl http://localhost:5080/health`.
7. Se vuoi confrontare agenti con e senza istruzioni, prepara due copie o due branch puliti. Evita operazioni di reset che possano cancellare lavoro non previsto.
8. Tieni aperto questo file come checklist e aumenta la dimensione del font del terminale.
9. Prova la versione reale di VS Code/Copilot che utilizzerai sul palco: l'invocazione manuale delle skill e le funzionalità sperimentali possono cambiare.
10. Prepara una versione già completata della funzionalità di annullamento, da mostrare come piano alternativo se la generazione live richiede troppo tempo.

**Attenzione:** il repository iniziale contiene già istruzioni e skill. Non presentare quindi l'esecuzione iniziale come un test senza contesto. Per un vero confronto, crea una copia di lavoro senza i file di guidance e dichiara esattamente quali hai rimosso. In alternativa usa un confronto puramente illustrativo.

### Comandi utili

```bash
dotnet --info
dotnet tool restore
dotnet csharpier --version
dotnet ef --version
dotnet restore DotNetConf26.slnx
dotnet build DotNetConf26.slnx
dotnet test DotNetConf26.slnx
bash scripts/validate.sh
dotnet run --project src/Orders.Api --urls http://localhost:5080
curl http://localhost:5080/health
```

## 1. Introduzione: il problema (00:00–05:00)

**Messaggio da trasmettere:** codice che compila non significa codice coerente con il progetto.

Tre domande fondamentali per un agente:

- Come è organizzato il repository e perché?
- Dove può eseguire il codice e i test?
- Come può dimostrare che una modifica è corretta?

**Mostra:** `src/`, `tests/`, gli endpoint già disponibili `POST /orders`, `GET /orders` e `GET /orders/{id}`.

**Prompt da incollare (sola analisi):**

> Analizza questo repository senza modificare alcun file. Spiega in italiano quali sono i confini tra i progetti, come avviene la creazione di un ordine e quali controlli verificano che le modifiche rispettino l'architettura. Cita i percorsi dei file da cui ricavi le informazioni.

**Risultato atteso:** l'agente identifica i layer Domain, Application, Infrastructure e API, rimandando ai documenti e ai test pertinenti.

## 2. Dare contesto all'agente (05:00–13:00)

**Apri in quest'ordine:**

1. `AGENTS.md`, punto di ingresso condiviso tra agenti.
2. `.github/copilot-instructions.md`, istruzioni specifiche per Copilot.
3. `.github/instructions/csharp.instructions.md`, istruzioni applicate ai file C#.
4. `docs/architecture.md`, `docs/domain.md` e una ADR in `docs/adr/`.

**Concetti da raccontare:**

- Istruzioni brevi, persistenti e facilmente individuabili.
- Documentazione approfondita caricata soltanto quando necessaria.
- Separazione tra indicazioni testuali e regole verificabili.
- Niente duplicazione della stessa regola in ogni file.

**Secondo prompt (sola analisi):**

> Individua le regole architetturali e di dominio presenti nel repository. Per ciascuna, indica se è soltanto documentata oppure se esiste un test o una regola di build che ne verifica il rispetto. Riporta i percorsi esatti.

**Passaggio narrativo:** conoscere il progetto non basta se manca l'ambiente per eseguirlo.

## 3. Toolchain .NET riproducibile (13:00–20:00)

**File da mostrare:**

- `global.json`: banda dell'SDK .NET e aggiornamenti patch.
- `Directory.Build.props`: nullable, analyzer, warning come errori, compilazione deterministica.
- `Directory.Build.targets`: controlli obbligatori sulle proprietà dei progetti.
- `Directory.Packages.props`: versioni centralizzate dei pacchetti NuGet.
- `.editorconfig`: convenzioni di stile.
- `.config/dotnet-tools.json`: tool locali **CSharpier 1.4.0** e **dotnet-ef 11.0.0**, senza installazione globale.
- `.devcontainer/devcontainer.json` e Dockerfile: SDK, estensioni e porta esposta.

**Comandi per la dimostrazione:**

```bash
dotnet --version
dotnet tool restore
dotnet csharpier --version
dotnet ef --version
dotnet build DotNetConf26.slnx
```

**Facoltativo:** mostra la differenza tra `dotnet csharpier format <file>` e `dotnet csharpier check <file>`, utilizzando un file modificato appositamente.

**Messaggio da sottolineare:** `global.json` stabilisce l'SDK; `Directory.Packages.props` stabilisce le versioni delle librerie; `dotnet-tools.json` stabilisce la versione degli strumenti CLI (CSharpier e dotnet-ef).

Un Dev Container offre un ambiente riproducibile, **non automaticamente** una barriera di sicurezza o un modello di autorizzazione per gli agenti. Alcuni runtime agentici possono supportarlo diversamente.

## 4. Agent Skills e procedure riutilizzabili (20:00–28:00)

**Mostra in sequenza:**

1. `.github/skills/implement-dotnet-feature/SKILL.md`.
2. Le sue directory `references/`, `scripts/` e `assets/`.
3. `.github/skills/validate-dotnet-architecture/SKILL.md`.
4. `.github/agents/dotnet-reviewer.agent.md`, agente specializzato nella revisione.

**Skill ufficiali Microsoft da mostrare, senza caricarle tutte nel contesto contemporaneamente:**

- `.github/skills/dotnet-webapi/SKILL.md`: endpoint Minimal API, semantica HTTP e DTO.
- `.github/skills/directory-build-organization/SKILL.md`: file `.props`, `.targets` e `Directory.Packages.props`.
- `.github/skills/property-patterns/SKILL.md`: condizioni e impostazioni MSBuild.
- `.github/skills/run-tests/SKILL.md`: esecuzione corretta dei test .NET.
- `.github/skills/test-gap-analysis/SKILL.md`: lacune nella copertura dei comportamenti, utile per l'annullamento.

Le copie sono tratte dal repository Microsoft `dotnet/skills`, rimangono in inglese e sono vincolate al commit indicato nel README. **Non sostituiscono** le skill specifiche Orders scritte in italiano.

**Messaggi chiave:**

- **Discovery:** metadati come nome e descrizione della skill.
- **Activation:** caricamento del contenuto di `SKILL.md` quando pertinente.
- **Resources:** riferimenti, template e script caricati quando servono.
- **Execution:** uso degli strumenti effettivamente disponibili.
- **Composizione:** lo stesso agente può riutilizzare skill diverse.

Distingui le proprietà della specifica Agent Skills dalle estensioni di Copilot/VS Code. Cita **Skills over MCP** soltanto come evoluzione sperimentale, senza demo.

**Comando:**

```bash
bash .github/skills/validate-dotnet-architecture/scripts/check-architecture.sh
```

**Prompt per preparare il task, senza modifiche:**

> Leggi la skill `implement-dotnet-feature` e la relativa checklist. Basandoti sulle regole presenti in `docs/domain.md`, elenca i progetti, le classi e le suite di test che dovremo modificare per implementare l'annullamento di un ordine. Non scrivere ancora codice. Rispondi in italiano.

## 5. Demo completa: implementazione dell'annullamento (28:00–42:00)

**Obiettivo:** aggiungere l'annullamento di un ordine, funzionalità intenzionalmente mancante e documentata in `docs/domain.md`.

### Passo A. Osservare il comportamento iniziale

Avvia l'API e crea un ordine:

```bash
curl -i -X POST http://localhost:5080/orders \
  -H "Content-Type: application/json" \
  -d '{"customer":"Ada","lines":[{"sku":"LIBRO","quantity":2,"unitPrice":12.50}]}'
```

Copia l'`id` restituito. Prima dell'implementazione, l'endpoint `POST /orders/{id}/cancel` non esiste.

### Passo B. Assegnare il task a Copilot

**Prompt completo da incollare:**

> Implementa l'annullamento degli ordini in questo repository utilizzando la skill `implement-dotnet-feature`. Segui `AGENTS.md`, `docs/domain.md` e le convenzioni esistenti. Aggiungi l'endpoint `POST /orders/{id:guid}/cancel` con queste regole: un ordine nello stato `Submitted` diventa `Cancelled`; un identificativo inesistente restituisce HTTP 404; un ordine già annullato restituisce HTTP 409. Implementa l'invariante nel Domain, coordina il caso d'uso in Application, mantieni nell'API soltanto la gestione HTTP e aggiungi test unitari e di integrazione. Non indebolire i vincoli architetturali e non introdurre pacchetti non necessari. Esegui lo script di validazione e riporta i risultati effettivi in italiano.

Se il runtime lo supporta, puoi usare l'invocazione esplicita `/implement-dotnet-feature` anziché affidarti alla selezione automatica.

### Passo C. Esaminare il codice generato

Controlla con la platea:

- La transizione di stato è implementata nel Domain?
- Una seconda cancellazione viene rifiutata dall'invariante?
- Application coordina il recupero dell'ordine e l'operazione?
- L'API restituisce correttamente 404 e 409, senza contenere la regola di business?
- Sono presenti test unitari e test di integrazione per successo, 404 e 409?
- Sono state introdotte dipendenze non consentite o pacchetti inutili?

**Non accettare il risultato soltanto perché compila.** Se emerge un errore sostanziale, chiedi all'agente una correzione mirata.

### Passo D. Eseguire le verifiche

```bash
bash scripts/validate.sh
# Controllo facoltativo del formato, dopo aver formattato i file modificati:
# dotnet csharpier format src tests
# dotnet csharpier check src tests
git diff --stat
git diff -- src/Orders.Domain src/Orders.Application src/Orders.Api tests
```

Se un test fallisce, mostra l'errore e chiedi una correzione. Non modificare i guardrail soltanto per ottenere una build verde.

### Passo E. Verificare gli endpoint HTTP

Se necessario, riavvia l'API. Crea un nuovo ordine con il comando del passo A e sostituisci il valore di `ID_ORDINE` con il GUID restituito.

```bash
ID_ORDINE="<guid-restituito-dalla-post>"
curl -i -X POST "http://localhost:5080/orders/$ID_ORDINE/cancel"
curl -i -X POST "http://localhost:5080/orders/$ID_ORDINE/cancel"
curl -i -X POST "http://localhost:5080/orders/00000000-0000-0000-0000-000000000001/cancel"
```

**Esiti attesi:** primo annullamento riuscito; secondo annullamento con HTTP 409; ordine inesistente con HTTP 404. Il codice preciso e il contenuto della prima risposta di successo dipendono dal contratto HTTP scelto nell'implementazione.

### Passo F. Coinvolgere il revisore

**Prompt di review:**

> Effettua una revisione dell'implementazione dell'annullamento usando la skill `validate-dotnet-architecture`. Verifica correttezza funzionale, architettura, copertura dei test e convenzioni .NET. Riporta gli eventuali problemi in ordine di gravità, indicando file e correzioni. Esegui i controlli disponibili e distingui i problemi osservati dai rischi ipotetici. Rispondi in italiano.

### Piano alternativo se la demo richiede troppo tempo

Mostra il diff di una versione già preparata e i risultati dei test ottenuti durante le prove. Non basare la durata dell'intervento sulla velocità di generazione dell'agente: il pubblico deve vedere il risultato e il processo di validazione.

## 6. Conclusioni (42:00–45:00)

```text
Instructions  -> Comprensione del progetto
Skills        -> Procedure riutilizzabili
Toolchain     -> Ambiente riproducibile
Tests         -> Verifica della correttezza
Architecture  -> Vincoli sulle dipendenze
```

**Messaggio finale:** un repository agent-ready non è una cartella piena di file Markdown. È un contratto ingegneristico che può essere eseguito e verificato.

**Fuori perimetro:** Spec Kit, approfondimento dei workflow GitHub/PR, .NET Aspire e demo di server MCP. Sono temi trattati in altre sessioni.

## Esperimenti facoltativi dopo la conferenza

- Confronta due worktree, uno con e uno senza istruzioni, sullo stesso task.
- Distribuisci task indipendenti tra worktree diversi e misura i conflitti di integrazione.
- Rafforza i test architetturali con ulteriori dipendenze vietate o analyzer.
- Mostra `dotnet tool restore` con la versione già fissata di CSharpier in `.config/dotnet-tools.json`.
- Confronta token, chiamate agli strumenti, durata, errori e test su **più esecuzioni**, senza trarre conclusioni statistiche da una sola demo.
