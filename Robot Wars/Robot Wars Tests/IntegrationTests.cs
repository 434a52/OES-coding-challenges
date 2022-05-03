using Microsoft.VisualStudio.TestTools.UnitTesting;
using OES.RobotWars.Enums;
using OES.RobotWars.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OES.RobotWars.Tests
{
  [TestClass]
  public class IntegrationTests
  {

    [TestMethod]
    public void TestRobotWarsServiceWithInputParsing()
    {
      var robotWarsService = RobotWars.CreateService();
      if (robotWarsService is null) {
        Assert.Fail("Failed to create RobotWarsService instance");
      } else {
        var inputParser = robotWarsService.GetTextInputParserService();
        try {
          var arenaBoundary1 = inputParser.ParseArenaDimension("5 5");
          if (arenaBoundary1 is null) {
            Assert.Fail("Failed to parse Arena boundary");
          } else {
            robotWarsService.SetArenaBoundaries(new Coordinate(0, 0), arenaBoundary1);
            (Coordinate robot1Coordinate, Orientation robot1Orientation) = inputParser.ParseRobotInitialPosition("1 2 N");
            var robot1Instructions = inputParser.ParseRobotInstructions("LMLMLMLMM");
            (Coordinate robot2Coordinate, Orientation robot2Orientation) = inputParser.ParseRobotInitialPosition("3 3 E");
            var robot2Instructions = inputParser.ParseRobotInstructions("MMRMMRMRRM");
            var robot1 = robotWarsService.CreateRobot(robot1Coordinate, robot1Orientation);
            var robot2 = robotWarsService.CreateRobot(robot2Coordinate, robot2Orientation);
            robotWarsService.InstructRobot(robot1, robot1Instructions);
            robotWarsService.InstructRobot(robot2, robot2Instructions);

            Assert.IsTrue(robot1.Position.X == 1, $"Robot1.Position.X; expected 1, actual {robot1.Position.X}");
            Assert.IsTrue(robot1.Position.Y == 3, $"Robot1.Position.Y; expected 3, actual {robot1.Position.Y}");
            Assert.IsTrue(robot1.Orientation == Orientation.North, $"Robot1.Orientation; expected North, actual {robot1.Orientation}");
            Assert.IsTrue(robot2.Position.X == 5, $"Robot2.Position.X; expected 5, actual {robot2.Position.X}");
            Assert.IsTrue(robot2.Position.Y == 1, $"Robot2.Position.Y; expected 1, actual {robot2.Position.Y}");
            Assert.IsTrue(robot2.Orientation == Orientation.East, $"Robot2.Orientation; expected East, actual {robot2.Orientation}");
          }
        } catch (Exception exception) {
          Assert.Fail(exception.Message);
        }
      }
    }

    [TestMethod]
    public void TestRobotWarsServiceWithoutInputParsing()
    {
      var robotWarsService = RobotWars.CreateService();
      if (robotWarsService is null) {
        Assert.Fail("Failed to create RobotWarsService instance");
      } else {
        robotWarsService.SetArenaBoundaries(new Coordinate(0, 0), new Coordinate(5, 5));
        var robot1 = robotWarsService.CreateRobot(new Coordinate(1, 2), Orientation.North);
        var robot2 = robotWarsService.CreateRobot(new Coordinate(3, 3), Orientation.East);
        var robot1Instructions = new List<RobotInstruction> {
          RobotInstruction.RotateLeft,
          RobotInstruction.Move,
          RobotInstruction.RotateLeft,
          RobotInstruction.Move,
          RobotInstruction.RotateLeft,
          RobotInstruction.Move,
          RobotInstruction.RotateLeft,
          RobotInstruction.Move,
          RobotInstruction.Move
        };
        var robot2Instructions = new List<RobotInstruction> {
          RobotInstruction.Move,
          RobotInstruction.Move,
          RobotInstruction.RotateRight,
          RobotInstruction.Move,
          RobotInstruction.Move,
          RobotInstruction.RotateRight,
          RobotInstruction.Move,
          RobotInstruction.RotateRight,
          RobotInstruction.RotateRight,
          RobotInstruction.Move
        };
        try {
          robotWarsService.InstructRobot(robot1, robot1Instructions);
          robotWarsService.InstructRobot(robot2, robot2Instructions);

          Assert.IsTrue(robot1.Position.X == 1, $"Robot1.Position.X; expected 1, actual {robot1.Position.X}");
          Assert.IsTrue(robot1.Position.Y == 3, $"Robot1.Position.Y; expected 3, actual {robot1.Position.Y}");
          Assert.IsTrue(robot1.Orientation == Orientation.North, $"Robot1.Orientation; expected North, actual {robot1.Orientation}");
          Assert.IsTrue(robot2.Position.X == 5, $"Robot2.Position.X; expected 5, actual {robot2.Position.X}");
          Assert.IsTrue(robot2.Position.Y == 1, $"Robot2.Position.Y; expected 1, actual {robot2.Position.Y}");
          Assert.IsTrue(robot2.Orientation == Orientation.East, $"Robot2.Orientation; expected East, actual {robot2.Orientation}");
        } catch (Exception exception) {
          Assert.Fail(exception.Message);
        }
      }
    }
  }

}