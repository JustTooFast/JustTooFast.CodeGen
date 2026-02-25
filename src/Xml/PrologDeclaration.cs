// Copyright 2024-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml;
public partial class PrologDeclaration : DeclarationBase
{
    public PrologDeclaration(PrologInfo prolog, IAppender appender)
        : this(prolog)
    {
        Appender = appender;
    }

    private partial void Validate()
    {
        //Ensure Xml is initialized
        m_Prolog.Xml ??= new XmlInfo();
    }
    
    public override void AppendDeclaration()
    {
        XmlDeclaration xmlDeclaration = new(m_Prolog.Xml, Appender);
        xmlDeclaration.AppendDeclaration();
    }
}