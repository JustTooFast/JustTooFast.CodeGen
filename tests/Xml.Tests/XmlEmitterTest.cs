// Copyright 2024-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml.Tests;

[TestClass]
public class XmlEmitterTest
{
    [TestMethod]
    public void EmitTo_WithDefaultVersion_ReturnVersion()
    {
        //Arrange
        var builder = new XmlDeclarationBuilder();
        
        var expected = "<?xml version=\"1.0\"?>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new XmlDeclarationEmitter(builder);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_WithEncoding_ReturnEncoding()
    {
        //Arrange
        var builder = new XmlDeclarationBuilder()
            .WithEncoding("UTF-8");
        
        var expected = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new XmlDeclarationEmitter(builder);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_WithEncodingEnum_ReturnEncoding()
    {
        //Arrange
        var builder = new XmlDeclarationBuilder()
            .WithEncoding(XmlEncoding.UTF_8);
        
        var expected = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new XmlDeclarationEmitter(builder);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_WithStandalone_ReturnStandalone()
    {
        //Arrange
        var builder = new XmlDeclarationBuilder()
            .WithStandalone("yes");
        
        var expected = "<?xml version=\"1.0\" standalone=\"yes\"?>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new XmlDeclarationEmitter(builder);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_WithStandaloneEnum_ReturnStandalone()
    {
        //Arrange
        var builder = new XmlDeclarationBuilder()
            .WithStandalone(XmlStandalone.Yes);
        
        var expected = "<?xml version=\"1.0\" standalone=\"yes\"?>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new XmlDeclarationEmitter(builder);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_WithAllParametersDefined_ReturnAllParameters()
    {
        //Arrange
        var builder = new XmlDeclarationBuilder()
            .WithEncoding(XmlEncoding.UTF_8)
            .WithStandalone(XmlStandalone.Yes);
        
        var expected = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new XmlDeclarationEmitter(builder);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(XmlFormatException))]   //Assert
    public void Validate_InvalidStandaloneValue_ThrowException()
    {
        //Arrange
        var builder = new XmlDeclarationBuilder()
            .WithStandalone("awesome");

        //Act
        var target = new XmlDeclarationEmitter(builder);
    }
}