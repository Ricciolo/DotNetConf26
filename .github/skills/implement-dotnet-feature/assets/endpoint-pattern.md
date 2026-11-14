# HTTP endpoint pattern

This is an illustrative shape, not a paste-ready implementation.

```csharp
app.MapPost("/resources/{id:guid}/action", async (
    Guid id,
    ResourceService service,
    CancellationToken cancellationToken) =>
{
    // Resolve the resource through the Application layer.
    // Return 404 when absent.
    // Convert domain rule conflicts to 409.
    // Return a stable response contract.
});
```

Keep domain invariants in Domain. Handle business errors at the HTTP boundary. Test success and failure responses.
