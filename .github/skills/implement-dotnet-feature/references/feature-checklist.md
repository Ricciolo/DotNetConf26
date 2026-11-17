# Checklist per implementare una funzionalità

## Prima di scrivere codice

- Chiarisci comportamento atteso, transizioni di stato e casi di errore.
- Consulta `docs/domain.md` e `docs/architecture.md`.
- Valuta se servano davvero una nuova interfaccia o un nuovo pacchetto NuGet.

## Ordine di implementazione

1. Introduci l'invariante nel progetto Domain, se necessario.
2. Esporre il caso d'uso tramite Application.
3. Modifica gli adapter di persistenza o dei sistemi esterni solo se necessario.
4. Collega route HTTP e contratti di richiesta/risposta nell'API.
5. Aggiungi test di dominio e test di integrazione API.

## Criteri di accettazione per l'annullamento

- `POST /orders/{id}/cancel` trasforma un ordine da `Submitted` a `Cancelled`.
- Identificativo inesistente: HTTP 404.
- Seconda richiesta di annullamento: HTTP 409.
- Le funzionalità esistenti di creazione, elenco e dettaglio restano operative.
- `dotnet build` e tutti i test devono terminare con esito positivo.

## Resoconto

Indica file modificati, verifiche realmente eseguite, codici HTTP e ipotesi adottate.
