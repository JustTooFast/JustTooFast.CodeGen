# DecimalLiteral

Represents a C# decimal literal.

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
    - `.WithValue("123.45m")`
  - Expected:
    - `123.45m`

## Validation Tests

- `Validate_MissingValue_ThrowException`
  - Builder:
    - no values
  - Exception:
    - `CSharpFormatException`

## Edge Case Tests

- `EmitTo_WithZeroValue_ReturnZeroValue`
  - Builder:
    - `.WithValue("0m")`
  - Expected:
    - `0m`

- `EmitTo_WithNegativeValue_ReturnNegativeValue`
  - Builder:
    - `.WithValue("-12.5m")`
  - Expected:
    - `-12.5m`