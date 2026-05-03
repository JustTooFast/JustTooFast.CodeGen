# 012 — Run Tests

## Purpose

Run tests once after all test transformations have been applied to the selected manifest.

This verifies that the batch of emitter implementations and tests work together.

## Input

One entity manifest from:

    docs/agent-work/manifests/

The manifest must contain an `## Entities` section.

## When To Run

Run this transformation once for the selected manifest after these transformations have completed:

    009 — Add Happy Path Tests
    010 — Add Validation Tests
    011 — Add Edge Case Tests

Do not run this transformation once per entity.

## Test Scope

Run tests for the test project associated with the manifest area.

The manifest area is inferred from the manifest title.

Example title:

    # CSharp 001 — Leaf Entities

Inferred area:

    CSharp

Default test project path:

    tests/JustTooFast.CodeGen.<Area>.Tests/JustTooFast.CodeGen.<Area>.Tests.csproj

For the example above:

    tests/JustTooFast.CodeGen.CSharp.Tests/JustTooFast.CodeGen.CSharp.Tests.csproj

## Rules

- Read the selected manifest.
- Infer the area from the manifest title.
- Run exactly one test command for the selected manifest.
- Do not run one test command per entity.
- Do not modify BID files.
- Do not modify emitter files.
- Do not modify test files.
- Do not create files.
- Do not fix test failures during this transformation.
- Do not perform any other transformation.

## Test Command

Run:

    dotnet test tests/JustTooFast.CodeGen.<Area>.Tests/JustTooFast.CodeGen.<Area>.Tests.csproj

If the test project path does not exist, run the repository’s normal solution-level test command instead.

## Success Criteria

The test command succeeds without failures.

Warnings may be reported but should not be fixed during this transformation.

## Failure Handling

If tests fail:

- stop
- do not modify files
- report the test command used
- report the failing tests
- report the failure messages
- identify likely related entities if possible
- mark the transformation as failed for the selected manifest

## Output Report

After applying this transformation, report:

- selected manifest
- inferred area
- test command used
- test result
- total tests run, if available
- passed tests, if available
- failed tests, if any
- skipped tests, if any
- errors, if any