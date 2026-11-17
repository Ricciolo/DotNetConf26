# Architettura della soluzione

Applicazione didattica a layer, volutamente piccola. Non è un'architettura di riferimento per la produzione.

```text
Client HTTP -> Orders.Api -> Orders.Application -> Orders.Domain
                         |                   ^
                         v                   |
                Orders.Infrastructure -------+
```

## Responsabilità

| Layer | Responsabilità | Dipendenze |
| --- | --- | --- |
| Domain | Entità, righe d'ordine e invarianti di business | Nessuna |
| Application | Casi d'uso di creazione, elenco e dettaglio; interfaccia del repository | Domain |
| Infrastructure | Repository in memoria | Application, Domain |
| API | Minimal API, DTO HTTP, registrazione dei servizi | Application, Infrastructure, Domain |

La direzione delle dipendenze è importante. `IOrderRepository` appartiene ad Application perché rappresenta ciò che serve ai casi d'uso. Infrastructure implementa tale interfaccia. API usa Domain soltanto per tradurre gli errori al confine HTTP: gli invarianti non vanno nell'endpoint.

## Vincoli verificabili

- `tests/Orders.ArchitectureTests`: verifica i `ProjectReference` consentiti.
- `Directory.Build.props`: definisce target framework, nullability, analyzer e warning come errori.
- `Directory.Build.targets`: interrompe la build in caso di impostazioni fondamentali non conformi.
- `Directory.Packages.props`: centralizza le versioni dei pacchetti NuGet.
- `global.json`: fissa la banda dell'SDK .NET 11, consentendo aggiornamenti patch.

## Scelte per l'esecuzione

L'API utilizza un repository in memoria, thread-safe, registrato come singleton. I dati non sopravvivono al riavvio. Non servono database, broker o credenziali: la demo resta concentrata sul repository, non sull'infrastruttura cloud.

## Cosa l'agente non deve fare

- Aggiungere una dipendenza diretta da Domain verso Infrastructure.
- Mettere le regole per annullare un ordine dentro l'endpoint HTTP.
- Sostituire i test architetturali con commenti o eliminare i test falliti.
- Introdurre un altro framework di dependency injection senza un motivo concreto.
- Dichiarare una verifica riuscita senza averne eseguito i comandi.

Le decisioni e i compromessi sono descritti in `docs/adr/`.
