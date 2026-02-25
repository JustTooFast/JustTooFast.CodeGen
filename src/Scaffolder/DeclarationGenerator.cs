// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Text;

namespace JustTooFast.CodeGen.Scaffolder;

/// <summary>
/// Generates a Declaration class which is responsible for
/// generating code based on inputs from an Info object.
/// <seealso cref="InfoGenerator"/>
/// </summary>
public class DeclarationGenerator : IGenerator
{
    private readonly BidEntity m_Entity;
    private readonly string m_TargetNamespace;

    public DeclarationGenerator(BidEntity entity, string targetNamespace)
    {
        m_Entity = entity ?? throw new ArgumentNullException(nameof(entity));

        if(string.IsNullOrWhiteSpace(targetNamespace))
            throw new ArgumentNullException(nameof(targetNamespace));

        m_TargetNamespace = targetNamespace;
    }

    /// <summary>
    /// Generates a Declaration class based on the <see cref="BidEntity"/>
    /// and targetNamespace.
    /// </summary>
    /// <returns>Generated Declaration class.</returns>
    public string Generate()
    {
        StringBuilder sb = new();

        //Add usings
        sb.AppendLineFeed("using System;");

        sb.AppendLineFeed();

        //Add namespace and class
        sb.AppendLineFeed($"namespace {m_TargetNamespace};")
            .AppendLineFeed($"public partial class {m_Entity.Name}Declaration")
            .AppendLineFeed("{");

        //Add field for info
        sb.AppendLineFeed($"    private readonly {m_Entity.Name}Info m_{m_Entity.Name};");

        sb.AppendLineFeed();

        //Add constructor
        string camelCaseName = m_Entity.Name.ToLowerFirstLetter();
        sb.AppendLineFeed($"    public {m_Entity.Name}Declaration({m_Entity.Name}Info {camelCaseName})")
            .AppendLineFeed("    {")
            .AppendLineFeed($"        m_{m_Entity.Name} = {camelCaseName} ?? throw new ArgumentNullException(nameof({camelCaseName}));")
            .AppendLineFeed()
            .AppendLineFeed("        Validate();")
            .AppendLineFeed("    }");

        sb.AppendLineFeed();

        //Add validate method stub
        sb.AppendLineFeed("    private partial void Validate();");

        //Close class
        sb.AppendLineFeed("}");

        string result = sb.ToString();

        return result;
    }
}
