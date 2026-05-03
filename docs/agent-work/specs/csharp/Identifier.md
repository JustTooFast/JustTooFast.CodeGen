# Identifier

Represents a C# identifier.

## BID

```bid
Text
--
--
--
```

## Validation

- `Text` is required.
- Throw `CSharpFormatException` when `Text` is null, empty, or whitespace.

## Emitter Logic

- Emit `Text`.
- Do not append whitespace.
- Do not append a newline.
- Do not manage indentation.

## Happy Path Tests

- `EmitTo_WithText_ReturnText`
  - Builder:
    - `.WithText("Customer")`
  - Expected:
    - `Customer`

## Validation Tests

- `Validate_MissingText_ThrowException`
  - Builder:
    - no values
  - Exception:
    - `CSharpFormatException`

## Edge Case Tests

None