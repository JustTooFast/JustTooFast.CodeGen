// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Text;

namespace JustTooFast.CodeGen.Scaffolder;

/// <summary>
/// Generates an Emitter class which is responsible for
/// generating code based on inputs from a Model object.
/// <seealso cref="ModelSourceEmitter"/>
/// </summary>
public class EmitterSourceEmitter : IGenerator, IEmitter
{
    private readonly EntityDefinition m_Entity;
    private readonly string m_TargetNamespace;

    public EmitterSourceEmitter(EntityDefinition entity, string targetNamespace)
    {
        m_Entity = entity ?? throw new ArgumentNullException(nameof(entity));

        if(string.IsNullOrWhiteSpace(targetNamespace))
            throw new ArgumentNullException(nameof(targetNamespace));

        m_TargetNamespace = targetNamespace;
    }

    /// <summary>
    /// Generates an Emitter class based on the <see cref="EntityDefinition"/>
    /// and targetNamespace.
    /// </summary>
    /// <returns>Generated Emitter class.</returns>
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
        a.AppendLine("using JustTooFast.CodeGen;");

        a.BlankLine();

        //Add namespace and class
        a.Append("namespace ");
        a.Append(m_TargetNamespace);
        a.AppendLine(';');
        a.BlankLine();
        a.Append("public partial class ");
        a.Append(m_Entity.Name);
        a.AppendLine("Emitter : IEmitter");
        a.AppendLine('{');

        using(a.IndentScope())
        {
            //Add field for model
            a.Append("private readonly ");
            a.Append(m_Entity.Name);
            a.Append("Model m_");
            a.Append(m_Entity.Name);
            a.AppendLine(';');

            a.BlankLine();

            //Add constructor
            string camelCaseName = m_Entity.Name.ToLowerFirstLetter();
            a.Append("public ");
            a.Append(m_Entity.Name);
            a.Append("Emitter(");
            a.Append(m_Entity.Name);
            a.Append("Model ");
            a.Append(camelCaseName);
            a.AppendLine(')');
            a.AppendLine('{');

            using(a.IndentScope())
            {
                a.Append("m_");
                a.Append(m_Entity.Name);
                a.Append(" = ");
                a.Append(camelCaseName);
                a.Append(" ?? throw new ArgumentNullException(nameof(");
                a.Append(camelCaseName);
                a.AppendLine("));");
                a.BlankLine();
                a.AppendLine("Validate();");
            }

            a.AppendLine('}');

            a.BlankLine();

            //Add EmitTo method stub
            a.AppendLine("public partial void EmitTo(IAppender appender);");
            
            a.BlankLine();

            //Add validate method stub
            a.AppendLine("private partial void Validate();");
        }

        //Close class
        a.AppendLine('}');
    }
}
