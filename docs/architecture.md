# Architecture

This is a teaching-sized layered application, not a production reference architecture.

```text
HTTP client -> Orders.Api -> Orders.Application -> Orders.Domain
                      |                ^
                      v                |
             Orders.Infrastructure ----+
```

## Responsibilities

| Layer | Responsibility | References |
| --- | --- | --- |
| Domain | Business invariants, Order, OrderLine | None |
| Application | Create/list/find use cases, repository port | Domain |
| Infrastructure | In-memory repository adapter | Application, Domain |
| API | Minimal API routes, HTTP DTOs, composition root | Application, Infrastructure, Domain |

Dependency direction matters. `IOrderRepository` belongs to Application because it expresses what use cases need. Infrastructure implements that abstraction. API uses Domain only for translation at the boundary; no domain rules should live in API.

## Architectural guardrails

- `tests/Orders.ArchitectureTests` checks expected `ProjectReference` edges.
- `Directory.Build.props` establishes the SDK target, nullability, analyzers and warnings-as-errors policy.
- `Directory.Build.targets` fails when core repository settings are overridden.
- `Directory.Packages.props` controls NuGet versions in one place.
- `global.json` pins a .NET 11 SDK feature band with patch roll-forward.

## Runtime choices

The API is stateless except for a thread-safe in-memory order repository registered as a singleton. Data disappears on process restart. No database or broker is used so the demo stays focused on repository engineering, not cloud infrastructure.

## What the agent must not do

- Add a direct Domain -> Infrastructure dependency.
- Put cancellation invariants in the HTTP endpoint.
- Replace architecture tests with comments or delete failing tests.
- Add another dependency injection framework without a concrete need.
- Return success when validation commands have not been executed.

See `docs/adr/` for the trade-offs behind this design.
