// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen.Xml;
public class XmlFileGenerator : IGenerator
{
    private readonly XmlFileModel m_XmlFile;

    public XmlFileGenerator(XmlFileModel xmlFile)
    {
        m_XmlFile = xmlFile ?? throw new ArgumentNullException(nameof(xmlFile));
    }

    public string Generate()
    {
        XmlFileEmitter xmlFileEmitter = new(m_XmlFile, new Appender());
        xmlFileEmitter.AppendDeclaration();

        return xmlFileEmitter.ToString();
    }
}
