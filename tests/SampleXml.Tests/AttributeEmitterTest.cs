// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen.SampleXml.Tests;

[TestClass]
public class AttributeEmitterTest
{
    [TestMethod]
    public void Generate_WithName_ReturnNameAndEmptyValue()
    {
        //Arrange
        var builder = new AttributeBuilder()
            .WithName("test");
        
        var expected = "test=\"\"";

        //Act
        var fmt = new Formatting(newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new AttributeEmitter(builder);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Generate_WithNameAndValue_ReturnNameAndValue()
    {
        //Arrange
        var builder = new AttributeBuilder()
            .WithName("myName")
            .WithValue("myValue");
        
        var expected = "myName=\"myValue\"";

        //Act
        var fmt = new Formatting(newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new AttributeEmitter(builder);
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
        var builder = new AttributeBuilder();

        //Act
        var target = new AttributeEmitter(builder);
    }
}