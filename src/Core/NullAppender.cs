// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen;

public sealed class NullAppender : IAppender
{
    public static readonly NullAppender Instance = new();

    private NullAppender() { }

    public void Append(string? value) { }

    public void Append(char value) { }

    public void Append(ReadOnlySpan<char> value) { }

    public void AppendLine() { }

    public void AppendLine(string? value) { }

    public void AppendLine(char value) { }

    public void AppendLine(ReadOnlySpan<char> value) { }
}
