using OES.RobotWars.Enums;
using OES.RobotWars.Models;

namespace OES.RobotWars.Interfaces
{
  public interface ITextInputParserService
  {
    Coordinate? ParseArenaDimension(string? input);
    (Coordinate, Orientation) ParseRobotInitialPosition(string? input);
    IEnumerable<RobotInstruction> ParseRobotInstructions(string? input);
  }
}