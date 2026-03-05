// Copyright 2024-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml;
public partial class PrologEmitter
{
    private partial void Validate()
    {
        //Ensure Xml is initialized
        m_Prolog.XmlDeclaration ??= new XmlDeclarationModel();
    }

    public partial void EmitTo(IAppender appender)
    {
        new XmlDeclarationEmitter(m_Prolog.XmlDeclaration).EmitTo(appender);
    }
}