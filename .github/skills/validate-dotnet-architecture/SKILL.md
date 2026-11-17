---
name: validate-dotnet-architecture
description: Verifica dipendenze tra progetti, regole di compilazione e test .NET. Utilizzala dopo modifiche all'architettura, ai progetti, ai pacchetti o durante una code review.
compatibility: Richiede .NET SDK 11
metadata:
  author: dotnet-conf-italia
  version: "1.0"
---

# Verificare l'architettura .NET

1. Consulta la tabella delle dipendenze nelle [regole architetturali](references/dependency-rules.md).
2. Esegui i test architetturali con `scripts/check-architecture.sh` oppure `scripts/check-architecture.ps1` su Windows.
3. Se la modifica interessa il comportamento applicativo, esegui anche `scripts/validate.sh` dalla radice del repository.
4. Identifica le dipendenze effettivamente non consentite senza proporre di disattivare i controlli.
5. Riporta in italiano i comandi eseguiti, i risultati e gli eventuali limiti della verifica.

Non modificare silenziosamente `Directory.Build.props`, `Directory.Build.targets`, `global.json` o `Directory.Packages.props` per ottenere una build positiva.
## Competenze Microsoft correlate

- `../directory-build-organization/SKILL.md`: organizzazione dei file `.props`, `.targets` e del Central Package Management.
- `../property-patterns/SKILL.md`: problemi specifici di valori e condizioni MSBuild.
- `../run-tests/SKILL.md`: comandi corretti per l'esecuzione dei test.
- `../test-gap-analysis/SKILL.md`: verifica di comportamenti non coperti quando esplicitamente richiesta.

Non caricare tutte le skill contemporaneamente: seleziona quelle pertinenti al problema. Le regole di dipendenza specifiche di Orders restano vincolanti.

