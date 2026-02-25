// Copyright 2024-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Xml.Tests;

[TestClass]
public class EnumExTest
{
    [TestMethod]
    public void GetDescription_ForEncodingEnum_ReturnDescription()
    {
        //Arrange
        string expected = "UTF-8";
        Encoding target = Encoding.UTF_8;

        //Act
        string actual = target.GetDescription();

        //Assert
        Assert.AreEqual(expected, actual);
    }
}
