// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Json;

public partial class StringValueEmitter
{
    private partial void Validate()
    {
    }

    public partial void EmitTo(IAppender appender)
    {
        appender.Append('"');
        appender.AppendJsonStringEscaped(m_StringValue.Value ?? "");
        appender.Append('"');
    }
}
