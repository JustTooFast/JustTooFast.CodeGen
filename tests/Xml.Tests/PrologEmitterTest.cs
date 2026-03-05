// Copyright 2024-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml.Tests;

[TestClass]
public class PrologEmitterTest
{
    [TestMethod]
    public void EmitTo_WithDefaultXml_ReturnXml()
    {
        //Arrange
        PrologBuilder builder = new();
        
        string expected = "<?xml version=\"1.0\"?>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        IAppender appender = new StringBuilderAppender(formatting: fmt);
        PrologEmitter target = new(builder);
        target.EmitTo(appender);
        string actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_WithXmlEncoding_ReturnXmlEncoding()
    {
        //Arrange
        PrologBuilder builder = new PrologBuilder()
            .WithXmlDeclaration(x => x
                .WithEncoding(XmlEncoding.UTF_8));
        
        string expected = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        IAppender appender = new StringBuilderAppender(formatting: fmt);
        PrologEmitter target = new(builder);
        target.EmitTo(appender);
        string actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }
}
