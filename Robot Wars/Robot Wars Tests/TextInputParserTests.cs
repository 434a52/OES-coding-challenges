using Microsoft.VisualStudio.TestTools.UnitTesting;
using OES.RobotWars.Enums;
using OES.RobotWars.Models;
using OES.RobotWars.Services;
using System.Collections.Generic;
using System.Linq;
using static OES.RobotWars.Tests.Specification;

namespace OES.RobotWars.Tests
{
  [TestClass]
  public class TextInputParserTests
  {

    [TestMethod]
    public void TestParseArenaDimension()
    {
      var parser = new TextInputParserService();
      var coord = parser.ParseArenaDimension(Input.ArenaBoundaryString);
      Assert.AreEqual(Input.ArenaBoundary, coord);
    }

    [DataTestMethod]
    [DataRow("5 5")]
    [DataRow("5   5")]
    [DataRow(" 5 5 ")]
    [DataRow("  5   5  ")]
    public void TestParseArenaDimensionWithExtraSpaces(string input)
    {
      var parser = new TextInputParserService();
      var coord = parser.ParseArenaDimension(input);
      Assert.AreEqual(new Coordinate(5, 5), coord);
    }

    [TestMethod]
    public void TestParseRobot1InitialPosition()
    {
      var parser = new TextInputParserService();
      (Coordinate coordinate, Orientation orientation) = parser.ParseRobotInitialPosition(Input.Robot1InitialPositionString);
      Assert.AreEqual(Input.Robot1InitialPosition, coordinate);
      Assert.AreEqual(Input.Robot1InitialOrientation, orientation);
    }

    [TestMethod]
    public void TestParseRobot2InitialPosition()
    {
      var parser = new TextInputParserService();
      (Coordinate coordinate, Orientation orientation) = parser.ParseRobotInitialPosition(Input.Robot2InitialPositionString);
      Assert.AreEqual(Input.Robot2InitialPosition, coordinate);
      Assert.AreEqual(Input.Robot2InitialOrientation, orientation);
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