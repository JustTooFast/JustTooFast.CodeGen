// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Globalization;

namespace JustTooFast.CodeGen.Json;

public partial class NumberValueBuilder
{
    // --- Integers (signed/unsigned) ---

    public NumberValueBuilder WithValue(sbyte value)  => WithValue(value.ToString(CultureInfo.InvariantCulture));
    public NumberValueBuilder WithValue(byte value)   => WithValue(value.ToString(CultureInfo.InvariantCulture));
    public NumberValueBuilder WithValue(short value)  => WithValue(value.ToString(CultureInfo.InvariantCulture));
    public NumberValueBuilder WithValue(ushort value) => WithValue(value.ToString(CultureInfo.InvariantCulture));
    public NumberValueBuilder WithValue(int value)    => WithValue(value.ToString(CultureInfo.InvariantCulture));
    public NumberValueBuilder WithValue(uint value)   => WithValue(value.ToString(CultureInfo.InvariantCulture));
    public NumberValueBuilder WithValue(long value)   => WithValue(value.ToString(CultureInfo.InvariantCulture));
    public NumberValueBuilder WithValue(ulong value)  => WithValue(value.ToString(CultureInfo.InvariantCulture));

    // --- Floating point / decimal ---

    public NumberValueBuilder WithValue(float value)
    {
        EnsureFinite(value, nameof(value));
        return WithValue(value.ToString("R", CultureInfo.InvariantCulture)); // round-trip
    }

    public NumberValueBuilder WithValue(double value)
    {
        EnsureFinite(value, nameof(value));
        return WithValue(value.ToString("R", CultureInfo.InvariantCulture)); // round-trip
    }

    public NumberValueBuilder WithValue(decimal value)
        => WithValue(value.ToString(CultureInfo.InvariantCulture));

    private static void EnsureFinite(float value, string paramName)
    {
        if (float.IsNaN(value) || float.IsInfinity(value))
            throw new ArgumentOutOfRangeException(paramName, value, "JSON numbers cannot be NaN or Infinity.");
    }

    private static void EnsureFinite(double value, string paramName)
    {
        if (double.IsNaN(value) || double.IsInfinity(value))
            throw new ArgumentOutOfRangeException(paramName, value, "JSON numbers cannot be NaN or Infinity.");
    }
}