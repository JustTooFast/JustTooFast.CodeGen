// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml.Tests;

[TestClass]
public class XmlFileEmitterTest
{
    [TestMethod]
    public void EmitTo_WithRootElement_ReturnWithRootElement()
    {
        //Arrange
        XmlFileBuilder builder = new XmlFileBuilder()
            .WithRootElement(x => x
                .WithName("catalog"));
        
        string expected =
@"<?xml version=""1.0""?>
<catalog></catalog>";

        //Act
        IAppender appender = new Appender();
        XmlFileEmitter target = new(builder);
        target.EmitTo(appender);
        string actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_WithPrologXmlEncoding_ReturnWithPrologXmlEncoding()
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
        IAppender appender = new Appender();
        XmlFileEmitter target = new(builder);
        target.EmitTo(appender);
        string actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_WithDisableProlog_ReturnWithoutProlog()
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
        IAppender appender1 = new Appender();
        XmlFileEmitter target1 = new(builder1);
        target1.EmitTo(appender1);
        string actual1 = appender1.ToString();

        IAppender appender2 = new Appender();
        XmlFileEmitter target2 = new(builder2);
        target2.EmitTo(appender2);
        string actual2 = appender2.ToString();

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
        XmlFileEmitter target = new(builder);
    }
}