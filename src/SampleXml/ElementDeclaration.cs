// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Text;

namespace JustTooFast.CodeGen.SampleXml;
public partial class ElementDeclaration
{
    private partial void Validate()
    {
        if (string.IsNullOrWhiteSpace(m_Element.Name))
            throw new Exception("Element Name is required.");
    }

    public string Generate()
    {
        StringBuilder sb = new();

        sb.Append($"<{m_Element.Name}");
        
        foreach (AttributeInfo attribute in m_Element.Attributes)
        {
            AttributeDeclaration ad = new(attribute);
            sb.Append($" {ad.Generate()}");
        }

        sb.Append('>');


        if (!string.IsNullOrWhiteSpace(m_Element.Text))
        {
            sb.Append(m_Element.Text);
        }
        else
        {
            foreach (ElementInfo element in m_Element.Elements)
            {
                ElementDeclaration ed = new(element);
                sb.AppendLine()
                    .Append(Indent(ed.Generate()));
            }

            if (m_Element.Elements.Count > 0)
                sb.AppendLine();
        }

        sb.Append($"</{m_Element.Name}>");

        string result = sb.ToString();

        return result;
    }

    private string Indent(string str)
    {
        StringBuilder sb = new();

        string[] lines = str.Split(Environment.NewLine);

        for (int i = 0; i < lines.Length; i++)
        {
            sb.Append($"  {lines[i]}");

            if (i < lines.Length - 1)
                sb.AppendLine();
        }

        return sb.ToString();
    }
}
