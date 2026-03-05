// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Buffers.Binary;
using System.Security.Cryptography;

namespace JustTooFast.CodeGen;

public sealed class HashingAppender : IAppender, IHasFormatting<IFormatting>, IDisposable
{
    private IncrementalHash _hash;

    public IFormatting Formatting { get; }
    public HashAlgorithmName AlgorithmName { get; }
    public IAppender? Inner { get; }

    public HashingAppender(
        IFormatting? formatting = null,
        HashAlgorithmName? algorithmName = null,
        IAppender? inner = null)
    {
        Formatting = formatting ?? CodeGen.Formatting.Default;
        AlgorithmName = algorithmName ?? HashAlgorithmName.SHA256;
        _hash = IncrementalHash.CreateHash(AlgorithmName);
        Inner = inner;
    }

    public void Reset()
    {
        _hash.Dispose();
        _hash = IncrementalHash.CreateHash(AlgorithmName);
    }

    public byte[] GetHashAndReset() => _hash.GetHashAndReset();

    public string GetHashHexAndReset()
    {
        var bytes = GetHashAndReset();
        return Convert.ToHexString(bytes);
    }

    public void Append(string? value)
    {
        if (value is null)
        {
            Inner?.Append((string?)null);
            return;
        }

        Append(value.AsSpan());
    }

    public void Append(char value)
    {
        HashChar(value);
        Inner?.Append(value);
    }

    public void Append(ReadOnlySpan<char> value)
    {
        if (value.Length == 0)
        {
            Inner?.Append(value);
            return;
        }

        HashChars(value);
        Inner?.Append(value);
    }

    public void AppendLine()
    {
        // Hash the configured newline sequence for AppendLine()
        Append(Formatting.NewLine);
    }

    public void AppendLine(string? value)
    {
        if (!string.IsNullOrEmpty(value))
            Append(value);

        AppendLine();
    }

    public void AppendLine(char value)
    {
        Append(value);
        AppendLine();
    }

    public void AppendLine(ReadOnlySpan<char> value)
    {
        if (value.Length > 0)
            Append(value);

        AppendLine();
    }

    private void HashChar(char c)
    {
        Span<byte> bytes = stackalloc byte[2];
        BinaryPrimitives.WriteUInt16LittleEndian(bytes, c);
        _hash.AppendData(bytes);
    }

    private void HashChars(ReadOnlySpan<char> chars)
    {
        Span<byte> buffer = stackalloc byte[512]; // 256 chars per chunk
        int i = 0;

        while (i < chars.Length)
        {
            int charsThisChunk = Math.Min(chars.Length - i, buffer.Length / 2);
            var bytesThisChunk = buffer.Slice(0, charsThisChunk * 2);

            for (int j = 0; j < charsThisChunk; j++)
            {
                BinaryPrimitives.WriteUInt16LittleEndian(
                    bytesThisChunk.Slice(j * 2, 2),
                    chars[i + j]);
            }

            _hash.AppendData(bytesThisChunk);
            i += charsThisChunk;
        }
    }

    public void Dispose()
    {
        _hash.Dispose();
    }
}