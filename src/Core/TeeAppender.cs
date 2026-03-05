// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen;

public sealed class TeeAppender : IAppender, IHasFormatting<IFormatting>
{
    private readonly IAppender _a;
    private readonly IAppender _b;

    public IFormatting Formatting { get; }

    public TeeAppender(IAppender a, IAppender b)
    {
        _a = a ?? throw new ArgumentNullException(nameof(a));
        _b = b ?? throw new ArgumentNullException(nameof(b));

        Formatting = ResolveFormatting(_a, _b);
    }

    private static IFormatting ResolveFormatting(IAppender a, IAppender b)
    {
        var fa = (a as IHasFormatting<IFormatting>)?.Formatting;
        var fb = (b as IHasFormatting<IFormatting>)?.Formatting;

        if (fa is null && fb is null)
            return CodeGen.Formatting.Default;

        if (fa is null)
            return fb!;

        if (fb is null)
            return fa;

        // Both non-null: require identical values.
        // We compare by values, not reference.
        if (!string.Equals(fa.NewLine, fb.NewLine, StringComparison.Ordinal) ||
            !string.Equals(fa.IndentUnit, fb.IndentUnit, StringComparison.Ordinal))
        {
            throw new ArgumentException(
                "TeeAppender requires both appenders to use the same formatting (NewLine and IndentUnit) when both provide IHasFormatting.",
                nameof(b));
        }

        return fa;
    }

    public void Append(string? value)
    {
        _a.Append(value);
        _b.Append(value);
    }

    public void Append(char value)
    {
        _a.Append(value);
        _b.Append(value);
    }

    public void Append(ReadOnlySpan<char> value)
    {
        _a.Append(value);
        _b.Append(value);
    }

    public void AppendLine()
    {
        _a.AppendLine();
        _b.AppendLine();
    }

    public void AppendLine(string? value)
    {
        _a.AppendLine(value);
        _b.AppendLine(value);
    }

    public void AppendLine(char value)
    {
        _a.AppendLine(value);
        _b.AppendLine(value);
    }

    public void AppendLine(ReadOnlySpan<char> value)
    {
        _a.AppendLine(value);
        _b.AppendLine(value);
    }
}