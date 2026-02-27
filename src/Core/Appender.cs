// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Text;

namespace JustTooFast.CodeGen;
public sealed class Appender : IAppender
{
    private readonly StringBuilder _sb = new();

    public string NewLine { get; }

    public Appender(string? newLine = null)
    {
        NewLine = newLine ?? "\n";
    }

    public void Append(string? value) => _sb.Append(value);
    public void Append(char value) => _sb.Append(value);
    public void Append(ReadOnlySpan<char> value) => _sb.Append(value);

    public void AppendLine() => _sb.Append(NewLine);
    public void AppendLine(string? value) => _sb.Append(value).Append(NewLine);
    public void AppendLine(ReadOnlySpan<char> value) => _sb.Append(value).Append(NewLine);

    public override string ToString() => _sb.ToString();
}