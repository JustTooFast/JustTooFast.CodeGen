// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen.Xml;
public class XmlSnippetGenerator : IGenerator
{
    private readonly XmlSnippetModel m_XmlSnippet;

    public XmlSnippetGenerator(XmlSnippetModel xmlSnippet)
    {
        m_XmlSnippet = xmlSnippet ?? throw new ArgumentNullException(nameof(xmlSnippet));
    }

    public string Generate()
    {
        IAppender appender = new Appender();
        XmlSnippetEmitter xmlSnippetEmitter = new(m_XmlSnippet);
        xmlSnippetEmitter.EmitTo(appender);

        return appender.ToString();
    }
}
