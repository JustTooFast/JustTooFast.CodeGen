// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen;

/*
public sealed class FormattingAppender : IAppender, IHasFormatting<IFormatting>
{
    private readonly IAppender _inner;

    public IFormatting Formatting { get; }

    public FormattingAppender(IAppender inner, IFormatting formatting)
    {
        _inner = inner ?? throw new ArgumentNullException(nameof(inner));
        Formatting = formatting ?? throw new ArgumentNullException(nameof(formatting));
    }

    public void Append(string? value) => _inner.Append(value);
    public void Append(char value) => _inner.Append(value);
    public void Append(ReadOnlySpan<char> value) => _inner.Append(value);

    public void AppendLine() => _inner.AppendLine();
    public void AppendLine(string? value) => _inner.AppendLine(value);
    public void AppendLine(char value) => _inner.AppendLine(value);
    public void AppendLine(ReadOnlySpan<char> value) => _inner.AppendLine(value);
}
*/

public sealed class FormattingAppender : IAppender, IHasFormatting<IFormatting>
{
    private readonly IAppender _inner;

    public IFormatting Formatting { get; }

    public FormattingAppender(IAppender inner, IFormatting formatting)
    {
        _inner = inner ?? throw new ArgumentNullException(nameof(inner));
        Formatting = formatting ?? throw new ArgumentNullException(nameof(formatting));
    }

    public void Append(string? value) => _inner.Append(value);
    public void Append(char value) => _inner.Append(value);
    public void Append(ReadOnlySpan<char> value) => _inner.Append(value);

    // Define line semantics via Formatting.NewLine, regardless of what inner does.
    public void AppendLine() => _inner.Append(Formatting.NewLine);

    public void AppendLine(string? value)
    {
        _inner.Append(value);
        _inner.Append(Formatting.NewLine);
    }

    public void AppendLine(char value)
    {
        _inner.Append(value);
        _inner.Append(Formatting.NewLine);
    }

    public void AppendLine(ReadOnlySpan<char> value)
    {
        _inner.Append(value);
        _inner.Append(Formatting.NewLine);
    }
}
