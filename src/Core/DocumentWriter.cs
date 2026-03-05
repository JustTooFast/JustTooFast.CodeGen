// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen;

public sealed class DocumentWriter
{
    private readonly IOutputSink _sink;

    public DocumentWriter(IOutputSink sink)
        => _sink = sink ?? throw new ArgumentNullException(nameof(sink));

    public void Write(IEmitter root, IFormatting? formatting = null)
    {
        if (root is null) throw new ArgumentNullException(nameof(root));

        using (_sink)
        {
            IAppender a = _sink.CreateAppender();

            if (formatting is not null)
            {
                // Caller override: always wrap so Formatting (and AppendLine behavior) is forced.
                a = new FormattingAppender(a, formatting);
            }
            else
            {
                // No override:
                // - if appender already has formatting, keep it (no wrap)
                // - else attach default formatting by wrapping
                if (a is not IHasFormatting<IFormatting>)
                    a = new FormattingAppender(a, CodeGen.Formatting.Default);
            }

            root.EmitTo(a);
            _sink.Complete();
        }
    }
}
