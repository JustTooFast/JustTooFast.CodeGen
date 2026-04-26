// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Text;

namespace JustTooFast.CodeGen.Scaffolder;

/// <summary>
/// Generates an Model class which is responsible for holding the
/// data points that will be used by an Emitter class to 
/// generate code.
/// <seealso cref="EmitterSourceEmitter"/>
/// </summary>
public class ModelSourceEmitter : IGenerator, IEmitter
{
    private readonly EntityDefinition m_Entity;
    private readonly string m_TargetNamespace;

    public ModelSourceEmitter(EntityDefinition entity, string targetNamespace)
    {
        m_Entity = entity ?? throw new ArgumentNullException(nameof(entity));
        
        if(string.IsNullOrWhiteSpace(targetNamespace))
            throw new ArgumentNullException(nameof(targetNamespace));

        m_TargetNamespace = targetNamespace;
    }

    /// <summary>
    /// Generates a Model class based on the <see cref="EntityDefinition"/>
    /// and targetNamespace.
    /// </summary>
    /// <returns>Generated Model class.</returns>
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
        a.AppendLine("using System.Collections.Generic;");

        a.BlankLine();

        //Add namespace and class
        a.Append("namespace ");
        a.Append(m_TargetNamespace);
        a.AppendLine(';');
        
        a.BlankLine();

        a.Append("public partial class ");
        a.Append(m_Entity.Name);
        a.AppendLine("Model");
        a.AppendLine('{');

        using (a.IndentScope())
        {
            bool isFirstElement = true;

            //Add fields for each attribute set
            foreach (string attributeSet in m_Entity.AttributeSets)
            {
                if (isFirstElement)
                    isFirstElement = false;

                a.Append("private readonly List<string> m_");
                a.Append(attributeSet.ToPlural());
                a.AppendLine(" = new();");
            }

            //Add fields for each entity set
            foreach (string entitySet in m_Entity.EntitySets)
            {
                if (isFirstElement)
                    isFirstElement = false;

                a.Append("private readonly List<");
                a.Append(entitySet);
                a.Append("Model> m_");
                a.Append(entitySet.ToPlural());
                a.AppendLine(" = new();");
            }

            //Add properties for each attribute
            foreach (string attribute in m_Entity.Attributes)
            {
                if (!isFirstElement)
                    a.BlankLine();
                else
                    isFirstElement = false;

                a.Append("public string ");
                a.AppendLine(attribute);
                a.AppendLine("{ get; set; }");
            }

            //Add properties for each entity
            foreach (string entity in m_Entity.Entities)
            {
                if (!isFirstElement)
                    a.BlankLine();
                else
                    isFirstElement = false;

                a.Append("public ");
                a.Append(entity);
                a.Append("Model ");
                a.AppendLine(entity);
                a.AppendLine("{ get; set; }");
            }

            //Add properties for each attribute set
            foreach (string attributeSet in m_Entity.AttributeSets)
            {
                if (!isFirstElement)
                    a.BlankLine();
                else
                    isFirstElement = false;

                string pluralAttributeSet = attributeSet.ToPlural();
                a.Append("public List<string> ");
                a.AppendLine(pluralAttributeSet);
                a.AppendLine("{");
                using (a.IndentScope())
                {
                    a.Append("get { return m_");
                    a.Append(pluralAttributeSet);
                    a.AppendLine("; }");
                }
                a.AppendLine("}");
            }

            //Add properties for each entity set
            foreach (string entitySet in m_Entity.EntitySets)
            {
                if (!isFirstElement)
                    a.BlankLine();
                else
                    isFirstElement = false;

                string pluralEntitySet = entitySet.ToPlural();
                a.Append("public List<");
                a.Append(entitySet);
                a.Append("Model> ");
                a.AppendLine(pluralEntitySet);
                a.AppendLine("{");
                using (a.IndentScope())
                {
                    a.Append("get { return m_");
                    a.Append(pluralEntitySet);
                    a.AppendLine("; }");
                }
                a.AppendLine("}");
            }
        }

        //Close class
        a.AppendLine('}');
    }
}
