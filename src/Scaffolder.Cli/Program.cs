// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;

namespace JustTooFast.CodeGen.Scaffolder.Cli;
internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Missing arguments. Please include inputFolder outputFolder targetNamespace arguments.");
        }
        else
        {
            Console.Write("Generating file(s)...");

            IFileHelper fileHelper = new FileHelper();
            IBidParser bidParser = new BidParser();

            string inputFolder = args[0];
            string outputFolder = args[1];
            string targetNamespace = args[2];

            Directory.CreateDirectory(outputFolder);

            IBidWriter bidWriter = new BidWriter(fileHelper, bidParser);
            int result = bidWriter.Write(inputFolder, outputFolder, targetNamespace);

            Console.WriteLine($"{result} file(s) generated.");
        }
    }
}