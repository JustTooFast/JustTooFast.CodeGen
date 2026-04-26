// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Scaffolder.Tests;

[TestClass]
public class EmitterSourceEmitterTest
{
    [TestMethod]
    public void EmitTo_WithBasicStructure_ReturnStructure()
    {
        //Arrange
        var entity = new EntityDefinition() { Name = "Test" };
        var targetNamespace = "MyNamespace";

        var expected =
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
        var fmt = new Formatting(indentUnit: "    ", newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new EmitterSourceEmitter(entity, targetNamespace);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }
}
