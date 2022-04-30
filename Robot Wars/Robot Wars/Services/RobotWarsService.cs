using OES.RobotWars.Enums;
using OES.RobotWars.Interfaces;
using OES.RobotWars.Models;

namespace OES.RobotWars.Services
{
  public sealed class RobotWarsService : IRobotWarsService
  {
    private const string ArenaNotInitialised = "Arena has not been initialised";
    private readonly IArena _arena;
    private readonly IRobotMoveValidationService _robotMoveValidationService;
    private readonly ITextInputParserService _textInputParserService;

    public RobotWarsService(IArena arena, IRobotMoveValidationService robotMoveValidationService, ITextInputParserService textInputParserService)
    {
      _arena = arena;
      _robotMoveValidationService = robotMoveValidationService;
      _textInputParserService = textInputParserService;
    }

    public void SetArenaBoundaries(Coordinate boundary0, Coordinate boundary1)
    {
      _arena.SetBoundaries(boundary0, boundary1);
    }

    public IRobot CreateRobot(Coordinate coordinate, Orientation orientation)
    {
      if (_arena is not null) {
        var robot = new Robot(coordinate, orientation);
        _arena.AddRobot(robot);
        return robot;
      } else {
        throw new NullReferenceException(ArenaNotInitialised);
      }
    }

    public void InstructRobot(IRobot robot, RobotInstruction instruction)
    {
      if (_arena is null) {
        throw new NullReferenceException(ArenaNotInitialised);
      } else if (instruction is RobotInstruction.RotateLeft) {
        robot.RotateLeft();
      } else if (instruction is RobotInstruction.RotateRight) {
        robot.RotateRight();
      } else if (instruction is RobotInstruction.Move) {
        if (!_robotMoveValidationService.IsMoveWithinArenaBoundaries(_arena, robot)) {
          throw new InvalidOperationException("Unable to move to robot outside arena boundaries");
        } else if (!_robotMoveValidationService.IsMoveToEmptyCoordinate(_arena, robot)) {
          throw new InvalidOperationException("Unable to move to robot to occupied coordinates");
        } else {
          robot.Move();
        }
      }
    }

    public IEnumerable<IRobot> GetRobots()
    {
      if (_arena is not null) {
        return _arena.Robots;
      } else {
        throw new NullReferenceException(ArenaNotInitialised);
      }
    }

    public ITextInputParserService GetTextInputParserService()
    {
      return _textInputParserService;
    }
  }
}
