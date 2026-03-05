// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Json;

public sealed class JsonFormatting : IJsonFormatting
{
    public static readonly JsonFormatting Compact = new(prettyPrint: false);

    public bool PrettyPrint { get; }
    public string IndentUnit { get; }
    public string NewLine { get; }

    public JsonFormatting(bool prettyPrint, string? indentUnit = null, string? newLine = null)
    {
        PrettyPrint = prettyPrint;
        IndentUnit = indentUnit ?? "  ";
        NewLine = newLine ?? "\n";
    }
}