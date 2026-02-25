// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Text;

namespace JustTooFast.CodeGen.Scaffolder;

/// <summary>
/// Generates a Builder class which is responsible for populating 
/// data points into an Info object using a method chaining syntax.
/// <seealso cref="InfoGenerator"/>
/// </summary>
public class BuilderGenerator : IGenerator
{
    private readonly BidEntity m_Entity;
    private readonly string m_TargetNamespace;

    public BuilderGenerator(BidEntity entity, string targetNamespace)
    {
        m_Entity = entity ?? throw new ArgumentNullException(nameof(entity));

        if(string.IsNullOrWhiteSpace(targetNamespace))
            throw new ArgumentNullException(nameof(targetNamespace));

        m_TargetNamespace = targetNamespace;
    }

    /// <summary>
    /// Generates a Builder class based on the <see cref="BidEntity"/>
    /// and targetNamespace.
    /// </summary>
    /// <returns>Generated Builder class.</returns>
    public string Generate()
    {
        StringBuilder sb = new();

        //Add usings
        sb.AppendLineFeed("using System;")
            .AppendLineFeed("using System.Collections.Generic;");

        sb.AppendLineFeed();

        //Add namespace and class
        sb.AppendLineFeed($"namespace {m_TargetNamespace};")
            .AppendLineFeed($"public partial class {m_Entity.Name}Builder")
            .AppendLineFeed("{");

        //Add info field
        sb.AppendLineFeed($"    private readonly {m_Entity.Name}Info m_{m_Entity.Name} = new();")
            .AppendLineFeed();

        //Add implicit operators
        sb.AppendLineFeed($"    public static implicit operator {m_Entity.Name}Info({m_Entity.Name}Builder builder)")
            .AppendLineFeed("    {")
            .AppendLineFeed($"        return builder.m_{m_Entity.Name};")
            .AppendLineFeed("    }");

        sb.AppendLineFeed();

        sb.AppendLineFeed($"    public static implicit operator {m_Entity.Name}Declaration({m_Entity.Name}Builder builder)")
            .AppendLineFeed("    {")
            .AppendLineFeed($"        return new {m_Entity.Name}Declaration(builder.m_{m_Entity.Name});")
            .AppendLineFeed("    }");

        //Add attributes
        foreach (string attribute in m_Entity.Attributes)
        {
            sb.AppendLineFeed();

            string camelCaseAttribute = attribute.ToLowerFirstLetter();

            sb.AppendLineFeed($"    public {m_Entity.Name}Builder With{attribute}(string {camelCaseAttribute})")
                .AppendLineFeed("    {")
                .AppendLineFeed($"        if (string.IsNullOrWhiteSpace({camelCaseAttribute}))")
                .AppendLineFeed($"            throw new ArgumentNullException(nameof({camelCaseAttribute}));")
                .AppendLineFeed()
                .AppendLineFeed($"        m_{m_Entity.Name}.{attribute} = {camelCaseAttribute};")
                .AppendLineFeed()
                .AppendLineFeed("        return this;")
                .AppendLineFeed("    }");
        }

        //Add entities
        foreach (string entity in m_Entity.Entities)
        {
            sb.AppendLineFeed();

            string camelCaseEntity = entity.ToLowerFirstLetter();

            sb.AppendLineFeed($"    public {m_Entity.Name}Builder With{entity}({entity}Info {camelCaseEntity})")
                .AppendLineFeed("    {")
                .AppendLineFeed($"        m_{m_Entity.Name}.{entity} = {camelCaseEntity} ?? throw new ArgumentNullException(nameof({camelCaseEntity}));")
                .AppendLineFeed()
                .AppendLineFeed("        return this;")
                .AppendLineFeed("    }");
            
            sb.AppendLineFeed();

            sb.AppendLineFeed($"    public {m_Entity.Name}Builder With{entity}(Func<{entity}Builder, {entity}Builder> builderFunc)")
                .AppendLineFeed("    {")
                .AppendLineFeed($"        {entity}Builder builder = new();")
                .AppendLineFeed()
                .AppendLineFeed($"        m_{m_Entity.Name}.{entity} = builderFunc(builder);")
                .AppendLineFeed()
                .AppendLineFeed($"        return this;")
                .AppendLineFeed("    }");
        }

        //Add attribute sets
        foreach (string attributeSet in m_Entity.AttributeSets)
        {
            sb.AppendLineFeed();

            string camelCaseAttributeSet = attributeSet.ToLowerFirstLetter();

            sb.AppendLineFeed($"    public {m_Entity.Name}Builder With{attributeSet}(string {camelCaseAttributeSet})")
                .AppendLineFeed("    {")
                .AppendLineFeed($"        if (string.IsNullOrWhiteSpace({camelCaseAttributeSet}))")
                .AppendLineFeed($"            throw new ArgumentNullException(nameof({camelCaseAttributeSet}));")
                .AppendLineFeed()
                .AppendLineFeed($"        m_{m_Entity.Name}.{attributeSet.ToPlural()}.Add({camelCaseAttributeSet});")
                .AppendLineFeed()
                .AppendLineFeed("        return this;")
                .AppendLineFeed("    }");

            sb.AppendLineFeed();

            sb.AppendLineFeed($"    public {m_Entity.Name}Builder With{attributeSet.ToPlural()}(string[] {camelCaseAttributeSet.ToPlural()})")
                .AppendLineFeed("    {")
                .AppendLineFeed($"        if ({camelCaseAttributeSet.ToPlural()} == null)")
                .AppendLineFeed($"            throw new ArgumentNullException(nameof({camelCaseAttributeSet.ToPlural()}));")
                .AppendLineFeed()
                .AppendLineFeed($"        m_{m_Entity.Name}.{attributeSet.ToPlural()}.AddRange({camelCaseAttributeSet.ToPlural()});")
                .AppendLineFeed()
                .AppendLineFeed("        return this;")
                .AppendLineFeed("    }");
        }

        //Add entity sets
        foreach (string entitySet in m_Entity.EntitySets)
        {
            sb.AppendLineFeed();

            string camelCaseEntitySet = entitySet.ToLowerFirstLetter();

            sb.AppendLineFeed($"    public {m_Entity.Name}Builder With{entitySet}({entitySet}Info {camelCaseEntitySet})")
                .AppendLineFeed("    {")
                .AppendLineFeed($"        if ({camelCaseEntitySet} == null)")
                .AppendLineFeed($"            throw new ArgumentNullException(nameof({camelCaseEntitySet}));")
                .AppendLineFeed()
                .AppendLineFeed($"        m_{m_Entity.Name}.{entitySet.ToPlural()}.Add({camelCaseEntitySet});")
                .AppendLineFeed()
                .AppendLineFeed("        return this;")
                .AppendLineFeed("    }");

            sb.AppendLineFeed();

            sb.AppendLineFeed($"    public {m_Entity.Name}Builder With{entitySet}(Func<{entitySet}Builder, {entitySet}Builder> builderFunc)")
                .AppendLineFeed("    {")
                .AppendLineFeed($"        {entitySet}Builder builder = new();")
                .AppendLineFeed()
                .AppendLineFeed($"        m_{m_Entity.Name}.{entitySet.ToPlural()}.Add(builderFunc(builder));")
                .AppendLineFeed()
                .AppendLineFeed($"        return this;")
                .AppendLineFeed("    }");

            sb.AppendLineFeed();

            sb.AppendLineFeed($"    public {m_Entity.Name}Builder With{entitySet.ToPlural()}({entitySet}Info[] {camelCaseEntitySet.ToPlural()})")
                .AppendLineFeed("    {")
                .AppendLineFeed($"        if ({camelCaseEntitySet.ToPlural()} == null)")
                .AppendLineFeed($"            throw new ArgumentNullException(nameof({camelCaseEntitySet.ToPlural()}));")
                .AppendLineFeed()
                .AppendLineFeed($"        m_{m_Entity.Name}.{entitySet.ToPlural()}.AddRange({camelCaseEntitySet.ToPlural()});")
                .AppendLineFeed()
                .AppendLineFeed("        return this;")
                .AppendLineFeed("    }");
        }

        //Close class
        sb.AppendLineFeed("}");

        string result = sb.ToString();

        return result;
    }
}
