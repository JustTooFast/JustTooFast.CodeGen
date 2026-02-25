// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml;
public interface IAppender
{
    void Append(string value);

    void Append(char value);

    void AppendLineFeed();

    void AppendLineFeed(string value);

    string ToString();
}