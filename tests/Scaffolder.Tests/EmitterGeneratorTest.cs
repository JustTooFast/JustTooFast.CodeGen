// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Scaffolder.Tests;

[TestClass]
public class EmitterGeneratorTest
{
    [TestMethod]
    public void Generate_WithBasicStructure_ReturnStructure()
    {
        //Arrange
        BidEntity entity = new() { Name = "Test" };
        string targetNamespace = "MyNamespace";

        string expected =
@"using System;
using JustTooFast.CodeGen;

namespace MyNamespace;
public partial class TestEmitter : IEmitter
{
    private readonly TestModel m_Test;

    public TestEmitter(TestModel test)
    {
        m_Test = test ?? throw new ArgumentNullException(nameof(test));

        Validate();
    }

    public partial void EmitTo(IAppender appender);

    private partial void Validate();
}
";

        //Act
        IGenerator target = new EmitterGenerator(entity, targetNamespace);
        string actual = target.Generate();

        //Assert
        Assert.AreEqual(expected, actual);
    }
}
