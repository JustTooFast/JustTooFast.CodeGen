// Copyright 2024-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml;
public partial class XmlDeclarationBuilder
{
    public XmlDeclarationBuilder WithEncoding(XmlEncoding encoding) => WithEncoding(encoding.GetDescription());

    public XmlDeclarationBuilder WithStandalone(XmlStandalone standalone) => WithStandalone(standalone.GetDescription());
}
