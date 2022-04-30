using OES.RobotWars.Enums;

namespace OES.RobotWars.Models
{
  public class Coordinate : IEquatable<Coordinate?>
  {
    public int X { get; set; }
    public int Y { get; set; }
    public Coordinate(int x, int y) { X = x; Y = y; }

    public Coordinate Move(int deltaX, int deltaY)
    {
      return new Coordinate(X + deltaX, Y + deltaY);
    }

    public Coordinate Move(Orientation orientation, int delta)
    {
      if (orientation is Orientation.North) {
        return new Coordinate(X, Y + delta);
      } else if (orientation is Orientation.East) {
        return new Coordinate(X + delta, Y);
      } else if (orientation is Orientation.South) {
        return new Coordinate(X, Y - delta);
      } else if (orientation is Orientation.West) {
        return new Coordinate(X - delta, Y);
      } else {
        return new Coordinate(X, Y);
      }
    }

    public override bool Equals(object? obj)
    {
      return Equals(obj as Coordinate);
    }

    public bool Equals(Coordinate? other)
    {
      return other != null &&
             X == other.X &&
             Y == other.Y;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(X, Y);
    }

    public static bool operator ==(Coordinate? left, Coordinate? right)
    {
      return EqualityComparer<Coordinate>.Default.Equals(left, right);
    }

    public static bool operator !=(Coordinate? left, Coordinate? right)
    {
      return !(left == right);
    }

    public override string ToString()
    {
      return $"{X} {Y}";
    }

  }
}
