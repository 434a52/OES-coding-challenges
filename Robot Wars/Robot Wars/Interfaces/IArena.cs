using OES.RobotWars.Models;

namespace OES.RobotWars.Interfaces
{
  public interface IArena
  {
    Guid Id { get; }
    Coordinate Boundary0 { get; }
    Coordinate Boundary1 { get; }
    List<IRobot> Robots { get; }

    void SetBoundaries(Coordinate boundary0, Coordinate boundary1);
    void AddRobot(IRobot robot);
  }
}