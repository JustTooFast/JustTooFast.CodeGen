# Specs

This folder contains entity specs for mechanical agent work.

A spec defines the details for one entity.

Manifests decide which entities are included in a batch.  
Transformations decide what mechanical step to apply.  
Specs provide the entity-specific details used by those transformations.

## Naming

Use this format:

    <Entity>.md

Example:

    BooleanLiteral.md

Specs may be grouped by area:

    specs/csharp/BooleanLiteral.md

## Format

Each spec should contain:

```markdown
# EntityName

Short description.

## BID

A fenced `bid` code block containing the exact BID file contents.

## Validation

Validation rules for the emitter.

## Emitter Logic

Emission rules for `EmitTo`.

## Happy Path Tests

Basic successful output tests.

## Validation Tests

Tests for invalid or missing required values.

## Edge Case Tests

Tests for escaping, formatting boundaries, or special values.
```

## BID Section

The BID section must contain the exact contents to write to the `.bid` file.

Use a fenced code block with `bid` as the language.

Example:

````markdown
## BID

```bid
Value
--
--
--
```
````

## Rules

- One spec per entity.
- The spec name should match the entity name from the manifest.
- Do not include transformation instructions.
- Do not include file paths unless a transformation specifically requires them.
- Keep entity-specific details in the spec.
- Keep reusable mechanical steps in transformations.
- The `## BID` section is copied by BID-related transformations.
- The validation, emitter logic, and test sections are read by later transformations.