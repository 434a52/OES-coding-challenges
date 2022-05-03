using OES.RobotWars.Models;

namespace OES.RobotWars.Interfaces
{
  public interface IArenaValidationService
  {
    bool IsArenaCoordinateEmpty(IArena arena, Coordinate coordinate);
    bool IsRobotWithinArenaBoundaries(IArena arena, IRobot robot);
    bool IsMoveWithinArenaBoundaries(IArena arena, IRobot robot);
    bool IsMoveToEmptyCoordinate(IArena arena, IRobot robot);
  }
}
