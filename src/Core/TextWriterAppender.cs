// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;

namespace JustTooFast.CodeGen;

public sealed class TextWriterAppender : IAppender, IHasFormatting<IFormatting>, IDisposable
{
    private readonly TextWriter _writer;
    private readonly bool _ownsWriter;

    public IFormatting Formatting { get; }

    public TextWriterAppender(TextWriter writer, IFormatting? formatting = null, bool ownsWriter = false)
    {
        _writer = writer ?? throw new ArgumentNullException(nameof(writer));
        Formatting = formatting ?? CodeGen.Formatting.Default;
        _ownsWriter = ownsWriter;
    }

    public void Append(string? value)
    {
        if (!string.IsNullOrEmpty(value))
            _writer.Write(value);
    }

    public void Append(char value)
    {
        _writer.Write(value);
    }

    public void Append(ReadOnlySpan<char> value)
    {
#if NETSTANDARD2_1_OR_GREATER || NET6_0_OR_GREATER
        _writer.Write(value);
#else
        if (value.Length == 0) return;
        _writer.Write(value.ToString());
#endif
    }

    public void AppendLine()
    {
        _writer.Write(Formatting.NewLine);
    }

    public void AppendLine(string? value)
    {
        _writer.Write(value);
        _writer.Write(Formatting.NewLine);
    }

    public void AppendLine(char value)
    {
        _writer.Write(value);
        _writer.Write(Formatting.NewLine);
    }

    public void AppendLine(ReadOnlySpan<char> value)
    {
#if NETSTANDARD2_1_OR_GREATER || NET6_0_OR_GREATER
        _writer.Write(value);
        _writer.Write(Formatting.NewLine);
#else
        if (value.Length != 0)
            _writer.Write(value.ToString());
        _writer.Write(Formatting.NewLine);
#endif
    }

    public void Flush() => _writer.Flush();

    public void Dispose()
    {
        if (_ownsWriter)
            _writer.Dispose();
    }
}