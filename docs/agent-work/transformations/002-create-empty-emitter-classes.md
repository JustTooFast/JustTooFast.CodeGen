# 002 — Create Empty Emitter Files

## Purpose

Create an empty emitter file for each entity listed in a selected manifest.

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

Create emitter files in the source folder for the manifest area.

The manifest area is inferred from the manifest title.

Example title:

    # CSharp 001 — Leaf Entities

Inferred area:

    CSharp

Emitter folder:

    src/JustTooFast.CodeGen.<Area>/

For the example above:

    src/JustTooFast.CodeGen.CSharp/

## File Naming Convention

For each entity, create:

    <Entity>Emitter.cs

Examples:

    NullLiteralEmitter.cs
    IdentifierEmitter.cs
    StringLiteralEmitter.cs

## File Contents

Created emitter files must be empty.

## Rules

- Read the selected manifest.
- Infer the area from the manifest title.
- Read the entities listed under `## Entities`.
- Create one empty emitter file for each entity.
- Create parent directories only if needed.
- Do not modify existing files.
- Do not create BID files.
- Do not create test files.
- Do not add namespaces.
- Do not add class declarations.
- Do not add comments.
- Do not add implementation logic.
- Do not perform any other transformation.

## Existing Files

If an emitter file already exists:

- leave it unchanged
- report it as skipped

## Output Report

After applying this transformation, report:

- selected manifest
- inferred area
- emitter folder
- created emitter files
- existing emitter files skipped
- errors or blocked items