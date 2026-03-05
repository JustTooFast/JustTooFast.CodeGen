// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen;

public interface IAppender
{
    void Append(string? value);
    void Append(char value);
    void Append(ReadOnlySpan<char> value);

    void AppendLine();
    void AppendLine(string? value);
    void AppendLine(char value);
    void AppendLine(ReadOnlySpan<char> value);
}