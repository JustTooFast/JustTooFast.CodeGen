# Transformations

This folder contains mechanical transformations for agent work.

A transformation defines one repeatable step that can be applied to a selected entity manifest.

Manifests decide which entities are included in a batch.  
Specs provide entity-specific details.  
Transformations define the mechanical work to perform.

## Naming

Use this format:

    <number>-<action-name>.md

Example:

    001-create-empty-bid-files.md

## How Transformations Are Used

Each transformation is applied to one selected manifest.

The transformation reads the manifest, finds the entities listed under `## Entities`, and performs the described step for each entity or for the full batch.

Some transformations are per-entity transformations, such as creating one file for each entity.

Some transformations are batch checkpoint transformations, such as building or running tests once after a full batch has been updated.

## Rules

- Keep transformations reusable.
- Do not put entity-specific behavior directly in transformations.
- Read entity-specific details from specs when needed.
- Do not infer additional entities beyond the selected manifest.
- Do not perform work outside the selected transformation.
- Do not combine multiple transformations into one step.
- Report created, modified, skipped, blocked, and failed items.

## Relationship Between Folders

    manifests/
      Lists entity batches.

    specs/
      Defines details for individual entities.

    transformations/
      Defines mechanical steps applied to manifests.

## Current Transformation Sequence

    001-create-empty-bid-files.md
    002-create-empty-emitter-files.md
    003-create-empty-test-files.md
    004-populate-bid-files.md
    005-build-after-bid-generation.md
    006-populate-emitter-structure.md
    007-build-after-emitter-structure.md
    008-implement-emitter-logic.md
    009-add-happy-path-tests.md
    010-add-validation-tests.md
    011-add-edge-case-tests.md
    012-run-tests.md

## Checkpoint Transformations

Build and test transformations are batch checkpoints.

They should run once for the selected manifest, not once per entity.

Examples:

    005-build-after-bid-generation.md
    007-build-after-emitter-structure.md
    012-run-tests.md

## Adding New Transformations

Add a new transformation when a new mechanical step is needed.

A transformation should explain:

- its purpose
- its input
- when to run it
- whether it runs per entity or once per manifest
- what files it may modify
- what files it must not modify
- how to handle missing files
- how to report results