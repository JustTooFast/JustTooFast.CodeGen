// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;

namespace JustTooFast.CodeGen.Xml.Tests;

[TestClass]
public class AppenderTest
{
    [TestMethod]
    public void Append_WithString_ReturnString()
    {
        //Arrange
        IAppender target = new Appender();
        string expected = "test";

        //Act
        target.Append(expected);
        string actual = target.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Append_WithChar_ReturnChar()
    {
        //Arrange
        IAppender target = new Appender();
        char expected = 'A';

        //Act
        target.Append(expected);
        string actual = target.ToString();

        //Assert
        Assert.AreEqual(expected.ToString(), actual);
    }

    [TestMethod]
    public void AppendLineFeed_NoArgs_ReturnLineFeed()
    {
        //Arrange
        IAppender target = new Appender();
        string expected = "\n";

        //Act
        target.AppendLineFeed();
        string actual = target.ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void AppendLineFeed_WithString_ReturnStringAndLineFeed()
    {
        //Arrange
        IAppender target = new Appender();
        string expected = "test";

        //Act
        target.AppendLineFeed(expected);
        string actual = target.ToString();

        //Assert
        Assert.AreEqual($"{expected}\n", actual);
    }
}
