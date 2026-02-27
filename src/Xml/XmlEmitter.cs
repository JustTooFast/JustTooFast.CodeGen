// Copyright 2024-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml;
public partial class XmlEmitter : IEmitter
{
    private partial void Validate()
    {
        if(!(m_Xml.Standalone == null ||
            m_Xml.Standalone == Standalone.Yes.GetDescription() ||
            m_Xml.Standalone == Standalone.No.GetDescription()))
        {
            throw new XmlFormatException("Xml Standalone must be either 'yes', 'no', or not used.");
        }
    }

    public void EmitTo(IAppender appender)
    {
        appender.Append("<?xml version=\"1.0\"");

        if (m_Xml.Encoding != null)
        {
            appender.Append(" encoding=\"");
            appender.Append(m_Xml.Encoding);
            appender.Append('"');
        }

        if (m_Xml.Standalone != null)
        {
            appender.Append(" standalone=\"");
            appender.Append(m_Xml.Standalone);
            appender.Append('"');
        }

        appender.Append("?>");
    }
}