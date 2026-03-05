// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen.Json;

public static class AppenderJsonEx
{
    public static void AppendJsonStringEscaped(this IAppender a, string s)
    {
        // Assumes caller already wrote opening quote
        int last = 0;

        for (int i = 0; i < s.Length; i++)
        {
            string? repl = s[i] switch
            {
                '"'  => "\\\"",
                '\\' => "\\\\",
                '\b' => "\\b",
                '\f' => "\\f",
                '\n' => "\\n",
                '\r' => "\\r",
                '\t' => "\\t",
                _ => null
            };

            // Control chars 0x00 - 0x1F must be escaped as \uXXXX
            if (repl is null && s[i] < 0x20)
                repl = $"\\u{(int)s[i]:x4}";

            if (repl is null) continue;

            if (i > last)
                a.Append(s.AsSpan(last, i - last));

            a.Append(repl);
            last = i + 1;
        }

        if (last < s.Length)
            a.Append(s.AsSpan(last));
    }
}