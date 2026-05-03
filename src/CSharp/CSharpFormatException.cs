// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Runtime.Serialization;

namespace JustTooFast.CodeGen.CSharp;

[Serializable]
public sealed class CSharpFormatException : FormatException
{
    public CSharpFormatException()
    {
    }

    public CSharpFormatException(string message)
        : base(message)
    {
    }

    public CSharpFormatException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    private CSharpFormatException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}