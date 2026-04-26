// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen.SampleXml.Tests;

[TestClass]
public class ElementEmitterTest
{
    [TestMethod]
    public void Generate_WithName_ReturnWithName()
    {
        //Arrange
        var builder = new ElementBuilder()
            .WithName("test");
        
        var expected = "<test></test>";

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
    public void Generate_WithText_ReturnWithText()
    {
        //Arrange
        var builder = new ElementBuilder()
            .WithName("myElement")
            .WithText("Hello, World!");
        
        var expected = "<myElement>Hello, World!</myElement>";

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
    public void Generate_WithSingleAttribute_ReturnWithAttribute()
    {
        //Arrange
        var builder = new ElementBuilder()
            .WithName("myElement")
            .WithAttribute(x => x
                .WithName("myAttribute"));
        
        var expected = "<myElement myAttribute=\"\"></myElement>";

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
    public void Generate_With2Attributes_ReturnWithAttributes()
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
    public void Generate_WithNestedElement_ReturnNestedElement()
    {
        //Arrange
        var builder = new ElementBuilder()
            .WithName("element")
            .WithElement(x => x
                .WithName("nestedElement"));
        
        var expected = 
@"<element>
  <nestedElement></nestedElement>
</element>";

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
    public void Generate_With2NestedElements_ReturnNestedElements()
    {
        //Arrange
        var builder = new ElementBuilder()
            .WithName("element")
            .WithElement(x => x
                .WithName("nestedElement1"))
            .WithElement(x => x
                .WithName("nestedElement2"));
        
        var expected =
@"<element>
  <nestedElement1></nestedElement1>
  <nestedElement2></nestedElement2>
</element>";

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
    public void Generate_WithNestedElementsInNestedElement_ReturnNestedElements()
    {
        //Arrange
        var builder = new ElementBuilder()
            .WithName("element")
            .WithElement(x => x
                .WithName("nestedElement1")
                .WithElement(y => y
                    .WithName("nestedElement2"))
                .WithElement(y => y
                    .WithName("nestedElement3")))
            .WithElement(x => x
                .WithName("nestedElement1")
                .WithElement(y => y
                    .WithName("nestedElement2"))
                .WithElement(y => y
                    .WithName("nestedElement3")));
        
        var expected =
@"<element>
  <nestedElement1>
    <nestedElement2></nestedElement2>
    <nestedElement3></nestedElement3>
  </nestedElement1>
  <nestedElement1>
    <nestedElement2></nestedElement2>
    <nestedElement3></nestedElement3>
  </nestedElement1>
</element>";

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
    [ExpectedException(typeof(Exception))]   //Assert
    public void Validate_MissingName_ThrowException()
    {
        //Arrange
        var builder = new ElementBuilder();

        //Act
        var target = new ElementEmitter(builder);
    }
}
