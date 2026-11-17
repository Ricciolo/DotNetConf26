---
name: implement-dotnet-feature
description: Implementa o estende funzionalità ASP.NET Core per gli ordini rispettando l'architettura a layer, gli invarianti di dominio e i test. Utilizzala per nuovi endpoint, casi d'uso applicativi e regole di business.
compatibility: Richiede .NET SDK 11 e una shell
metadata:
  author: dotnet-conf-italia
  version: "1.0"
---

# Implementare una funzionalità .NET

Leggi prima `AGENTS.md`. Carica solo i riferimenti pertinenti alla richiesta. Scrivi il piano e il riepilogo in italiano.

## Procedura

1. Consulta la [checklist della funzionalità](references/feature-checklist.md).
2. Leggi `docs/architecture.md` e le regole di business pertinenti in `docs/domain.md`.
3. Identifica i layer da modificare: Domain, Application, Infrastructure, API e test.
4. Prima di aggiungere una nuova regola di dominio, scrivi un test che ne mostri il comportamento atteso.
5. Implementa il cambiamento verticale minimo, rispettando le dipendenze tra progetti.
6. Aggiungi un test di integrazione se cambia un contratto HTTP.
7. Esegui `scripts/validate.sh` di questa skill o l'equivalente PowerShell del repository.
8. Riepiloga le modifiche, i risultati dei test e gli eventuali problemi residui.

## Vincoli

- Non implementare invarianti di business soltanto nell'API.
- Non aggiungere pacchetti senza controllare `Directory.Packages.props`.
- Non aggirare warning trattati come errori né vincoli architetturali.
- Non dichiarare test superati senza averli eseguiti.
- Non usare credenziali o dati di produzione nei test.
- Mantieni identificatori C# e route esistenti; usa l'italiano per spiegazioni e messaggi rivolti all'utente.

## Attività dimostrativa

Aggiungi `POST /orders/{id}/cancel`. Solo un ordine nello stato `Submitted` può essere annullato. Restituisci 404 se l'ordine non esiste e 409 se è già stato annullato. Verifica sia il dominio sia le risposte HTTP.
