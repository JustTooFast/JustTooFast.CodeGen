// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen.SampleXml;
public partial class AttributeEmitter
{
    private partial void Validate()
    {
        if (string.IsNullOrWhiteSpace(m_Attribute.Name))
            throw new Exception("Attribute Name is required.");
    }

    public partial void EmitTo(IAppender appender)
    {
        appender.Append(m_Attribute.Name);
        appender.Append("=\"");

        if (!string.IsNullOrWhiteSpace(m_Attribute.Value))
            appender.Append(m_Attribute.Value);
        
        appender.Append('"');
    }
}
