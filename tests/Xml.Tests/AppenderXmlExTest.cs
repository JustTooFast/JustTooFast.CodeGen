// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml.Tests;

[TestClass]
public class AppenderXmlExTest
{
    [TestMethod]
    public void AppendXmlTextEscaped_NoSpecialChars_AppendsUnchanged()
    {
        //Arrange
        IAppender a = new Appender("\n");
        string input = "hello world 123";

        //Act
        a.AppendXmlTextEscaped(input);

        //Assert
        Assert.AreEqual(input, a.ToString());
    }

    [TestMethod]
    public void AppendXmlTextEscaped_EscapesAmpLtGt_ButNotQuotes()
    {
        //Arrange
        IAppender a = new Appender("\n");
        string input = "a&b<c>d\"e'f";

        //Act
        a.AppendXmlTextEscaped(input);

        //Assert
        //Quotes should remain unchanged in text mode
        Assert.AreEqual("a&amp;b&lt;c&gt;d\"e'f", a.ToString());
    }

    [TestMethod]
    public void AppendXmlAttributeValueEscaped_EscapesAmpLtGtAndQuotes()
    {
        //Arrange
        IAppender a = new Appender("\n");
        string input = "a&b<c>d\"e'f";

        //Act
        a.AppendXmlAttributeValueEscaped(input);

        //Assert
        Assert.AreEqual("a&amp;b&lt;c&gt;d&quot;e&apos;f", a.ToString());
    }

    [TestMethod]
    public void AppendXmlTextEscaped_OnlyQuotes_InputUnchanged()
    {
        //Arrange
        IAppender a = new Appender("\n");
        string input = "\"'";

        //Act
        a.AppendXmlTextEscaped(input);

        //Assert
        Assert.AreEqual("\"'", a.ToString());
    }

    [TestMethod]
    public void AppendXmlAttributeValueEscaped_OnlyQuotes_EscapesBoth()
    {
        //Arrange
        IAppender a = new Appender("\n");
        string input = "\"'";

        //Act
        a.AppendXmlAttributeValueEscaped(input);

        //Assert
        Assert.AreEqual("&quot;&apos;", a.ToString());
    }

    [TestMethod]
    public void AppendXmlTextEscaped_EmptyString_AppendsEmpty()
    {
        //Arrange
        IAppender a = new Appender("\n");

        //Act
        a.AppendXmlTextEscaped(string.Empty);

        //Assert
        Assert.AreEqual(string.Empty, a.ToString());
    }

    [TestMethod]
    public void AppendXmlAttributeValueEscaped_EmptyString_AppendsEmpty()
    {
        //Arrange
        IAppender a = new Appender("\n");

        //Act
        a.AppendXmlAttributeValueEscaped(string.Empty);

        //Assert
        Assert.AreEqual(string.Empty, a.ToString());
    }

    [TestMethod]
    public void AppendXmlTextEscaped_MultipleEscapes_ProducesExpectedOutput()
    {
        //Arrange
        IAppender a = new Appender("\n");
        string input = "&&<<>>";

        //Act
        a.AppendXmlTextEscaped(input);

        //Assert
        Assert.AreEqual("&amp;&amp;&lt;&lt;&gt;&gt;", a.ToString());
    }

    [TestMethod]
    public void AppendXmlAttributeValueEscaped_MultipleEscapes_ProducesExpectedOutput()
    {
        //Arrange
        IAppender a = new Appender("\n");
        string input = "&&<<>>\"\"''";

        //Act
        a.AppendXmlAttributeValueEscaped(input);

        //Assert
        Assert.AreEqual("&amp;&amp;&lt;&lt;&gt;&gt;&quot;&quot;&apos;&apos;", a.ToString());
    }

    [TestMethod]
    public void AppendXmlTextEscaped_MixedContent_EscapesAndPreservesOtherChars()
    {
        //Arrange
        IAppender a = new Appender("\n");
        string input = "ab&cd<ef>gh";

        //Act
        a.AppendXmlTextEscaped(input);

        //Assert
        Assert.AreEqual("ab&amp;cd&lt;ef&gt;gh", a.ToString());
    }

    [TestMethod]
    public void AppendXmlAttributeValueEscaped_MixedContent_EscapesAndPreservesOtherChars()
    {
        //Arrange
        IAppender a = new Appender("\n");
        string input = "ab&cd<ef>g\"h'i";

        //Act
        a.AppendXmlAttributeValueEscaped(input);

        //Assert
        Assert.AreEqual("ab&amp;cd&lt;ef&gt;g&quot;h&apos;i", a.ToString());
    }
}