# QualifiedName

Represents a C# qualified name made from one or more parts.

## BID

```bid
--
--
Part
--
```

## Validation

- At least one `Part` is required.
- Throw `CSharpFormatException` when no parts are provided.

## Emitter Logic

- Emit each `Part`.
- Separate parts with `.`.
- Do not add whitespace around `.`.
- Do not append whitespace.
- Do not append a newline.
- Do not manage indentation.

## Happy Path Tests

- `EmitTo_WithSinglePart_ReturnPart`
  - Builder:
    - `.AddPart("System")`
  - Expected:
    - `System`

- `EmitTo_WithMultipleParts_ReturnDottedName`
  - Builder:
    - `.AddPart("System")`
    - `.AddPart("Text")`
  - Expected:
    - `System.Text`

## Validation Tests

- `Validate_MissingParts_ThrowException`
  - Builder:
    - no parts
  - Exception:
    - `CSharpFormatException`

## Edge Case Tests

None