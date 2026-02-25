// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml.Tests;

[TestClass]
public class XmlFileBuilderExTest
{
    [TestMethod]
    public void Generate_WithRootElement_ReturnWithRootElement()
    {
        //Arrange
        XmlFileBuilder target = new XmlFileBuilder()
            .WithRootElement(x => x
                .WithName("catalog"));
        
        string expected =
@"<?xml version=""1.0""?>
<catalog></catalog>";

        //Act
        string actual = target.Generate();

        //Assert
        Assert.AreEqual(expected, actual);
    }
}
