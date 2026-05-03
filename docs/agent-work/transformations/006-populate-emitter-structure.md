# 006 — Populate Emitter Structure

## Purpose

Populate each empty emitter file with the standard emitter class structure.

This transformation adds only the reusable file structure. It does not implement validation or emission logic.

## Input

One entity manifest from:

    docs/agent-work/manifests/

The manifest must contain an `## Entities` section.

## When To Run

Run this transformation after:

    005 — Build After BID Generation

has completed successfully for the selected manifest.

## Entity Parsing

Read every bullet item under the `## Entities` section.

Each bullet item is treated as one entity name.

Example:

    - BooleanLiteral

Entity name:

    BooleanLiteral

## File Location Convention

Populate emitter files in the source folder for the manifest area.

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

For each entity, populate:

    <Entity>Emitter.cs

Examples:

    BooleanLiteralEmitter.cs
    IdentifierEmitter.cs
    StringLiteralEmitter.cs

## Namespace Convention

Use the namespace inferred from the manifest area:

    JustTooFast.CodeGen.<Area>

For the example above:

    JustTooFast.CodeGen.CSharp

## Class Naming Convention

For each entity, create this class:

    <Entity>Emitter

Examples:

    BooleanLiteralEmitter
    IdentifierEmitter
    StringLiteralEmitter

## File Contents

Replace each emitter file with this structure:

    // Copyright 2023-2026 Matthew Yancer
    // SPDX-License-Identifier: Apache-2.0

    namespace JustTooFast.CodeGen.<Area>;

    public partial class <Entity>Emitter
    {
        private partial void Validate()
        {
        }

        public partial void EmitTo(IAppender appender)
        {
        }
    }

## Rules

- Read the selected manifest.
- Infer the area from the manifest title.
- Read the entities listed under `## Entities`.
- Populate only emitter files derived from entities in the selected manifest.
- Do not modify BID files.
- Do not modify test files.
- Do not implement validation logic.
- Do not implement emission logic.
- Do not read entity specs during this transformation.
- Do not add comments beyond the copyright and SPDX headers.
- Do not perform any other transformation.

## Missing Files

If an emitter file does not exist:

- create it
- populate it with the standard emitter structure

## Existing Emitter Contents

If an emitter file exists and is empty:

- populate it with the standard emitter structure

If an emitter file exists and already has content:

- replace it only if it exactly matches an earlier empty or standard scaffold
- otherwise leave it unchanged
- report it as skipped to avoid overwriting hand-written work

## Output Report

After applying this transformation, report:

- selected manifest
- inferred area
- emitter folder
- populated emitter files
- created emitter files
- skipped emitter files
- errors or blocked items