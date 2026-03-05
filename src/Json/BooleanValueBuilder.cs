// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Json;

public partial class BooleanValueBuilder
{
    public BooleanValueBuilder WithValue(bool value) => WithValue(value.ToString().ToLowerInvariant());
}