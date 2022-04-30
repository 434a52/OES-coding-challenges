using OES.RobotWars.Models;

namespace OES.RobotWars.Interfaces
{
  public interface IArena
  {
    Coordinate? Boundary0 { get; set; }
    Coordinate? Boundary1 { get; set; }
    List<IRobot> Robots { get; }

    void SetBoundaries(Coordinate boundary0, Coordinate boundary1);
    void AddRobot(IRobot robot);
  }
}