// Copyright 2024-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml;
public partial class XmlDeclarationEmitter
{
    private partial void Validate()
    {
        if(!(m_XmlDeclaration.Standalone == null ||
            m_XmlDeclaration.Standalone == XmlStandalone.Yes.GetDescription() ||
            m_XmlDeclaration.Standalone == XmlStandalone.No.GetDescription()))
        {
            throw new XmlFormatException("Xml.Standalone must be either 'yes', 'no', or not used.");
        }
    }

    public partial void EmitTo(IAppender appender)
    {
        appender.Append("<?xml version=\"1.0\"");

        if (m_XmlDeclaration.Encoding != null)
        {
            appender.Append(" encoding=\"");
            appender.Append(m_XmlDeclaration.Encoding);
            appender.Append('"');
        }

        if (m_XmlDeclaration.Standalone != null)
        {
            appender.Append(" standalone=\"");
            appender.Append(m_XmlDeclaration.Standalone);
            appender.Append('"');
        }

        appender.Append("?>");
    }
}