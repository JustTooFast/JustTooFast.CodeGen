// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Text;

namespace JustTooFast.CodeGen;

public sealed class FileSink : IOutputSink
{
    private readonly StreamSink _inner;

    public FileSink(
        string path,
        bool overwrite = true,
        Encoding? encoding = null,
        IFormatting? formatting = null)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path is required.", nameof(path));

        var stream = new FileStream(
            path,
            overwrite ? FileMode.Create : FileMode.CreateNew,
            FileAccess.Write,
            FileShare.Read);

        _inner = new StreamSink(stream, leaveOpen: false, encoding: encoding, formatting: formatting);
    }

    public IAppender CreateAppender() => _inner.CreateAppender();
    public void Complete() => _inner.Complete();
    public void Dispose() => _inner.Dispose();
}