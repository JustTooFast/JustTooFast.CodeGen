// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Scaffolder.Tests;

[TestClass]
public class InfoGeneratorTest
{
    [TestMethod]
    public void Generate_With3Attributes_ReturnAttributes()
    {
        //Arrange
        BidEntity entity = new() { Name = "Test" };
        entity.Attributes.AddRange(new string[] {"Item1", "Item2", "Item3"});
        string targetNamespace = "MyNamespace";

        string expected =
@"using System.Collections.Generic;

namespace MyNamespace;
public partial class TestInfo
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
        IGenerator target = new InfoGenerator(entity, targetNamespace);
        string actual = target.Generate();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Generate_With3Entities_ReturnEntities()
    {
        //Arrange
        BidEntity entity = new() { Name = "Test" };
        entity.Entities.AddRange(new string[] {"Item1", "Item2", "Item3"});
        string targetNamespace = "MyNamespace";

        string expected =
@"using System.Collections.Generic;

namespace MyNamespace;
public partial class TestInfo
{
    public Item1Info Item1
    { get; set; }

    public Item2Info Item2
    { get; set; }

    public Item3Info Item3
    { get; set; }
}
";

        //Act
        IGenerator target = new InfoGenerator(entity, targetNamespace);
        string actual = target.Generate();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Generate_With3AttributeSets_ReturnAttributeSets()
    {
        //Arrange
        BidEntity entity = new() { Name = "Test" };
        entity.AttributeSets.AddRange(new string[] {"Item1", "Item2", "Item3"});
        string targetNamespace = "MyNamespace";

        string expected =
@"using System.Collections.Generic;

namespace MyNamespace;
public partial class TestInfo
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
        IGenerator target = new InfoGenerator(entity, targetNamespace);
        string actual = target.Generate();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Generate_With3EntitySets_ReturnEntitySets()
    {
        //Arrange
        BidEntity entity = new() { Name = "Test" };
        entity.EntitySets.AddRange(new string[] {"Item1", "Item2", "Item3"});
        string targetNamespace = "MyNamespace";

        string expected =
@"using System.Collections.Generic;

namespace MyNamespace;
public partial class TestInfo
{
    private readonly List<Item1Info> m_Item1s = new();
    private readonly List<Item2Info> m_Item2s = new();
    private readonly List<Item3Info> m_Item3s = new();

    public List<Item1Info> Item1s
    {
        get { return m_Item1s; }
    }

    public List<Item2Info> Item2s
    {
        get { return m_Item2s; }
    }

    public List<Item3Info> Item3s
    {
        get { return m_Item3s; }
    }
}
";

        //Act
        IGenerator target = new InfoGenerator(entity, targetNamespace);
        string actual = target.Generate();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Generate_WithOneOfEach_ReturnAll()
    {
        //Arrange
        BidEntity entity = new() { Name = "Test2" };
        entity.Attributes.Add("Item1");
        entity.Entities.Add("Item2");
        entity.AttributeSets.Add("Item3");
        entity.EntitySets.Add("Item4");
        string targetNamespace = "MyNamespace";

        string expected =
@"using System.Collections.Generic;

namespace MyNamespace;
public partial class Test2Info
{
    private readonly List<string> m_Item3s = new();
    private readonly List<Item4Info> m_Item4s = new();

    public string Item1
    { get; set; }

    public Item2Info Item2
    { get; set; }

    public List<string> Item3s
    {
        get { return m_Item3s; }
    }

    public List<Item4Info> Item4s
    {
        get { return m_Item4s; }
    }
}
";

        //Act
        IGenerator target = new InfoGenerator(entity, targetNamespace);
        string actual = target.Generate();

        //Assert
        Assert.AreEqual(expected, actual);
    }
}
