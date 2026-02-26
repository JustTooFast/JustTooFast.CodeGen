// Copyright 2024-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml;
public partial class PrologEmitter : EmitterBase
{
    public PrologEmitter(PrologModel prolog, IAppender appender)
        : this(prolog)
    {
        Appender = appender;
    }

    private partial void Validate()
    {
        //Ensure Xml is initialized
        m_Prolog.Xml ??= new XmlModel();
    }
    
    public override void AppendDeclaration()
    {
        XmlEmitter xmlEmitter = new(m_Prolog.Xml, Appender);
        xmlEmitter.AppendDeclaration();
    }
}