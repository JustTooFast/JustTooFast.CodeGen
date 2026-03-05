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
        XmlDeclarationBuilder builder = new();
        
        string expected = "<?xml version=\"1.0\"?>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        IAppender appender = new StringBuilderAppender(formatting: fmt);
        XmlDeclarationEmitter target = new(builder);
        target.EmitTo(appender);
        string actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_WithEncoding_ReturnEncoding()
    {
        //Arrange
        XmlDeclarationBuilder builder = new XmlDeclarationBuilder()
            .WithEncoding("UTF-8");
        
        string expected = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        IAppender appender = new StringBuilderAppender(formatting: fmt);
        XmlDeclarationEmitter target = new(builder);
        target.EmitTo(appender);
        string actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_WithEncodingEnum_ReturnEncoding()
    {
        //Arrange
        XmlDeclarationBuilder builder = new XmlDeclarationBuilder()
            .WithEncoding(XmlEncoding.UTF_8);
        
        string expected = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        IAppender appender = new StringBuilderAppender(formatting: fmt);
        XmlDeclarationEmitter target = new(builder);
        target.EmitTo(appender);
        string actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_WithStandalone_ReturnStandalone()
    {
        //Arrange
        XmlDeclarationBuilder builder = new XmlDeclarationBuilder()
            .WithStandalone("yes");
        
        string expected = "<?xml version=\"1.0\" standalone=\"yes\"?>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        IAppender appender = new StringBuilderAppender(formatting: fmt);
        XmlDeclarationEmitter target = new(builder);
        target.EmitTo(appender);
        string actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_WithStandaloneEnum_ReturnStandalone()
    {
        //Arrange
        XmlDeclarationBuilder builder = new XmlDeclarationBuilder()
            .WithStandalone(XmlStandalone.Yes);
        
        string expected = "<?xml version=\"1.0\" standalone=\"yes\"?>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        IAppender appender = new StringBuilderAppender(formatting: fmt);
        XmlDeclarationEmitter target = new(builder);
        target.EmitTo(appender);
        string actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_WithAllParametersDefined_ReturnAllParameters()
    {
        //Arrange
        XmlDeclarationBuilder builder = new XmlDeclarationBuilder()
            .WithEncoding(XmlEncoding.UTF_8)
            .WithStandalone(XmlStandalone.Yes);
        
        string expected = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        IAppender appender = new StringBuilderAppender(formatting: fmt);
        XmlDeclarationEmitter target = new(builder);
        target.EmitTo(appender);
        string actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(XmlFormatException))]   //Assert
    public void Validate_InvalidStandaloneValue_ThrowException()
    {
        //Arrange
        XmlDeclarationBuilder builder = new XmlDeclarationBuilder()
            .WithStandalone("awesome");

        //Act
        XmlDeclarationEmitter target = new(builder);
    }
}