using OES.RobotWars.Interfaces;
using OES.RobotWars.Models;

namespace OES.RobotWars.Services
{
  public class ArenaValidationService : IArenaValidationService
  {
    public bool IsArenaCoordinateEmpty(IArena arena, Coordinate coordinate)
    {
      return IsCoordinateEmpty(arena, coordinate);
    }

    public bool IsRobotWithinArenaBoundaries(IArena arena, IRobot robot)
    {
      return IsCoordinateWithinArenaBoundaries(arena, robot.Position);
    }

    public bool IsMoveWithinArenaBoundaries(IArena arena, IRobot robot)
    {
      var moveTo = robot.Position.Move(robot.Orientation, +1);
      return IsCoordinateWithinArenaBoundaries(arena, moveTo);
    }
     
    public bool IsMoveToEmptyCoordinate(IArena arena, IRobot robot)
    {
      var moveTo = robot.Position.Move(robot.Orientation, +1);
      return IsCoordinateEmpty(arena, moveTo);
    }

    private static bool IsCoordinateEmpty(IArena arena, Coordinate coordinate)
    {
      return !arena.Robots.Any(r => r.Position == coordinate);
    }

    private static bool IsCoordinateWithinArenaBoundaries(IArena arena, Coordinate coordinate)
    {
      return
        coordinate.X >= arena.Boundary0.X &&
        coordinate.Y >= arena.Boundary0.Y &&
        coordinate.X <= arena.Boundary1.X &&
        coordinate.Y <= arena.Boundary1.Y;
    }

  }
}
