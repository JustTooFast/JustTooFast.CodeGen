// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml;
public partial class RootElementEmitter : EmitterBase
{
    public RootElementEmitter(RootElementModel rootElement, IAppender appender)
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

        foreach (AttributeModel attribute in m_RootElement.Attributes)
        {
            Appender.Append(' ');
            AttributeEmitter attributeEmitter = new(attribute, Appender);
            attributeEmitter.AppendDeclaration();
        }

        Appender.Append('>');

        foreach (ElementModel element in m_RootElement.Elements)
        {
            Appender.AppendLineFeed();
            ElementEmitter elementEmitter = new(element, Appender)
            {
                TabLevel = 1
            };
            elementEmitter.AppendDeclaration();
        }

        if (m_RootElement.Elements.Count > 0)
            Appender.AppendLineFeed();

        Appender.Append($"</{m_RootElement.Name}>");
    }
}