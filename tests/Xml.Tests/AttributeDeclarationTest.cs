// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen.Xml.Tests;

[TestClass]
public class AttributeDeclarationTest
{
    [TestMethod]
    public void AppendDeclaration_WithName_ReturnNameAndEmptyValue()
    {
        //Arrange
        AttributeBuilder builder = new AttributeBuilder()
            .WithName("id");
        
        string expected = "id=\"\"";

        //Act
        AttributeDeclaration target = new(builder, new Appender());
        target.AppendDeclaration();
        string actual = target.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void AppendDeclaration_WithNameAndValue_ReturnNameAndValue()
    {
        //Arrange
        AttributeBuilder builder = new AttributeBuilder()
            .WithName("id")
            .WithValue("bk101");
        
        string expected = "id=\"bk101\"";

        //Act
        AttributeDeclaration target = new(builder, new Appender());
        target.AppendDeclaration();
        string actual = target.ToString();

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
        AttributeDeclaration target = new(builder, new Appender());
    }
}
