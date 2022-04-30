using Microsoft.VisualStudio.TestTools.UnitTesting;
using OES.RobotWars.Enums;
using OES.RobotWars.Models;

namespace OES.RobotWars.Tests
{
  [TestClass]
  public class RobotUnitTests
  {
    [TestMethod]
    public void TestRobotMove()
    {
      var robot = new Robot(new Coordinate(1, 1), Orientation.North);
      robot.Move();
      Assert.AreEqual(new Coordinate(1, 2), robot.Position);
    }

    [TestMethod]
    public void TestRobotMoveSequence()
    {
      var robot = new Robot(new Coordinate(3, 3), Orientation.East);
      // MMRMMRMRRM
      robot.Move();
      robot.Move();
      robot.RotateRight();
      robot.Move();
      robot.Move();
      robot.RotateRight();
      robot.Move();
      robot.RotateRight();
      robot.RotateRight();
      robot.Move();
      // 5 1 E
      Assert.AreEqual(new Coordinate(5, 1), robot.Position);
      Assert.AreEqual(Orientation.East, robot.Orientation);
    }

    [TestMethod]
    public void TestRobotRotateLeftSimpleCase()
    {
      var robot = new Robot(new Coordinate(1, 1), Orientation.South);
      robot.RotateLeft();
      Assert.AreEqual(Orientation.East, robot.Orientation);
    }

    [TestMethod]
    public void TestRobotRotateLeftEdgeCase()
    {
      var robot = new Robot(new Coordinate(1, 1), Orientation.North);
      robot.RotateLeft();
      Assert.AreEqual(Orientation.West, robot.Orientation);
    }

    [TestMethod]
    public void TestRobotRotateRightSimpleCase()
    {
      var robot = new Robot(new Coordinate(1, 1), Orientation.North);
      robot.RotateRight();
      Assert.AreEqual(Orientation.East, robot.Orientation);
    }

    [TestMethod]
    public void TestRobotRotateRightEdgeCase()
    {
      var robot = new Robot(new Coordinate(1, 1), Orientation.West);
      robot.RotateRight();
      Assert.AreEqual(Orientation.North, robot.Orientation);
    }
  }
}