// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System.Collections.Generic;

namespace JustTooFast.CodeGen.Scaffolder;

/// <summary>
/// Holds the data points used by <see cref="BuilderGenerator"/>,
/// <see cref="InfoGenerator"/>, and <see cref="DeclarationGenerator"/>
/// to generate Builder, Info, and Declaration classes.
/// </summary>
public class BidEntity
{
    private readonly List<string> m_Attributes = new();
    private readonly List<string> m_Entities = new();
    private readonly List<string> m_AttributeSets = new();
    private readonly List<string> m_EntitySets = new();

    /// <summary>
    /// The root name of the Builder, Info, Declaration classes.
    /// </summary>
    public string Name
    { get; set;}
    
    /// <summary>
    /// Each attribute is a single data point that can be
    /// used to generate code.
    /// </summary>
    public List<string> Attributes
    {
        get { return m_Attributes; }
    }

    /// <summary>
    /// Each entity is a reference to a single child component
    /// that can generate its own code.
    /// </summary>
    public List<string> Entities
    {
        get { return m_Entities; }
    }

    /// <summary>
    /// Each attribute set is a collection of related data
    /// points that can be used to generate code.
    /// </summary>
    public List<string> AttributeSets
    {
        get { return m_AttributeSets; }
    }

    /// <summary>
    /// Each entity set is a collection of related child
    /// components that can generate their own code.
    /// </summary>
    public List<string> EntitySets
    {
        get { return m_EntitySets; }
    }
}
