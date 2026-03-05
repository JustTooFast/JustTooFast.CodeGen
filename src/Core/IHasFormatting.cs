// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen;

public interface IHasFormatting<out TFormatting>
    where TFormatting : IFormatting
{
    TFormatting Formatting { get; }
}
