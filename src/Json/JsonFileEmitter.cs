// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Json;

public partial class JsonFileEmitter
{
    private partial void Validate()
    {
        if (m_JsonFile.Value is null)
            throw new JsonFormatException("JsonFile.Value is required.");
    }

    public partial void EmitTo(IAppender appender)
    {
        IAppender a = appender;

        // If caller didn't provide formatting, supply JSON formatting via a wrapper.
        if (appender is not IHasFormatting<IFormatting>)
        {
            bool pretty = BoolText.Parse(m_JsonFile.PrettyPrint, "JsonFile.PrettyPrint");
            var formatting = new JsonFormatting(prettyPrint: pretty);

            a = new FormattingAppender(appender, formatting);
        }

        new ValueEmitter(m_JsonFile.Value).EmitTo(a);
    }
}
