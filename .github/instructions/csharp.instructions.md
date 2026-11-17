---
applyTo: "**/*.cs"
description: "Convenzioni C# e ASP.NET Core per il progetto Orders"
---

# Convenzioni C#

- Usa namespace file-scoped, nullable reference types e nomi espliciti nelle API pubbliche.
- Preferisci la constructor injection; colloca le interfacce nel layer che le utilizza.
- Mantieni regole e invarianti di business in `Orders.Domain`.
- Colloca i contratti HTTP in `Orders.Api.Contracts`; non esporre dettagli di persistenza.
- Converti gli errori di business nei codici HTTP corretti al confine API.
- Accetta e propaga `CancellationToken` nelle operazioni asincrone di I/O.
- Preferisci metodi piccoli ed effetti collaterali espliciti; evita astrazioni non necessarie.
- Utilizza `TimeProvider` quando il comportamento temporale deve essere testabile.
- Verifica casi positivi, input non validi e transizioni di stato.
- Esegui i test architetturali quando modifichi un `.csproj` o un riferimento tra progetti.
- Usa l'italiano nei messaggi di errore rivolti all'utente, senza cambiare identificatori tecnici o route esistenti.
