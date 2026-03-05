// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Buffers;
using System.IO;
using System.Text;

namespace JustTooFast.CodeGen;

public sealed class StreamAppender : IAppender, IHasFormatting<IFormatting>, IDisposable
{
    private readonly Stream _stream;
    private readonly Encoding _encoding;
    private readonly bool _leaveOpen;

    // Reused for small writes
    private byte[] _smallBytes;

    public IFormatting Formatting { get; }

    public StreamAppender(
        Stream stream,
        IFormatting? formatting = null,
        Encoding? encoding = null,
        bool leaveOpen = true,
        int smallBufferSize = 256)
    {
        _stream = stream ?? throw new ArgumentNullException(nameof(stream));
        if (!stream.CanWrite)
            throw new ArgumentException("Stream must be writable.", nameof(stream));

        Formatting = formatting ?? CodeGen.Formatting.Default;

        // Default: UTF-8 without BOM (typically what you want for JSON/XML/YAML text)
        _encoding = encoding ?? new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
        _leaveOpen = leaveOpen;

        if (smallBufferSize <= 0) smallBufferSize = 256;
        _smallBytes = ArrayPool<byte>.Shared.Rent(smallBufferSize);
    }

    public void Append(string? value)
    {
        if (string.IsNullOrEmpty(value)) return;
        Append(value.AsSpan());
    }

    public void Append(char value)
    {
        Span<char> one = stackalloc char[1];
        one[0] = value;
        Append(one);
    }

    public void Append(ReadOnlySpan<char> value)
    {
        if (value.Length == 0) return;

        int maxBytes = _encoding.GetMaxByteCount(value.Length);

        if (maxBytes <= _smallBytes.Length)
        {
#if NETSTANDARD2_1_OR_GREATER || NET6_0_OR_GREATER
            int bytesWritten = _encoding.GetBytes(value, _smallBytes);
#else
            int bytesWritten = _encoding.GetBytes(value.ToString(), 0, value.Length, _smallBytes, 0);
#endif
            _stream.Write(_smallBytes, 0, bytesWritten);
            return;
        }

        byte[] rented = ArrayPool<byte>.Shared.Rent(maxBytes);
        try
        {
#if NETSTANDARD2_1_OR_GREATER || NET6_0_OR_GREATER
            int bytesWritten = _encoding.GetBytes(value, rented);
#else
            // Worst-case: allocate a temporary string (no span-based encoding on older TFMs)
            string tmp = value.ToString();
            int bytesWritten = _encoding.GetBytes(tmp, 0, tmp.Length, rented, 0);
#endif
            _stream.Write(rented, 0, bytesWritten);
        }
        finally
        {
            ArrayPool<byte>.Shared.Return(rented);
        }
    }

    public void AppendLine() => Append(Formatting.NewLine);

    public void AppendLine(string? value)
    {
        Append(value);
        Append(Formatting.NewLine);
    }

    public void AppendLine(char value)
    {
        Append(value);
        Append(Formatting.NewLine);
    }

    public void AppendLine(ReadOnlySpan<char> value)
    {
        Append(value);
        Append(Formatting.NewLine);
    }

    public void Flush() => _stream.Flush();

    public void Dispose()
    {
        if (_smallBytes is not null)
        {
            ArrayPool<byte>.Shared.Return(_smallBytes);
            _smallBytes = null!;
        }

        if (!_leaveOpen)
            _stream.Dispose();
    }
}
