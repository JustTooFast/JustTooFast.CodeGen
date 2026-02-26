// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;

namespace JustTooFast.CodeGen.Scaffolder.Tests;

[TestClass]
public class BidWriterTest
{
    [TestMethod]
    public void Write_With2Files_FilesWritten()
    {
        //Arrange
        FileHelperSpy fileSpy = new();
        BidParserSpy parserSpy = new();
        int expectedCallsTo_GetFileNamesInFolder = 1;
        int expectedCallsTo_Read = 2;
        int expectedCallsTo_Parse = 2;
        int expectedCallsTo_Write = 6;

        //Act
        IBidWriter target = new BidWriter(fileSpy, parserSpy);
        int actualNumWrittenFiles = target.Write("Input", "Output", "MyNamespace");

        //Assert
        Assert.AreEqual(expectedCallsTo_GetFileNamesInFolder, fileSpy.CallsTo_GetFileNamesInFolder);
        Assert.AreEqual(expectedCallsTo_Read, fileSpy.CallsTo_Read);
        Assert.AreEqual(expectedCallsTo_Parse, parserSpy.CallsTo_Parse);
        Assert.AreEqual(expectedCallsTo_Write, fileSpy.CallsTo_Write);
        Assert.AreEqual(expectedCallsTo_Write, fileSpy.WrittenFiles.Count);
        Assert.AreEqual(expectedCallsTo_Write, actualNumWrittenFiles);
        Assert.IsTrue(fileSpy.ReadFilePaths.Contains($"Input{Path.DirectorySeparatorChar}First.bid"));
        Assert.IsTrue(fileSpy.ReadFilePaths.Contains($"Input{Path.DirectorySeparatorChar}Second.bid"));
        Assert.IsTrue(fileSpy.WrittenFiles.ContainsKey($"Output{Path.DirectorySeparatorChar}FirstModel.gen.cs"));
        Assert.IsTrue(fileSpy.WrittenFiles.ContainsKey($"Output{Path.DirectorySeparatorChar}SecondModel.gen.cs"));
        Assert.IsTrue(fileSpy.WrittenFiles.ContainsKey($"Output{Path.DirectorySeparatorChar}FirstBuilder.gen.cs"));
        Assert.IsTrue(fileSpy.WrittenFiles.ContainsKey($"Output{Path.DirectorySeparatorChar}SecondBuilder.gen.cs"));
        Assert.IsTrue(fileSpy.WrittenFiles.ContainsKey($"Output{Path.DirectorySeparatorChar}FirstEmitter.gen.cs"));
        Assert.IsTrue(fileSpy.WrittenFiles.ContainsKey($"Output{Path.DirectorySeparatorChar}SecondEmitter.gen.cs"));
        Assert.IsTrue(fileSpy.WrittenFiles[$"Output{Path.DirectorySeparatorChar}FirstModel.gen.cs"].Contains("class FirstModel"));
        Assert.IsTrue(fileSpy.WrittenFiles[$"Output{Path.DirectorySeparatorChar}FirstBuilder.gen.cs"].Contains("class FirstBuilder"));
        Assert.IsTrue(fileSpy.WrittenFiles[$"Output{Path.DirectorySeparatorChar}FirstEmitter.gen.cs"].Contains("class FirstEmitter"));
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]  //Assert
    public void Write_WithNullFileHelper_ThrowException()
    {
        //Arrange
        FileHelperSpy fileHelper = null;
        BidParserSpy parserSpy = new();

        //Act
        IBidWriter target = new BidWriter(fileHelper, parserSpy);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]  //Assert
    public void Write_WithNullParser_ThrowException()
    {
        //Arrange
        FileHelperSpy fileSpy = new();
        BidParserSpy parser = null;

        //Act
        IBidWriter target = new BidWriter(fileSpy, parser);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]  //Assert
    public void Write_WithNullInputFolder_ThrowException()
    {
        //Arrange
        FileHelperSpy fileSpy = new();
        BidParserSpy parserSpy = new();
        string inputFolder = null;
        string outputFolder = "Output";
        string targetNamespace = "MyNamespace";

        //Act
        IBidWriter target = new BidWriter(fileSpy, parserSpy);
        target.Write(inputFolder, outputFolder, targetNamespace);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]  //Assert
    public void Write_WithNullOutputFolder_ThrowException()
    {
        //Arrange
        FileHelperSpy fileSpy = new();
        BidParserSpy parserSpy = new();
        string inputFolder = "Input";
        string outputFolder = null;
        string targetNamespace = "MyNamespace";

        //Act
        IBidWriter target = new BidWriter(fileSpy, parserSpy);
        target.Write(inputFolder, outputFolder, targetNamespace);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]  //Assert
    public void Write_WithNullTargetNamespace_ThrowException()
    {
        //Arrange
        FileHelperSpy fileSpy = new();
        BidParserSpy parserSpy = new();
        string inputFolder = "Input";
        string outputFolder = "Output";
        string targetNamespace = null;

        //Act
        IBidWriter target = new BidWriter(fileSpy, parserSpy);
        target.Write(inputFolder, outputFolder, targetNamespace);
    }
}
