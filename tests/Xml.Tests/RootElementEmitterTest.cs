// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml.Tests;

[TestClass]
public class RootElementEmitterTest
{
    [TestMethod]
    public void EmitTo_WithName_ReturnWithName()
    {
        //Arrange
        var builder = new RootElementBuilder()
            .WithName("rootElement");
        
        var expected = "<rootElement></rootElement>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new RootElementEmitter(builder);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_WithSingleAttribute_ReturnWithAttribute()
    {
        //Arrange
        var builder = new RootElementBuilder()
            .WithName("rootElement")
            .WithAttribute(x => x
                .WithName("myAttribute"));
        
        var expected = "<rootElement myAttribute=\"\"></rootElement>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new RootElementEmitter(builder);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_With2Attributes_ReturnWithAttributes()
    {
        //Arrange
        var builder = new RootElementBuilder()
            .WithName("rootElement")
            .WithAttribute(x => x
                .WithName("first"))
            .WithAttribute(x => x
                .WithName("second"));
        
        var expected = "<rootElement first=\"\" second=\"\"></rootElement>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new RootElementEmitter(builder);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_WithChildElement_ReturnChildElement()
    {
        //Arrange
        var builder = new RootElementBuilder()
            .WithName("catalog")
            .WithElement(x => x
                .WithName("book"));
        
        var expected =
@"<catalog>
  <book></book>
</catalog>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new RootElementEmitter(builder);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_With2ChildElements_ReturnChildElements()
    {
        //Arrange
        var builder = new RootElementBuilder()
            .WithName("catalog")
            .WithElement(x => x
                .WithName("book"))
            .WithElement(x => x
                .WithName("book"));
        
        var expected =
@"<catalog>
  <book></book>
  <book></book>
</catalog>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new RootElementEmitter(builder);
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
        var builder = new RootElementBuilder();

        //Act
        var target = new RootElementEmitter(builder);
    }
}