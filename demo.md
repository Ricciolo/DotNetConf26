# Live demo script: Agent-ready .NET repository

**Event:** .NET Conference Italia 2026, 18 November 2026  
**Session:** GitHub Copilot e .NET: progettare un repository per lo sviluppo agentico  
**Duration:** 45 minutes

This is the speaker-facing script. `README.md` is the attendee-facing entrypoint.

## 0. Preparation before going on stage (not counted in 45 minutes)

1. Have .NET SDK 11.0.100 (or a newer patch of the 11.0.1xx feature band), Git, VS Code and GitHub Copilot available.
2. Open the project using **Dev Containers: Reopen in Container**, or use a local terminal with SDK 11 already installed.
3. Check `dotnet --info` and `dotnet --list-sdks`.
4. Run `dotnet tool restore` and `dotnet csharpier --version` to show that the formatter version is reproducible. Then execute `bash scripts/validate.sh` (PowerShell: `./scripts/validate.ps1`).
5. Run `dotnet run --project src/Orders.Api --urls http://localhost:5080` in a dedicated terminal.
6. Verify `curl http://localhost:5080/health`.
7. Prepare two copies of the repo or two clean branches if you want to contrast agent behavior; resetting modifications during a live demo must not delete unexpected changes.
8. Keep this file visible as a speaker checklist; enlarge the terminal font.
9. Test the Copilot runtime and the available skill invocation syntax in the exact VS Code version used on stage. Experimental features are not guaranteed.

**Important:** Do not claim the basic sample has no instructions. It already contains them. For a true baseline comparison, create a disposable branch/worktree in which guidance is intentionally removed and document what was removed; otherwise describe the first example as illustrative, not an A/B test.

### Useful commands

```bash
dotnet --info
dotnet tool restore
dotnet csharpier --version
dotnet restore DotNetConf26.slnx
dotnet build DotNetConf26.slnx
dotnet test DotNetConf26.slnx
bash scripts/validate.sh
dotnet run --project src/Orders.Api --urls http://localhost:5080
curl http://localhost:5080/health
```

## 1. Opening: the problem (0:00–5:00)

**Slide/message:** Compiling code is not the same as respecting a software system.

Explain the three questions an agent must answer:
- How is this repository structured and why?
- In which environment can I run and test it?
- How do I know the change is actually correct?

**Show:** `src/`, `tests/`, existing `POST /orders`, `GET /orders` and `GET /orders/{id}`.

Prompt idea (read-only):

> Inspect this repository and explain its project boundaries, how an order is created and what checks prove the change is correct. Do not modify files.

Expected: the agent identifies the Domain, Application, Infrastructure and API layers and references repository docs and tests.

## 2. Give the agent the right context (5:00–13:00)

**Show in this order:**
1. `AGENTS.md` as a brief, portable entrypoint.
2. `.github/copilot-instructions.md` for Copilot guidance.
3. `.github/instructions/csharp.instructions.md` scoped to C# files.
4. `docs/architecture.md`, `docs/domain.md` and one ADR.

**Explain:** Keep short persistent instructions; put detailed information close to the relevant work. Avoid copying the same guidance into every file. Instructions are not enforcement.

Live question:

> Which files define the architectural and domain rules, and which rules are checked automatically? Answer with paths, not guesses.

**Transition:** The repository should also reproduce the toolchain an agent needs.

## 3. Make .NET reproducible (13:00–20:00)

**Show:**
- `global.json`: SDK feature band and patch roll-forward.
- `Directory.Build.props`: nullable, analyzers, warnings-as-errors, deterministic compilation.
- `Directory.Build.targets`: executable checks on project settings.
- `Directory.Packages.props`: centrally pinned NuGet versions.
- `.editorconfig`: code style.
- `.config/dotnet-tools.json`: CSharpier **1.4.0** pinned as a local tool; no global install.
- Show `dotnet tool restore` and `dotnet csharpier --version`. Optionally format a small C# file with `dotnet csharpier format <file>` and verify it with `dotnet csharpier check <file>`.
- `.devcontainer/devcontainer.json` + Dockerfile: SDK and editor extensions, forwarded port.

**Command:**

```bash
dotnet --version
dotnet tool restore
dotnet csharpier --version
dotnet build DotNetConf26.slnx
```

**Explain:** Dev Containers are a reproducible developer environment, not automatically an isolation or authorization boundary for agents. Host, container and background-agent integrations may have different support.

## 4. Skills as reusable workflows (20:00–28:00)

**Show:**
1. `.github/skills/implement-dotnet-feature/SKILL.md`.
2. Its `references/`, `scripts/` and `assets/` folders.
3. `.github/skills/validate-dotnet-architecture/SKILL.md` and its checks.
4. `.github/agents/dotnet-reviewer.agent.md` as a specialist.

**Explain:** Skill discovery reads metadata, activation loads instructions, supporting references/scripts are loaded on demand (progressive disclosure). Distinguish the Agent Skills specification from VS Code-specific frontmatter. Only mention Skills over MCP as an emerging preview, without demo.

Run:

```bash
bash .github/skills/validate-dotnet-architecture/scripts/check-architecture.sh
```

Suggested prompt:

> Read the implement-dotnet-feature skill and its feature checklist. Tell me which source files and test suites would change to implement order cancellation, but do not code yet.

## 5. End-to-end agent demo (28:00–42:00)

**Goal:** Implement order cancellation, the deliberately missing feature described in `docs/domain.md`.

### Step A: Inspect the initial behavior

```bash
curl -i -X POST http://localhost:5080/orders \
  -H "Content-Type: application/json" \
  -d '{"customer":"Ada","lines":[{"sku":"BOOK","quantity":2,"unitPrice":12.50}]}'
```

Copy the returned `id`. Before implementation, `POST /orders/{id}/cancel` is not mapped.

### Step B: Give the implementation task to Copilot

Prompt to paste:

> Implement order cancellation in this repository using the `implement-dotnet-feature` skill. Follow `AGENTS.md` and the domain rules. Add `POST /orders/{id:guid}/cancel`: a submitted order becomes Cancelled; unknown IDs return 404; cancelling twice returns 409. Put the invariant in Domain, orchestrate it in Application, keep HTTP mapping in API, and add unit and integration tests. Do not weaken architecture rules or introduce dependencies. Run the validation script and report the actual results.

If supported by the active agent harness, invoke `/implement-dotnet-feature` explicitly instead of relying on auto-selection.

### Step C: Review the changed files

Check for:
- A Domain transition that rejects cancellation when the order is not Submitted.
- An Application use case handling lookup and persistence responsibilities.
- HTTP 404 and 409 mapping in API, without business logic embedded in the endpoint.
- Unit tests for cancellation and double cancellation.
- Integration tests for success, 404 and 409.
- No new dependency in Domain or unauthorized NuGet package.

Do not rubber-stamp the generated code. Ask the agent to correct substantive issues.

### Step D: Validate

```bash
bash scripts/validate.sh
# Optional formatting validation after explicitly formatting the changed files:
# dotnet csharpier format src tests
# dotnet csharpier check src tests
git diff --stat
git diff -- src/Orders.Domain src/Orders.Application src/Orders.Api tests
```

If tests fail, show the error and ask for a targeted fix. Do not alter guardrails merely to get a green build.

### Step E: Exercise HTTP behavior

Restart the API if needed. Create an order as in Step A and copy its ID.

```bash
# Replace the placeholder with the actual returned order ID
ORDER_ID="<guid-from-post-response>"
curl -i -X POST "http://localhost:5080/orders/$ORDER_ID/cancel"
curl -i -X POST "http://localhost:5080/orders/$ORDER_ID/cancel"
curl -i -X POST "http://localhost:5080/orders/00000000-0000-0000-0000-000000000001/cancel"
```

Expected after implementation: successful first cancellation; 409 on second attempt; 404 for missing order. The exact successful response format depends on the chosen contract.

### Step F: Ask the reviewer

> Review the cancellation implementation using the validate-dotnet-architecture skill. List correctness, architecture and testing findings with file paths. Run available checks and distinguish observed failures from risks.

### If time runs short

Use the prepared changed-file diff or show the test results captured during rehearsal. Do not assume an AI coding task completes in a predictable amount of time. The audience should see the completed concept, not an indefinitely running agent.

## 6. Conclusion (42:00–45:00)

```text
Instructions -> help the agent understand
Skills       -> teach repeatable procedures
Toolchain    -> makes the environment reproducible
Tests        -> verify correctness
Architecture -> constrains dependency direction
```

**Takeaway:** An agent-ready repository is not just a folder with Markdown. It is an executable engineering contract.

Out of scope: Spec Kit, GitHub PR workflow deep dives, .NET Aspire and a live MCP server. Another conference session covers those topics.

## Optional post-talk experiments (not part of the 45 minutes)

- Compare a disposable worktree with and without instruction files, using the same feature request.
- Split independent tasks across worktrees and compare merge conflict rates.
- Make architecture rules stronger with additional forbidden dependency tests or analyzers.
- Add a genuinely required local dotnet tool to `.config/dotnet-tools.json` and use `dotnet tool restore`.
- Measure tokens, tool calls, time and validation failures across repeated runs rather than drawing statistical conclusions from one demo.
