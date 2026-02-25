// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JustTooFast.CodeGen.Scaffolder.Tests;
public class FileHelperSpy : IFileHelper
{
    private readonly List<string> m_ReadFilePaths = new();
    private readonly Dictionary<string, string> m_WrittenFiles = new();

    public int CallsTo_GetFileNamesInFolder
    { get; set; }

    public int CallsTo_Read
    { get; set; }

    public int CallsTo_Write
    { get; set; }

    public List<string> ReadFilePaths
    {
        get { return m_ReadFilePaths; }
    }

    public Dictionary<string, string> WrittenFiles
    {
        get { return m_WrittenFiles; }
    }

    public string[] GetFilesInFolder(string folderPath)
    {
        CallsTo_GetFileNamesInFolder++;

        List<string> result = new();

        if (folderPath.Contains("Input"))
        {
            result.AddRange(new string[]
                {
                    $"{folderPath}{Path.DirectorySeparatorChar}First.bid",
                    $"{folderPath}{Path.DirectorySeparatorChar}Second.bid"
                });
        }

        return result.ToArray();
    }
    
    public File Read(string filePath)
    {
        CallsTo_Read++;

        File result = new() { Path = filePath };

        if (filePath.Contains("First"))
        {
            result.Contents = new StringBuilder()
                .AppendLine("FirstItem")
                .AppendLine("--")
                .AppendLine("--")
                .AppendLine("--")
                .ToString();
        }
        else if (filePath.Contains("Second"))
        {
            result.Contents = new StringBuilder()
                .AppendLine("SecondItem")
                .AppendLine("--")
                .AppendLine("--")
                .AppendLine("--")
                .ToString();
        }

        ReadFilePaths.Add(filePath);

        return result;
    }

    public void Write(string filePath, string fileContents)
    {
        CallsTo_Write++;

        WrittenFiles.Add(filePath, fileContents);
    }
}
