// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Json;
public partial class JsonFileBuilder
{
    public JsonFileBuilder WithPrettyPrint(bool prettyPrint)
    {
        m_JsonFile.PrettyPrint = prettyPrint.ToString().ToLowerInvariant();

        return this;
    }

    public JsonFileBuilder AsPrettyPrint() => WithPrettyPrint(true);
}
