// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml.Tests;

[TestClass]
public class AttributeEmitterTest
{
    [TestMethod]
    public void EmitTo_WithName_ReturnNameAndEmptyValue()
    {
        //Arrange
        var builder = new AttributeBuilder()
            .WithName("id");
        
        var expected = "id=\"\"";

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
    public void EmitTo_WithNameAndValue_ReturnNameAndValue()
    {
        //Arrange
        var builder = new AttributeBuilder()
            .WithName("id")
            .WithValue("bk101");
        
        var expected = "id=\"bk101\"";

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
    [ExpectedException(typeof(XmlFormatException))]   //Assert
    public void Validate_MissingName_ThrowException()
    {
        //Arrange
        var builder = new AttributeBuilder();

        //Act
        var target = new AttributeEmitter(builder);
    }
}
