// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Scaffolder;

/// <summary>
/// Contract for writing generated Builder, Model, and Emitter classes.
/// </summary>
public interface IBidWriter
{
    /// <summary>
    /// Converts "bid" domain specific language (DSL) input files into
    /// generated builder, model, and emitter classes and writes
    /// them to an output folder.
    /// </summary>
    /// <param name="inputFolder">The folder holding "bid" DSL files to be parsed.</param>
    /// <param name="outputFolder">The folder where generated classes are written.</param>
    /// <returns>The number of written files.</returns>
    int Write(string inputFolder, string outputFolder, string targetNamespace);
}
