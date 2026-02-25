// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml.Tests;

[TestClass]
public class XmlSnippetBuilderExTest
{
    [TestMethod]
    public void Generate_WithElement_ReturnWithElement()
    {
        //Arrange
        XmlSnippetBuilder target = new XmlSnippetBuilder()
            .WithElement(x => x
                .WithName("book"));
        
        string expected = "<book></book>";

        //Act
        string actual = target.Generate();

        //Assert
        Assert.AreEqual(expected, actual);
    }
}
