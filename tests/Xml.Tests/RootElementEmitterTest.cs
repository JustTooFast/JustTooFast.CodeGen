// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml.Tests;

[TestClass]
public class RootElementEmitterTest
{
    [TestMethod]
    public void AppendDeclaration_WithName_ReturnWithName()
    {
        //Arrange
        RootElementBuilder builder = new RootElementBuilder()
            .WithName("rootElement");
        
        string expected = "<rootElement></rootElement>";

        //Act
        RootElementEmitter target = new(builder, new Appender());
        target.AppendDeclaration();
        string actual = target.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void AppendDeclaration_WithSingleAttribute_ReturnWithAttribute()
    {
        //Arrange
        RootElementBuilder builder = new RootElementBuilder()
            .WithName("rootElement")
            .WithAttribute(x => x
                .WithName("myAttribute"));
        
        string expected = "<rootElement myAttribute=\"\"></rootElement>";

        //Act
        RootElementEmitter target = new(builder, new Appender());
        target.AppendDeclaration();
        string actual = target.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void AppendDeclaration_With2Attributes_ReturnWithAttributes()
    {
        //Arrange
        RootElementBuilder builder = new RootElementBuilder()
            .WithName("rootElement")
            .WithAttribute(x => x
                .WithName("first"))
            .WithAttribute(x => x
                .WithName("second"));
        
        string expected = "<rootElement first=\"\" second=\"\"></rootElement>";

        //Act
        RootElementEmitter target = new(builder, new Appender());
        target.AppendDeclaration();
        string actual = target.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void AppendDeclaration_WithChildElement_ReturnChildElement()
    {
        //Arrange
        RootElementBuilder builder = new RootElementBuilder()
            .WithName("catalog")
            .WithElement(x => x
                .WithName("book"));
        
        string expected =
@"<catalog>
  <book></book>
</catalog>";

        //Act
        RootElementEmitter target = new(builder, new Appender());
        target.AppendDeclaration();
        string actual = target.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void AppendDeclaration_With2ChildElements_ReturnChildElements()
    {
        //Arrange
        RootElementBuilder builder = new RootElementBuilder()
            .WithName("catalog")
            .WithElement(x => x
                .WithName("book"))
            .WithElement(x => x
                .WithName("book"));
        
        string expected =
@"<catalog>
  <book></book>
  <book></book>
</catalog>";

        //Act
        RootElementEmitter target = new(builder, new Appender());
        target.AppendDeclaration();
        string actual = target.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(XmlFormatException))]   //Assert
    public void Validate_MissingName_ThrowException()
    {
        //Arrange
        RootElementBuilder builder = new();

        //Act
        RootElementEmitter target = new(builder, new Appender());
    }
}