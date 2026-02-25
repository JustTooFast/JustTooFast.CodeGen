// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Scaffolder;

/// <summary>
/// Holds the contents of a file.
/// </summary>
public class File
{
    /// <summary>
    /// The full path to the file.
    /// </summary>
    public string Path
    { get; set; }

    /// <summary>
    /// The contents of the file.
    /// </summary>
    public string Contents
    { get; set; }
}
