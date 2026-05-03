# StringLiteral

Represents a C# string literal.

## BID

```bid
Value
--
--
--
```

## Validation

None

## Emitter Logic

- Emit an opening double quote.
- Emit `Value` escaped as a C# string literal value.
- Emit a closing double quote.
- Do not append whitespace.
- Do not append a newline.
- Do not manage indentation.

## Happy Path Tests

- `EmitTo_WithValue_ReturnQuotedValue`
  - Builder:
    - `.WithValue("hello")`
  - Expected:
    - `"hello"`

- `EmitTo_WithEmptyValue_ReturnEmptyStringLiteral`
  - Builder:
    - `.WithValue("")`
  - Expected:
    - `""`

## Validation Tests

None

## Edge Case Tests

- `EmitTo_WithQuoteInValue_ReturnEscapedQuote`
  - Builder:
    - `.WithValue("hello \"world\"")`
  - Expected:
    - `"hello \"world\""`

- `EmitTo_WithBackslashInValue_ReturnEscapedBackslash`
  - Builder:
    - `.WithValue("C:\\Temp")`
  - Expected:
    - `"C:\\Temp"`

- `EmitTo_WithNewLineInValue_ReturnEscapedNewLine`
  - Builder:
    - `.WithValue("Line 1\nLine 2")`
  - Expected:
    - `"Line 1\nLine 2"`

- `EmitTo_WithTabInValue_ReturnEscapedTab`
  - Builder:
    - `.WithValue("Column\tValue")`
  - Expected:
    - `"Column\tValue"`