// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;

namespace JustTooFast.CodeGen.Json;

public static class JsonWriter
{
    // 1) Default: write file atomically, formatting derived from model
    public static void WriteFile(JsonFileModel model, string path)
    {
        using var sink = new AtomicFileSink(path);
        Write(model, sink);
    }

    // 2) Direct overwrite (rare)
    public static void WriteFileDirect(JsonFileModel model, string path)
    {
        using var sink = new FileSink(path);
        Write(model, sink);
    }

    // 3) Generic sink (lets callers decide destination)
    public static void Write(JsonFileModel model, IOutputSink sink)
    {
        if (model is null) throw new ArgumentNullException(nameof(model));
        if (sink is null) throw new ArgumentNullException(nameof(sink));

        var root = new JsonFileEmitter(model);

        // Derive formatting override from model (or pass null if you want sink formatting to win)
        bool pretty = BoolText.Parse(model.PrettyPrint, "JsonFile.PrettyPrint");
        IFormatting fmt = new JsonFormatting(prettyPrint: pretty);

        new DocumentWriter(sink).Write(root, fmt);
    }

    // 4) Stream/TextWriter helpers
    public static void Write(JsonFileModel model, Stream stream, bool leaveOpen = true)
    {
        using var sink = new StreamSink(stream, leaveOpen: leaveOpen);
        Write(model, sink);
    }

    public static void Write(JsonFileModel model, TextWriter writer, bool leaveOpen = true)
    {
        using var sink = new TextWriterSink(writer, ownsWriter: !leaveOpen);
        Write(model, sink);
    }

    // 5) To string
    public static string ToString(JsonFileModel model)
    {
        var sbSink = new StringBuilderSink();
        Write(model, sbSink);
        return sbSink.GetText();
    }
}
