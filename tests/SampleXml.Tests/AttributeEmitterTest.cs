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
        AttributeBuilder builder = new AttributeBuilder()
            .WithName("test");
        
        string expected = "test=\"\"";

        //Act
        IAppender appender = new Appender("\n");
        AttributeEmitter target = new(builder);
        target.EmitTo(appender);
        string actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Generate_WithNameAndValue_ReturnNameAndValue()
    {
        //Arrange
        AttributeBuilder builder = new AttributeBuilder()
            .WithName("myName")
            .WithValue("myValue");
        
        string expected = "myName=\"myValue\"";

        //Act
        IAppender appender = new Appender("\n");
        AttributeEmitter target = new(builder);
        target.EmitTo(appender);
        string actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception))]   //Assert
    public void Validate_MissingName_ThrowException()
    {
        //Arrange
        AttributeBuilder builder = new();

        //Act
        AttributeEmitter target = new(builder);
    }
}