// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Text;

namespace JustTooFast.CodeGen;

public sealed class StreamSink : IOutputSink
{
    private readonly Stream _stream;
    private readonly bool _leaveOpen;
    private readonly Encoding? _encoding;
    private readonly IFormatting? _formatting; // optional: lets sink provide formatting if desired

    private StreamAppender? _appender;
    private bool _created;

    public StreamSink(Stream stream, bool leaveOpen = true, Encoding? encoding = null, IFormatting? formatting = null)
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        _leaveOpen = leaveOpen;
        _encoding = encoding;
        _formatting = formatting;
    }

    public IAppender CreateAppender()
    {
        if (_created) throw new InvalidOperationException("CreateAppender() may only be called once.");
        _created = true;

        _appender = new StreamAppender(
            _stream,
            formatting: _formatting,          // if null, StreamAppender will use CodeGen.Formatting.Default
            encoding: _encoding,
            leaveOpen: _leaveOpen);

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
