// Copyright 2023-2026 Matthew Yancer
// SPDX-License-Identifier: Apache-2.0

namespace JustTooFast.CodeGen.Scaffolder.Tests;

[TestClass]
public class StringExTest
{
    #region ToLowerFirstLetter Tests

    [TestMethod]
    public void ToLowerFirstLetter_AlreadyLower_ReturnStr()
    {
        //Arrange
        string expected = "awesome";
        string target = "awesome";

        //Act
        string actual = target.ToLowerFirstLetter();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ToLowerFirstLetter_Upper_ReturnLower()
    {
        //Arrange
        string expected = "cool";
        string target = "Cool";

        //Act
        string actual = target.ToLowerFirstLetter();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ToLowerFirstLetter_PascalCase_ReturnCamelCase()
    {
        //Arrange
        string expected = "myClass";
        string target = "MyClass";

        //Act
        string actual = target.ToLowerFirstLetter();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ToLowerFirstLetter_SingleUpper_ReturnLower()
    {
        //Arrange
        string expected = "d";
        string target = "D";

        //Act
        string actual = target.ToLowerFirstLetter();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ToLowerFirstLetter_AllUpper_ReturnFirstLower()
    {
        //Arrange
        string expected = "cHOCOLATE";
        string target = "CHOCOLATE";

        //Act
        string actual = target.ToLowerFirstLetter();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ToLowerFirstLetter_Empty_ReturnEmpty()
    {
        //Arrange
        string expected = string.Empty;
        string target = string.Empty;

        //Act
        string actual = target.ToLowerFirstLetter();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ToLowerFirstLetter_Null_ReturnNull()
    {
        //Arrange
        string expected = null;
        string target = null;

        //Act
        string actual = target.ToLowerFirstLetter();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    #endregion

    #region ToPlural Tests

    [TestMethod]
    public void ToPlural_RegularNoun_AddS()
    {
        //cat - cats
        //house - houses
        //ton - tons

        //Arrange
        string expected1 = "cats";
        string target1 = "cat";

        string expected2 = "houses";
        string target2 = "house";

        string expected3 = "tons";
        string target3 = "ton";

        //Act
        string actual1 = target1.ToPlural();
        string actual2 = target2.ToPlural();
        string actual3 = target3.ToPlural();

        //Assert
        Assert.AreEqual(expected1, actual1);
        Assert.AreEqual(expected2, actual2);
        Assert.AreEqual(expected3, actual3);
    }

    [TestMethod]
    public void ToPlural_EndsInSOrVariant_AddEs()
    {
        //-s, -ss, -sh, -ch, -x, -z
        //iris - irises
        //truss - trusses
        //marsh - marshes
        //lunch - lunches
        //tax - taxes
        //blitz - blitzes

        //Arrange
        string expected1 = "irises";
        string target1 = "iris";

        string expected2 = "trusses";
        string target2 = "truss";

        string expected3 = "marshes";
        string target3 = "marsh";

        string expected4 = "lunches";
        string target4 = "lunch";

        string expected5 = "taxes";
        string target5 = "tax";

        string expected6 = "blitzes";
        string target6 = "blitz";

        //Act
        string actual1 = target1.ToPlural();
        string actual2 = target2.ToPlural();
        string actual3 = target3.ToPlural();
        string actual4 = target4.ToPlural();
        string actual5 = target5.ToPlural();
        string actual6 = target6.ToPlural();

        //Assert
        Assert.AreEqual(expected1, actual1);
        Assert.AreEqual(expected2, actual2);
        Assert.AreEqual(expected3, actual3);
        Assert.AreEqual(expected4, actual4);
        Assert.AreEqual(expected5, actual5);
        Assert.AreEqual(expected6, actual6);
    }

    [TestMethod]
    public void ToPlural_SomeEndsInSOrZ_DoubleSOrZAddEs()
    {
        //fez - fezzes

        //Arrange
        string expected1 = "fezzes";
        string target1 = "fez";

        //Act
        string actual1 = target1.ToPlural();

        //Assert
        Assert.AreEqual(expected1, actual1);
    }

    [TestMethod]
    public void ToPlural_EndsInF_ChangeToVeAddS()
    {
        //wife - wives
        //wolf - wolves
        //leaf - leaves
        //knife - knives
        //life - lives
        //loaf - loaves
        //self - selves

        //Arrange
        string expected1 = "wives";
        string target1 = "wife";

        string expected2 = "wolves";
        string target2 = "wolf";

        string expected3 = "leaves";
        string target3 = "leaf";

        string expected4 = "knives";
        string target4 = "knife";

        string expected5 = "lives";
        string target5 = "life";

        string expected6 = "loaves";
        string target6 = "loaf";

        string expected7 = "selves";
        string target7 = "self";

        //Act
        string actual1 = target1.ToPlural();
        string actual2 = target2.ToPlural();
        string actual3 = target3.ToPlural();
        string actual4 = target4.ToPlural();
        string actual5 = target5.ToPlural();
        string actual6 = target6.ToPlural();
        string actual7 = target7.ToPlural();

        //Assert
        Assert.AreEqual(expected1, actual1);
        Assert.AreEqual(expected2, actual2);
        Assert.AreEqual(expected3, actual3);
        Assert.AreEqual(expected4, actual4);
        Assert.AreEqual(expected5, actual5);
        Assert.AreEqual(expected6, actual6);
        Assert.AreEqual(expected7, actual7);
    }

    [TestMethod]
    public void ToPlural_EndsInFf_AddS()
    {
        //cliff - cliffs

        //Arrange
        string expected1 = "cliffs";
        string target1 = "cliff";

        //Act
        string actual1 = target1.ToPlural();

        //Assert
        Assert.AreEqual(expected1, actual1);
    }

    [TestMethod]
    public void ToPlural_EndsInFExceptions_AddS()
    {
        //roof - roofs
        //belief - beliefs
        //chef - chefs
        //chief - chiefs
        //dwarf - dwarfs
        //safe - safes
        //reef - reefs

        //Arrange
        string expected1 = "roofs";
        string target1 = "roof";

        string expected2 = "beliefs";
        string target2 = "belief";

        string expected3 = "chefs";
        string target3 = "chef";

        string expected4 = "chiefs";
        string target4 = "chief";

        string expected5 = "dwarfs";
        string target5 = "dwarf";

        string expected6 = "safes";
        string target6 = "safe";

        string expected7 = "reefs";
        string target7 = "reef";

        //Act
        string actual1 = target1.ToPlural();
        string actual2 = target2.ToPlural();
        string actual3 = target3.ToPlural();
        string actual4 = target4.ToPlural();
        string actual5 = target5.ToPlural();
        string actual6 = target6.ToPlural();
        string actual7 = target7.ToPlural();

        //Assert
        Assert.AreEqual(expected1, actual1);
        Assert.AreEqual(expected2, actual2);
        Assert.AreEqual(expected3, actual3);
        Assert.AreEqual(expected4, actual4);
        Assert.AreEqual(expected5, actual5);
        Assert.AreEqual(expected6, actual6);
        Assert.AreEqual(expected7, actual7);
    }

    [TestMethod]
    public void ToPlural_EndsInYWithConsonant_ChangeToIes()
    {
        //city - cities
        //puppy - puppies

        //Arrange
        string expected1 = "cities";
        string target1 = "city";

        string expected2 = "puppies";
        string target2 = "puppy";

        //Act
        string actual1 = target1.ToPlural();
        string actual2 = target2.ToPlural();

        //Assert
        Assert.AreEqual(expected1, actual1);
        Assert.AreEqual(expected2, actual2);
    }

    [TestMethod]
    public void ToPlural_EndsInYWithVowel_AddS()
    {
        //ray - rays
        //boy - boys

        //Arrange
        string expected1 = "rays";
        string target1 = "ray";

        string expected2 = "boys";
        string target2 = "boy";

        //Act
        string actual1 = target1.ToPlural();
        string actual2 = target2.ToPlural();

        //Assert
        Assert.AreEqual(expected1, actual1);
        Assert.AreEqual(expected2, actual2);
    }

    [TestMethod]
    public void ToPlural_EndsInO_AddEs()
    {
        //potato - potatoes
        //tomato - tomatoes

        //Arrange
        string expected1 = "potatoes";
        string target1 = "potato";

        string expected2 = "tomatoes";
        string target2 = "tomato";

        //Act
        string actual1 = target1.ToPlural();
        string actual2 = target2.ToPlural();

        //Assert
        Assert.AreEqual(expected1, actual1);
        Assert.AreEqual(expected2, actual2);
    }

    [TestMethod]
    public void ToPlural_EndsInOExceptions_AddS()
    {
        //photo - photos
        //piano - pianos
        //halo - halos

        //Arrange
        string expected1 = "photos";
        string target1 = "photo";

        string expected2 = "pianos";
        string target2 = "piano";

        string expected3 = "halos";
        string target3 = "halo";

        //Act
        string actual1 = target1.ToPlural();
        string actual2 = target2.ToPlural();
        string actual3 = target3.ToPlural();

        //Assert
        Assert.AreEqual(expected1, actual1);
        Assert.AreEqual(expected2, actual2);
        Assert.AreEqual(expected3, actual3);
    }

    [TestMethod]
    public void ToPlural_EndsInUs_ChangeToI()
    {
        //cactus - cacti
        //focus - foci

        //Arrange
        string expected1 = "cacti";
        string target1 = "cactus";

        string expected2 = "foci";
        string target2 = "focus";

        //Act
        string actual1 = target1.ToPlural();
        string actual2 = target2.ToPlural();

        //Assert
        Assert.AreEqual(expected1, actual1);
        Assert.AreEqual(expected2, actual2);
    }

    [TestMethod]
    public void ToPlural_EndsInIs_ChangeToEs()
    {
        //analysis - analyses
        //ellipsis - ellipses

        //Arrange
        string expected1 = "analyses";
        string target1 = "analysis";

        string expected2 = "ellipses";
        string target2 = "ellipsis";

        //Act
        string actual1 = target1.ToPlural();
        string actual2 = target2.ToPlural();

        //Assert
        Assert.AreEqual(expected1, actual1);
        Assert.AreEqual(expected2, actual2);
    }

    [TestMethod]
    public void ToPlural_EndsInOn_ChangeToA()
    {
        //phenomenon - phenomena
        //criterion - criteria

        //Arrange
        string expected1 = "phenomena";
        string target1 = "phenomenon";

        string expected2 = "criteria";
        string target2 = "criterion";

        //Act
        string actual1 = target1.ToPlural();
        string actual2 = target2.ToPlural();

        //Assert
        Assert.AreEqual(expected1, actual1);
        Assert.AreEqual(expected2, actual2);
    }

    [TestMethod]
    public void ToPlural_NoChange_NoChange()
    {
        //sheep - sheep
        //series - series
        //species - species
        //deer - deer
        //fish - fish

        //Arrange
        string expected1 = "sheep";
        string target1 = "sheep";

        string expected2 = "series";
        string target2 = "series";

        string expected3 = "species";
        string target3 = "species";

        string expected4 = "deer";
        string target4 = "deer";

        string expected5 = "fish";
        string target5 = "fish";

        //Act
        string actual1 = target1.ToPlural();
        string actual2 = target2.ToPlural();
        string actual3 = target3.ToPlural();
        string actual4 = target4.ToPlural();
        string actual5 = target5.ToPlural();

        //Assert
        Assert.AreEqual(expected1, actual1);
        Assert.AreEqual(expected2, actual2);
        Assert.AreEqual(expected3, actual3);
        Assert.AreEqual(expected4, actual4);
        Assert.AreEqual(expected5, actual5);
    }

    [TestMethod]
    public void ToPlural_Irregular_SpecialChanges()
    {
        //child - children
        //goose - geese
        //man - men
        //woman - women
        //tooth - teeth
        //foot - feet
        //mouse - mice
        //person - people

        //Arrange
        string expected1 = "children";
        string target1 = "child";

        string expected2 = "geese";
        string target2 = "goose";

        string expected3 = "men";
        string target3 = "man";

        string expected4 = "women";
        string target4 = "woman";

        string expected5 = "teeth";
        string target5 = "tooth";

        string expected6 = "feet";
        string target6 = "foot";

        string expected7 = "mice";
        string target7 = "mouse";
        
        string expected8 = "people";
        string target8 = "person";

        //Act
        string actual1 = target1.ToPlural();
        string actual2 = target2.ToPlural();
        string actual3 = target3.ToPlural();
        string actual4 = target4.ToPlural();
        string actual5 = target5.ToPlural();
        string actual6 = target6.ToPlural();
        string actual7 = target7.ToPlural();
        string actual8 = target8.ToPlural();

        //Assert
        Assert.AreEqual(expected1, actual1);
        Assert.AreEqual(expected2, actual2);
        Assert.AreEqual(expected3, actual3);
        Assert.AreEqual(expected4, actual4);
        Assert.AreEqual(expected5, actual5);
        Assert.AreEqual(expected6, actual6);
        Assert.AreEqual(expected7, actual7);
        Assert.AreEqual(expected8, actual8);
    }

   [TestMethod]
    public void ToPlural_Empty_ReturnEmpty()
    {
        //Arrange
        string expected = string.Empty;
        string target = string.Empty;

        //Act
        string actual = target.ToPlural();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ToPlural_Null_ReturnNull()
    {
        //Arrange
        string expected = null;
        string target = null;

        //Act
        string actual = target.ToPlural();

        //Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void ToPlural_SingleLetter_AddS()
    {
        //ofxzs
        //Arrange
        string expected1 = "As";
        string target1 = "A";

        string expected2 = "Os";
        string target2 = "O";

        string expected3 = "Fs";
        string target3 = "F";

        string expected4 = "Xs";
        string target4 = "X";

        string expected5 = "Zs";
        string target5 = "Z";

        string expected6 = "Ss";
        string target6 = "S";

        //Act
        string actual1 = target1.ToPlural();
        string actual2 = target2.ToPlural();
        string actual3 = target3.ToPlural();
        string actual4 = target4.ToPlural();
        string actual5 = target5.ToPlural();
        string actual6 = target6.ToPlural();

        //Assert
        Assert.AreEqual(expected1, actual1);
        Assert.AreEqual(expected2, actual2);
        Assert.AreEqual(expected3, actual3);
        Assert.AreEqual(expected4, actual4);
        Assert.AreEqual(expected5, actual5);
        Assert.AreEqual(expected6, actual6);
    }

    #endregion

    #region SplitIntoLines Tests

    [TestMethod]
    public void SplitIntoLines_Windows_Return3Lines()
    {
        //Arrange
        string[] expected = new string[]
        {
            "line1",
            "line2",
            "line3"
        };
        string target = "line1\r\nline2\r\nline3\r\n";

        //Act
        string[] actual = target.SplitIntoLines();

        //Assert
        Assert.AreEqual(expected.Length, actual.Length);
        Assert.AreEqual(expected[0], actual[0]);
        Assert.AreEqual(expected[1], actual[1]);
        Assert.AreEqual(expected[2], actual[2]);
    }

    [TestMethod]
    public void SplitIntoLines_Unix_Return3Lines()
    {
        //Arrange
        string[] expected = new string[]
        {
            "line1",
            "line2",
            "line3"
        };
        string target = "line1\nline2\nline3\n";

        //Act
        string[] actual = target.SplitIntoLines();

        //Assert
        Assert.AreEqual(expected.Length, actual.Length);
        Assert.AreEqual(expected[0], actual[0]);
        Assert.AreEqual(expected[1], actual[1]);
        Assert.AreEqual(expected[2], actual[2]);
    }

    [TestMethod]
    public void SplitIntoLines_MacClassic_Return3Lines()
    {
        //Arrange
        string[] expected = new string[]
        {
            "line1",
            "line2",
            "line3"
        };
        string target = "line1\rline2\rline3\r";

        //Act
        string[] actual = target.SplitIntoLines();

        //Assert
        Assert.AreEqual(expected.Length, actual.Length);
        Assert.AreEqual(expected[0], actual[0]);
        Assert.AreEqual(expected[1], actual[1]);
        Assert.AreEqual(expected[2], actual[2]);
    }

    [TestMethod]
    public void SplitIntoLines_WithoutLineEndingAtEnd_Return3Lines()
    {
        //Arrange
        string[] expected = new string[]
        {
            "line1",
            "line2",
            "line3"
        };
        string target = "line1\r\nline2\r\nline3";

        //Act
        string[] actual = target.SplitIntoLines();

        //Assert
        Assert.AreEqual(expected.Length, actual.Length);
        Assert.AreEqual(expected[0], actual[0]);
        Assert.AreEqual(expected[1], actual[1]);
        Assert.AreEqual(expected[2], actual[2]);
    }

    [TestMethod]
    public void SplitIntoLines_WithSingleLine_ReturnLine()
    {
        //Arrange
        string[] expected = new string[]
        {
            "line1"
        };
        string target = "line1\n";

        //Act
        string[] actual = target.SplitIntoLines();

        //Assert
        Assert.AreEqual(expected.Length, actual.Length);
        Assert.AreEqual(expected[0], actual[0]);
    }

    [TestMethod]
    public void SplitIntoLines_WithEmptyString_ReturnEmptyArray()
    {
        //Arrange
        string[] expected = System.Array.Empty<string>();
        string target = string.Empty;

        //Act
        string[] actual = target.SplitIntoLines();

        //Assert
        Assert.AreEqual(expected.Length, actual.Length);
    }

    [TestMethod]
    public void SplitIntoLines_WithNull_ReturnEmptyArray()
    {
        //Arrange
        string[] expected = System.Array.Empty<string>();
        string target = null;

        //Act
        string[] actual = target.SplitIntoLines();

        //Assert
        Assert.AreEqual(expected.Length, actual.Length);
    }

    #endregion
}
