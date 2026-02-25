// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System.Text;

namespace JustTooFast.CodeGen.Xml;
public class Appender : IAppender
{
    private readonly StringBuilder m_StringBuilder = new();

    public void Append(string value)
    {
        m_StringBuilder.Append(value);
    }

    public void Append(char value)
    {
        m_StringBuilder.Append(value);
    }

    public void AppendLineFeed()
    {
        m_StringBuilder.Append('\n');
    }

    public void AppendLineFeed(string value)
    {
        m_StringBuilder.Append($"{value}\n");
    }

    public override string ToString()
    {
        return m_StringBuilder.ToString();
    }
}