// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml;
public partial class XmlSnippetEmitter : IEmitter
{
    private partial void Validate()
    {
        if (m_XmlSnippet.Elements.Count == 0)
            throw new XmlFormatException("XmlSnippet missing Elements.");
    }

    public void EmitTo(IAppender appender)
    {
        bool isFirst = true;
        foreach (ElementModel element in m_XmlSnippet.Elements)
        {
            //Skip line feed on first element
            if(isFirst)
                isFirst = false;
            else
                appender.AppendLine();

            new ElementEmitter(element).EmitTo(appender);
        }
    }
}