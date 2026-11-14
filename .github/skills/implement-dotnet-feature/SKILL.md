---
name: implement-dotnet-feature
description: Implement or extend an ASP.NET Core Orders feature while respecting layered architecture, domain invariants and executable tests. Use when adding endpoints, application operations or business rules.
license: MIT
compatibility: Requires .NET SDK 11 and a shell
metadata:
  author: dotnet-conf-italia
  version: "1.0"
---

# Implement a .NET feature

Read `AGENTS.md` first. Load only the references that apply to the request.

## Procedure

1. Consult [feature checklist](references/feature-checklist.md).
2. Read `docs/architecture.md` and the relevant business rules in `docs/domain.md`.
3. Identify the change surface: Domain, Application, Infrastructure, API and tests.
4. Add an invariant test before implementing a new domain rule.
5. Implement the smallest coherent vertical change, respecting project references.
6. Add an API integration test if an HTTP contract changes.
7. Run `scripts/validate.sh` from this skill, or the repository's PowerShell equivalent.
8. Summarize the changes, test results and remaining caveats.

## Constraints

- Do not place business invariants solely in the API.
- Do not introduce a package without checking `Directory.Packages.props`.
- Do not bypass warnings-as-errors or architectural constraints.
- Do not claim tests passed unless you ran them.
- Do not include secrets or production data in fixtures.

## Example task

Add `POST /orders/{id}/cancel`. Only submitted orders may be cancelled; return 404 for an unknown ID and 409 for an already-cancelled order. Cover domain behavior and HTTP responses.
