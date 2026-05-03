# BooleanLiteral

Represents a C# boolean literal.

## BID

```bid
Value
--
--
--
```

## Validation

- `Value` is required.
- `Value` must be a supported bool-like value.
- Throw `CSharpFormatException` when `Value` is null, empty, whitespace, or not bool-like.

## Emitter Logic

- Emit `true` when `Value` parses as true.
- Emit `false` when `Value` parses as false.
- Output must be lowercase.
- Do not append whitespace.
- Do not append a newline.
- Do not manage indentation.

## Happy Path Tests

- `EmitTo_WithTrueValue_ReturnTrue`
  - Builder:
    - `.WithValue("true")`
  - Expected:
    - `true`

- `EmitTo_WithFalseValue_ReturnFalse`
  - Builder:
    - `.WithValue("false")`
  - Expected:
    - `false`

## Validation Tests

- `Validate_MissingValue_ThrowException`
  - Builder:
    - no values
  - Exception:
    - `CSharpFormatException`

- `Validate_InvalidValue_ThrowException`
  - Builder:
    - `.WithValue("maybe")`
  - Exception:
    - `CSharpFormatException`

## Edge Case Tests

- `EmitTo_WithUppercaseTrueValue_ReturnTrue`
  - Builder:
    - `.WithValue("True")`
  - Expected:
    - `true`

- `EmitTo_WithUppercaseFalseValue_ReturnFalse`
  - Builder:
    - `.WithValue("False")`
  - Expected:
    - `false`