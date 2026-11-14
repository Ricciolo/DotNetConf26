# GitHub Copilot repository guidance

Follow the repository contract in `AGENTS.md`. Before coding, read the relevant sections of `docs/architecture.md` and `docs/domain.md`.

Prefer small, targeted diffs. Respect existing project boundaries. Do not add NuGet dependencies if the BCL already supplies what is needed. Each behavior change requires unit or integration tests.

Use the repository's Agent Skills for feature work or architectural review. Never say a validation succeeded unless its command was actually executed and returned successfully.
