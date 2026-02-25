// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml;

/// <summary>
/// Contract for generating code.
/// </summary>
public interface IGenerator
{
    /// <summary>
    /// Generates code.
    /// </summary>
    /// <returns>Generated code.</returns>
    string Generate();
}
