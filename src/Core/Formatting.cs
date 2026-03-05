// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen;

public sealed class Formatting : IFormatting
{
    public string NewLine { get; }
    public string IndentUnit { get; }

    public Formatting(string? indentUnit = null, string? newLine = null)
    {
        IndentUnit = indentUnit ?? "  ";
        NewLine = newLine ?? "\n";
    }

    public static readonly Formatting Default = new();
}
