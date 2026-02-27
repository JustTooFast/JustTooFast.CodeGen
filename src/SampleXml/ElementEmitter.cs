// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Text;

namespace JustTooFast.CodeGen.SampleXml;
public partial class ElementEmitter : IEmitter
{
    private partial void Validate()
    {
        if (string.IsNullOrWhiteSpace(m_Element.Name))
            throw new Exception("Element Name is required.");
    }

    public void EmitTo(IAppender appender)
    {
        appender.Append('<');
        appender.Append(m_Element.Name);
        
        foreach (AttributeModel attribute in m_Element.Attributes)
        {
            appender.Append(' ');
            AttributeEmitter ae = new(attribute);
            ae.EmitTo(appender);
        }

        appender.Append('>');

        if (!string.IsNullOrWhiteSpace(m_Element.Text))
        {
            appender.Append(m_Element.Text);
        }
        else
        {
            if (m_Element.Elements.Count > 0)
            {
                appender.AppendLine();

                var indented = new IndentedAppender(appender, "  ");

                for (int i = 0; i < m_Element.Elements.Count; i++)
                {
                    new ElementEmitter(m_Element.Elements[i]).EmitTo(indented);

                    if (i < m_Element.Elements.Count - 1)
                        indented.AppendLine();
                }

                appender.AppendLine(); // end of children block
            }
        }

        appender.Append("</");
        appender.Append(m_Element.Name);
        appender.Append('>');
    }
}
