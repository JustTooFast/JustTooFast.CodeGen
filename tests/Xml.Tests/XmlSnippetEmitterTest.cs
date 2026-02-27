// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml.Tests;

[TestClass]
public class XmlSnippetEmitterTest
{
    [TestMethod]
    public void EmitTo_WithElement_ReturnWithElement()
    {
        //Arrange
        XmlSnippetBuilder builder = new XmlSnippetBuilder()
            .WithElement(x => x
                .WithName("book"));
        
        string expected = "<book></book>";

        //Act
        IAppender appender = new Appender("\n");
        XmlSnippetEmitter target = new(builder);
        target.EmitTo(appender);
        string actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_With2Elements_ReturnWithElements()
    {
        //Arrange
        XmlSnippetBuilder builder = new XmlSnippetBuilder()
            .WithElement(x => x
                .WithName("book"))
            .WithElement(x => x
                .WithName("book"));
        
        string expected =
@"<book></book>
<book></book>";

        //Act
        IAppender appender = new Appender("\n");
        XmlSnippetEmitter target = new(builder);
        target.EmitTo(appender);
        string actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(XmlFormatException))]   //Assert
    public void Validate_MissingElement_ThrowException()
    {
        //Arrange
        XmlSnippetBuilder builder = new();

        //Act
        XmlSnippetEmitter target = new(builder);
    }
}