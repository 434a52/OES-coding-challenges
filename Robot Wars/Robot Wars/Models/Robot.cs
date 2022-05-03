using OES.RobotWars.Enums;
using OES.RobotWars.Interfaces;

namespace OES.RobotWars.Models
{
  public class Robot : IRobot
  {
    public Guid Id { get; set; }
    public Coordinate Position { get; set; }
    public Orientation Orientation { get; set; }

    public Robot(Coordinate position, Orientation orientation)
    {
      Id = Guid.NewGuid();
      Position = position;
      Orientation = orientation;
    }

    public void Do(IEnumerable<RobotInstruction> instructions)
    {
      foreach (var instruction in instructions) {
        if (instruction is RobotInstruction.RotateLeft) {
          RotateLeft();
        } else if (instruction is RobotInstruction.RotateRight) {
          RotateRight();
        } else if (instruction is RobotInstruction.Move) {
          Move();
        }
      }
    }

    public void RotateLeft()
    {
      if (Orientation is Orientation.North) {
        Orientation = Orientation.West;
      } else {
        Orientation = (Orientation)(int)Orientation - 1;
      }
    }

    public void RotateRight()
    {
      if (Orientation is Orientation.West) {
        Orientation = Orientation.North;
      } else {
        Orientation = (Orientation)(int)Orientation + 1;
      }
    }

    public void Move()
    {
      Position = Position.Move(Orientation, +1);
    }

    public override string ToString()
    {
      return $"{Position} {Orientation.ToString()[..1]}";
    }

  }
}
