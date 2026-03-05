// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml;
public partial class XmlFileBuilder
{
    public XmlFileBuilder WithDisableProlog(bool disableProlog)
    {
        m_XmlFile.DisableProlog = disableProlog;

        return this;
    }

    public XmlFileBuilder AsDisableProlog() => WithDisableProlog(true);
}
