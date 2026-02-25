// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Scaffolder;

/// <summary>
/// Provides string extension methods.
/// </summary>
public static class StringEx
{
    /// <summary>
    /// Converts the first character in a string to lower case.
    /// </summary>
    /// <param name="str">The string to convert.</param>
    /// <returns>The converted string.</returns>
    public static string ToLowerFirstLetter(this string str)
    {
        string result;
        
        if (string.IsNullOrWhiteSpace(str))
        {
            result = str;
        }
        else
        {
            string firstChar = str[..1].ToLower();

            string otherChars = string.Empty;
            if (str.Length > 1)
                otherChars = str[1..];

            result = $"{firstChar}{otherChars}";
        }

        return result;
    }

    /// <summary>
    /// Pluralizes a string. Assumes the string contains
    /// a noun in camelCase or PascalCase.
    /// </summary>
    /// <param name="str">The string to convert.</param>
    /// <returns>The converted string.</returns>
    public static string ToPlural(this string str)
    {
        string result;

        if (string.IsNullOrWhiteSpace(str))
        {
            result = str;
        }
        else if (str.Length == 1)
        {
            result = $"{str}s";
        }
        else
        {
            string upStr = str.ToUpper();
            string last1 = upStr[^1].ToString();
            string last2 = upStr[^2..];

            //words with no change
            if (upStr.EndsWith("SHEEP") || upStr.EndsWith("SERIES") || upStr.EndsWith("SPECIES") ||
                upStr.EndsWith("DEER") || upStr.EndsWith("FISH") || upStr.EndsWith("MOOSE") ||
                upStr.EndsWith("COD") || upStr.EndsWith("TROUT"))
            {
                //sheep, series, species, deer, fish, moose, cod, trout
                result = str;
            } //irregular words
            else if (upStr.EndsWith("CHILD") || upStr.EndsWith("GOOSE") || upStr.EndsWith("MAN") || //includes woman
                upStr.EndsWith("TOOTH") || upStr.EndsWith("FOOT") || upStr.EndsWith("MOUSE") ||
                upStr.EndsWith("PERSON") || upStr == "OX")
            {
                //child - children
                //goose - geese
                //man - men
                //woman - women
                //tooth - teeth
                //foot - feet
                //mouse - mice
                //person - people
                //ox - oxen
                if (upStr.EndsWith("CHILD"))
                    result = $"{str}ren";
                else if (upStr.EndsWith("GOOSE"))
                    result = $"{str[..^4]}eese";
                else if (upStr.EndsWith("MAN"))  //includes woman
                    result = $"{str[..^2]}en";
                else if (upStr.EndsWith("TOOTH"))
                    result = $"{str[..^4]}eeth";
                else if (upStr.EndsWith("FOOT"))
                    result = $"{str[..^3]}eet";
                else if (upStr.EndsWith("MOUSE"))
                    result = $"{str[..^4]}ice";
                else if (upStr.EndsWith("PERSON"))
                    result = $"{str[..^4]}ople";
                else if (upStr == "OX")
                    result = $"{str}en";
                else
                    result = str;
            } //-is => change -is to -es
            else if (last2 == "IS" &&
                (!upStr.EndsWith("IRIS")))
            {
                result = $"{str[..^2]}es";
            } //-us => change -us to -i
            else if (last2 == "US")
            {
                result = $"{str[..^2]}i";
            } //some -s or -z => double s or z, add -es
            else if ((last1 == "S" || last1 == "Z") &&
                (upStr.EndsWith("FEZ")))
            {
                result = $"{str}{str[^1]}es";
            } //-s, -ss, -sh, -ch, -x, -z => add -es
            else if (last1 == "S" || last1 == "X" || last1 == "Z" ||
                last2 == "SS" || last2 == "SH" || last2 == "CH")
            {
                result = $"{str}es";
            } //-ff => add -s
            else if (last2 == "FF")
            {
                result = $"{str}s";
            } //-f, -fe exceptions
            else if (last1 == "F" &&
                (upStr.EndsWith("ROOF") || upStr.EndsWith("BELIEF") || upStr.EndsWith("CHEF") ||
                upStr.EndsWith("CHIEF") || upStr.EndsWith("DWARF") || upStr.EndsWith("REEF")))
            {
                //roof, belief, chef, chief, dwarf, reef
                result = $"{str}s";
            }
            else if (last2 == "FE" &&
                (upStr.EndsWith("SAFE")))
            {
                //safe
                result = $"{str}s";
            } //most -f, -fe => change f to v, add -es
            else if (last1 == "F")
            {
                result = $"{str[..^1]}ves";
            }
            else if (last2 == "FE")
            {
                result = $"{str[..^2]}ves";
            } //-vowel + y => add -s
            else if (last1 == "Y" &&
                (last2[0] == 'A' || last2[0] == 'E' || last2[0] == 'I' || last2[0] == 'O' || last2[0] == 'U'))
            {
                result = $"{str}s";
            } //-consonant + y => change -y to -ies
            else if (last1 == "Y")
            {
                result = $"{str[..^1]}ies";
            } //-o exceptions
            else if (last1 == "O" &&
                (upStr.EndsWith("PHOTO") || upStr.EndsWith("PIANO") || upStr.EndsWith("HALO") ||
                upStr.EndsWith("SOLO") || upStr.EndsWith("TANGELO") || upStr.EndsWith("PICCOLO") ||
                upStr.EndsWith("VIRTUOSO") || upStr.EndsWith("ARCHIPELAGO") || upStr.EndsWith("AUTO") ||
                upStr.EndsWith("ALTO")))
            {
                //photo, piano, halo, solo, tangelo, piccolo, virtuoso, archipelago, auto, alto
                result = $"{str}s";
            } //-o => add -es
            else if (last1 == "O")
            {
                result = $"{str}es";
            } //some -on => change -on to -a
            else if (last2 == "ON" &&
                (upStr.EndsWith("PHENOMENON") || upStr.EndsWith("CRITERION")))
            {
                //phenomenon, criterion
                result = $"{str[..^2]}a";
            }
            else //default => add -s
            {
                result = $"{str}s";
            }
        }

        return result;
    }

    /// <summary>
    /// Splits a string into a string array based
    /// on line endings. Assumes uniform line endings.
    /// </summary>
    /// <param name="str">The string to split.</param>
    /// <returns>An array of the split string.</returns>
    public static string[] SplitIntoLines(this string str)
    {
        string[] result;

        if(string.IsNullOrEmpty(str))
        {
            result = System.Array.Empty<string>();
        }
        else
        {
            if (str.Contains("\r\n"))
            {
                result = str.Split("\r\n");
            }
            else if (str.Contains('\r'))
            {
                result = str.Split('\r');
            }
            else if (str.Contains('\n'))
            {
                result = str.Split('\n');
            }
            else
            {
                result = new string[1] { str };
            }
        }

        if((result.Length > 1) && (result[^1] == string.Empty))
        {
            result = result[..^1];
        }

        return result;
    }
}
