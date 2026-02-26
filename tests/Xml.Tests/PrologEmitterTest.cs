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
        IAppender appender = new Appender();
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
            .WithXml(x => x
                .WithEncoding(Encoding.UTF_8));
        
        string expected = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";

        //Act
        IAppender appender = new Appender();
        PrologEmitter target = new(builder);
        target.EmitTo(appender);
        string actual = appender.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }
}
