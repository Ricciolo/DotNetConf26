# ADR 0001: Layered architecture with explicit dependencies

**Status:** Accepted

## Context

An agent can generate valid C# that violates existing design boundaries. We want a small application with boundaries clear enough to explain and verify.

## Decision

Use four projects: Domain, Application, Infrastructure and API. Keep interfaces with the consuming application layer. Test approved project-reference directions.

## Consequences

Layer violations are discoverable by repeatable tests, rather than relying only on prompts. The extra projects are deliberate teaching overhead for a small example.
