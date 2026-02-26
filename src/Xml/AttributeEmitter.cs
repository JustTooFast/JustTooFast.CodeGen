// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml;
public partial class AttributeEmitter : IEmitter
{
    private partial void Validate()
    {
        if (string.IsNullOrWhiteSpace(m_Attribute.Name))
            throw new XmlFormatException("Attribute Name is required.");
    }

    public void EmitTo(IAppender appender)
    {
        appender.Append($"{m_Attribute.Name}=\"");

        if (!string.IsNullOrWhiteSpace(m_Attribute.Value))
            appender.Append(m_Attribute.Value);

        appender.Append('\"'); 
    }
}