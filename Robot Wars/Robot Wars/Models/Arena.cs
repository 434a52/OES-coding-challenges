using OES.RobotWars.Interfaces;

namespace OES.RobotWars.Models
{
  public class Arena : IArena
  {
    public Guid Id { get; }
    public Coordinate Boundary0 { get; private set; } = new Coordinate(0, 0);
    public Coordinate Boundary1 { get; private set; } = new Coordinate(10, 10);
    public List<IRobot> Robots { get; } = new List<IRobot>();

    public Arena()
    {
      Id = Guid.NewGuid();
    }

    public void SetBoundaries(Coordinate boundary0, Coordinate boundary1)
    {
      Boundary0 = boundary0 ?? throw new ArgumentNullException(nameof(boundary0));
      Boundary1 = boundary1 ?? throw new ArgumentNullException(nameof(boundary1));
      if (Boundary0.X > Boundary1.X || Boundary0.Y > Boundary1.Y) {
        throw new ArgumentOutOfRangeException(nameof(boundary1), "Invalid Arena Boundary");
      }
    }

    public void AddRobot(IRobot robot)
    {
      Robots.Add(robot);
    }
  }
}
