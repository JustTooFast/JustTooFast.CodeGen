// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Runtime.Serialization;

namespace JustTooFast.CodeGen.Xml;

[Serializable]
public sealed class XmlFormatException : FormatException
{
    public XmlFormatException()
    {
    }

    public XmlFormatException(string message)
        : base(message)
    {
    }

    public XmlFormatException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    private XmlFormatException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}