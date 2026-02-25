// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen.SampleXml;
public partial class AttributeDeclaration
{
    private partial void Validate()
    {
        if (string.IsNullOrWhiteSpace(m_Attribute.Name))
            throw new Exception("Attribute Name is required.");
    }

    public string Generate()
    {
        string result = string.Empty;
        if (string.IsNullOrWhiteSpace(m_Attribute.Value))
            result = $"{m_Attribute.Name}=\"\"";
        else
            result = $"{m_Attribute.Name}=\"{m_Attribute.Value}\"";
        
        return result;
    }
}
