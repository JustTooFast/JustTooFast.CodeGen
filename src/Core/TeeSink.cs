// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen;

public sealed class TeeSink : IOutputSink
{
    private readonly IOutputSink _left;
    private readonly IOutputSink _right;

    private bool _created;

    public TeeSink(IOutputSink left, IOutputSink right)
    {
        _left = left ?? throw new ArgumentNullException(nameof(left));
        _right = right ?? throw new ArgumentNullException(nameof(right));
    }

    public IAppender CreateAppender()
    {
        if (_created) throw new InvalidOperationException("CreateAppender() may only be called once.");
        _created = true;

        var leftAppender = _left.CreateAppender();
        var rightAppender = _right.CreateAppender();

        return new TeeAppender(leftAppender, rightAppender);
    }

    public void Complete()
    {
        // Important: complete both even if one throws, so we don't leave temp files hanging, etc.
        Exception? first = null;

        try { _left.Complete(); }
        catch (Exception ex) { first = ex; }

        try { _right.Complete(); }
        catch (Exception ex) { first ??= ex; }

        if (first is not null) throw first;
    }

    public void Dispose()
    {
        Exception? first = null;

        try { _left.Dispose(); }
        catch (Exception ex) { first = ex; }

        try { _right.Dispose(); }
        catch (Exception ex) { first ??= ex; }

        if (first is not null) throw first;
    }
}