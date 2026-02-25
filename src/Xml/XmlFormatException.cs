// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen.Xml;
public class XmlFormatException : Exception
{
    public XmlFormatException(string message) : base(message)
    { }
}
