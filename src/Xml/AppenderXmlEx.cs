// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen.Xml;

public static class AppenderXmlEx
{
    public static void AppendXmlTextEscaped(this IAppender a, string s)
    {
        AppendXmlEscapedCore(a, s, escapeQuotes: false);
    }

    public static void AppendXmlAttributeValueEscaped(this IAppender a, string s)
    {
        AppendXmlEscapedCore(a, s, escapeQuotes: true);
    }

    private static void AppendXmlEscapedCore(IAppender a, string s, bool escapeQuotes)
    {
        int last = 0;

        for (int i = 0; i < s.Length; i++)
        {
            string? repl = s[i] switch
            {
                '&' => "&amp;",
                '<' => "&lt;",
                '>' => "&gt;", // optional but fine
                '"' when escapeQuotes => "&quot;",
                '\'' when escapeQuotes => "&apos;",
                _ => null
            };

            if (repl is null) continue;

            if (i > last)
                a.Append(s.AsSpan(last, i - last)); // append chunk without allocating

            a.Append(repl); // small constants are fine
            last = i + 1;
        }

        if (last < s.Length)
            a.Append(s.AsSpan(last));
    }
}
