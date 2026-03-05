// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Json;

public partial class PropertyEmitter
{
    private partial void Validate()
    {
        if (string.IsNullOrWhiteSpace(m_Property.Name))
            throw new JsonFormatException("Property.Name is required.");

        if (m_Property.Value is null)
            throw new JsonFormatException("Property.Value is required.");
    }

    public partial void EmitTo(IAppender appender)
    {
        appender.Append('"');
        appender.AppendJsonStringEscaped(m_Property.Name);
        appender.Append('"');

        appender.Append(':');

        new ValueEmitter(m_Property.Value).EmitTo(appender);
    }
}
