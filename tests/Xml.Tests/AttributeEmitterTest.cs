// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen.Xml.Tests;

[TestClass]
public class AttributeEmitterTest
{
    [TestMethod]
    public void EmitTo_WithName_ReturnNameAndEmptyValue()
    {
        //Arrange
        AttributeBuilder builder = new AttributeBuilder()
            .WithName("id");
        
        string expected = "id=\"\"";

        //Act
        var fmt = new Formatting(newLine: "\n");
        IAppender appender = new StringBuilderAppender(formatting: fmt);
        AttributeEmitter target = new(builder);
        target.EmitTo(appender);
        string actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_WithNameAndValue_ReturnNameAndValue()
    {
        //Arrange
        AttributeBuilder builder = new AttributeBuilder()
            .WithName("id")
            .WithValue("bk101");
        
        string expected = "id=\"bk101\"";

        //Act
        var fmt = new Formatting(newLine: "\n");
        IAppender appender = new StringBuilderAppender(formatting: fmt);
        AttributeEmitter target = new(builder);
        target.EmitTo(appender);
        string actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(XmlFormatException))]   //Assert
    public void Validate_MissingName_ThrowException()
    {
        //Arrange
        AttributeBuilder builder = new();

        //Act
        AttributeEmitter target = new(builder);
    }
}
