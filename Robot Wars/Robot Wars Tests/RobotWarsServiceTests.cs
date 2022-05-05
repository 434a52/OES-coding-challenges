using Microsoft.VisualStudio.TestTools.UnitTesting;
using OES.RobotWars.Enums;
using OES.RobotWars.Interfaces;
using OES.RobotWars.Models;
using System;
using System.Collections.Generic;
using static OES.RobotWars.Tests.Specification;

namespace OES.RobotWars.Tests
{
  [TestClass]
  public class RobotWarsServiceTests
  {

    [TestMethod]
    public void TestRobotWarsServiceTextInterface()
    {
      var robotWarsService = RobotWars.CreateService();
      if (robotWarsService is null) {
        Assert.Fail("Failed to create RobotWarsService instance");
      } else {
        var inputParser = robotWarsService.GetTextInputParserService();
        try {
          var arenaBoundary1 = inputParser.ParseArenaDimension(Input.ArenaBoundaryString);
          (Coordinate robot1InitialPosition, Orientation robot1InitialOrientation) = inputParser.ParseRobotInitialPosition(Input.Robot1InitialPositionString);
          var robot1Instructions = inputParser.ParseRobotInstructions(Input.Robot1InstructionsString);
          (Coordinate robot2InitialPosition, Orientation robot2InitialOrientation) = inputParser.ParseRobotInitialPosition(Input.Robot2InitialPositionString);
          var robot2Instructions = inputParser.ParseRobotInstructions(Input.Robot2InstructionsString);

          (IRobot robot1, IRobot robot2) = TestRobotWarsService(
            robotWarsService,
            arenaBoundary1,
            robot1InitialPosition,
            robot1InitialOrientation,
            robot2InitialPosition,
            robot2InitialOrientation,
            robot1Instructions,
            robot2Instructions
          );

          TestRobot1FinalPosition(robot1);
          TestRobot2FinalPosition(robot2);

          Assert.IsTrue(robot1.ToString() == Output.Robot1FinalPositionString);
          Assert.IsTrue(robot2.ToString() == Output.Robot2FinalPositionString);
        } catch (Exception exception) {
          Assert.Fail(exception.Message);
        }
      }
    }

    [TestMethod]
    public void TestRobotWarsServiceObjectInterface()
    {
      var robotWarsService = RobotWars.CreateService();
      if (robotWarsService is null) {
        Assert.Fail("Failed to create RobotWarsService instance");
      } else {
        try {
          (IRobot robot1, IRobot robot2) = TestRobotWarsService(
            robotWarsService,
            Input.ArenaBoundary,
            Input.Robot1InitialPosition, 
            Input.Robot1InitialOrientation,
            Input.Robot2InitialPosition, 
            Input.Robot2InitialOrientation,
            Input.Robot1Instructions,
            Input.Robot2Instructions
          );

          TestRobot1FinalPosition(robot1);
          TestRobot2FinalPosition(robot2);
        } catch (Exception exception) {
          Assert.Fail(exception.Message);
        }
      }
    }

    static private (IRobot robot1, IRobot robot2) TestRobotWarsService(
      IRobotWarsService robotWarsService,
      Coordinate arenaBoundary,
      Coordinate robot1InitialPosition,
      Orientation robot1InitialOrientation,
      Coordinate robot2InitialPosition,
      Orientation robot2InitialOrientation,
      IEnumerable<RobotInstruction> robot1Instructions,
      IEnumerable<RobotInstruction> robot2Instructions)
    {
      robotWarsService.SetArenaBoundaries(new Coordinate(0, 0), arenaBoundary);
      var robot1 = robotWarsService.CreateRobot(robot1InitialPosition, robot1InitialOrientation);
      robotWarsService.InstructRobot(robot1, robot1Instructions);
      var robot2 = robotWarsService.CreateRobot(robot2InitialPosition, robot2InitialOrientation);
      robotWarsService.InstructRobot(robot2, robot2Instructions);
      return (robot1, robot2);
    }

    static private void TestRobot1FinalPosition(IRobot robot1)
    {
      Assert.IsTrue(robot1.Position == Output.Robot1FinalPosition, $"Robot1 final position; expected {Output.Robot1FinalPosition}, actual {robot1.Position}");
      Assert.IsTrue(robot1.Orientation == Output.Robot1FinalOrientation, $"Robot1 final orientation; expected {Output.Robot1FinalOrientation}, actual {robot1.Orientation}");
    }

    static private void TestRobot2FinalPosition(IRobot robot2)
    {
      Assert.IsTrue(robot2.Position == Output.Robot2FinalPosition, $"Robot2 final position; expected {Output.Robot2FinalPosition}, actual {robot2.Position}");
      Assert.IsTrue(robot2.Orientation == Output.Robot2FinalOrientation, $"Robot2 final orientation; expected {Output.Robot2FinalOrientation}, actual {robot2.Orientation}");
    }

  }
}