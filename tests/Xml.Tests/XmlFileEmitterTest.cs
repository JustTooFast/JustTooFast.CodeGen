// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml.Tests;

[TestClass]
public class XmlFileEmitterTest
{
    [TestMethod]
    public void AppendDeclaration_WithRootElement_ReturnWithRootElement()
    {
        //Arrange
        XmlFileBuilder builder = new XmlFileBuilder()
            .WithRootElement(x => x
                .WithName("catalog"));
        
        string expected =
@"<?xml version=""1.0""?>
<catalog></catalog>";

        //Act
        XmlFileEmitter target = new(builder, new Appender());
        target.AppendDeclaration();
        string actual = target.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void AppendDeclaration_WithPrologXmlEncoding_ReturnWithPrologXmlEncoding()
    {
        //Arrange
        XmlFileBuilder builder = new XmlFileBuilder()
            .WithProlog(x => x
                .WithXml(y => y
                    .WithEncoding(Encoding.UTF_8)))
            .WithRootElement(x => x
                .WithName("catalog"));
        
        string expected =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<catalog></catalog>";

        //Act
        XmlFileEmitter target = new(builder, new Appender());
        target.AppendDeclaration();
        string actual = target.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void AppendDeclaration_WithDisableProlog_ReturnWithoutProlog()
    {
        //Arrange
        XmlFileBuilder builder1 = new XmlFileBuilder()
            .WithDisableProlog(true)
            .WithRootElement(x => x
                .WithName("catalog"));
        
        XmlFileBuilder builder2 = new XmlFileBuilder()
            .AsDisableProlog()
            .WithRootElement(x => x
                .WithName("catalog"));
        
        string expected = "<catalog></catalog>";

        //Act
        XmlFileEmitter target1 = new(builder1, new Appender());
        target1.AppendDeclaration();
        string actual1 = target1.ToString();

        XmlFileEmitter target2 = new(builder2, new Appender());
        target2.AppendDeclaration();
        string actual2 = target2.ToString();

        //Assert
        Assert.AreEqual(expected, actual1);
        Assert.AreEqual(expected, actual2);
    }

    [TestMethod]
    [ExpectedException(typeof(XmlFormatException))]   //Assert
    public void Validate_MissingRootElement_ThrowException()
    {
        //Arrange
        XmlFileBuilder builder = new();

        //Act
        XmlFileEmitter target = new(builder, new Appender());
    }
}