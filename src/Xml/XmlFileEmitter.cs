// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml;
public partial class XmlFileEmitter
{
    private partial void Validate()
    {
        //Ensure Prolog is initialized
        m_XmlFile.Prolog ??= new PrologModel();

        if (m_XmlFile.RootElement == null)
            throw new XmlFormatException("XmlFile.RootElement is required.");
    }

    public partial void EmitTo(IAppender appender)
    {
        // If caller already provided formatting, don't override.
        IAppender a = appender;

        if (appender is not IHasFormatting<IFormatting>)
        {
            var formatting = Formatting.Default;

            a = new FormattingAppender(appender, formatting);
        }

        if (!m_XmlFile.DisableProlog)
        {
            new PrologEmitter(m_XmlFile.Prolog).EmitTo(appender);

            appender.AppendLine();
        }

        new RootElementEmitter(m_XmlFile.RootElement).EmitTo(appender);
    }
}