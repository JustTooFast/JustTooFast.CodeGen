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
        var builder = new XmlFileBuilder()
            .WithRootElement(x => x
                .WithName("catalog"));
        
        var expected =
@"<?xml version=""1.0""?>
<catalog></catalog>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new XmlFileEmitter(builder);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_WithPrologXmlEncoding_ReturnWithPrologXmlEncoding()
    {
        //Arrange
        var builder = new XmlFileBuilder()
            .WithProlog(x => x
                .WithXmlDeclaration(y => y
                    .WithEncoding(XmlEncoding.UTF_8)))
            .WithRootElement(x => x
                .WithName("catalog"));
        
        var expected =
@"<?xml version=""1.0"" encoding=""UTF-8""?>
<catalog></catalog>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new XmlFileEmitter(builder);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_WithDisableProlog_ReturnWithoutProlog()
    {
        //Arrange
        var builder1 = new XmlFileBuilder()
            .WithDisableProlog(true)
            .WithRootElement(x => x
                .WithName("catalog"));
        
        var builder2 = new XmlFileBuilder()
            .AsDisableProlog()
            .WithRootElement(x => x
                .WithName("catalog"));
        
        var expected = "<catalog></catalog>";

        //Act
        var appender1 = new StringBuilderAppender();
        var target1 = new XmlFileEmitter(builder1);
        target1.EmitTo(appender1);
        var actual1 = appender1.ToString();

        var appender2 = new StringBuilderAppender();
        var target2 = new XmlFileEmitter(builder2);
        target2.EmitTo(appender2);
        var actual2 = appender2.ToString();

        //Assert
        Assert.AreEqual(expected, actual1);
        Assert.AreEqual(expected, actual2);
    }

    [TestMethod]
    [ExpectedException(typeof(XmlFormatException))]   //Assert
    public void Validate_MissingRootElement_ThrowException()
    {
        //Arrange
        var builder = new XmlFileBuilder();

        //Act
        var target = new XmlFileEmitter(builder);
    }
}