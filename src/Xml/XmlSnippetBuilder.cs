// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml;
public partial class XmlSnippetBuilder
{
    public static implicit operator XmlSnippetGenerator(XmlSnippetBuilder builder)
    {
        return new XmlSnippetGenerator(builder.m_XmlSnippet);
    }
}
