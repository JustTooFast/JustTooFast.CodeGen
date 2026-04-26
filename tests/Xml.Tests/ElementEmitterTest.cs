// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml.Tests;

[TestClass]
public class ElementEmitterTest
{
    [TestMethod]
    public void EmitTo_WithName_ReturnWithName()
    {
        //Arrange
        var builder = new ElementBuilder()
            .WithName("book");
        
        var expected = "<book></book>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new ElementEmitter(builder);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_WithText_ReturnWithText()
    {
        //Arrange
        var builder = new ElementBuilder()
            .WithName("price")
            .WithText("44.95");
        
        var expected = "<price>44.95</price>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new ElementEmitter(builder);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_WithSingleAttribute_ReturnWithAttribute()
    {
        //Arrange
        var builder = new ElementBuilder()
            .WithName("book")
            .WithAttribute(x => x
                .WithName("id"));
        
        var expected = "<book id=\"\"></book>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new ElementEmitter(builder);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_With2Attributes_ReturnWithAttributes()
    {
        //Arrange
        var builder = new ElementBuilder()
            .WithName("test")
            .WithAttribute(x => x
                .WithName("first"))
            .WithAttribute(x => x
                .WithName("second"));
        
        var expected = "<test first=\"\" second=\"\"></test>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new ElementEmitter(builder);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_WithChildElement_ReturnChildElement()
    {
        //Arrange
        var builder = new ElementBuilder()
            .WithName("book")
            .WithElement(x => x
                .WithName("title"));
        
        var expected =
@"<book>
  <title></title>
</book>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new ElementEmitter(builder);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_With2ChildElements_ReturnChildElements()
    {
        //Arrange
        var builder = new ElementBuilder()
            .WithName("book")
            .WithElement(x => x
                .WithName("author"))
            .WithElement(x => x
                .WithName("title"));
        
        var expected =
@"<book>
  <author></author>
  <title></title>
</book>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new ElementEmitter(builder);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_WithNestedChildElements_ReturnChildElements()
    {
        //Arrange
        var builder = new ElementBuilder()
            .WithName("catalog")
            .WithElement(x => x
                .WithName("book")
                .WithElement(y => y
                    .WithName("author"))
                .WithElement(y => y
                    .WithName("title")))
            .WithElement(x => x
                .WithName("book")
                .WithElement(y => y
                    .WithName("author"))
                .WithElement(y => y
                    .WithName("title")));
        
        var expected =
@"<catalog>
  <book>
    <author></author>
    <title></title>
  </book>
  <book>
    <author></author>
    <title></title>
  </book>
</catalog>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new ElementEmitter(builder);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(XmlFormatException))]   //Assert
    public void Validate_MissingName_ThrowException()
    {
        //Arrange
        var builder = new ElementBuilder();

        //Act
        var target = new ElementEmitter(builder);
    }

    [TestMethod]
    [ExpectedException(typeof(XmlFormatException))]   //Assert
    public void Validate_BothTextAndElement_ThrowException()
    {
        //Arrange
        var builder = new ElementBuilder()
            .WithName("book")
            .WithText("XML Developer's Guide")
            .WithElement(x => x
                .WithName("title"));

        //Act
        var target = new ElementEmitter(builder);
    }
}