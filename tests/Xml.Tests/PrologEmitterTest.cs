// Copyright 2024-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml.Tests;

[TestClass]
public class PrologEmitterTest
{
    [TestMethod]
    public void AppendDeclaration_WithDefaultXml_ReturnXml()
    {
        //Arrange
        PrologBuilder builder = new();
        
        string expected = "<?xml version=\"1.0\"?>";

        //Act
        PrologEmitter target = new(builder, new Appender());
        target.AppendDeclaration();
        string actual = target.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void AppendDeclaration_WithXmlEncoding_ReturnXmlEncoding()
    {
        //Arrange
        PrologBuilder builder = new PrologBuilder()
            .WithXml(x => x
                .WithEncoding(Encoding.UTF_8));
        
        string expected = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";

        //Act
        PrologEmitter target = new(builder, new Appender());
        target.AppendDeclaration();
        string actual = target.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }
}
