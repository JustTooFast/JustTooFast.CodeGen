// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Json;

public interface IJsonFormatting : IFormatting
{
    bool PrettyPrint { get; }
}