// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen.Xml;
public class XmlSnippetGenerator : IGenerator
{
    private readonly XmlSnippetInfo m_XmlSnippet;

    public XmlSnippetGenerator(XmlSnippetInfo xmlSnippet)
    {
        m_XmlSnippet = xmlSnippet ?? throw new ArgumentNullException(nameof(xmlSnippet));
    }

    public string Generate()
    {
        XmlSnippetDeclaration xmlSnippetDeclaration = new(m_XmlSnippet, new Appender());
        xmlSnippetDeclaration.AppendDeclaration();

        return xmlSnippetDeclaration.ToString();
    }
}
