namespace OES.RobotWars.Interfaces
{
  public interface IRobotMoveValidationService
  {
    bool IsMoveWithinArenaBoundaries(IArena arena, IRobot robot);
    bool IsMoveToEmptyCoordinate(IArena arena, IRobot robot);
  }
}
