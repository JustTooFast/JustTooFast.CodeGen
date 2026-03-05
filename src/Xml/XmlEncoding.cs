// Copyright 2024-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System.ComponentModel;

namespace JustTooFast.CodeGen.Xml;
public enum XmlEncoding
{
    [Description("UTF-8")]
    UTF_8,

    [Description("UTF-16")]
    UTF_16,

    [Description("ISO-10646-UCS-2")]
    ISO_10646_UCS_2,

    [Description("ISO-10646-UCS-4")]
    ISO_10646_UCS_4,

    [Description("ISO-8859-1")]
    ISO_8859_1,

    [Description("ISO-8859-2")]
    ISO_8859_2,

    [Description("ISO-8859-3")]
    ISO_8859_3,

    [Description("ISO-8859-4")]
    ISO_8859_4,

    [Description("ISO-8859-5")]
    ISO_8859_5,

    [Description("ISO-8859-6")]
    ISO_8859_6,

    [Description("ISO-8859-7")]
    ISO_8859_7,

    [Description("ISO-8859-8")]
    ISO_8859_8,

    [Description("ISO-8859-9")]
    ISO_8859_9,

    [Description("ISO-2022-JP")]
    ISO_2022_JP,

    [Description("Shift_JIS")]
    Shift_JIS,

    [Description("EUC-JP")]
    EUC_JP,

    [Description("US-ASCII")]
    US_ASCII
}
