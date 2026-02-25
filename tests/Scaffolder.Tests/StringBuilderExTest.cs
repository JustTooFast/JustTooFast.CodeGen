// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.Text;

namespace JustTooFast.CodeGen.Scaffolder.Tests;

[TestClass]
public class StringBuilderExTest
{
    [TestMethod]
    public void AppendLineFeed_NoString_AddLineFeed()
    {
        //Arrange
        StringBuilder target = new();
        string expected = "\n";

        //Act
        string actual = target.AppendLineFeed().ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void AppendLineFeed_WithString_AddStringWithLineFeed()
    {
        //Arrange
        StringBuilder target = new();
        string expected = "test\n";

        //Act
        string actual = target.AppendLineFeed("test").ToString();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))] //Assert
    public void AppendLineFeed_NoStringNullStringBuilder_ThrowException()
    {
        //Arrange
        StringBuilder target = null;

        //Act
        target.AppendLineFeed();
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))] //Assert
    public void AppendLineFeed_WithStringNullStringBuilder_ThrowException()
    {
        //Arrange
        StringBuilder target = null;

        //Act
        target.AppendLineFeed("test");
    }
}