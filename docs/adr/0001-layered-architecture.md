# ADR 0001: Architettura a layer e dipendenze esplicite

**Stato:** Accettata

## Contesto

Un agente può generare codice C# corretto dal punto di vista sintattico, ma non rispettare i confini architetturali. Vogliamo un'applicazione piccola, con regole facili da spiegare e da verificare.

## Decisione

Usare quattro progetti: Domain, Application, Infrastructure e API. Le interfacce sono definite nel layer applicativo che le utilizza. Verificare i riferimenti consentiti tramite test architetturali.

## Conseguenze

Le violazioni diventano rilevabili da test ripetibili invece di dipendere soltanto dai prompt. La suddivisione in più progetti introduce complessità intenzionale, utile a scopo didattico.
