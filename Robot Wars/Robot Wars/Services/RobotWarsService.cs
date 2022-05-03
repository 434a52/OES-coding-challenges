using OES.RobotWars.Enums;
using OES.RobotWars.Interfaces;
using OES.RobotWars.Models;

namespace OES.RobotWars.Services
{
  public sealed class RobotWarsService : IRobotWarsService
  {

    private readonly IArena _arena;
    private readonly IArenaValidationService _arenaValidationService;
    private readonly ITextInputParserService _textInputParserService;

    public RobotWarsService(IArena arena, IArenaValidationService arenaValidationService, ITextInputParserService textInputParserService)
    {
      _arena = arena;
      _arenaValidationService = arenaValidationService;
      _textInputParserService = textInputParserService;
    }

    public void SetArenaBoundaries(Coordinate boundary0, Coordinate boundary1)
    {
      _arena.SetBoundaries(boundary0, boundary1);
    }

    public IRobot CreateRobot(Coordinate coordinate, Orientation orientation)
    {
      var robot = new Robot(coordinate, orientation);
      if (!_arenaValidationService.IsRobotWithinArenaBoundaries(_arena, robot)) {
        throw new InvalidOperationException("Unable to create robot outside of arena boundaries");
      } else if (!_arenaValidationService.IsArenaCoordinateEmpty(_arena, coordinate)) {
        throw new InvalidOperationException("Unable to create the robot, the coordinate is not available");
      } else {
        _arena.AddRobot(robot);
        return robot;
      }
    }

    public void InstructRobot(IRobot robot, IEnumerable<RobotInstruction> instructions)
    {
      foreach (var instruction in instructions) {
        InstructRobot(robot, instruction);
      }
    }

    public void InstructRobot(IRobot robot, RobotInstruction instruction)
    {
      if (instruction is RobotInstruction.RotateLeft) {
        robot.RotateLeft();
      } else if (instruction is RobotInstruction.RotateRight) {
        robot.RotateRight();
      } else if (instruction is RobotInstruction.Move) {
        if (!_arenaValidationService.IsMoveWithinArenaBoundaries(_arena, robot)) {
          throw new InvalidOperationException("Unable to move the robot outside arena boundaries");
        } else if (!_arenaValidationService.IsMoveToEmptyCoordinate(_arena, robot)) {
          throw new InvalidOperationException("Unable to move the robot, the coordinate is not available");
        } else {
          robot.Move();
        }
      }
    }

    public IEnumerable<IRobot> GetRobots()
    {
      return _arena.Robots;
    }

    public ITextInputParserService GetTextInputParserService()
    {
      return _textInputParserService;
    }

  }
}
