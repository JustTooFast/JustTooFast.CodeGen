// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml;
public partial class XmlFileDeclaration : DeclarationBase
{
    public XmlFileDeclaration(XmlFileInfo xmlFile, IAppender appender)
        : this(xmlFile)
    {
        Appender = appender;
    }

    private partial void Validate()
    {
        //Ensure Prolog is initialized
        m_XmlFile.Prolog ??= new PrologInfo();

        if (m_XmlFile.RootElement == null)
            throw new XmlFormatException("XmlFile RootElement is required.");
    }
    
    public override void AppendDeclaration()
    {
        if (!m_XmlFile.DisableProlog)
        {
            PrologDeclaration prologDeclaration = new(m_XmlFile.Prolog, Appender);
            prologDeclaration.AppendDeclaration();

            Appender.AppendLineFeed();
        }

        RootElementDeclaration rootElementDeclaration = new(m_XmlFile.RootElement, Appender);
        rootElementDeclaration.AppendDeclaration();
    }
}