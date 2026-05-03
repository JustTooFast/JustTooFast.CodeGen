# PredefinedType

Represents a C# predefined type.

## BID

```bid
Name
--
--
--
```

## Validation

- `Name` is required.
- Throw `CSharpFormatException` when `Name` is null, empty, or whitespace.

## Emitter Logic

- Emit `Name`.
- Do not append whitespace.
- Do not append a newline.
- Do not manage indentation.

## Happy Path Tests

- `EmitTo_WithName_ReturnName`
  - Builder:
    - `.WithName("string")`
  - Expected:
    - `string`

## Validation Tests

- `Validate_MissingName_ThrowException`
  - Builder:
    - no values
  - Exception:
    - `CSharpFormatException`

## Edge Case Tests

- `EmitTo_WithIntName_ReturnInt`
  - Builder:
    - `.WithName("int")`
  - Expected:
    - `int`

- `EmitTo_WithBoolName_ReturnBool`
  - Builder:
    - `.WithName("bool")`
  - Expected:
    - `bool`