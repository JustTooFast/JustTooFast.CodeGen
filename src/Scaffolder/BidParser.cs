// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;

namespace JustTooFast.CodeGen.Scaffolder;

/// <summary>
/// Converts from a "bid" domain specific language (DSL)
/// file into a <see cref="EntityDefinition"/> object.
/// </summary>
public class BidParser : IBidParser
{
    /// <summary>
    /// Parses file formatted with "bid" DSL into a
    /// <see cref="EntityDefinition"/> object.
    /// </summary>
    /// <param name="file">The "bid" DSL file to parse.</param>
    /// <returns>The resulting <see cref="EntityDefinition"/> object.</returns>
    public EntityDefinition Parse(File file)
    {
        if(file == null)
            throw new ArgumentNullException(nameof(file));
        if(string.IsNullOrEmpty(file.Path))
            throw new ArgumentException("Required argument: file.Path");

        EntityDefinition result = new();

        //Get the entity name
        //The file name represents the entity name
        string[] pathParts = file.Path.Split(Path.DirectorySeparatorChar);
        string fileName = pathParts[^1];
        string[] fileNameParts = fileName.Split('.');
        result.Name = fileNameParts[0];

        string[] lines = file.Contents.SplitIntoLines();

        int section = 0;
        foreach (string line in lines)
        {
            //The file itself is divided into four sections
            //with each section separated by a line with "--" on it.

            //if end of section, move to next section
            if (line == "--")
            {
                section++;
                continue;
            }

            //The first section has a list of attributes, separated by new lines.
            //Add attribute
            if (section == 0)
            {
                result.Attributes.Add(line);
            }

            //The second section has a list of child entities, separated by new lines.
            //Add child entity
            if (section == 1)
            {
                result.Entities.Add(line);
            }

            //The third section has a list of attribute sets, separated by new lines.
            //Add attribute set
            if (section == 2)
            {
                result.AttributeSets.Add(line);
            }

            //The forth section has a list of child entity sets, separated by new lines.
            //Add child entity set
            if (section == 3)
            {
                result.EntitySets.Add(line);
            }

            //NOTE: For each child entity and entity set, there should be another file
            //(with matching name) providing details.
        }

        return result;
    }
}
