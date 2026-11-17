# Dominio: gestione degli ordini

Il progetto dimostra come gli agenti possano rispettare regole di business esplicite, verificabili tramite test.

## Regole già implementate

- **Cliente:** nome obbligatorio, da 1 a 120 caratteri, esclusi gli spazi iniziali e finali.
- **Righe:** ogni ordine deve contenere da 1 a 100 righe.
- **Codice articolo (SKU):** da 1 a 64 caratteri; viene normalizzato in maiuscolo.
- **Unicità:** uno SKU può comparire una sola volta per ordine, senza distinzione tra maiuscole e minuscole.
- **Quantità:** deve essere maggiore di zero.
- **Prezzo unitario:** non può essere negativo.
- **Totale ordine:** somma di quantità × prezzo unitario di ciascuna riga.
- **Stato iniziale:** ogni nuovo ordine viene creato nello stato `Submitted` (inviato), con data e ora UTC.

## Funzionalità da implementare durante la demo: annullamento ordine

**Requisito:** permettere l'annullamento di un ordine già inviato.

- Solo un ordine nello stato `Submitted` può essere annullato.
- L'annullamento riuscito cambia lo stato in `Cancelled` (annullato).
- Se l'ordine non esiste: **HTTP 404**.
- Se l'ordine è già stato annullato: **HTTP 409**.
- Endpoint da aggiungere: `POST /orders/{id:guid}/cancel`.
- La creazione, l'elenco, il dettaglio e il calcolo dei totali devono continuare a funzionare.

## Confini architetturali

- **Domain:** controlla la transizione di stato e impedisce annullamenti non validi.
- **Application:** coordina il caso d'uso e il recupero dell'ordine.
- **Infrastructure:** mantiene i dettagli della persistenza, senza regole di business.
- **API:** converte l'esito nei codici HTTP appropriati.

Questi requisiti sono intenzionalmente documentati, ma **la cancellazione non è ancora implementata**: costituisce l'esercizio live con Copilot. I nomi dei tipi e dei valori enum esistenti restano in inglese per non modificare i contratti tecnici della solution.
