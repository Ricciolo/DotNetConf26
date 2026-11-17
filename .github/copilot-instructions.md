# Istruzioni GitHub Copilot per il repository

Segui il contratto contenuto in `AGENTS.md`. Prima di scrivere codice, consulta le sezioni pertinenti di `docs/architecture.md` e `docs/domain.md`.

Preferisci modifiche piccole e mirate. Rispetta i confini tra i progetti. Non introdurre librerie NuGet se la libreria standard .NET offre già le funzionalità necessarie. Ogni modifica del comportamento richiede test unitari o di integrazione.

Utilizza le Agent Skills presenti nel repository per implementare funzionalità o verificare l'architettura. Non dichiarare mai che una verifica è riuscita se non hai eseguito realmente il comando con esito positivo.

Comunica con lo sviluppatore in italiano. Mantieni invariati nomi tecnici, namespace e contratti HTTP già definiti.
