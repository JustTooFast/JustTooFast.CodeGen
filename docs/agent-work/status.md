# Agent Work Status

This file tracks transformation progress for each manifest.

Manifests define batches of entities.  
Transformations define mechanical steps that can be applied to those batches.  
Specs provide the entity-specific details used by transformations.

## Rules

- Update this file after each completed transformation.
- Track progress by manifest.
- A transformation is complete for a manifest only when it has been applied to every entity in that manifest, or when the transformation is explicitly a batch checkpoint.
- Batch checkpoint transformations, such as build and test steps, run once per manifest.
- If a transformation fails, record the failure and stop.
- If an entity is skipped or blocked, record the reason in the notes.
- Do not mark a later transformation complete if an earlier required transformation failed.
- Do not use this file to define new entities, specs, or transformation behavior.

## Status Values

- `Not Started`
- `In Progress`
- `Blocked`
- `Failed`
- `Complete`

## Transformation Sequence

1. `001-create-empty-bid-files.md`
2. `002-create-empty-emitter-files.md`
3. `003-create-empty-test-files.md`
4. `004-populate-bid-files.md`
5. `005-build-after-bid-generation.md`
6. `006-populate-emitter-structure.md`
7. `007-build-after-emitter-structure.md`
8. `008-implement-emitter-logic.md`
9. `009-add-happy-path-tests.md`
10. `010-add-validation-tests.md`
11. `011-add-edge-case-tests.md`
12. `012-run-tests.md`

## Manifest Progress

| Manifest | Status | Last Completed Transformation | Current / Next Transformation | Notes |
|---|---|---|---|---|
| `csharp-001-leaf-entities.md` | `Not Started` | None | `001-create-empty-bid-files.md` | Initial C# leaf entity batch. |
| `csharp-002-simple-composition-entities.md` | `Not Started` | None | None | Do not process yet. |

## Activity Log

| Date | Manifest | Transformation | Result | Notes |
|---|---|---|---|---|