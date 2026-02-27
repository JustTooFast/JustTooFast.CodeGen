// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml;
public partial class XmlFileEmitter : IEmitter
{
    private partial void Validate()
    {
        //Ensure Prolog is initialized
        m_XmlFile.Prolog ??= new PrologModel();

        if (m_XmlFile.RootElement == null)
            throw new XmlFormatException("XmlFile RootElement is required.");
    }

    public void EmitTo(IAppender appender)
    {
        if (!m_XmlFile.DisableProlog)
        {
            new PrologEmitter(m_XmlFile.Prolog).EmitTo(appender);

            appender.AppendLine();
        }

        new RootElementEmitter(m_XmlFile.RootElement).EmitTo(appender);
    }
}