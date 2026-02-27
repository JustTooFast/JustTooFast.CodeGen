// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml;
public partial class ElementEmitter : IEmitter
{
    private partial void Validate()
    {
        if (string.IsNullOrWhiteSpace(m_Element.Name))
            throw new XmlFormatException("Element Name is required.");

        if ((m_Element.Text != null) && (m_Element.Elements.Count > 0))
            throw new XmlFormatException("Element cannot have both Text and Child Elements.");
    }
    
    public void EmitTo(IAppender appender)
    {
        appender.Append('<');
        appender.Append(m_Element.Name);

        foreach (AttributeModel attribute in m_Element.Attributes)
        {
            appender.Append(' ');
            new AttributeEmitter(attribute).EmitTo(appender);
        }

        appender.Append('>');

        if (!string.IsNullOrWhiteSpace(m_Element.Text))
        {
            appender.AppendXmlTextEscaped(m_Element.Text);
        }
        else if (m_Element.Elements.Count > 0)
        {
            appender.AppendLine();

            // children should be indented relative to current line
            IAppender indented = new IndentedAppender(appender, XmlFormatting.IndentUnit);

            for (int i = 0; i < m_Element.Elements.Count; i++)
            {
                new ElementEmitter(m_Element.Elements[i]).EmitTo(indented);

                // IMPORTANT: newline via wrapper so next line indents correctly
                indented.AppendLine();
            }
        }

        appender.Append("</");
        appender.Append(m_Element.Name);
        appender.Append('>');
    }
}