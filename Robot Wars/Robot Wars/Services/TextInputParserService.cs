using OES.RobotWars.Enums;
using OES.RobotWars.Interfaces;
using OES.RobotWars.Models;

namespace OES.RobotWars.Services
{
  public class TextInputParserService : ITextInputParserService
  {
    private readonly Dictionary<char, Orientation> orientationLookup = new() {
      { 'N', Orientation.North },
      { 'E', Orientation.East },
      { 'S', Orientation.South },
      { 'W', Orientation.West }
    };

    public Coordinate? ParseArenaDimension(string? input)
    {
      if (string.IsNullOrEmpty(input?.Trim())) {
        throw new ArgumentNullException(nameof(input), "Invalid Arena dimension");
      } else {
        var splits = input.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        return new Coordinate(int.Parse(splits[0]), int.Parse(splits[1]));
      }
    }

    public (Coordinate, Orientation) ParseRobotInitialPosition(string? input)
    {
      if (string.IsNullOrEmpty(input?.Trim())) {
        throw new ArgumentNullException(nameof(input), "Invalid Arena dimension");
      } else {
        var splits = input.ToUpper().Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        if (splits.Length != 3) {
          throw new FormatException(nameof(input));
        } else {
          var coordinate = new Coordinate(int.Parse(splits[0]), int.Parse(splits[1]));
          if (orientationLookup.TryGetValue(splits[2][0], out var orientation)) {
            return (coordinate, orientation);
          } else {
            throw new FormatException(nameof(input));
          }
        }
      }
    }

    public IEnumerable<RobotInstruction> ParseRobotInstructions(string? input)
    {
      if (string.IsNullOrEmpty(input?.Trim())) {
        yield break;
      } else {
        foreach (var item in input.Trim().ToUpper()) {
          if (item == 'L') {
            yield return RobotInstruction.RotateLeft;
          } else if (item == 'R') {
            yield return RobotInstruction.RotateRight;
          } else if (item == 'M') {
            yield return RobotInstruction.Move;
          } else {
            throw new ArgumentException($"Invalid robot instruction; {item}. Only L, R & M are accepted");
          }
        }
      }
    }

  }
}
