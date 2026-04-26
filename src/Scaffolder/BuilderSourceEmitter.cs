// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Text;

namespace JustTooFast.CodeGen.Scaffolder;

/// <summary>
/// Generates a Builder class which is responsible for populating 
/// data points into a Model object using a method chaining syntax.
/// <seealso cref="ModelSourceEmitter"/>
/// </summary>
public class BuilderSourceEmitter : IGenerator, IEmitter
{
    private readonly EntityDefinition m_Entity;
    private readonly string m_TargetNamespace;

    public BuilderSourceEmitter(EntityDefinition entity, string targetNamespace)
    {
        m_Entity = entity ?? throw new ArgumentNullException(nameof(entity));

        if(string.IsNullOrWhiteSpace(targetNamespace))
            throw new ArgumentNullException(nameof(targetNamespace));

        m_TargetNamespace = targetNamespace;
    }

    /// <summary>
    /// Generates a Builder class based on the <see cref="EntityDefinition"/>
    /// and targetNamespace.
    /// </summary>
    /// <returns>Generated Builder class.</returns>
    public string Generate()
    {
        IFormatting fmt = new Formatting(indentUnit: "    ", newLine: "\n");
        IAppender appender = new StringBuilderAppender(formatting: fmt);
        EmitTo(appender);
        return appender.ToString() ?? string.Empty;
    }

    public void EmitTo(IAppender appender)
    {
        var a = appender.EnsureIndenting();

        //Add usings
        a.AppendLine("using System;");
        a.AppendLine("using System.Collections.Generic;");

        a.BlankLine();

        //Add namespace and class
        a.Append("namespace ");
        a.Append(m_TargetNamespace);
        a.AppendLine(';');

        a.BlankLine();

        a.Append("public partial class ");
        a.Append(m_Entity.Name);
        a.AppendLine("Builder");
        a.AppendLine('{');

        using(a.IndentScope())
        {
            //Add model field
            a.AppendLine($"private readonly {m_Entity.Name}Model m_{m_Entity.Name} = new();");
            a.BlankLine();

            //Add implicit operators
            a.AppendLine($"public static implicit operator {m_Entity.Name}Model({m_Entity.Name}Builder builder)");
            a.AppendLine('{');

            using(a.IndentScope())
            {
                a.AppendLine($"return builder.m_{m_Entity.Name};");
            }

            a.AppendLine('}');

            a.BlankLine();

            a.AppendLine($"public static implicit operator {m_Entity.Name}Emitter({m_Entity.Name}Builder builder)");
            a.AppendLine('{');

            using(a.IndentScope())
            {
                a.AppendLine($"return new {m_Entity.Name}Emitter(builder.m_{m_Entity.Name});");
            }

            a.AppendLine('}');

            //Add attributes
            foreach (string attribute in m_Entity.Attributes)
            {
                a.AppendLine();

                string camelCaseAttribute = attribute.ToLowerFirstLetter();

                a.AppendLine($"public {m_Entity.Name}Builder With{attribute}(string {camelCaseAttribute})");
                a.AppendLine('{');

                using(a.IndentScope())
                {
                    a.AppendLine($"if (string.IsNullOrWhiteSpace({camelCaseAttribute}))");

                    a.Indent();
                    a.AppendLine($"throw new ArgumentNullException(nameof({camelCaseAttribute}));");
                    a.Outdent();

                    a.BlankLine();
                    a.AppendLine($"m_{m_Entity.Name}.{attribute} = {camelCaseAttribute};");
                    a.BlankLine();
                    a.AppendLine("return this;");
                }

                a.AppendLine('}');
            }

            //Add entities
            foreach (string entity in m_Entity.Entities)
            {
                a.AppendLine();

                string camelCaseEntity = entity.ToLowerFirstLetter();

                a.AppendLine($"public {m_Entity.Name}Builder With{entity}({entity}Model {camelCaseEntity})");
                a.AppendLine('{');

                using(a.IndentScope())
                {
                    a.AppendLine($"m_{m_Entity.Name}.{entity} = {camelCaseEntity} ?? throw new ArgumentNullException(nameof({camelCaseEntity}));");
                    a.BlankLine();
                    a.AppendLine("return this;");
                }

                a.AppendLine('}');
                
                a.AppendLine();

                a.AppendLine($"public {m_Entity.Name}Builder With{entity}(Func<{entity}Builder, {entity}Builder> builderFunc)");
                a.AppendLine('{');

                using(a.IndentScope())
                {
                    a.AppendLine($"{entity}Builder builder = new();");
                    a.BlankLine();
                    a.AppendLine($"m_{m_Entity.Name}.{entity} = builderFunc(builder);");
                    a.BlankLine();
                    a.AppendLine($"return this;");
                }

                a.AppendLine('}');
            }

            //Add attribute sets
            foreach (string attributeSet in m_Entity.AttributeSets)
            {
                a.AppendLine();

                string camelCaseAttributeSet = attributeSet.ToLowerFirstLetter();

                a.AppendLine($"public {m_Entity.Name}Builder With{attributeSet}(string {camelCaseAttributeSet})");
                a.AppendLine('{');

                using(a.IndentScope())
                {
                    a.AppendLine($"if (string.IsNullOrWhiteSpace({camelCaseAttributeSet}))");

                    a.Indent();
                    a.AppendLine($"throw new ArgumentNullException(nameof({camelCaseAttributeSet}));");
                    a.Outdent();

                    a.BlankLine();
                    a.AppendLine($"m_{m_Entity.Name}.{attributeSet.ToPlural()}.Add({camelCaseAttributeSet});");
                    a.BlankLine();
                    a.AppendLine("return this;");
                }
                
                a.AppendLine('}');

                a.AppendLine();

                a.AppendLine($"public {m_Entity.Name}Builder With{attributeSet.ToPlural()}(string[] {camelCaseAttributeSet.ToPlural()})");
                a.AppendLine('{');

                using(a.IndentScope())
                {
                    a.AppendLine($"if ({camelCaseAttributeSet.ToPlural()} == null)");

                    a.Indent();
                    a.AppendLine($"throw new ArgumentNullException(nameof({camelCaseAttributeSet.ToPlural()}));");
                    a.Outdent();
                    
                    a.BlankLine();
                    a.AppendLine($"m_{m_Entity.Name}.{attributeSet.ToPlural()}.AddRange({camelCaseAttributeSet.ToPlural()});");
                    a.BlankLine();
                    a.AppendLine("return this;");
                }

                a.AppendLine('}');
            }

            //Add entity sets
            foreach (string entitySet in m_Entity.EntitySets)
            {
                a.AppendLine();

                string camelCaseEntitySet = entitySet.ToLowerFirstLetter();

                a.AppendLine($"public {m_Entity.Name}Builder With{entitySet}({entitySet}Model {camelCaseEntitySet})");
                a.AppendLine('{');

                using(a.IndentScope())
                {
                    a.AppendLine($"if ({camelCaseEntitySet} == null)");
                    
                    a.Indent();
                    a.AppendLine($"throw new ArgumentNullException(nameof({camelCaseEntitySet}));");
                    a.Outdent();

                    a.BlankLine();
                    a.AppendLine($"m_{m_Entity.Name}.{entitySet.ToPlural()}.Add({camelCaseEntitySet});");
                    a.BlankLine();
                    a.AppendLine("return this;");
                }

                a.AppendLine('}');

                a.AppendLine();

                a.AppendLine($"public {m_Entity.Name}Builder With{entitySet}(Func<{entitySet}Builder, {entitySet}Builder> builderFunc)");
                a.AppendLine('{');

                using(a.IndentScope())
                {
                    a.AppendLine($"{entitySet}Builder builder = new();");
                    a.BlankLine();
                    a.AppendLine($"m_{m_Entity.Name}.{entitySet.ToPlural()}.Add(builderFunc(builder));");
                    a.BlankLine();
                    a.AppendLine($"return this;");
                }

                a.AppendLine('}');

                a.BlankLine();

                a.AppendLine($"public {m_Entity.Name}Builder With{entitySet.ToPlural()}({entitySet}Model[] {camelCaseEntitySet.ToPlural()})");
                a.AppendLine('{');

                using(a.IndentScope())
                {
                    a.AppendLine($"if ({camelCaseEntitySet.ToPlural()} == null)");

                    a.Indent();
                    a.AppendLine($"throw new ArgumentNullException(nameof({camelCaseEntitySet.ToPlural()}));");
                    a.Outdent();

                    a.BlankLine();
                    a.AppendLine($"m_{m_Entity.Name}.{entitySet.ToPlural()}.AddRange({camelCaseEntitySet.ToPlural()});");
                    a.BlankLine();
                    a.AppendLine("return this;");
                }

                a.AppendLine('}');
            }
        }

        //Close class
        a.AppendLine('}');
    }
}
