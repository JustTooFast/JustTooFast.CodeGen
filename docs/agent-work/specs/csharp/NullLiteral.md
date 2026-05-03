# NullLiteral

Represents a C# null literal.

## BID

```bid
--
--
--
```

## Validation

None

## Emitter Logic

- Emit `null`.
- Do not append whitespace.
- Do not append a newline.
- Do not manage indentation.

## Happy Path Tests

- `EmitTo_ReturnNull`
  - Builder:
    - no values
  - Expected:
    - `null`

## Validation Tests

None

## Edge Case Tests

None