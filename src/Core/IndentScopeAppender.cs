// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Collections.Generic;

namespace JustTooFast.CodeGen;

/// <summary>
/// Adds multi-level indentation by stacking fixed-indent <see cref="IndentedAppender"/> wrappers.
/// </summary>
public sealed class IndentScopeAppender : IIndentingAppender, IHasFormatting<IFormatting>
{
    private readonly Stack<IAppender> _stack = new();
    private IAppender _current;

    public IFormatting Formatting { get; }

    public IndentScopeAppender(IAppender inner)
    {
        _current = inner ?? throw new ArgumentNullException(nameof(inner));

        Formatting = (inner as IHasFormatting<IFormatting>)?.Formatting
            ?? CodeGen.Formatting.Default;
    }

    public IDisposable IndentScope(string? indentUnitOverride = null)
    {
        Indent(indentUnitOverride);
        return new Popper(this);
    }

    public void Indent(string? indentUnitOverride = null)
    {
        _stack.Push(_current);
        _current = new IndentedAppender(_current, indentUnitOverride);
    }

    public void Outdent()
    {
        if (_stack.Count == 0)
            return; // no-op by design

        _current = _stack.Pop();
    }

    private sealed class Popper : IDisposable
    {
        private IndentScopeAppender? _owner;
        public Popper(IndentScopeAppender owner) => _owner = owner;

        public void Dispose()
        {
            var o = _owner;
            if (o is null) return;
            _owner = null;
            o.Outdent();
        }
    }

    // Forward IAppender calls to the current target
    public void Append(string? value) => _current.Append(value);
    public void Append(char value) => _current.Append(value);
    public void Append(ReadOnlySpan<char> value) => _current.Append(value);

    public void AppendLine() => _current.AppendLine();
    public void AppendLine(string? value) => _current.AppendLine(value);
    public void AppendLine(char value) => _current.AppendLine(value);
    public void AppendLine(ReadOnlySpan<char> value) => _current.AppendLine(value);
}
