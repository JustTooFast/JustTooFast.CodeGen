// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen.Xml;
public class XmlFileGenerator : IGenerator
{
    private readonly XmlFileInfo m_XmlFile;

    public XmlFileGenerator(XmlFileInfo xmlFile)
    {
        m_XmlFile = xmlFile ?? throw new ArgumentNullException(nameof(xmlFile));
    }

    public string Generate()
    {
        XmlFileDeclaration xmlFileDeclaration = new(m_XmlFile, new Appender());
        xmlFileDeclaration.AppendDeclaration();

        return xmlFileDeclaration.ToString();
    }
}
