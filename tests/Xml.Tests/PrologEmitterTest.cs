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
        var builder = new PrologBuilder();
        
        var expected = "<?xml version=\"1.0\"?>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new PrologEmitter(builder);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void EmitTo_WithXmlEncoding_ReturnXmlEncoding()
    {
        //Arrange
        var builder = new PrologBuilder()
            .WithXmlDeclaration(x => x
                .WithEncoding(XmlEncoding.UTF_8));
        
        var expected = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";

        //Act
        var fmt = new Formatting(newLine: "\n");
        var appender = new StringBuilderAppender(formatting: fmt);
        var target = new PrologEmitter(builder);
        target.EmitTo(appender);
        var actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }
}
