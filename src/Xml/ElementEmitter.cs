// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml;
public partial class ElementEmitter : IEmitter
{
    public int TabLevel
    { get; set; }

    private partial void Validate()
    {
        if (string.IsNullOrWhiteSpace(m_Element.Name))
            throw new XmlFormatException("Element Name is required.");

        if ((m_Element.Text != null) && (m_Element.Elements.Count > 0))
            throw new XmlFormatException("Element cannot have both Text and Child Elements.");
    }

    public void EmitTo(IAppender appender)
    {
        const string TAB = "  ";

        for(int i = 0; i < TabLevel; i++)
            appender.Append(TAB);

        appender.Append($"<{m_Element.Name}");

        foreach (AttributeModel attribute in m_Element.Attributes)
        {
            appender.Append(' ');

            AttributeEmitter attributeEmitter = new(attribute);
            attributeEmitter.EmitTo(appender);
        }

        appender.Append('>');

        if (!string.IsNullOrWhiteSpace(m_Element.Text))
        {
            appender.Append(m_Element.Text);
        }
        else
        {
            foreach (ElementModel element in m_Element.Elements)
            {
                appender.AppendLineFeed();
                ElementEmitter elementEmitter = new(element)
                {
                    TabLevel = TabLevel + 1
                };
                elementEmitter.EmitTo(appender);
            }

            if (m_Element.Elements.Count > 0)
                appender.AppendLineFeed();
        }

        if(m_Element.Elements.Count > 0)
        {
            for(int i = 0; i < TabLevel; i++)
                appender.Append(TAB);
        }

        appender.Append($"</{m_Element.Name}>");
    }
}