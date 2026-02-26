// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen.Xml;
public abstract class EmitterBase
{
    private IAppender m_Appender;

    public IAppender Appender
    {
        protected get
        {
            if (m_Appender == null)
                throw new Exception("Appender is not initialized.");
            else
                return m_Appender;
        }
        set
        {
            m_Appender = value;
        }
    }

    public abstract void AppendDeclaration();

    public override string ToString()
    {
        return m_Appender.ToString();
    }
}
