# 010 — Add Validation Tests

## Purpose

Add validation unit tests for each entity in the selected manifest using the matching entity spec.

This transformation adds only validation tests. It does not add happy path tests or edge case tests.

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

    009 — Add Happy Path Tests

has completed for the selected manifest.

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
2. Read the `## Validation Tests` section.
3. Add only the tests listed in that section.
4. Do not use the happy path or edge case test sections during this transformation.

If the `## Validation Tests` section says `None`:

- do not add validation tests for that entity
- report the entity as skipped

## File Location Convention

Modify test files in the test folder for the manifest area.

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

For each entity, modify:

    <Entity>EmitterTest.cs

Examples:

    BooleanLiteralEmitterTest.cs
    IdentifierEmitterTest.cs
    StringLiteralEmitterTest.cs

## Expected Starting Structure

Each test file is expected to exist.

If a test file is empty, populate it with the standard test file structure before adding validation tests:

    // Copyright 2023-2026 Matthew Yancer
    // SPDX-License-Identifier: Apache-2.0

    namespace JustTooFast.CodeGen.<Area>.Tests;

    [TestClass]
    public class <Entity>EmitterTest
    {
    }

If a test file already contains the standard test class, add validation tests inside the class body.

## Test Method Pattern

Use the existing MSTest expected-exception pattern:

    [TestMethod]
    [ExpectedException(typeof(<ExceptionType>))]   //Assert
    public void <TestName>()
    {
        //Arrange
        var builder = new <Entity>Builder()
            <builder calls from spec>;

        //Act
        var target = new <Entity>Emitter(builder);
    }

For tests where the spec says the builder has no values, use:

    var builder = new <Entity>Builder();

The exception type comes from the matching validation test entry in the spec.

## Implementation Rules

- Read the selected manifest.
- Infer the area from the manifest title.
- Read the entities listed under `## Entities`.
- For each entity, read the matching spec file.
- Add only tests listed under `## Validation Tests`.
- Modify only test files derived from entities in the selected manifest.
- Do not modify BID files.
- Do not modify emitter files.
- Do not add happy path tests.
- Do not add edge case tests.
- Do not create missing spec files.
- Do not invent additional tests.
- Do not add new test helpers.
- Do not change existing test method names.
- Do not change existing test expectations.
- Do not perform any other transformation.

## Missing Files

If a test file does not exist:

- create it
- populate it with the standard test file structure
- add the validation tests from the spec

If a spec file does not exist:

- do not create it
- do not modify the test file
- report the entity as blocked

If the spec has no `## Validation Tests` section:

- do not modify the test file
- report the entity as blocked

## Existing Tests

If a validation test already exists with the same method name:

- leave it unchanged
- report it as skipped

If a test file contains hand-written content:

- add only missing validation tests when it is safe to do so
- do not rewrite unrelated tests

## Output Report

After applying this transformation, report:

- selected manifest
- inferred area
- test folder
- added validation tests
- skipped existing tests
- skipped entities with no validation tests
- blocked entities and reasons
- errors