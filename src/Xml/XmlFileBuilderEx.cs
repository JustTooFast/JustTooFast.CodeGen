// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml;
public static class XmlFileBuilderEx
{
    public static string Generate(this XmlFileBuilder builder)
    {
        XmlFileGenerator generator = builder;
        return generator.Generate();
    }
}
