// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Json;

public partial class ArrayValueEmitter
{
    private partial void Validate()
    {
        // Allow empty arrays
        // If your model disallows null collections, no-op is fine.
        if (m_ArrayValue.Values is null)
            throw new JsonFormatException("ArrayValue.Values collection is not initialized.");
    }

    public partial void EmitTo(IAppender appender)
    {
        // Discover core formatting through wrapper chains; preserve runtime type.
        var fmtBase = (appender as IHasFormatting<IFormatting>)?.Formatting;

        // Pull JSON-specific formatting if available; otherwise fall back.
        var fmt = fmtBase as IJsonFormatting ?? JsonFormatting.Compact;

        appender.Append('[');

        if (m_ArrayValue.Values.Count == 0)
        {
            appender.Append(']');
            return;
        }

        if (!fmt.PrettyPrint)
        {
            for (int i = 0; i < m_ArrayValue.Values.Count; i++)
            {
                if (i > 0) appender.Append(',');
                new ValueEmitter(m_ArrayValue.Values[i]).EmitTo(appender);
            }

            appender.Append(']');
            return;
        }

        // Pretty
        appender.AppendLine();

        IAppender indented = new IndentedAppender(appender);

        for (int i = 0; i < m_ArrayValue.Values.Count; i++)
        {
            new ValueEmitter(m_ArrayValue.Values[i]).EmitTo(indented);

            if (i < m_ArrayValue.Values.Count - 1)
                indented.Append(',');

            indented.AppendLine();
        }

        appender.Append(']');
    }
}
