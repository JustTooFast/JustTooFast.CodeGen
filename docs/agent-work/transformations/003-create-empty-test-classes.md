# 003 — Create Empty Test Files

## Purpose

Create an empty test file for each entity listed in a selected manifest.

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

Create test files in the test folder for the manifest area.

The manifest area is inferred from the manifest title.

Example title:

    # CSharp 001 — Leaf Entities

Inferred area:

    CSharp

Test folder:

    tests/JustTooFast.CodeGen.<Area>.Tests/

For the example above:

    tests/JustTooFast.CodeGen.CSharp.Tests/

## File Naming Convention

For each entity, create:

    <Entity>EmitterTest.cs

Examples:

    NullLiteralEmitterTest.cs
    IdentifierEmitterTest.cs
    StringLiteralEmitterTest.cs

## File Contents

Created test files must be empty.

## Rules

- Read the selected manifest.
- Infer the area from the manifest title.
- Read the entities listed under `## Entities`.
- Create one empty test file for each entity.
- Create parent directories only if needed.
- Do not modify existing files.
- Do not create BID files.
- Do not create emitter files.
- Do not add namespaces.
- Do not add test class declarations.
- Do not add comments.
- Do not add test methods.
- Do not add implementation logic.
- Do not perform any other transformation.

## Existing Files

If a test file already exists:

- leave it unchanged
- report it as skipped

## Output Report

After applying this transformation, report:

- selected manifest
- inferred area
- test folder
- created test files
- existing test files skipped
- errors or blocked items