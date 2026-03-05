// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Json;

public partial class NullValueEmitter
{
    private partial void Validate()
    {
        //Nothing to validate
    }

    public partial void EmitTo(IAppender appender)
    {
        appender.Append("null");
    }
}
