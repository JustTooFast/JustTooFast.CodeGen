// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen;

public sealed class IndentedAppender : IAppender
{
    private readonly IAppender _inner;
    private readonly string _indent;
    private bool _atLineStart = true;

    public IndentedAppender(IAppender inner, string indent = "  ")
    {
        _inner = inner ?? throw new ArgumentNullException(nameof(inner));
        _indent = indent ?? throw new ArgumentNullException(nameof(indent));
    }

    private void WriteIndentIfNeeded()
    {
        if (_atLineStart)
        {
            _inner.Append(_indent);
            _atLineStart = false;
        }
    }

    public void Append(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return;

        Append(value.AsSpan());
    }

    public void Append(char value)
    {
        if (value == '\r' || value == '\n')
        {
            _inner.Append(value);
            _atLineStart = true;
            return;
        }

        WriteIndentIfNeeded();
        _inner.Append(value);
    }

    public void Append(ReadOnlySpan<char> value)
    {
        if (value.Length == 0)
            return;

        int i = 0;
        int lastChunkStart = 0;

        while (i < value.Length)
        {
            char c = value[i];

            if (c == '\r' || c == '\n')
            {
                //write chunk before newline
                if (i > lastChunkStart)
                {
                    WriteIndentIfNeeded();
                    _inner.Append(value.Slice(lastChunkStart, i - lastChunkStart));
                }

                //write newline sequence
                if (c == '\r')
                {
                    //handle \r\n
                    if (i + 1 < value.Length && value[i + 1] == '\n')
                    {
                        _inner.Append('\r');
                        _inner.Append('\n');
                        i += 2;
                    }
                    else
                    {
                        _inner.Append('\r');
                        i += 1;
                    }
                }
                else //'\n'
                {
                    _inner.Append('\n');
                    i += 1;
                }

                _atLineStart = true;
                lastChunkStart = i;
                continue;
            }

            i++;
        }

        //trailing chunk
        if (value.Length > lastChunkStart)
        {
            WriteIndentIfNeeded();
            _inner.Append(value.Slice(lastChunkStart));
        }
    }

    public void AppendLine()
    {
        _inner.AppendLine();
        _atLineStart = true;
    }

    public void AppendLine(string? value)
    {
        if (!string.IsNullOrEmpty(value))
            Append(value);
        AppendLine();
    }

    public void AppendLine(ReadOnlySpan<char> value)
    {
        if (value.Length > 0)
            Append(value);
        AppendLine();
    }
}