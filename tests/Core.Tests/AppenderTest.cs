// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen.Tests;

[TestClass]
public class AppenderTest
{
    [TestMethod]
    public void Ctor_NoArgs_DefaultNewLineIsLF()
    {
        //Arrange/Act
        var target = new Appender();

        //Assert
        Assert.AreEqual("\n", target.NewLine);
    }

    [TestMethod]
    public void Ctor_WithNewLine_SetsNewLine()
    {
        //Arrange/Act
        var target = new Appender("\r\n");

        //Assert
        Assert.AreEqual("\r\n", target.NewLine);
    }

    [TestMethod]
    public void Append_WithNullString_AppendsNothing()
    {
        //Arrange
        IAppender target = new Appender("\n");

        //Act
        target.Append((string?)null);

        //Assert
        Assert.AreEqual("", target.ToString());
    }

    [TestMethod]
    public void Append_WithEmptyString_AppendsNothing()
    {
        //Arrange
        IAppender target = new Appender("\n");

        //Act
        target.Append(string.Empty);

        //Assert
        Assert.AreEqual("", target.ToString());
    }

    [TestMethod]
    public void Append_WithString_AppendsString()
    {
        //Arrange
        IAppender target = new Appender();
        string expected = "test";

        //Act
        target.Append(expected);

        //Assert
        Assert.AreEqual(expected, target.ToString());
    }

    [TestMethod]
    public void Append_WithChar_AppendsChar()
    {
        //Arrange
        IAppender target = new Appender();
        char expected = 'A';

        //Act
        target.Append(expected);

        //Assert
        Assert.AreEqual("A", target.ToString());
    }

    [TestMethod]
    public void Append_WithEmptySpan_AppendsNothing()
    {
        //Arrange
        IAppender target = new Appender("\n");
        ReadOnlySpan<char> empty = ReadOnlySpan<char>.Empty;

        //Act
        target.Append(empty);

        //Assert
        Assert.AreEqual("", target.ToString());
    }

    [TestMethod]
    public void Append_WithSpan_AppendsSpan()
    {
        //Arrange
        IAppender target = new Appender();
        string value = "abcdef";
        ReadOnlySpan<char> span = value.AsSpan(1, 3); // "bcd"

        //Act
        target.Append(span);

        //Assert
        Assert.AreEqual("bcd", target.ToString());
    }

    [TestMethod]
    public void AppendLine_NoArgs_AppendsNewLine()
    {
        //Arrange
        IAppender target = new Appender("\n");

        //Act
        target.AppendLine();

        //Assert
        Assert.AreEqual("\n", target.ToString());
    }

    [TestMethod]
    public void AppendLine_WithNullString_AppendsOnlyNewLine()
    {
        //Arrange
        IAppender target = new Appender("\n");

        //Act
        target.AppendLine((string?)null);

        //Assert
        Assert.AreEqual("\n", target.ToString());
    }

    [TestMethod]
    public void AppendLine_WithEmptyString_AppendsOnlyNewLine()
    {
        //Arrange
        IAppender target = new Appender("\n");

        //Act
        target.AppendLine(string.Empty);

        //Assert
        Assert.AreEqual("\n", target.ToString());
    }

    [TestMethod]
    public void AppendLine_WithString_AppendsStringAndNewLine()
    {
        //Arrange
        IAppender target = new Appender("\n");
        string value = "test";

        //Act
        target.AppendLine(value);

        //Assert
        Assert.AreEqual("test\n", target.ToString());
    }

    [TestMethod]
    public void AppendLine_WithEmptySpan_AppendsOnlyNewLine()
    {
        //Arrange
        IAppender target = new Appender("\n");
        ReadOnlySpan<char> empty = ReadOnlySpan<char>.Empty;

        //Act
        target.AppendLine(empty);

        //Assert
        Assert.AreEqual("\n", target.ToString());
    }

    [TestMethod]
    public void AppendLine_WithSpan_AppendsSpanAndNewLine()
    {
        //Arrange
        IAppender target = new Appender("\n");
        string value = "abcdef";
        ReadOnlySpan<char> span = value.AsSpan(2, 2); // "cd"

        //Act
        target.AppendLine(span);

        //Assert
        Assert.AreEqual("cd\n", target.ToString());
    }

    [TestMethod]
    public void Append_MultipleCalls_AppendsInOrder()
    {
        //Arrange
        IAppender target = new Appender("\n");

        //Act
        target.Append("a");
        target.Append('b');
        target.Append("cd".AsSpan());
        target.AppendLine();
        target.AppendLine("e");

        //Assert
        Assert.AreEqual("abcd\ne\n", target.ToString());
    }

    [TestMethod]
    public void Append_MixNullAndEmpty_DoesNotInsertLiteralNull()
    {
        //Arrange
        IAppender target = new Appender("\n");

        //Act
        target.Append((string?)null);
        target.Append(string.Empty);
        target.Append('A');
        target.Append((string?)null);
        target.Append(ReadOnlySpan<char>.Empty);
        target.Append('B');

        //Assert
        Assert.AreEqual("AB", target.ToString());
    }

    [TestMethod]
    public void AppendLine_MixNullEmptyAndContent_ProducesExpectedOutput()
    {
        //Arrange
        IAppender target = new Appender("\n");

        //Act
        target.AppendLine((string?)null);          // "\n"
        target.AppendLine(string.Empty);          // "\n"
        target.AppendLine("X");                   // "X\n"
        target.AppendLine(ReadOnlySpan<char>.Empty); // "\n"

        //Assert
        Assert.AreEqual("\n\nX\n\n", target.ToString());
    }
}
