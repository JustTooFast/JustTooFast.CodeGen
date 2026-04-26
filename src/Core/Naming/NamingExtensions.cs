// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen.Naming;

public static class NamingExtensions
{
    public static string LowercaseFirstInvariant(this string value)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        char first = value[0];
        char lower = char.ToLowerInvariant(first);

        if (first == lower)
            return value;

        return string.Create(
            value.Length,
            (lower, value),
            static (span, state) =>
            {
                span[0] = state.lower;
                state.value.AsSpan(1).CopyTo(span[1..]);
            });
    }

    public static string ToPlural(this string value) =>
        EnglishPluralizer.Pluralize(value);
}
