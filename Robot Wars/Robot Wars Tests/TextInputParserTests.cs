using Microsoft.VisualStudio.TestTools.UnitTesting;
using OES.RobotWars.Enums;
using OES.RobotWars.Models;
using OES.RobotWars.Services;
using System.Collections.Generic;
using System.Linq;

namespace OES.RobotWars.Tests
{
  [TestClass]
  public class TextInputParserTests
  {

    /* INPUT
    5 5
    1 2 N
    LMLMLMLMM
    3 3 E
    MMRMMRMRRM 
    */

    /* OUTPUT
    1 3 N
    5 1 E
    */

    [TestMethod]
    public void TestParseArenaDimension()
    {
      var parser = new TextInputParserService();
      var coord = parser.ParseArenaDimension("5 5");
      Assert.AreEqual(new Coordinate(5, 5), coord);
    }

    [TestMethod]
    public void TestParseArenaDimensionWithExtraSpaces()
    {
      var parser = new TextInputParserService();
      var coord = parser.ParseArenaDimension(" 5   5 ");
      Assert.AreEqual(new Coordinate(5, 5), coord);
    }

    [TestMethod]
    public void TestParseRobot1InitialPosition()
    {
      var parser = new TextInputParserService();
      (Coordinate coordinate, Orientation orientation) = parser.ParseRobotInitialPosition("1 2 N");
      Assert.AreEqual(new Coordinate(1, 2), coordinate);
      Assert.AreEqual(Orientation.North, orientation);
    }

    [TestMethod]
    public void TestParseRobot2InitialPosition()
    {
      var parser = new TextInputParserService();
      (Coordinate coordinate, Orientation orientation) = parser.ParseRobotInitialPosition("3 3 E");
      Assert.AreEqual(new Coordinate(3, 3), coordinate);
      Assert.AreEqual(Orientation.East, orientation);
    }

    [TestMethod]
    public void TestInstructionParser()
    {
      var parser = new TextInputParserService();
      var parsedInstructions = parser.ParseRobotInstructions("LRM").ToList();
      var expectedInstructions = new List<RobotInstruction> {
        RobotInstruction.RotateLeft,
        RobotInstruction.RotateRight,
        RobotInstruction.Move
      };
      Assert.IsTrue(expectedInstructions.SequenceEqual(parsedInstructions));
    }

  }
}