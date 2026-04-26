// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Scaffolder.Tests;

[TestClass]
public class ModelSourceEmitterTest
{
    [TestMethod]
    public void EmitTo_With3Attributes_ReturnAttributes()
    {
        //Arrange
        var entity = new EntityDefinition() { Name = "Test" };
        entity.Attributes.AddRange(new string[] {"Item1", "Item2", "Item3"});
        var targetNamespace = "MyNamespace";

        var expected =
@"using System.Collections.Generic;

namespace MyNamespace;

public partial class TestModel
{
    public string Item1
    { get; set; }

    public string Item2
    { get; set; }

    public string Item3
    { get; set; }
}
";

        //Act
        var fmt = new Formatting(indentUnit: "    ", newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new ModelSourceEmitter(entity, targetNamespace);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_With3Entities_ReturnEntities()
    {
        //Arrange
        var entity = new EntityDefinition() { Name = "Test" };
        entity.Entities.AddRange(new string[] {"Item1", "Item2", "Item3"});
        var targetNamespace = "MyNamespace";

        var expected =
@"using System.Collections.Generic;

namespace MyNamespace;

public partial class TestModel
{
    public Item1Model Item1
    { get; set; }

    public Item2Model Item2
    { get; set; }

    public Item3Model Item3
    { get; set; }
}
";

        //Act
        var fmt = new Formatting(indentUnit: "    ", newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new ModelSourceEmitter(entity, targetNamespace);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_With3AttributeSets_ReturnAttributeSets()
    {
        //Arrange
        var entity = new EntityDefinition() { Name = "Test" };
        entity.AttributeSets.AddRange(new string[] {"Item1", "Item2", "Item3"});
        var targetNamespace = "MyNamespace";

        var expected =
@"using System.Collections.Generic;

namespace MyNamespace;

public partial class TestModel
{
    private readonly List<string> m_Item1s = new();
    private readonly List<string> m_Item2s = new();
    private readonly List<string> m_Item3s = new();

    public List<string> Item1s
    {
        get { return m_Item1s; }
    }

    public List<string> Item2s
    {
        get { return m_Item2s; }
    }

    public List<string> Item3s
    {
        get { return m_Item3s; }
    }
}
";

        //Act
        var fmt = new Formatting(indentUnit: "    ", newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new ModelSourceEmitter(entity, targetNamespace);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_With3EntitySets_ReturnEntitySets()
    {
        //Arrange
        var entity = new EntityDefinition() { Name = "Test" };
        entity.EntitySets.AddRange(new string[] {"Item1", "Item2", "Item3"});
        var targetNamespace = "MyNamespace";

        var expected =
@"using System.Collections.Generic;

namespace MyNamespace;

public partial class TestModel
{
    private readonly List<Item1Model> m_Item1s = new();
    private readonly List<Item2Model> m_Item2s = new();
    private readonly List<Item3Model> m_Item3s = new();

    public List<Item1Model> Item1s
    {
        get { return m_Item1s; }
    }

    public List<Item2Model> Item2s
    {
        get { return m_Item2s; }
    }

    public List<Item3Model> Item3s
    {
        get { return m_Item3s; }
    }
}
";

        //Act
        var fmt = new Formatting(indentUnit: "    ", newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new ModelSourceEmitter(entity, targetNamespace);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_WithOneOfEach_ReturnAll()
    {
        //Arrange
        var entity = new EntityDefinition() { Name = "Test2" };
        entity.Attributes.Add("Item1");
        entity.Entities.Add("Item2");
        entity.AttributeSets.Add("Item3");
        entity.EntitySets.Add("Item4");
        var targetNamespace = "MyNamespace";

        var expected =
@"using System.Collections.Generic;

namespace MyNamespace;

public partial class Test2Model
{
    private readonly List<string> m_Item3s = new();
    private readonly List<Item4Model> m_Item4s = new();

    public string Item1
    { get; set; }

    public Item2Model Item2
    { get; set; }

    public List<string> Item3s
    {
        get { return m_Item3s; }
    }

    public List<Item4Model> Item4s
    {
        get { return m_Item4s; }
    }
}
";

        //Act
        var fmt = new Formatting(indentUnit: "    ", newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new ModelSourceEmitter(entity, targetNamespace);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }
}
