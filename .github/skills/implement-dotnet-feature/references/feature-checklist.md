# Feature checklist

## Before coding
- Clarify expected behavior, state transitions and error cases.
- Read `docs/domain.md` and `docs/architecture.md`.
- Identify whether a new interface or a package is really required.

## Coding order
1. Model the invariant in the Domain project, if applicable.
2. Expose the use case through the Application project.
3. Implement storage or external adapters only if required.
4. Wire HTTP routes and request/response contracts in API.
5. Add domain tests and API integration tests.

## Acceptance criteria for order cancellation
- `POST /orders/{id}/cancel` changes a submitted order to Cancelled.
- Unknown ID returns 404.
- Cancelling an already-cancelled order returns 409.
- Existing create, list and get behavior continues to work.
- `dotnet build` and all tests pass.

## Output
Report modified files, validations performed, HTTP status codes and any assumptions.
