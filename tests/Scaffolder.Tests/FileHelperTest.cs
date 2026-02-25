// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

using System;
using System.IO;
using System.Text;

namespace JustTooFast.CodeGen.Scaffolder.Tests;

[TestClass]
public class FileHelperTest
{
    private const string FOLDER_PATH = "MyTestFiles";

    [TestInitialize]
    public void Initialize()
    {
        if (Directory.Exists(FOLDER_PATH))
        {
            Directory.Delete(FOLDER_PATH, true);
        }
        Directory.CreateDirectory(FOLDER_PATH);
    }

    [ClassCleanup]
    public static void ClassCleanup()
    {
        if (Directory.Exists(FOLDER_PATH))
        {
            Directory.Delete(FOLDER_PATH, true);
        }
    }

    #region Read Tests

    [TestMethod]
    public void Read_FileWithSingleLine_ReturnLine()
    {
        //Arrange
        File expected = new()
        {
            Path = $"{FOLDER_PATH}{Path.DirectorySeparatorChar}MyTest.txt",
            Contents = "Awesome"
        };

        using (StreamWriter sw = new(expected.Path))
        {
            sw.Write(expected.Contents);
        }

        //Act
        IFileHelper target = new FileHelper();
        File actual = target.Read(expected.Path);

        //Assert
        Assert.AreEqual(expected.Path, actual.Path);
        Assert.AreEqual(expected.Contents, actual.Contents);
    }

    [TestMethod]
    public void Read_FileWith3Lines_Return3Lines()
    {
        //Arrange
        File expected = new()
        {
            Path = $"{FOLDER_PATH}{Path.DirectorySeparatorChar}MyTest.txt",
            Contents = new StringBuilder()
            .AppendLine("1")
            .AppendLine("2")
            .AppendLine("3")
            .ToString()
        };

        using (StreamWriter sw = new(expected.Path))
        {
            sw.Write(expected.Contents);
        }

        //Act
        IFileHelper target = new FileHelper();
        File actual = target.Read(expected.Path);

        //Assert
        Assert.AreEqual(expected.Path, actual.Path);
        Assert.AreEqual(expected.Contents, actual.Contents);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))] //Assert
    public void Read_WithNullFilePath_ThrowException()
    {
        //Arrange
        string filePath = null;

        //Act
        IFileHelper target = new FileHelper();
        target.Read(filePath);
    }

    #endregion

    #region Write Tests

    [TestMethod]
    public void Write_WithString_OuputString()
    {
        //Arrange
        string filePath = $"{FOLDER_PATH}{Path.DirectorySeparatorChar}MyTest.txt";
        string expected = "Awesome";

        //Act
        IFileHelper target = new FileHelper();
        target.Write(filePath, expected);

        //Assert
        string actual;
        using (StreamReader sr = new(filePath))
        {
            actual = sr.ReadToEnd();
        }
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void Write_WithAnotherString_OuputString()
    {
        //Arrange
        string filePath = $"{FOLDER_PATH}{Path.DirectorySeparatorChar}MyTest.txt";
        string expected = "Cool!";

        //Act
        IFileHelper target = new FileHelper();
        target.Write(filePath, expected);

        //Assert
        string actual;
        using (StreamReader sr = new(filePath))
        {
            actual = sr.ReadToEnd();
        }
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))] //Assert
    public void Write_WithNullFilePath_ThrowException()
    {
        //Arrange
        string filePath = null;
        string fileContents = "Something";

        //Act
        IFileHelper target = new FileHelper();
        target.Write(filePath, fileContents);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))] //Assert
    public void Write_WithNullFileContents_ThrowException()
    {
        //Arrange
        string filePath = $"{FOLDER_PATH}{Path.DirectorySeparatorChar}MyTest.txt";
        string fileContents = null;

        //Act
        IFileHelper target = new FileHelper();
        target.Write(filePath, fileContents);
    }

    #endregion

    #region GetFilesInFolder Tests

    [TestMethod]
    public void GetFilesInFolder_With3Files_Return3Names()
    {
        //Arrange
        string expected1 = $"{FOLDER_PATH}{Path.DirectorySeparatorChar}MyFile1.txt";
        string expected2 = $"{FOLDER_PATH}{Path.DirectorySeparatorChar}MyFile2.txt";
        string expected3 = $"{FOLDER_PATH}{Path.DirectorySeparatorChar}MyFile3.txt";

        using (StreamWriter sw = new(expected1))
        {
            sw.WriteLine("anything");
        }
        using (StreamWriter sw = new(expected2))
        {
            sw.WriteLine("anything");
        }
        using (StreamWriter sw = new(expected3))
        {
            sw.WriteLine("anything");
        }

        //Act
        IFileHelper target = new FileHelper();
        string[] actual = target.GetFilesInFolder(FOLDER_PATH);

        //Assert
        Assert.AreEqual(3, actual.Length);
        Assert.AreEqual(expected1, actual[0]);
        Assert.AreEqual(expected2, actual[1]);
        Assert.AreEqual(expected3, actual[2]);
    }

    [TestMethod]
    public void GetFilesInFolder_WithSingleFile_ReturnName()
    {
        //Arrange
        string expected = $"{FOLDER_PATH}{Path.DirectorySeparatorChar}MyFile.txt";;

        using (StreamWriter sw = new(expected))
        {
            sw.WriteLine("anything");
        }

        //Act
        IFileHelper target = new FileHelper();
        string[] actual = target.GetFilesInFolder(FOLDER_PATH);

        //Assert
        Assert.AreEqual(1, actual.Length);
        Assert.AreEqual(expected, actual[0]);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))] //Assert
    public void GetFilesInFolder_WithNullFolderPath_ThrowException()
    {
        //Arrange
        string folderPath = null;

        //Act
        IFileHelper target = new FileHelper();
        target.GetFilesInFolder(folderPath);
    }

    #endregion
}