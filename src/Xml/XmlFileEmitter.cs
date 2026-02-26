// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml;
public partial class XmlFileEmitter : EmitterBase
{
    public XmlFileEmitter(XmlFileModel xmlFile, IAppender appender)
        : this(xmlFile)
    {
        Appender = appender;
    }

    private partial void Validate()
    {
        //Ensure Prolog is initialized
        m_XmlFile.Prolog ??= new PrologModel();

        if (m_XmlFile.RootElement == null)
            throw new XmlFormatException("XmlFile RootElement is required.");
    }
    
    public override void AppendDeclaration()
    {
        if (!m_XmlFile.DisableProlog)
        {
            PrologEmitter prologEmitter = new(m_XmlFile.Prolog, Appender);
            prologEmitter.AppendDeclaration();

            Appender.AppendLineFeed();
        }

        RootElementEmitter rootElementEmitter = new(m_XmlFile.RootElement, Appender);
        rootElementEmitter.AppendDeclaration();
    }
}