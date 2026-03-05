// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml;
public partial class RootElementEmitter
{
    private partial void Validate()
    {
        if (string.IsNullOrWhiteSpace(m_RootElement.Name))
            throw new XmlFormatException("RootElement.Name is required.");
    }

    public partial void EmitTo(IAppender appender)
    {
        var fmt = (appender as IHasFormatting<IFormatting>)?.Formatting ?? Formatting.Default;

        appender.Append("<");
        appender.Append(m_RootElement.Name);

        foreach (AttributeModel attribute in m_RootElement.Attributes)
        {
            appender.Append(' ');
            new AttributeEmitter(attribute).EmitTo(appender);
        }

        appender.Append('>');

        IAppender indented = new IndentedAppender(appender);

        foreach (ElementModel element in m_RootElement.Elements)
        {
            indented.AppendLine();
            new ElementEmitter(element).EmitTo(indented);
        }

        if (m_RootElement.Elements.Count > 0)
            appender.AppendLine();

        appender.Append("</");
        appender.Append(m_RootElement.Name);
        appender.Append('>');
    }
}