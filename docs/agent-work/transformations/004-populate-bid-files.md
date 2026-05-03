# 004 — Populate BID Files

## Purpose

Populate each entity BID file from the matching entity spec.

## Input

One entity manifest from:

    docs/agent-work/manifests/

The manifest must contain an `## Entities` section.

One matching spec file for each entity from:

    docs/agent-work/specs/<area>/

The area is inferred from the manifest title.

Example manifest title:

    # CSharp 001 — Leaf Entities

Inferred area:

    csharp

Example entity:

    BooleanLiteral

Expected spec file:

    docs/agent-work/specs/csharp/BooleanLiteral.md

## Entity Parsing

Read every bullet item under the `## Entities` section.

Each bullet item is treated as one entity name.

Example:

    - BooleanLiteral

Entity name:

    BooleanLiteral

## Spec Parsing

For each entity:

1. Open the matching spec file.
2. Find the `## BID` section.
3. Find the fenced `bid` code block immediately under `## BID`.
4. Copy only the contents inside the fenced code block.
5. Do not copy the opening or closing fence.
6. Preserve the contents exactly.

Example spec section:

    ## BID

    ```bid
    Value
    --
    --
    --
    ```

Copied BID contents:

    Value
    --
    --
    --

## File Location Convention

Populate BID files in the input folder for the manifest area.

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

For each entity, populate:

    <Entity>.bid

Examples:

    BooleanLiteral.bid
    Identifier.bid
    StringLiteral.bid

## Rules

- Read the selected manifest.
- Infer the area from the manifest title.
- Read the entities listed under `## Entities`.
- For each entity, read the matching spec file.
- Copy the contents of the spec's fenced `bid` block into the matching `.bid` file.
- Modify only `.bid` files derived from entities in the selected manifest.
- Do not modify emitter files.
- Do not modify test files.
- Do not create missing spec files.
- Do not invent BID contents.
- Do not add comments to BID files.
- Do not perform any other transformation.

## Missing Files

If a BID file does not exist:

- create it
- populate it from the spec

If a spec file does not exist:

- do not create the spec
- do not populate the BID file
- report the entity as blocked

If the spec exists but has no `## BID` section:

- do not populate the BID file
- report the entity as blocked

If the `## BID` section has no fenced `bid` block:

- do not populate the BID file
- report the entity as blocked

## Existing BID Contents

If a BID file already has contents:

- replace the entire file with the contents from the spec's fenced `bid` block

## Output Report

After applying this transformation, report:

- selected manifest
- inferred area
- BID input folder
- populated BID files
- created BID files
- blocked entities and reasons
- errors