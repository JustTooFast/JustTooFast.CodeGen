// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System.Reflection.Metadata.Ecma335;

namespace JustTooFast.CodeGen.Xml;
public partial class AttributeDeclaration : DeclarationBase
{
    public AttributeDeclaration(AttributeInfo attribute, IAppender appender)
        : this(attribute)
    {
        Appender = appender;
    }

    private partial void Validate()
    {
        if (string.IsNullOrWhiteSpace(m_Attribute.Name))
            throw new XmlFormatException("Attribute Name is required.");
    }

    public override void AppendDeclaration()
    {
        Appender.Append($"{m_Attribute.Name}=\"");

        if (!string.IsNullOrWhiteSpace(m_Attribute.Value))
            Appender.Append(m_Attribute.Value);

        Appender.Append('\"');
    }
}