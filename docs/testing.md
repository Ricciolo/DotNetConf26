# Strategia di test e validazione

- `Orders.UnitTests`: invarianti di dominio e comportamento degli stati.
- `Orders.IntegrationTests`: test dell'API ASP.NET Core in-process tramite `WebApplicationFactory`.
- `Orders.ArchitectureTests`: dipendenze tra progetti e controlli su alcuni assembly compilati.

## Validazione completa

```bash
bash scripts/validate.sh
```

Per eseguire solo i test architetturali:

```bash
dotnet test tests/Orders.ArchitectureTests/Orders.ArchitectureTests.csproj
```

## Formattazione riproducibile

Il manifest `.config/dotnet-tools.json` fissa CSharpier 1.4.0 come tool locale:

```bash
dotnet tool restore
dotnet csharpier format src tests
dotnet csharpier check src tests
```

Gli script di validazione ripristinano e verificano il formatter, ma il controllo di formattazione non è ancora un blocco della CI. Sul codice iniziale potrebbe essere necessario eseguire `format` prima di `check`.

I test non richiedono database esterni né Docker. Il Dev Container offre riproducibilità, ma non è indispensabile se .NET 11 è già disponibile.

Per aggiungere l'annullamento, crea prima un test di dominio sulla transizione di stato; poi implementa il comportamento minimo. Aggiungi test API per il successo, HTTP 404 e HTTP 409.

Non dichiarare mai che i test sono passati senza averli eseguiti realmente.
