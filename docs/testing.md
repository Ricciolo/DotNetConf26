# Testing strategy

- `Orders.UnitTests`: fast domain invariants and business state.
- `Orders.IntegrationTests`: in-process ASP.NET Core API tests using WebApplicationFactory.
- `Orders.ArchitectureTests`: project-reference graph and selected compiled dependency checks.

Run:

```bash
bash scripts/validate.sh
```

Or run a targeted suite:

```bash
dotnet test tests/Orders.ArchitectureTests/Orders.ArchitectureTests.csproj
```

Tests do not require an external database or Docker. The Dev Container improves reproducibility but is not necessary when .NET 11 is already installed.

When adding order cancellation, first create a failing domain test for the state transition, then implement the smallest change. Follow with API tests for success, 404 and 409.

Never state that tests passed without an actual successful execution.
