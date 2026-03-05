// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen.Json;

public partial class BooleanValueEmitter
{
    private partial void Validate()
    {
        if (string.IsNullOrWhiteSpace(m_BooleanValue.Value))
            throw new JsonFormatException("BooleanValue.Value is required.");

        // Normalize/validate allowed values
        string v = m_BooleanValue.Value.Trim();

        if (!v.Equals("true", StringComparison.OrdinalIgnoreCase) &&
            !v.Equals("false", StringComparison.OrdinalIgnoreCase))
        {
            throw new JsonFormatException("BooleanValue.Value must be 'true' or 'false'.");
        }
    }

    public partial void EmitTo(IAppender appender)
    {
        // Emit canonical JSON form (lowercase)
        bool b = m_BooleanValue.Value.Trim().Equals("true", StringComparison.OrdinalIgnoreCase);
        appender.Append(b ? "true" : "false");
    }
}
