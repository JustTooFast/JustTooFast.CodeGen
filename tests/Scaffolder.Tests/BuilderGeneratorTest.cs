// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Scaffolder.Tests;

[TestClass]
public class BuilderGeneratorTest
{
    [TestMethod]
    public void Generate_With2Attributes_ReturnAttributes()
    {
        //Arrange
        BidEntity entity = new() { Name = "Test" };
        entity.Attributes.AddRange(new string[] {"Item1", "Item2"});
        string targetNamespace = "MyNamespace";

        string expected =
@"using System;
using System.Collections.Generic;

namespace MyNamespace;
public partial class TestBuilder
{
    private readonly TestInfo m_Test = new();

    public static implicit operator TestInfo(TestBuilder builder)
    {
        return builder.m_Test;
    }

    public static implicit operator TestDeclaration(TestBuilder builder)
    {
        return new TestDeclaration(builder.m_Test);
    }

    public TestBuilder WithItem1(string item1)
    {
        if (string.IsNullOrWhiteSpace(item1))
            throw new ArgumentNullException(nameof(item1));

        m_Test.Item1 = item1;

        return this;
    }

    public TestBuilder WithItem2(string item2)
    {
        if (string.IsNullOrWhiteSpace(item2))
            throw new ArgumentNullException(nameof(item2));

        m_Test.Item2 = item2;

        return this;
    }
}
";

        //Act
        IGenerator target = new BuilderGenerator(entity, targetNamespace);
        string actual = target.Generate();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Generate_With2Entities_ReturnEntities()
    {
        //Arrange
        BidEntity entity = new() { Name = "Test" };
        entity.Entities.AddRange(new string[] {"Item1", "Item2"});
        string targetNamespace = "MyNamespace";

        string expected =
@"using System;
using System.Collections.Generic;

namespace MyNamespace;
public partial class TestBuilder
{
    private readonly TestInfo m_Test = new();

    public static implicit operator TestInfo(TestBuilder builder)
    {
        return builder.m_Test;
    }

    public static implicit operator TestDeclaration(TestBuilder builder)
    {
        return new TestDeclaration(builder.m_Test);
    }

    public TestBuilder WithItem1(Item1Info item1)
    {
        m_Test.Item1 = item1 ?? throw new ArgumentNullException(nameof(item1));

        return this;
    }

    public TestBuilder WithItem1(Func<Item1Builder, Item1Builder> builderFunc)
    {
        Item1Builder builder = new();

        m_Test.Item1 = builderFunc(builder);

        return this;
    }

    public TestBuilder WithItem2(Item2Info item2)
    {
        m_Test.Item2 = item2 ?? throw new ArgumentNullException(nameof(item2));

        return this;
    }

    public TestBuilder WithItem2(Func<Item2Builder, Item2Builder> builderFunc)
    {
        Item2Builder builder = new();

        m_Test.Item2 = builderFunc(builder);

        return this;
    }
}
";

        //Act
        IGenerator target = new BuilderGenerator(entity, targetNamespace);
        string actual = target.Generate();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Generate_With2AttributeSets_ReturnAttributeSets()
    {
        //Arrange
        BidEntity entity = new() { Name = "Test" };
        entity.AttributeSets.AddRange(new string[] {"Item1", "Item2"});
        string targetNamespace = "MyNamespace";

        string expected =
@"using System;
using System.Collections.Generic;

namespace MyNamespace;
public partial class TestBuilder
{
    private readonly TestInfo m_Test = new();

    public static implicit operator TestInfo(TestBuilder builder)
    {
        return builder.m_Test;
    }

    public static implicit operator TestDeclaration(TestBuilder builder)
    {
        return new TestDeclaration(builder.m_Test);
    }

    public TestBuilder WithItem1(string item1)
    {
        if (string.IsNullOrWhiteSpace(item1))
            throw new ArgumentNullException(nameof(item1));

        m_Test.Item1s.Add(item1);

        return this;
    }

    public TestBuilder WithItem1s(string[] item1s)
    {
        if (item1s == null)
            throw new ArgumentNullException(nameof(item1s));

        m_Test.Item1s.AddRange(item1s);

        return this;
    }

    public TestBuilder WithItem2(string item2)
    {
        if (string.IsNullOrWhiteSpace(item2))
            throw new ArgumentNullException(nameof(item2));

        m_Test.Item2s.Add(item2);

        return this;
    }

    public TestBuilder WithItem2s(string[] item2s)
    {
        if (item2s == null)
            throw new ArgumentNullException(nameof(item2s));

        m_Test.Item2s.AddRange(item2s);

        return this;
    }
}
";

        //Act
        IGenerator target = new BuilderGenerator(entity, targetNamespace);
        string actual = target.Generate();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Generate_With2EntitySets_ReturnEntitySets()
    {
        //Arrange
        BidEntity entity = new() { Name = "Test" };
        entity.EntitySets.AddRange(new string[] {"Item1", "Item2"});
        string targetNamespace = "MyNamespace";

        string expected =
@"using System;
using System.Collections.Generic;

namespace MyNamespace;
public partial class TestBuilder
{
    private readonly TestInfo m_Test = new();

    public static implicit operator TestInfo(TestBuilder builder)
    {
        return builder.m_Test;
    }

    public static implicit operator TestDeclaration(TestBuilder builder)
    {
        return new TestDeclaration(builder.m_Test);
    }

    public TestBuilder WithItem1(Item1Info item1)
    {
        if (item1 == null)
            throw new ArgumentNullException(nameof(item1));

        m_Test.Item1s.Add(item1);

        return this;
    }

    public TestBuilder WithItem1(Func<Item1Builder, Item1Builder> builderFunc)
    {
        Item1Builder builder = new();

        m_Test.Item1s.Add(builderFunc(builder));

        return this;
    }

    public TestBuilder WithItem1s(Item1Info[] item1s)
    {
        if (item1s == null)
            throw new ArgumentNullException(nameof(item1s));

        m_Test.Item1s.AddRange(item1s);

        return this;
    }

    public TestBuilder WithItem2(Item2Info item2)
    {
        if (item2 == null)
            throw new ArgumentNullException(nameof(item2));

        m_Test.Item2s.Add(item2);

        return this;
    }

    public TestBuilder WithItem2(Func<Item2Builder, Item2Builder> builderFunc)
    {
        Item2Builder builder = new();

        m_Test.Item2s.Add(builderFunc(builder));

        return this;
    }

    public TestBuilder WithItem2s(Item2Info[] item2s)
    {
        if (item2s == null)
            throw new ArgumentNullException(nameof(item2s));

        m_Test.Item2s.AddRange(item2s);

        return this;
    }
}
";

        //Act
        IGenerator target = new BuilderGenerator(entity, targetNamespace);
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
@"using System;
using System.Collections.Generic;

namespace MyNamespace;
public partial class Test2Builder
{
    private readonly Test2Info m_Test2 = new();

    public static implicit operator Test2Info(Test2Builder builder)
    {
        return builder.m_Test2;
    }

    public static implicit operator Test2Declaration(Test2Builder builder)
    {
        return new Test2Declaration(builder.m_Test2);
    }

    public Test2Builder WithItem1(string item1)
    {
        if (string.IsNullOrWhiteSpace(item1))
            throw new ArgumentNullException(nameof(item1));

        m_Test2.Item1 = item1;

        return this;
    }

    public Test2Builder WithItem2(Item2Info item2)
    {
        m_Test2.Item2 = item2 ?? throw new ArgumentNullException(nameof(item2));

        return this;
    }

    public Test2Builder WithItem2(Func<Item2Builder, Item2Builder> builderFunc)
    {
        Item2Builder builder = new();

        m_Test2.Item2 = builderFunc(builder);

        return this;
    }

    public Test2Builder WithItem3(string item3)
    {
        if (string.IsNullOrWhiteSpace(item3))
            throw new ArgumentNullException(nameof(item3));

        m_Test2.Item3s.Add(item3);

        return this;
    }

    public Test2Builder WithItem3s(string[] item3s)
    {
        if (item3s == null)
            throw new ArgumentNullException(nameof(item3s));

        m_Test2.Item3s.AddRange(item3s);

        return this;
    }

    public Test2Builder WithItem4(Item4Info item4)
    {
        if (item4 == null)
            throw new ArgumentNullException(nameof(item4));

        m_Test2.Item4s.Add(item4);

        return this;
    }

    public Test2Builder WithItem4(Func<Item4Builder, Item4Builder> builderFunc)
    {
        Item4Builder builder = new();

        m_Test2.Item4s.Add(builderFunc(builder));

        return this;
    }

    public Test2Builder WithItem4s(Item4Info[] item4s)
    {
        if (item4s == null)
            throw new ArgumentNullException(nameof(item4s));

        m_Test2.Item4s.AddRange(item4s);

        return this;
    }
}
";

        //Act
        IGenerator target = new BuilderGenerator(entity, targetNamespace);
        string actual = target.Generate();

        //Assert
        Assert.AreEqual(expected, actual);
    }
}
