using System;

namespace JustTooFast.CodeGen;

public static class BoolText
{
    public static bool Parse(string? value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{paramName} is required.", paramName);

        string v = value.Trim();

        if (v.Equals("true", StringComparison.OrdinalIgnoreCase)) return true;
        if (v.Equals("false", StringComparison.OrdinalIgnoreCase)) return false;

        throw new ArgumentException($"{paramName} must be 'true' or 'false'.", paramName);
    }

    public static bool TryParse(string? value, out bool result)
    {
        result = false;
        if (string.IsNullOrWhiteSpace(value)) return false;

        string v = value.Trim();
        if (v.Equals("true", StringComparison.OrdinalIgnoreCase)) { result = true; return true; }
        if (v.Equals("false", StringComparison.OrdinalIgnoreCase)) { result = false; return true; }

        return false;
    }
}
