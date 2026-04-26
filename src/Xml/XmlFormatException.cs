// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen.Xml;

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
}