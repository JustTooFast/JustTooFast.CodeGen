// Copyright 2024-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml;
public partial class PrologEmitter : IEmitter
{
    private partial void Validate()
    {
        //Ensure Xml is initialized
        m_Prolog.Xml ??= new XmlModel();
    }

    public void EmitTo(IAppender appender)
    {
        XmlEmitter xmlEmitter = new(m_Prolog.Xml);
        xmlEmitter.EmitTo(appender);
    }
}