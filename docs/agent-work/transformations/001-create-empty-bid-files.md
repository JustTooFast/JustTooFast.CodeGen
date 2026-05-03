# 001 — Create Empty BID Files

## Purpose

Create an empty BID file for each entity listed in a selected manifest.

## Input

One entity manifest from:

    docs/agent-work/manifests/

The manifest must contain an `## Entities` section.

Example manifest:

    # CSharp 001 — Leaf Entities

    Initial C# code generation leaf entities.

    ## Entities

    - NullLiteral
    - Identifier
    - PredefinedType

## Entity Parsing

Read every bullet item under the `## Entities` section.

Each bullet item is treated as one entity name.

Example:

    - NullLiteral

Entity name:

    NullLiteral

## File Location Convention

Create BID files in the input folder for the manifest area.

The manifest area is inferred from the manifest title.

Example title:

    # CSharp 001 — Leaf Entities

Inferred area:

    CSharp

BID input folder:

    src/JustTooFast.CodeGen.<Area>/Input/

For the example above:

    src/JustTooFast.CodeGen.CSharp/Input/

## File Naming Convention

For each entity, create:

    <Entity>.bid

Examples:

    NullLiteral.bid
    Identifier.bid
    StringLiteral.bid

## Rules

- Read the selected manifest.
- Infer the area from the manifest title.
- Read the entities listed under `## Entities`.
- Create one empty `.bid` file for each entity.
- Create parent directories only if needed.
- Do not modify existing files.
- Do not create emitter files.
- Do not create test files.
- Do not populate BID fields.
- Do not add comments to BID files.
- Do not perform any other transformation.

## File Contents

Created BID files must be empty.

## Existing Files

If a BID file already exists:

- leave it unchanged
- report it as skipped

## Output Report

After applying this transformation, report:

- selected manifest
- inferred area
- BID input folder
- created BID files
- existing BID files skipped
- errors or blocked items