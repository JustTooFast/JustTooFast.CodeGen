// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;

namespace JustTooFast.CodeGen.Xml;

public static class XmlWriter
{
    // ---------------------------
    // XmlFile
    // ---------------------------

    public static void WriteFile(XmlFileModel model, string path)
    {
        using var sink = new AtomicFileSink(path);
        Write(model, sink);
    }

    public static void WriteFileDirect(XmlFileModel model, string path)
    {
        using var sink = new FileSink(path);
        Write(model, sink);
    }

    public static void Write(XmlFileModel model, IOutputSink sink)
    {
        if (model is null) throw new ArgumentNullException(nameof(model));
        if (sink is null) throw new ArgumentNullException(nameof(sink));

        var root = new XmlFileEmitter(model);

        IFormatting fmt = Formatting.Default;

        new DocumentWriter(sink).Write(root, fmt);
    }

    public static void Write(XmlFileModel model, Stream stream, bool leaveOpen = true)
    {
        using var sink = new StreamSink(stream, leaveOpen: leaveOpen);
        Write(model, sink);
    }

    public static void Write(XmlFileModel model, TextWriter writer, bool leaveOpen = true)
    {
        using var sink = new TextWriterSink(writer, ownsWriter: !leaveOpen);
        Write(model, sink);
    }

    public static string ToString(XmlFileModel model)
    {
        using var sink = new StringBuilderSink();
        Write(model, sink);
        return sink.GetText();
    }

    // ---------------------------
    // XmlSnippet
    // ---------------------------

    public static void Write(XmlSnippetModel model, IOutputSink sink)
    {
        if (model is null) throw new ArgumentNullException(nameof(model));
        if (sink is null) throw new ArgumentNullException(nameof(sink));

        var root = new XmlSnippetEmitter(model);

        IFormatting fmt = Formatting.Default;

        new DocumentWriter(sink).Write(root, fmt);
    }

    public static void Write(XmlSnippetModel model, Stream stream, bool leaveOpen = true)
    {
        using var sink = new StreamSink(stream, leaveOpen: leaveOpen);
        Write(model, sink);
    }

    public static void Write(XmlSnippetModel model, TextWriter writer, bool leaveOpen = true)
    {
        using var sink = new TextWriterSink(writer, ownsWriter: !leaveOpen);
        Write(model, sink);
    }

    public static string ToString(XmlSnippetModel model)
    {
        using var sink = new StringBuilderSink();
        Write(model, sink);
        return sink.GetText();
    }
}
