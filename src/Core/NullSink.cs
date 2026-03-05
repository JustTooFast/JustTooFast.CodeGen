// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen;

public sealed class NullSink : IOutputSink
{
    private readonly IAppender _appender = NullAppender.Instance;
    private bool _created;

    public IAppender CreateAppender()
    {
        if (_created) throw new InvalidOperationException("CreateAppender() may only be called once.");
        _created = true;
        return _appender;
    }

    public void Complete() { }
    public void Dispose() { }
}