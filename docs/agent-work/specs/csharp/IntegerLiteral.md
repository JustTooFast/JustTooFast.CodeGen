# IntegerLiteral

Represents a C# integer literal.

## BID

```bid
Value
--
--
--
```

## Validation

- `Value` is required.
- Throw `CSharpFormatException` when `Value` is null, empty, or whitespace.

## Emitter Logic

- Emit `Value`.
- Do not append whitespace.
- Do not append a newline.
- Do not manage indentation.

## Happy Path Tests

- `EmitTo_WithValue_ReturnValue`
  - Builder:
    - `.WithValue("42")`
  - Expected:
    - `42`

## Validation Tests

- `Validate_MissingValue_ThrowException`
  - Builder:
    - no values
  - Exception:
    - `CSharpFormatException`

## Edge Case Tests

- `EmitTo_WithZeroValue_ReturnZeroValue`
  - Builder:
    - `.WithValue("0")`
  - Expected:
    - `0`

- `EmitTo_WithNegativeValue_ReturnNegativeValue`
  - Builder:
    - `.WithValue("-1")`
  - Expected:
    - `-1`