// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Scaffolder;

/// <summary>
/// Contract for converting from a "bid" domain specific language (DSL)
/// file into a <see cref="EntityDefinition"/> object.
/// </summary>
public interface IBidParser
{
    /// <summary>
    /// Parses file formatted with "bid" DSL into a
    /// <see cref="EntityDefinition"/> object.
    /// </summary>
    /// <param name="file">The "bid" DSL file to parse.</param>
    /// <returns>The resulting <see cref="EntityDefinition"/> object.</returns>
    EntityDefinition Parse(File file);
}
