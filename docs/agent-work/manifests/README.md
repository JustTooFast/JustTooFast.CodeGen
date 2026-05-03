# Manifests

This folder contains entity manifests.

A manifest is a named batch of entities that should move through mechanical transformations together.

Manifests do not describe files directly. They list entities only.

Files are created or modified by transformations.

## Naming

Use this format:

```text
<area>-<number>-<description>-entities.md
```

Example:

```text
csharp-001-leaf-entities.md
```

## Format

Each manifest should contain:

```markdown
# CSharp 001 — Leaf Entities

Short description.

## Entities

- EntityName
- AnotherEntityName
```

## Rules

- List entities only.
- Do not list file paths.
- Do not include implementation instructions.
- Do not include transformation steps.
- Add new manifests as new batches of entities are identified.