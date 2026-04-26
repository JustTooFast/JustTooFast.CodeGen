// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen;

public interface IIndentingAppender : IAppender
{
    IDisposable IndentScope(string? indentUnitOverride = null);
    void Indent(string? indentUnitOverride = null);
    void Outdent();
}