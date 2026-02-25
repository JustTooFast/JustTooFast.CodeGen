// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml;
public partial class RootElementDeclaration : DeclarationBase
{
    public RootElementDeclaration(RootElementInfo rootElement, IAppender appender)
        : this(rootElement)
    {
        Appender = appender;
    }

    private partial void Validate()
    {
        if (string.IsNullOrWhiteSpace(m_RootElement.Name))
            throw new XmlFormatException("RootElement Name is required.");
    }

    public override void AppendDeclaration()
    {
        Appender.Append($"<{m_RootElement.Name}");

        foreach (AttributeInfo attribute in m_RootElement.Attributes)
        {
            Appender.Append(' ');
            AttributeDeclaration attributeDeclaration = new(attribute, Appender);
            attributeDeclaration.AppendDeclaration();
        }

        Appender.Append('>');

        foreach (ElementInfo element in m_RootElement.Elements)
        {
            Appender.AppendLineFeed();
            ElementDeclaration elementDeclaration = new(element, Appender)
            {
                TabLevel = 1
            };
            elementDeclaration.AppendDeclaration();
        }

        if (m_RootElement.Elements.Count > 0)
            Appender.AppendLineFeed();

        Appender.Append($"</{m_RootElement.Name}>");
    }
}