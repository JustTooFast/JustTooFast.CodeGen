// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Json;

public partial class NumberValueEmitter
{
    private partial void Validate()
    {
        if (string.IsNullOrWhiteSpace(m_NumberValue.Value))
            throw new JsonFormatException("NumberValue.Value is required.");
    }

    public partial void EmitTo(IAppender appender)
    {
        // Emit exactly as provided (caller responsible for validity for now)
        appender.Append(m_NumberValue.Value);
    }
}
