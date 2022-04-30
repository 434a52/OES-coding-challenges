using OES.RobotWars.Interfaces;

namespace OES.RobotWars.Services
{
  public class RobotMoveValidationService : IRobotMoveValidationService
  {
    public bool IsMoveWithinArenaBoundaries(IArena arena, IRobot robot)
    {
      var moveTo = robot.Position.Move(robot.Orientation, +1);
      return
        moveTo.X >= arena.Boundary0?.X &&
        moveTo.Y >= arena.Boundary0?.Y &&
        moveTo.X <= arena.Boundary1?.X &&
        moveTo.Y <= arena.Boundary1?.Y;

    }
     
    public bool IsMoveToEmptyCoordinate(IArena arena, IRobot robot)
    {
      var moveTo = robot.Position.Move(robot.Orientation, +1);
      return !arena.Robots.Any(r => r.Position == moveTo);
    }
  }
}
