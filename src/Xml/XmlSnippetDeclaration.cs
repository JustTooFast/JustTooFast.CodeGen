// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml;
public partial class XmlSnippetDeclaration : DeclarationBase
{
    public XmlSnippetDeclaration(XmlSnippetInfo xmlSnippet, IAppender appender)
        : this(xmlSnippet)
    {
        Appender = appender;
    }

    private partial void Validate()
    {
        if (m_XmlSnippet.Elements.Count == 0)
            throw new XmlFormatException("XmlSnippet missing Elements.");
    }
    
    public override void AppendDeclaration()
    {
        bool isFirst = true;
        foreach (ElementInfo element in m_XmlSnippet.Elements)
        {
            //Skip line feed on first element
            if(isFirst)
                isFirst = false;
            else
                Appender.AppendLineFeed();

            ElementDeclaration elementDeclaration = new(element, Appender);
            elementDeclaration.AppendDeclaration();
        }
    }
}