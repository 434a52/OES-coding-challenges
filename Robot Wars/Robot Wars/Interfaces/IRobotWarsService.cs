using OES.RobotWars.Enums;
using OES.RobotWars.Models;

namespace OES.RobotWars.Interfaces
{
  public interface IRobotWarsService
  {
    void SetArenaBoundaries(Coordinate boundary0, Coordinate boundary1);
    IRobot CreateRobot(Coordinate coordinate, Orientation orientation);
    void InstructRobot(IRobot robot, RobotInstruction instruction);
    IEnumerable<IRobot> GetRobots();
    ITextInputParserService GetTextInputParserService();
  }
}
