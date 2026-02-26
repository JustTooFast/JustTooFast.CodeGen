// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Text;

namespace JustTooFast.CodeGen.Scaffolder;

/// <summary>
/// Generates an Model class which is responsible for holding the
/// data points that will be used by an Emitter class to 
/// generate code.
/// <seealso cref="EmitterGenerator"/>
/// </summary>
public class ModelGenerator : IGenerator
{
    private readonly BidEntity m_Entity;
    private readonly string m_TargetNamespace;

    public ModelGenerator(BidEntity entity, string targetNamespace)
    {
        m_Entity = entity ?? throw new ArgumentNullException(nameof(entity));
        
        if(string.IsNullOrWhiteSpace(targetNamespace))
            throw new ArgumentNullException(nameof(targetNamespace));

        m_TargetNamespace = targetNamespace;
    }

    /// <summary>
    /// Generates a Model class based on the <see cref="BidEntity"/>
    /// and targetNamespace.
    /// </summary>
    /// <returns>Generated Model class.</returns>
    public string Generate()
    {
        StringBuilder sb = new();

        //Add usings
        sb.AppendLineFeed("using System.Collections.Generic;");

        sb.AppendLineFeed();

        //Add namespace and class
        sb.AppendLineFeed($"namespace {m_TargetNamespace};")
            .AppendLineFeed($"public partial class {m_Entity.Name}Model")
            .AppendLineFeed("{");

       bool isFirstElement = true;

        //Add fields for each attribute set
        foreach (string attributeSet in m_Entity.AttributeSets)
        {
            if (isFirstElement)
            {
                isFirstElement = false;
            }

            sb.AppendLineFeed($"    private readonly List<string> m_{attributeSet.ToPlural()} = new();");
        }

        //Add fields for each entity set
        foreach (string entitySet in m_Entity.EntitySets)
        {
            if (isFirstElement)
            {
                isFirstElement = false;
            }

            sb.AppendLineFeed($"    private readonly List<{entitySet}Model> m_{entitySet.ToPlural()} = new();");
        }

        //Add properties for each attribute
        foreach (string attribute in m_Entity.Attributes)
        {
            if (!isFirstElement)
            {
                sb.AppendLineFeed();
            }
            else
            {
                isFirstElement = false;
            }

            sb.AppendLineFeed($"    public string {attribute}")
                .AppendLineFeed("    { get; set; }");
        }

        //Add properties for each entity
        foreach (string entity in m_Entity.Entities)
        {
            if (!isFirstElement)
            {
                sb.AppendLineFeed();
            }
            else
            {
                isFirstElement = false;
            }

            sb.AppendLineFeed($"    public {entity}Model {entity}")
                .AppendLineFeed("    { get; set; }");
        }

        //Add properties for each attribute set
        foreach (string attributeSet in m_Entity.AttributeSets)
        {
            if (!isFirstElement)
            {
                sb.AppendLineFeed();
            }
            else
            {
                isFirstElement = false;
            }

            string pluralAttributeSet = attributeSet.ToPlural();
            sb.AppendLineFeed($"    public List<string> {pluralAttributeSet}")
                .AppendLineFeed("    {")
                .AppendLineFeed($"        get {{ return m_{pluralAttributeSet}; }}")
                .AppendLineFeed("    }");
        }

        //Add properties for each entity set
        foreach (string entitySet in m_Entity.EntitySets)
        {
            if (!isFirstElement)
            {
                sb.AppendLineFeed();
            }
            else
            {
                isFirstElement = false;
            }

            string pluralEntitySet = entitySet.ToPlural();
            sb.AppendLineFeed($"    public List<{entitySet}Model> {pluralEntitySet}")
                .AppendLineFeed("    {")
                .AppendLineFeed($"        get {{ return m_{pluralEntitySet}; }}")
                .AppendLineFeed("    }");
        }

        //Close class
        sb.AppendLineFeed("}");

        string result = sb.ToString();

        return result;
    }
}
