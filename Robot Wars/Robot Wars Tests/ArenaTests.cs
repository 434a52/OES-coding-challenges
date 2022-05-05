using Microsoft.VisualStudio.TestTools.UnitTesting;
using OES.RobotWars.Enums;
using OES.RobotWars.Models;
using OES.RobotWars.Services;

namespace OES.RobotWars.Tests
{
  [TestClass]
  public class ArenaTests
  {
    [DataTestMethod]
    [DataRow(0, 0, 3, DisplayName = "Test Arena Left Boundary")]
    [DataRow(0, 0, 2, DisplayName = "Test Arena Bottom Boundary")]
    [DataRow(5, 5, 0, DisplayName = "Test Arena Right Boundary")]
    [DataRow(5, 5, 1, DisplayName = "Test Arena Top Boundary")]
    public void TestArenaBoundaries(int x, int y, int orientation)
    {
      (var arena, var robot, var validator) = CreateTestSubjects(new Coordinate(x, y), (Orientation)orientation);
      Assert.IsFalse(validator.IsMoveWithinArenaBoundaries(arena, robot));
    }

    [TestMethod]
    public void TestRobotCollision()
    {
      var robot1 = new Robot(new Coordinate(2, 2), Orientation.North);
      var robot2 = new Robot(new Coordinate(2, 3), Orientation.North);
      var arena = new Arena();
      arena.SetBoundaries(new Coordinate(0, 0), new Coordinate(5, 5));
      arena.AddRobot(robot1);
      arena.AddRobot(robot2);

      var validator = new ArenaValidationService();
      Assert.IsFalse(validator.IsMoveToEmptyCoordinate(arena, robot1));
    }

    static private (Arena, Robot, ArenaValidationService) CreateTestSubjects(Coordinate initialRobotPosition, Orientation initialRobotOrientation)
    {
      var arena = new Arena();
      var robot = new Robot(initialRobotPosition, initialRobotOrientation);
      var validator = new ArenaValidationService();
      arena.SetBoundaries(new Coordinate(0, 0), new Coordinate(5, 5));
      return (arena, robot, validator);
    }

  }
}