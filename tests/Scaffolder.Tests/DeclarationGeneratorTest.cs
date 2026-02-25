// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Scaffolder.Tests;

[TestClass]
public class DeclarationGeneratorTest
{
    [TestMethod]
    public void Generate_WithBasicStructure_ReturnStructure()
    {
        //Arrange
        BidEntity entity = new() { Name = "Test" };
        string targetNamespace = "MyNamespace";

        string expected =
@"using System;

namespace MyNamespace;
public partial class TestDeclaration
{
    private readonly TestInfo m_Test;

    public TestDeclaration(TestInfo test)
    {
        m_Test = test ?? throw new ArgumentNullException(nameof(test));

        Validate();
    }

    private partial void Validate();
}
";

        //Act
        IGenerator target = new DeclarationGenerator(entity, targetNamespace);
        string actual = target.Generate();

        //Assert
        Assert.AreEqual(expected, actual);
    }
}
