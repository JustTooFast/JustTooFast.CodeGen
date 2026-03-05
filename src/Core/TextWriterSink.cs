// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;

namespace JustTooFast.CodeGen;

public sealed class TextWriterSink : IOutputSink
{
    private readonly TextWriter _writer;
    private readonly bool _ownsWriter;
    private readonly IFormatting? _formatting;

    private TextWriterAppender? _appender;
    private bool _created;

    public TextWriterSink(TextWriter writer, bool ownsWriter = false, IFormatting? formatting = null)
    {
        _writer = writer ?? throw new ArgumentNullException(nameof(writer));
        _ownsWriter = ownsWriter;
        _formatting = formatting;
    }

    public IAppender CreateAppender()
    {
        if (_created) throw new InvalidOperationException("CreateAppender() may only be called once.");
        _created = true;

        _appender = new TextWriterAppender(_writer, formatting: _formatting, ownsWriter: _ownsWriter);
        return _appender;
    }

    public void Complete()
    {
        _appender?.Flush();
    }

    public void Dispose()
    {
        _appender?.Dispose();
        _appender = null;
    }
}