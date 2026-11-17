# Esempio di endpoint HTTP

Questa è una traccia illustrativa, non un'implementazione pronta da copiare.

```csharp
app.MapPost("/resources/{id:guid}/action", async (
    Guid id,
    ResourceService service,
    CancellationToken cancellationToken) =>
{
    // Recupera la risorsa attraverso il layer Application.
    // Restituisci 404 se la risorsa non esiste.
    // Converti eventuali conflitti di dominio in 409.
    // Usa un contratto di risposta stabile.
});
```

Mantieni gli invarianti nel Domain. Gestisci gli errori di business al confine HTTP. Testa sia il successo sia i casi di errore.
