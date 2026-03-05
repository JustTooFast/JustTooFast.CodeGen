// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Text;

namespace JustTooFast.CodeGen;

public sealed class StringBuilderAppender : IAppender, IHasFormatting<IFormatting>
{
    private readonly StringBuilder _sb;

    public IFormatting Formatting { get; }

    public StringBuilderAppender(StringBuilder? sb = null, IFormatting? formatting = null)
    {
        _sb = sb ?? new StringBuilder();
        Formatting = formatting ?? CodeGen.Formatting.Default;
    }

    public StringBuilder Builder => _sb;

    public void Append(string? value) => _sb.Append(value);
    public void Append(char value) => _sb.Append(value);
    public void Append(ReadOnlySpan<char> value) => _sb.Append(value);

    public void AppendLine() => _sb.Append(Formatting.NewLine);

    public void AppendLine(string? value)
    {
        _sb.Append(value);
        _sb.Append(Formatting.NewLine);
    }

    public void AppendLine(char value)
    {
        _sb.Append(value);
        _sb.Append(Formatting.NewLine);
    }

    public void AppendLine(ReadOnlySpan<char> value)
    {
        _sb.Append(value);
        _sb.Append(Formatting.NewLine);
    }

    public override string ToString() => _sb.ToString();
}