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
        var builder = new XmlSnippetBuilder()
            .WithElement(x => x
                .WithName("book"));
        
        var expected = "<book></book>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new XmlSnippetEmitter(builder);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_With2Elements_ReturnWithElements()
    {
        //Arrange
        var builder = new XmlSnippetBuilder()
            .WithElement(x => x
                .WithName("book"))
            .WithElement(x => x
                .WithName("book"));
        
        var expected =
@"<book></book>
<book></book>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new XmlSnippetEmitter(builder);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(XmlFormatException))]   //Assert
    public void Validate_MissingElement_ThrowException()
    {
        //Arrange
        var builder = new XmlSnippetBuilder();

        //Act
        var target = new XmlSnippetEmitter(builder);
    }
}