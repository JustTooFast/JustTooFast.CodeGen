// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml.Tests;

[TestClass]
public class XmlSnippetDeclarationTest
{
    [TestMethod]
    public void AppendDeclaration_WithElement_ReturnWithElement()
    {
        //Arrange
        XmlSnippetBuilder builder = new XmlSnippetBuilder()
            .WithElement(x => x
                .WithName("book"));
        
        string expected = "<book></book>";

        //Act
        XmlSnippetDeclaration target = new(builder, new Appender());
        target.AppendDeclaration();
        string actual = target.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void AppendDeclaration_With2Elements_ReturnWithElements()
    {
        //Arrange
        XmlSnippetBuilder builder = new XmlSnippetBuilder()
            .WithElement(x => x
                .WithName("book"))
            .WithElement(x => x
                .WithName("book"));
        
        string expected =
@"<book></book>
<book></book>";

        //Act
        XmlSnippetDeclaration target = new(builder, new Appender());
        target.AppendDeclaration();
        string actual = target.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(XmlFormatException))]   //Assert
    public void Validate_MissingElement_ThrowException()
    {
        //Arrange
        XmlSnippetBuilder builder = new();

        //Act
        XmlSnippetDeclaration target = new(builder, new Appender());
    }
}