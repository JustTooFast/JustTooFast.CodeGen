// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Text;

namespace JustTooFast.CodeGen;

public sealed class AtomicFileSink : IOutputSink
{
    private readonly string _destPath;
    private readonly Encoding? _encoding;
    private readonly IFormatting? _formatting;

    private readonly string _tempPath;

    private StreamSink? _inner;
    private bool _created;

    public bool Written { get; private set; }   // true if destination was updated
    public bool Skipped { get; private set; }   // true if destination already matched

    public AtomicFileSink(string destPath, Encoding? encoding = null, IFormatting? formatting = null)
    {
        if (string.IsNullOrWhiteSpace(destPath))
            throw new ArgumentException("Destination path is required.", nameof(destPath));

        _destPath = destPath;
        _encoding = encoding;
        _formatting = formatting;

        string dir = Path.GetDirectoryName(_destPath) ?? "";
        string file = Path.GetFileName(_destPath);
        _tempPath = Path.Combine(dir, $".{file}.tmp.{Guid.NewGuid():N}");
    }

    public IAppender CreateAppender()
    {
        if (_created) throw new InvalidOperationException("CreateAppender() may only be called once.");
        _created = true;

        Directory.CreateDirectory(Path.GetDirectoryName(_destPath) ?? ".");

        var tmpStream = new FileStream(
            _tempPath,
            FileMode.CreateNew,
            FileAccess.Write,
            FileShare.Read,
            bufferSize: 64 * 1024,
            options: FileOptions.SequentialScan);

        _inner = new StreamSink(tmpStream, leaveOpen: false, encoding: _encoding, formatting: _formatting);
        return _inner.CreateAppender();
    }

    public void Complete()
    {
        if (_inner is null) throw new InvalidOperationException("CreateAppender() must be called before Complete().");

        _inner.Complete();
        _inner.Dispose();
        _inner = null;

        // If destination doesn't exist, just move temp into place.
        if (!File.Exists(_destPath))
        {
            EnsureDirectoryExists(_destPath);
            File.Move(_tempPath, _destPath);
            Written = true;
            return;
        }

        // Compare temp vs dest; if equal, delete temp and skip.
        if (FilesAreEqual(_tempPath, _destPath))
        {
            File.Delete(_tempPath);
            Skipped = true;
            return;
        }

        // Different: atomically replace destination.
        ReplaceAtomically(_tempPath, _destPath);
        Written = true;
    }

    public void Dispose()
    {
        // If something failed before Complete(), clean up temp.
        try
        {
            _inner?.Dispose();
            _inner = null;

            if (File.Exists(_tempPath))
                File.Delete(_tempPath);
        }
        catch
        {
            // swallow cleanup exceptions
        }
    }

    private static void EnsureDirectoryExists(string path)
    {
        string? dir = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(dir))
            Directory.CreateDirectory(dir);
    }

    private static void ReplaceAtomically(string tempPath, string destPath)
    {
        // File.Replace is atomic on Windows (and generally the right tool when available).
        // On platforms where it may throw in some cases, fall back to Move-overwrite.
        try
        {
            // If you want backups, pass a backup file path instead of null.
            File.Replace(tempPath, destPath, destinationBackupFileName: null, ignoreMetadataErrors: true);
        }
        catch
        {
#if NET6_0_OR_GREATER
            File.Move(tempPath, destPath, overwrite: true);
#else
            // .NET Standard fallback: delete then move (not perfectly atomic).
            File.Delete(destPath);
            File.Move(tempPath, destPath);
#endif
        }
    }

    private static bool FilesAreEqual(string aPath, string bPath)
    {
        var aInfo = new FileInfo(aPath);
        var bInfo = new FileInfo(bPath);

        if (aInfo.Length != bInfo.Length)
            return false;

        const int BufferSize = 64 * 1024;
        byte[] aBuf = new byte[BufferSize];
        byte[] bBuf = new byte[BufferSize];

        using var a = new FileStream(aPath, FileMode.Open, FileAccess.Read, FileShare.Read, BufferSize, FileOptions.SequentialScan);
        using var b = new FileStream(bPath, FileMode.Open, FileAccess.Read, FileShare.Read, BufferSize, FileOptions.SequentialScan);

        while (true)
        {
            int aRead = a.Read(aBuf, 0, aBuf.Length);
            int bRead = b.Read(bBuf, 0, bBuf.Length);

            if (aRead != bRead)
                return false;

            if (aRead == 0)
                return true;

            for (int i = 0; i < aRead; i++)
            {
                if (aBuf[i] != bBuf[i])
                    return false;
            }
        }
    }
}