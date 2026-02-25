// Copyright 2024-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml.Tests;

[TestClass]
public class XmlDeclarationTest
{
    [TestMethod]
    public void AppendDeclaration_WithDefaultVersion_ReturnVersion()
    {
        //Arrange
        XmlBuilder builder = new();
        
        string expected = "<?xml version=\"1.0\"?>";

        //Act
        XmlDeclaration target = new(builder, new Appender());
        target.AppendDeclaration();
        string actual = target.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void AppendDeclaration_WithEncoding_ReturnEncoding()
    {
        //Arrange
        XmlBuilder builder = new XmlBuilder()
            .WithEncoding("UTF-8");
        
        string expected = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";

        //Act
        XmlDeclaration target = new(builder, new Appender());
        target.AppendDeclaration();
        string actual = target.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void AppendDeclaration_WithEncodingEnum_ReturnEncoding()
    {
        //Arrange
        XmlBuilder builder = new XmlBuilder()
            .WithEncoding(Encoding.UTF_8);
        
        string expected = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";

        //Act
        XmlDeclaration target = new(builder, new Appender());
        target.AppendDeclaration();
        string actual = target.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void AppendDeclaration_WithStandalone_ReturnStandalone()
    {
        //Arrange
        XmlBuilder builder = new XmlBuilder()
            .WithStandalone("yes");
        
        string expected = "<?xml version=\"1.0\" standalone=\"yes\"?>";

        //Act
        XmlDeclaration target = new(builder, new Appender());
        target.AppendDeclaration();
        string actual = target.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void AppendDeclaration_WithStandaloneEnum_ReturnStandalone()
    {
        //Arrange
        XmlBuilder builder = new XmlBuilder()
            .WithStandalone(Standalone.Yes);
        
        string expected = "<?xml version=\"1.0\" standalone=\"yes\"?>";

        //Act
        XmlDeclaration target = new(builder, new Appender());
        target.AppendDeclaration();
        string actual = target.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void AppendDeclaration_WithAllParametersDefined_ReturnAllParameters()
    {
        //Arrange
        XmlBuilder builder = new XmlBuilder()
            .WithEncoding(Encoding.UTF_8)
            .WithStandalone(Standalone.Yes);
        
        string expected = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>";

        //Act
        XmlDeclaration target = new(builder, new Appender());
        target.AppendDeclaration();
        string actual = target.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(XmlFormatException))]   //Assert
    public void Validate_InvalidStandaloneValue_ThrowException()
    {
        //Arrange
        XmlBuilder builder = new XmlBuilder()
            .WithStandalone("awesome");

        //Act
        XmlDeclaration target = new(builder, new Appender());
    }
}