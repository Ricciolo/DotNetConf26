---
applyTo: "**/*.cs"
description: "C# and ASP.NET Core conventions for the Orders sample"
---

# C# conventions

- Use file-scoped namespaces, nullable reference types and explicit names for public APIs.
- Use constructor injection and keep interfaces at the consuming layer.
- Put business rules and invariants in `Orders.Domain`.
- Keep HTTP contracts in `Orders.Api.Contracts`; do not expose persistence details to clients.
- Map business errors to appropriate HTTP status codes at the API boundary.
- Accept and pass `CancellationToken` for asynchronous I/O.
- Keep methods small and side effects explicit; avoid needless abstractions.
- Use `TimeProvider` where time must be testable.
- Add tests for success, invalid inputs and state transitions.
- Run the architecture suite whenever changing a `.csproj` or project reference.
