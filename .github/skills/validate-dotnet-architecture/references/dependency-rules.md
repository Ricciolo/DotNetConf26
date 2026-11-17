# Dipendenze consentite tra progetti

| Progetto | Progetti referenziabili |
| --- | --- |
| Orders.Domain | Nessun altro progetto Orders |
| Orders.Application | Orders.Domain |
| Orders.Infrastructure | Orders.Domain, Orders.Application |
| Orders.Api | Orders.Domain, Orders.Application, Orders.Infrastructure |

I test in `tests/Orders.ArchitectureTests` verificano i riferimenti tra progetti e alcune dipendenze degli assembly compilati.

Le regole di compilazione sono definite anche in `Directory.Build.props`, `Directory.Build.targets` e `.editorconfig`.

In caso di violazione, correggi la direzione della dipendenza o sposta l'interfaccia nel layer che la utilizza. Non rimuovere le asserzioni solo per far passare i test.
