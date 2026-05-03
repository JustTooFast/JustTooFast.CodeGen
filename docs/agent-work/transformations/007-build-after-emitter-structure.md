# 007 — Build After Emitter Structure

## Purpose

Run one build after all emitter files for the selected manifest have been populated with the standard emitter structure.

This verifies that the batch of emitter scaffolds compiles before later transformations implement validation or emission logic.

## Input

One entity manifest from:

    docs/agent-work/manifests/

The manifest must contain an `## Entities` section.

## When To Run

Run this transformation once for the selected manifest after transformation `006 — Populate Emitter Structure` has completed for every entity in the manifest.

Do not run this transformation once per entity.

## Build Scope

Run the build for the solution or project associated with the manifest area.

The manifest area is inferred from the manifest title.

Example title:

    # CSharp 001 — Leaf Entities

Inferred area:

    CSharp

Default project path:

    src/JustTooFast.CodeGen.<Area>/JustTooFast.CodeGen.<Area>.csproj

For the example above:

    src/JustTooFast.CodeGen.CSharp/JustTooFast.CodeGen.CSharp.csproj

## Rules

- Read the selected manifest.
- Infer the area from the manifest title.
- Run exactly one build for the selected manifest.
- Do not run one build per entity.
- Do not modify BID files.
- Do not modify emitter files.
- Do not modify test files.
- Do not create files.
- Do not fix build errors during this transformation.
- Do not perform any other transformation.

## Build Command

Run:

    dotnet build src/JustTooFast.CodeGen.<Area>/JustTooFast.CodeGen.<Area>.csproj

If the project path does not exist, run the repository’s normal solution-level build command instead.

## Success Criteria

The build succeeds without errors.

Warnings may be reported but should not be fixed during this transformation.

## Failure Handling

If the build fails:

- stop
- do not modify files
- report the build command used
- report the failing errors
- identify likely related entities if possible
- mark the transformation as failed for the selected manifest

## Output Report

After applying this transformation, report:

- selected manifest
- inferred area
- build command used
- build result
- warnings, if any
- errors, if any