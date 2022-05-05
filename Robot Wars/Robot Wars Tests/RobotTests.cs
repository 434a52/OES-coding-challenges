using Microsoft.VisualStudio.TestTools.UnitTesting;
using OES.RobotWars.Enums;
using OES.RobotWars.Models;

namespace OES.RobotWars.Tests
{
  [TestClass]
  public class RobotTests
  {
    [TestMethod]
    public void TestRobotMove()
    {
      var robot = new Robot(new Coordinate(1, 1), Orientation.North);
      robot.Move();
      Assert.AreEqual(new Coordinate(1, 2), robot.Position);
    }

    [DataTestMethod]
    [DataRow(0, 3, DisplayName = "Rotate Left from North")]
    [DataRow(1, 0, DisplayName = "Rotate Left from East")]
    [DataRow(2, 1, DisplayName = "Rotate Left from South")]
    [DataRow(3, 2, DisplayName = "Rotate Left from West")]
    public void TestRobotRotateLeft(int startOrientation, int expectedOrientation)
    {
      var robot = new Robot(new Coordinate(1, 1), (Orientation)startOrientation);
      robot.RotateLeft();
      Assert.AreEqual((Orientation)expectedOrientation, robot.Orientation);
    }

    [DataTestMethod]
    [DataRow(0, 1, DisplayName = "Rotate Right from North")]
    [DataRow(1, 2, DisplayName = "Rotate Right from East")]
    [DataRow(2, 3, DisplayName = "Rotate Right from South")]
    [DataRow(3, 0, DisplayName = "Rotate Right from West")]
    public void TestRobotRotateRight(int startOrientation, int expectedOrientation)
    {
      var robot = new Robot(new Coordinate(1, 1), (Orientation)startOrientation);
      robot.RotateRight();
      Assert.AreEqual((Orientation)expectedOrientation, robot.Orientation);
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

  }
}