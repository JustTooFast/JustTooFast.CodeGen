// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen.Scaffolder.Tests;
public class BidParserSpy : IBidParser
{
    public int CallsTo_Parse
    { get; set; }

    public BidEntity Parse(File file)
    {
        CallsTo_Parse++;

        BidEntity entity = new();

        if (file.Path.Contains("First"))
            entity.Name = "First";
        else if (file.Path.Contains("Second"))
            entity.Name = "Second";

        string[] lines = file.Contents.SplitIntoLines();

        if (lines.Length > 0)
        {
            if (lines[0] != "--")
            {
                entity.Attributes.Add(lines[0]);
            }
        }

        return entity;
    }
}
