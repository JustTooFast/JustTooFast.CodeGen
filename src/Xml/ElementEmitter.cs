// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Text;

namespace JustTooFast.CodeGen.Xml;
public partial class ElementEmitter : EmitterBase
{
    public ElementEmitter(ElementModel element, IAppender appender)
        : this(element)
    {
        Appender = appender;
    }

    public int TabLevel
    { get; set; }

    private partial void Validate()
    {
        if (string.IsNullOrWhiteSpace(m_Element.Name))
            throw new XmlFormatException("Element Name is required.");

        if ((m_Element.Text != null) && (m_Element.Elements.Count > 0))
            throw new XmlFormatException("Element cannot have both Text and Child Elements.");
    }

    public override void AppendDeclaration()
    {
        const string TAB = "  ";

        for(int i = 0; i < TabLevel; i++)
            Appender.Append(TAB);

        Appender.Append($"<{m_Element.Name}");

        foreach (AttributeModel attribute in m_Element.Attributes)
        {
            Appender.Append(' ');

            AttributeEmitter attributeEmitter = new(attribute, Appender);
            attributeEmitter.AppendDeclaration();
        }

        Appender.Append('>');

        if (!string.IsNullOrWhiteSpace(m_Element.Text))
        {
            Appender.Append(m_Element.Text);
        }
        else
        {
            foreach (ElementModel element in m_Element.Elements)
            {
                Appender.AppendLineFeed();
                ElementEmitter elementEmitter = new(element, Appender)
                {
                    TabLevel = TabLevel + 1
                };
                elementEmitter.AppendDeclaration();
            }

            if (m_Element.Elements.Count > 0)
                Appender.AppendLineFeed();
        }

        if(m_Element.Elements.Count > 0)
        {
            for(int i = 0; i < TabLevel; i++)
                Appender.Append(TAB);
        }

        Appender.Append($"</{m_Element.Name}>");
    }
}