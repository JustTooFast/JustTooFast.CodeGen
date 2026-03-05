// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Runtime.Serialization;

namespace JustTooFast.CodeGen.Json;

[Serializable]
public sealed class JsonFormatException : FormatException
{
    public JsonFormatException()
    {
    }

    public JsonFormatException(string message)
        : base(message)
    {
    }

    public JsonFormatException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    private JsonFormatException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}