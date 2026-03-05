// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Text;

namespace JustTooFast.CodeGen;

public sealed class StringBuilderSink : IOutputSink
{
    private readonly StringBuilder _sb;
    private readonly IFormatting? _formatting;

    private StringBuilderAppender? _appender;
    private bool _created;

    public StringBuilderSink(StringBuilder? sb = null, IFormatting? formatting = null)
    {
        _sb = sb ?? new StringBuilder();
        _formatting = formatting;
    }

    public IAppender CreateAppender()
    {
        if (_created) throw new InvalidOperationException("CreateAppender() may only be called once.");
        _created = true;

        _appender = new StringBuilderAppender(_sb, formatting: _formatting);
        return _appender;
    }

    public void Complete() { }

    public string GetText() => _sb.ToString();

    public void Dispose()
    {
        // nothing
    }
}