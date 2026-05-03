# 008 — Implement Emitter Logic

## Purpose

Implement `Validate` and `EmitTo` for each emitter in the selected manifest using the matching entity spec.

This transformation fills in entity-specific validation and emission behavior. It does not add or modify tests.

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

## When To Run

Run this transformation after:

    007 — Build After Emitter Structure

has completed successfully for the selected manifest.

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
2. Read the `## Validation` section.
3. Read the `## Emitter Logic` section.
4. Use only those sections to implement `Validate` and `EmitTo`.

Do not use the test sections to infer additional behavior.

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

For each entity, modify:

    <Entity>Emitter.cs

Examples:

    BooleanLiteralEmitter.cs
    IdentifierEmitter.cs
    StringLiteralEmitter.cs

## Expected Starting Structure

Each emitter file is expected to contain the standard structure from transformation `006 — Populate Emitter Structure`:

    public partial class <Entity>Emitter
    {
        private partial void Validate()
        {
        }

        public partial void EmitTo(IAppender appender)
        {
        }
    }

## Implementation Rules

- Read the selected manifest.
- Infer the area from the manifest title.
- Read the entities listed under `## Entities`.
- For each entity, read the matching spec file.
- Implement `Validate` from the spec's `## Validation` section.
- Implement `EmitTo` from the spec's `## Emitter Logic` section.
- Modify only emitter files derived from entities in the selected manifest.
- Do not modify BID files.
- Do not modify test files.
- Do not create missing spec files.
- Do not add new abstractions.
- Do not add helper methods unless the spec explicitly requires them.
- Do not change class names, namespaces, or method signatures.
- Do not add comments explaining the implementation.
- Do not perform any other transformation.

## Validation Rules

If the spec's `## Validation` section says `None`:

- Leave `Validate` empty.

If the spec defines validation rules:

- Implement only those rules.
- Use the exception type named in the spec.
- Use the entity backing field generated from the BID model.

Example for `Identifier`:

    if (string.IsNullOrWhiteSpace(m_Identifier.Text))
        throw new CSharpFormatException("Identifier.Text is required.");

## EmitTo Rules

If the spec's `## Emitter Logic` section says to emit a literal token:

    appender.Append("null");

If the spec says to emit a scalar property:

    appender.Append(m_Identifier.Text);

If the spec says to emit multiple items separated by a delimiter:

- Iterate the generated collection.
- Append the delimiter only between items.
- Do not append leading or trailing delimiters.

Example pattern:

    var isFirst = true;

    foreach (var part in m_QualifiedName.Parts)
    {
        if (!isFirst)
            appender.Append('.');

        appender.Append(part);
        isFirst = false;
    }

## Missing Files

If an emitter file does not exist:

- do not create it
- report the entity as blocked

If a spec file does not exist:

- do not create it
- do not modify the emitter file
- report the entity as blocked

If the spec has no `## Validation` section:

- do not modify the emitter file
- report the entity as blocked

If the spec has no `## Emitter Logic` section:

- do not modify the emitter file
- report the entity as blocked

## Existing Emitter Contents

If an emitter file still has empty `Validate` and `EmitTo` methods from the standard scaffold:

- populate those methods.

If an emitter file already contains implementation logic:

- do not overwrite it automatically.
- report it as skipped to avoid overwriting hand-written work.

## Output Report

After applying this transformation, report:

- selected manifest
- inferred area
- emitter folder
- implemented emitter files
- skipped emitter files
- blocked entities and reasons
- errors