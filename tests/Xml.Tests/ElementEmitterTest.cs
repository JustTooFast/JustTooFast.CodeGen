// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml.Tests;

[TestClass]
public class ElementEmitterTest
{
    [TestMethod]
    public void AppendDeclaration_WithName_ReturnWithName()
    {
        //Arrange
        ElementBuilder builder = new ElementBuilder()
            .WithName("book");
        
        string expected = "<book></book>";

        //Act
        ElementEmitter target = new(builder, new Appender());
        target.AppendDeclaration();
        string actual = target.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void AppendDeclaration_WithText_ReturnWithText()
    {
        //Arrange
        ElementBuilder builder = new ElementBuilder()
            .WithName("price")
            .WithText("44.95");
        
        string expected = "<price>44.95</price>";

        //Act
        ElementEmitter target = new(builder, new Appender());
        target.AppendDeclaration();
        string actual = target.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void AppendDeclaration_WithSingleAttribute_ReturnWithAttribute()
    {
        //Arrange
        ElementBuilder builder = new ElementBuilder()
            .WithName("book")
            .WithAttribute(x => x
                .WithName("id"));
        
        string expected = "<book id=\"\"></book>";

        //Act
        ElementEmitter target = new(builder, new Appender());
        target.AppendDeclaration();
        string actual = target.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void AppendDeclaration_With2Attributes_ReturnWithAttributes()
    {
        //Arrange
        ElementBuilder builder = new ElementBuilder()
            .WithName("test")
            .WithAttribute(x => x
                .WithName("first"))
            .WithAttribute(x => x
                .WithName("second"));
        
        string expected = "<test first=\"\" second=\"\"></test>";

        //Act
        ElementEmitter target = new(builder, new Appender());
        target.AppendDeclaration();
        string actual = target.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void AppendDeclaration_WithChildElement_ReturnChildElement()
    {
        //Arrange
        ElementBuilder builder = new ElementBuilder()
            .WithName("book")
            .WithElement(x => x
                .WithName("title"));
        
        string expected =
@"<book>
  <title></title>
</book>";

        //Act
        ElementEmitter target = new(builder, new Appender());
        target.AppendDeclaration();
        string actual = target.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void AppendDeclaration_With2ChildElements_ReturnChildElements()
    {
        //Arrange
        ElementBuilder builder = new ElementBuilder()
            .WithName("book")
            .WithElement(x => x
                .WithName("author"))
            .WithElement(x => x
                .WithName("title"));
        
        string expected =
@"<book>
  <author></author>
  <title></title>
</book>";

        //Act
        ElementEmitter target = new(builder, new Appender());
        target.AppendDeclaration();
        string actual = target.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void AppendDeclaration_WithNestedChildElements_ReturnChildElements()
    {
        //Arrange
        ElementBuilder builder = new ElementBuilder()
            .WithName("catalog")
            .WithElement(x => x
                .WithName("book")
                .WithElement(y => y
                    .WithName("author"))
                .WithElement(y => y
                    .WithName("title")))
            .WithElement(x => x
                .WithName("book")
                .WithElement(y => y
                    .WithName("author"))
                .WithElement(y => y
                    .WithName("title")));
        
        string expected =
@"<catalog>
  <book>
    <author></author>
    <title></title>
  </book>
  <book>
    <author></author>
    <title></title>
  </book>
</catalog>";

        //Act
        ElementEmitter target = new(builder, new Appender());
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
        ElementBuilder builder = new();

        //Act
        ElementEmitter target = new(builder, new Appender());
    }

    [TestMethod]
    [ExpectedException(typeof(XmlFormatException))]   //Assert
    public void Validate_BothTextAndElement_ThrowException()
    {
        //Arrange
        ElementBuilder builder = new ElementBuilder()
            .WithName("book")
            .WithText("XML Developer's Guide")
            .WithElement(x => x
                .WithName("title"));

        //Act
        ElementEmitter target = new(builder, new Appender());
    }
}