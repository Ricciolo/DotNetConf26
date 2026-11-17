# ADR 0002: Persistenza in memoria per la demo

**Stato:** Accettata

## Contesto

La sessione tratta repository, istruzioni per agenti e validazione, non SQL o infrastrutture distribuite.

## Decisione

Usare un repository in memoria, thread-safe, registrato come singleton. Evitare dipendenze esterne e credenziali.

## Conseguenze

L'applicazione può essere avviata con un solo comando. I dati si perdono al riavvio. Persistenza duratura, migrazioni, autenticazione e resilienza produttiva restano fuori dall'esempio.
