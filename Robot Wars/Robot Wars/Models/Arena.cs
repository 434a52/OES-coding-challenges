using OES.RobotWars.Interfaces;

namespace OES.RobotWars.Models
{
  public class Arena : IArena
  {
    public Coordinate? Boundary0 { get; set; }
    public Coordinate? Boundary1 { get; set; }
    public List<IRobot> Robots { get; } = new List<IRobot>();

    public void SetBoundaries(Coordinate boundary0, Coordinate boundary1)
    {
      Boundary0 = boundary0 ?? throw new ArgumentNullException(nameof(boundary0));
      Boundary1 = boundary1 ?? throw new ArgumentNullException(nameof(boundary1));
    }

    public void AddRobot(IRobot robot)
    {
      Robots.Add(robot);
    }
  }
}
