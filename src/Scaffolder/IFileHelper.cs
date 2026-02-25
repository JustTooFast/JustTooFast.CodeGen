// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Scaffolder;

/// <summary>
/// Contract for file handling routines.
/// </summary>
public interface IFileHelper
{
    /// <summary>
    /// Gets a list of files (with full path)
    /// from a given folder.
    /// </summary>
    /// <param name="folderPath">The folder to get a list of files from.</param>
    /// <returns>The list of files (with full path).</returns>
    string[] GetFilesInFolder(string folderPath);

    /// <summary>
    /// Reads a given file into a <see cref="File"/> object.
    /// </summary>
    /// <param name="filePath">The full path to the file.</param>
    /// <returns>The resulting <see cref="File"/> object.</returns>
    File Read(string filePath);

    /// <summary>
    /// Writes specified file contents to a given file.
    /// </summary>
    /// <param name="filePath">The full path to the file.</param>
    /// <param name="fileContents">The file contents to write.</param>
    void Write(string filePath, string fileContents);
}
