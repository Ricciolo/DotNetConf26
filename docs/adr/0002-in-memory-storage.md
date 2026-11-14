# ADR 0002: Use in-memory storage for the stage demo

**Status:** Accepted

## Context

The talk is about repositories, agent guidance and validation, not SQL or distributed infrastructure.

## Decision

Use a concurrent in-memory repository singleton. Avoid external dependencies and credentials.

## Consequences

The application can start with a single command. Data is lost at restart; persistence, migrations, authentication and production resilience are out of scope.
