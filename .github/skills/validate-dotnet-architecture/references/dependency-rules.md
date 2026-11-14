# Approved project dependencies

| Project | May reference |
| --- | --- |
| Orders.Domain | None of the other Orders projects |
| Orders.Application | Orders.Domain |
| Orders.Infrastructure | Orders.Domain, Orders.Application |
| Orders.Api | Orders.Domain, Orders.Application, Orders.Infrastructure |

The tests under `tests/Orders.ArchitectureTests` enforce project-reference rules and inspect selected assembly dependencies.

Build controls also live in `Directory.Build.props`, `Directory.Build.targets` and `.editorconfig`.

When a violation occurs, fix the dependency direction or move contracts to the consuming layer. Do not remove assertions to make the change appear valid.
