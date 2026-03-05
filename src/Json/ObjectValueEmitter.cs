// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Json;

public partial class ObjectValueEmitter
{
    private partial void Validate()
    {
        if (m_ObjectValue.Properties is null)
            throw new JsonFormatException("ObjectValue.Properties collection is not initialized.");
    }

    public partial void EmitTo(IAppender appender)
    {
        var fmtBase = (appender as IHasFormatting<IFormatting>)?.Formatting;
        var fmt = fmtBase as IJsonFormatting ?? JsonFormatting.Compact;

        appender.Append('{');

        if (m_ObjectValue.Properties.Count == 0)
        {
            appender.Append('}');
            return;
        }

        if (!fmt.PrettyPrint)
        {
            for (int i = 0; i < m_ObjectValue.Properties.Count; i++)
            {
                if (i > 0) appender.Append(',');
                new PropertyEmitter(m_ObjectValue.Properties[i]).EmitTo(appender);
            }

            appender.Append('}');
            return;
        }

        // Pretty
        appender.AppendLine();

        // Prefer letting the wrapper discover formatting from the chain:
        IAppender indented = new IndentedAppender(appender);

        for (int i = 0; i < m_ObjectValue.Properties.Count; i++)
        {
            new PropertyEmitter(m_ObjectValue.Properties[i]).EmitTo(indented);

            if (i < m_ObjectValue.Properties.Count - 1)
                indented.Append(',');

            indented.AppendLine();
        }

        appender.Append('}');
    }
}
