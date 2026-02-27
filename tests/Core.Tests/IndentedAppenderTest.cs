// Copyright 2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen.Tests;

[TestClass]
public class IndentedAppenderTest
{
    [TestMethod]
    public void Append_SingleLine_IndentsOnce()
    {
        //Arrange
        IAppender inner = new Appender("\n");
        IAppender indented = new IndentedAppender(inner, "  ");

        //Act
        indented.Append("abc");

        //Assert
        Assert.AreEqual("  abc", inner.ToString());
    }

    [TestMethod]
    public void Append_TwoCallsSameLine_IndentOnlyOnce()
    {
        //Arrange
        IAppender inner = new Appender("\n");
        IAppender indented = new IndentedAppender(inner, "  ");

        //Act
        indented.Append("a");
        indented.Append("b");

        //Assert
        Assert.AreEqual("  ab", inner.ToString());
    }

    [TestMethod]
    public void Append_WithLfInString_IndentsAfterNewline()
    {
        //Arrange
        IAppender inner = new Appender("\n");
        IAppender indented = new IndentedAppender(inner, "  ");

        //Act
        indented.Append("a\nb");

        //Assert
        Assert.AreEqual("  a\n  b", inner.ToString());
    }

    [TestMethod]
    public void Append_WithCrLfInString_IndentsAfterCrLf()
    {
        //Arrange
        IAppender inner = new Appender("\n");
        IAppender indented = new IndentedAppender(inner, "  ");

        //Act
        indented.Append("a\r\nb");

        //Assert
        Assert.AreEqual("  a\r\n  b", inner.ToString());
    }

    [TestMethod]
    public void Append_WithCrOnlyInString_IndentsAfterCr()
    {
        //Arrange
        IAppender inner = new Appender("\n");
        IAppender indented = new IndentedAppender(inner, "  ");

        //Act
        indented.Append("a\rb");

        //Assert
        Assert.AreEqual("  a\r  b", inner.ToString());
    }

    [TestMethod]
    public void Append_StringEndsWithNewline_NextAppendIsIndented()
    {
        //Arrange
        IAppender inner = new Appender("\n");
        IAppender indented = new IndentedAppender(inner, "  ");

        //Act
        indented.Append("a\n");
        indented.Append("b");

        //Assert
        Assert.AreEqual("  a\n  b", inner.ToString());
    }

    [TestMethod]
    public void Append_EmptyOrNullString_DoesNothing()
    {
        //Arrange
        IAppender inner = new Appender("\n");
        IAppender indented = new IndentedAppender(inner, "  ");

        //Act
        indented.Append((string?)null);
        indented.Append(string.Empty);

        //Assert
        Assert.AreEqual("", inner.ToString());
    }

    [TestMethod]
    public void Append_EmptySpan_DoesNothing()
    {
        //Arrange
        IAppender inner = new Appender("\n");
        IAppender indented = new IndentedAppender(inner, "  ");

        //Act
        indented.Append(ReadOnlySpan<char>.Empty);

        //Assert
        Assert.AreEqual("", inner.ToString());
    }

    [TestMethod]
    public void Append_WithSpanContainingNewlines_IndentsAfterNewline()
    {
        //Arrange
        IAppender inner = new Appender("\n");
        IAppender indented = new IndentedAppender(inner, "  ");

        //Act
        string s = "x\ny";
        indented.Append(s.AsSpan());

        //Assert
        Assert.AreEqual("  x\n  y", inner.ToString());
    }

    [TestMethod]
    public void Append_CharNewline_SetsLineStartForNextAppend()
    {
        //Arrange
        IAppender inner = new Appender("\n");
        IAppender indented = new IndentedAppender(inner, "  ");

        //Act
        indented.Append('x');
        indented.Append('\n');
        indented.Append('y');

        //Assert
        Assert.AreEqual("  x\n  y", inner.ToString());
    }

    [TestMethod]
    public void AppendLine_NoArgs_IndentsNextLine()
    {
        //Arrange
        IAppender inner = new Appender("\n");
        IAppender indented = new IndentedAppender(inner, "  ");

        //Act
        indented.Append("a");
        indented.AppendLine();
        indented.Append("b");

        //Assert
        Assert.AreEqual("  a\n  b", inner.ToString());
    }

    [TestMethod]
    public void AppendLine_NoArgs_AppendsOnlyNewline()
    {
        //Arrange
        IAppender inner = new Appender("\n");
        IAppender indented = new IndentedAppender(inner, "  ");

        //Act
        indented.AppendLine();

        //Assert
        Assert.AreEqual("\n", inner.ToString());
    }

    [TestMethod]
    public void AppendLine_WithNullString_AppendsOnlyNewline()
    {
        //Arrange
        IAppender inner = new Appender("\n");
        IAppender indented = new IndentedAppender(inner, "  ");

        //Act
        indented.AppendLine((string?)null);

        //Assert
        Assert.AreEqual("\n", inner.ToString());
    }

    [TestMethod]
    public void AppendLine_WithEmptyString_AppendsOnlyNewline()
    {
        //Arrange
        IAppender inner = new Appender("\n");
        IAppender indented = new IndentedAppender(inner, "  ");

        //Act
        indented.AppendLine(string.Empty);

        //Assert
        Assert.AreEqual("\n", inner.ToString());
    }

    [TestMethod]
    public void AppendLine_WithString_IndentsContentAndAddsNewline()
    {
        //Arrange
        IAppender inner = new Appender("\n");
        IAppender indented = new IndentedAppender(inner, "  ");

        //Act
        indented.AppendLine("abc");

        //Assert
        Assert.AreEqual("  abc\n", inner.ToString());
    }

    [TestMethod]
    public void AppendLine_WithSpan_IndentsContentAndAddsNewline()
    {
        //Arrange
        IAppender inner = new Appender("\n");
        IAppender indented = new IndentedAppender(inner, "  ");

        //Act
        indented.AppendLine("abc".AsSpan());

        //Assert
        Assert.AreEqual("  abc\n", inner.ToString());
    }

    [TestMethod]
    public void AppendLine_TwoLines_AppendsIndentOnEachLine()
    {
        // Arrange
        IAppender inner = new Appender("\n");
        IAppender target = new IndentedAppender(inner, "  ");

        // Act
        target.AppendLine("one");
        target.AppendLine("two");

        // Assert
        Assert.AreEqual("  one\n  two\n", inner.ToString());
    }

    [TestMethod]
    public void AppendLine_WithStringContainingNewline_IndentsEachLineAndAddsTrailingNewline()
    {
        //Arrange
        IAppender inner = new Appender("\n");
        IAppender indented = new IndentedAppender(inner, "  ");

        //Act
        indented.AppendLine("a\nb");

        //Assert
        Assert.AreEqual("  a\n  b\n", inner.ToString());
    }

    [TestMethod]
    public void Append_ThenAppendLineValue_IndentOnceAndAddsNewline()
    {
        // Arrange
        IAppender inner = new Appender("\n");
        IAppender target = new IndentedAppender(inner, "  ");

        // Act
        target.Append("a");
        target.AppendLine("b"); // same line: should not re-indent before b

        // Assert
        Assert.AreEqual("  ab\n", inner.ToString());
    }

    [TestMethod]
    public void AppendLine_ThenAppend_IndentsAfterLineBreak()
    {
        // Arrange
        IAppender inner = new Appender("\n");
        IAppender target = new IndentedAppender(inner, "  ");

        // Act
        target.AppendLine("a");
        target.Append("b");

        // Assert
        Assert.AreEqual("  a\n  b", inner.ToString());
    }

    [TestMethod]
    public void Append_CustomIndent_IsUsed()
    {
        //Arrange
        IAppender inner = new Appender("\n");
        IAppender indented = new IndentedAppender(inner, "--");

        //Act
        indented.Append("x\n");
        indented.Append("y");

        //Assert
        Assert.AreEqual("--x\n--y", inner.ToString());
    }
}