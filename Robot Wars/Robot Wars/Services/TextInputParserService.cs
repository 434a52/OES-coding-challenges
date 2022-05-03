using OES.RobotWars.Enums;
using OES.RobotWars.Interfaces;
using OES.RobotWars.Models;
using System.Text.RegularExpressions;

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

    public Coordinate ParseArenaDimension(string? input)
    {
      var coords = CleanInputString(input, collapseWhitespace: true);
      if (string.IsNullOrWhiteSpace(coords)) {
        throw new ArgumentNullException(nameof(input), "Invalid Arena dimension");
      } else {
        var splits = GetArgumentParts(coords);
        if (splits.Length != 2) {
          throw new ArgumentException("Invalid Arena dimension", nameof(input));
        } else {
          return CreateCoordinate(splits[0], splits[1]);
        }
      }
    }

    public (Coordinate, Orientation) ParseRobotInitialPosition(string? input)
    {
      var robotPosition = CleanInputString(input, collapseWhitespace: true);
      if (string.IsNullOrWhiteSpace(robotPosition)) {
        throw new ArgumentNullException(nameof(input), "Invalid Robot Position");
      } else {
        var splits = GetArgumentParts(robotPosition);
        if (splits.Length != 3) {
          throw new ArgumentException("Invalid Robot position", nameof(input));
        } else {
          var coordinate = CreateCoordinate(splits[0], splits[1]);
          if (orientationLookup.TryGetValue(splits[2][0], out var orientation)) {
            return (coordinate, orientation);
          } else {
            throw new ArgumentException("Invalid Robot orientation", nameof(input));
          }
        }
      }
    }

    public IEnumerable<RobotInstruction> ParseRobotInstructions(string? input)
    {
      var instruction = CleanInputString(input, removeAllWhitespace: true);
      if (string.IsNullOrWhiteSpace(instruction)) {
        yield break;
      } else {
        foreach (var item in instruction) {
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

    private static Coordinate CreateCoordinate(string input0, string input1)
    {
      if (int.TryParse(input0, out var x)) {
        if (int.TryParse(input1, out var y)) {
          if (x < 0) {
            throw new ArgumentException("Invalid Arena X dimension; Coordinates should be greater than 0", nameof(input0));
          } else if (y < 0) {
            throw new ArgumentException("Invalid Arena Y dimension; Coordinates should be greater than 0", nameof(input1));
          } else {
            return new Coordinate(x, y);
          }
        } else {
          throw new ArgumentException("Invalid Arena Y dimension", nameof(input1));
        }
      } else {
        throw new ArgumentException("Invalid Arena X dimension", nameof(input0));
      }
    }

    private static string CleanInputString(string? input, bool collapseWhitespace = false, bool removeAllWhitespace = false)
    {
      if (input is null) {
        return string.Empty;
      } else if (removeAllWhitespace) {
        input = Regex.Replace(input, @"\s+", "");
      } else if (collapseWhitespace) {
        input = Regex.Replace(input, @"\s{2,}", " ");
      }
      return input.Trim().ToUpper();
    }

    private static string[] GetArgumentParts(string input)
    {
      return input.ToUpper().Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    }

  }
}
