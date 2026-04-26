// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen.Naming;

public static class EnglishPluralizer
{
    private static readonly string[] s_unchangedPluralSuffixes =
    [
        "SHEEP",
        "SERIES",
        "SPECIES",
        "DEER",
        "FISH",
        "MOOSE",
        "COD",
        "TROUT",
        "SALMON",
        "BISON",
        "BUFFALO",
        "ELK",
        "SWINE",
        "AIRCRAFT",
        "SPACECRAFT",
        "HOVERCRAFT",
        "WATERCRAFT"
    ];

    public static string Pluralize(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return value;

        if (value.Length == 1)
            return value + "s";

        string upper = value.ToUpperInvariant();

        if (IsUnchangedPlural(upper))
            return value;

        if (TryPluralizeIrregular(value, upper, out string plural))
            return plural;

        return ApplySuffixRules(value, upper);
    }

    private static bool IsUnchangedPlural(string upper)
    {
        foreach (string suffix in s_unchangedPluralSuffixes)
        {
            if (upper.EndsWith(suffix, StringComparison.Ordinal))
                return true;
        }

        return false;
    }

    private static bool TryPluralizeIrregular(string value, string upper, out string plural)
    {
        if (upper == "OX")
        {
            plural = value + "en";
            return true;
        }

        if (upper.EndsWith("CHILD", StringComparison.Ordinal))
        {
            plural = value + "ren";
            return true;
        }

        if (upper.EndsWith("GOOSE", StringComparison.Ordinal))
        {
            plural = value[..^4] + "eese";
            return true;
        }

        if (upper.EndsWith("WOMAN", StringComparison.Ordinal))
        {
            plural = value[..^2] + "en";
            return true;
        }

        if (upper.EndsWith("MAN", StringComparison.Ordinal))
        {
            plural = value[..^2] + "en";
            return true;
        }

        if (upper.EndsWith("TOOTH", StringComparison.Ordinal))
        {
            plural = value[..^4] + "eeth";
            return true;
        }

        if (upper.EndsWith("FOOT", StringComparison.Ordinal))
        {
            plural = value[..^3] + "eet";
            return true;
        }

        if (upper.EndsWith("MOUSE", StringComparison.Ordinal))
        {
            plural = value[..^4] + "ice";
            return true;
        }

        if (upper.EndsWith("LOUSE", StringComparison.Ordinal))
        {
            plural = value[..^4] + "ice";
            return true;
        }

        if (upper.EndsWith("PERSON", StringComparison.Ordinal))
        {
            plural = value[..^4] + "ople";
            return true;
        }

        plural = value;
        return false;
    }

    private static string ApplySuffixRules(string value, string upper)
    {
        char last = upper[^1];
        char secondLast = upper[^2];

        if (TryPluralizeIsEnding(value, upper, out string plural))
            return plural;

        if (TryPluralizeUsEnding(value, upper, out plural))
            return plural;

        // some -s or -z => double s or z, add -es
        if ((last == 'S' || last == 'Z') &&
            upper.EndsWith("FEZ", StringComparison.Ordinal))
        {
            return value + value[^1] + "es";
        }

        // -s, -ss, -sh, -ch, -x, -z => add -es
        if (last == 'S' || last == 'X' || last == 'Z' ||
            upper.EndsWith("SS", StringComparison.Ordinal) ||
            upper.EndsWith("SH", StringComparison.Ordinal) ||
            upper.EndsWith("CH", StringComparison.Ordinal))
        {
            return value + "es";
        }

        // -ff => add -s
        if (upper.EndsWith("FF", StringComparison.Ordinal))
        {
            return value + "s";
        }

        // -f exceptions => add -s
        if (last == 'F' &&
            (upper.EndsWith("ROOF", StringComparison.Ordinal) ||
             upper.EndsWith("BELIEF", StringComparison.Ordinal) ||
             upper.EndsWith("CHEF", StringComparison.Ordinal) ||
             upper.EndsWith("CHIEF", StringComparison.Ordinal) ||
             upper.EndsWith("DWARF", StringComparison.Ordinal) ||
             upper.EndsWith("REEF", StringComparison.Ordinal)))
        {
            return value + "s";
        }

        // -fe exceptions => add -s
        if (upper.EndsWith("SAFE", StringComparison.Ordinal))
        {
            return value + "s";
        }

        // most -f => -ves
        if (last == 'F')
        {
            return value[..^1] + "ves";
        }

        // most -fe => -ves
        if (upper.EndsWith("FE", StringComparison.Ordinal))
        {
            return value[..^2] + "ves";
        }

        // vowel + y => add -s
        if (last == 'Y' && IsVowel(secondLast))
        {
            return value + "s";
        }

        // consonant + y => -ies
        if (last == 'Y')
        {
            return value[..^1] + "ies";
        }

        // -o exceptions => add -s
        if (last == 'O' &&
            (upper.EndsWith("PHOTO", StringComparison.Ordinal) ||
             upper.EndsWith("PIANO", StringComparison.Ordinal) ||
             upper.EndsWith("HALO", StringComparison.Ordinal) ||
             upper.EndsWith("SOLO", StringComparison.Ordinal) ||
             upper.EndsWith("TANGELO", StringComparison.Ordinal) ||
             upper.EndsWith("PICCOLO", StringComparison.Ordinal) ||
             upper.EndsWith("VIRTUOSO", StringComparison.Ordinal) ||
             upper.EndsWith("ARCHIPELAGO", StringComparison.Ordinal) ||
             upper.EndsWith("AUTO", StringComparison.Ordinal) ||
             upper.EndsWith("ALTO", StringComparison.Ordinal)))
        {
            return value + "s";
        }

        // -o => add -es
        if (last == 'O')
        {
            return value + "es";
        }

        // some -on => -a
        if (upper.EndsWith("PHENOMENON", StringComparison.Ordinal) ||
            upper.EndsWith("CRITERION", StringComparison.Ordinal))
        {
            return value[..^2] + "a";
        }

        // default => add -s
        return value + "s";
    }

    private static bool TryPluralizeIsEnding(string value, string upper, out string plural)
    {
        if (!upper.EndsWith("IS", StringComparison.Ordinal))
        {
            plural = value;
            return false;
        }

        if (upper.EndsWith("IRIS", StringComparison.Ordinal))
        {
            plural = value + "es";
            return true;
        }

        if (upper.EndsWith("ANALYSIS", StringComparison.Ordinal) ||
            upper.EndsWith("BASIS", StringComparison.Ordinal) ||
            upper.EndsWith("CRISIS", StringComparison.Ordinal) ||
            upper.EndsWith("DIAGNOSIS", StringComparison.Ordinal) ||
            upper.EndsWith("ELLIPSIS", StringComparison.Ordinal) ||
            upper.EndsWith("HYPOTHESIS", StringComparison.Ordinal) ||
            upper.EndsWith("NEUROSIS", StringComparison.Ordinal) ||
            upper.EndsWith("OASIS", StringComparison.Ordinal) ||
            upper.EndsWith("PARENTHESIS", StringComparison.Ordinal) ||
            upper.EndsWith("SYNOPSIS", StringComparison.Ordinal) ||
            upper.EndsWith("THESIS", StringComparison.Ordinal))
        {
            plural = value[..^2] + "es";
            return true;
        }

        plural = value + "es";
        return true;
    }

    private static bool TryPluralizeUsEnding(string value, string upper, out string plural)
    {
        if (!upper.EndsWith("US", StringComparison.Ordinal))
        {
            plural = value;
            return false;
        }

        // words that commonly take classical plurals
        if (upper.EndsWith("CACTUS", StringComparison.Ordinal) ||
            upper.EndsWith("FUNGUS", StringComparison.Ordinal) ||
            upper.EndsWith("NUCLEUS", StringComparison.Ordinal) ||
            upper.EndsWith("RADIUS", StringComparison.Ordinal) ||
            upper.EndsWith("STIMULUS", StringComparison.Ordinal))
        {
            plural = value[..^2] + "i";
            return true;
        }

        // practical English / codegen-friendly forms
        if (upper.EndsWith("BONUS", StringComparison.Ordinal) ||
            upper.EndsWith("CAMPUS", StringComparison.Ordinal) ||
            upper.EndsWith("CIRCUS", StringComparison.Ordinal) ||
            upper.EndsWith("FOCUS", StringComparison.Ordinal) ||
            upper.EndsWith("STATUS", StringComparison.Ordinal) ||
            upper.EndsWith("VIRUS", StringComparison.Ordinal))
        {
            plural = value + "es";
            return true;
        }

        // conservative default
        plural = value + "es";
        return true;
    }

    private static bool IsVowel(char c) =>
        c == 'A' || c == 'E' || c == 'I' || c == 'O' || c == 'U';
}
