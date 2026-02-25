// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml;
public static class XmlSnippetBuilderEx
{
    public static string Generate(this XmlSnippetBuilder builder)
    {
        XmlSnippetGenerator generator = builder;
        return generator.Generate();
    }
}
