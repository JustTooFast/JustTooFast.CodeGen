// Copyright 2024-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml;
public partial class XmlDeclaration : DeclarationBase
{
    public XmlDeclaration(XmlInfo xml, IAppender appender)
        : this(xml)
    {
        Appender = appender;
    }

    private partial void Validate()
    {
        if(!(m_Xml.Standalone == null ||
            m_Xml.Standalone == Standalone.Yes.GetDescription() ||
            m_Xml.Standalone == Standalone.No.GetDescription()))
        {
            throw new XmlFormatException("Xml Standalone must be either 'yes', 'no', or not used.");
        }
    }
    
    public override void AppendDeclaration()
    {
        Appender.Append("<?xml version=\"1.0\"");

        if (m_Xml.Encoding != null)
            Appender.Append($" encoding=\"{m_Xml.Encoding}\"");

        if (m_Xml.Standalone != null)
            Appender.Append($" standalone=\"{m_Xml.Standalone}\"");

        Appender.Append("?>");
    }
}