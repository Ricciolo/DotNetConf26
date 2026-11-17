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
