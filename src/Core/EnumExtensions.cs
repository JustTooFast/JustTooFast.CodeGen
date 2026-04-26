// Copyright 2024-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.ComponentModel;
using System.Reflection;

namespace JustTooFast.CodeGen;
public static class EnumExtensions
{
    public static string GetDescription(this Enum value)
    {
        string result = value.ToString();

        Type enumType = value.GetType();
        MemberInfo[] memberInfo = enumType.GetMember(value.ToString());
        if ((memberInfo != null && memberInfo.Length > 0))
        {
            DescriptionAttribute[] attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            if ((attributes != null) && (attributes.Length > 0))
            {
                result = attributes[0].Description;
            }
        }

        return result;
    }
}
