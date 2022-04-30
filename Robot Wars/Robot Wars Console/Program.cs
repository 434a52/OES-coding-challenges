using OES.RobotWars;
using OES.RobotWars.Enums;
using OES.RobotWars.Models;

Func<string, string> ReadLineWithDefault = (defaultValue) => {
  var input = Console.ReadLine();
  return string.IsNullOrEmpty(input) ? defaultValue : input;
};

Console.WriteLine("Welcome to OES Robot Wars");
Console.WriteLine();
var robotWarsService = RobotWars.CreateService();

if (robotWarsService is null) {
  Console.WriteLine("Unable to initialise the Robot Wars Service");
} else {
  var inputParser = robotWarsService.GetTextInputParserService();
  Console.WriteLine("Please provide the arena dimensions [5 5]");
  try {
    var arenaDimension = inputParser.ParseArenaDimension(ReadLineWithDefault("5 5"));
    if (arenaDimension is null) {
      Console.WriteLine("Unable to calculate the arena dimensions");
    } else {
      Console.WriteLine("Please provide the number of robots taking part [2]");
      if (!int.TryParse(ReadLineWithDefault("2"), out int robotCount)) {
        Console.WriteLine("Unable to calculate the number of robots taking part");
      } else {
        robotWarsService.SetArenaBoundaries(new Coordinate(0, 0), arenaDimension);
        for (int i = 0; i < robotCount; i++) {
          var defaultRobotPosition = robotCount == 2 ? i == 0 ? "1 2 N" : "3 3 E" : "";
          var defaultRobotInstructions = robotCount == 2 ? i == 0 ? "LMLMLMLMM" : "MMRMMRMRRM" : "";
          Console.WriteLine($"Please provide the starting position and orientation for robot {i + 1} [{defaultRobotPosition}]");
          (Coordinate coordinate, Orientation orientation) = inputParser.ParseRobotInitialPosition(ReadLineWithDefault(defaultRobotPosition));
          var robot = robotWarsService.CreateRobot(coordinate, orientation);
          if (robot is null) {
            throw new NullReferenceException($"Unable to create robot {i + 1}");
          } else {
            Console.WriteLine($"Please provide instructions for robot {i + 1} [{defaultRobotInstructions}]");
            var instructions = inputParser.ParseRobotInstructions(ReadLineWithDefault(defaultRobotInstructions));
            foreach (var instruction in instructions) {
              robotWarsService.InstructRobot(robot, instruction);
            }
          }
        }
        Console.WriteLine("Robots are currently at the following positions;");
        var robots = robotWarsService.GetRobots();
        if (robots.Any()) {
          Console.WriteLine($"{string.Join(Environment.NewLine, robots.Select(r => r.ToString()))}");
        } else {
          Console.WriteLine("No robots were placed in the arena");
        }
      }
    }
  } catch (Exception exception) {
    Console.WriteLine($"Robots Wars has encountered an error; {exception.Message}");
    Console.WriteLine();
    Console.WriteLine("Press any key to exit");
    Console.ReadKey(true);
    Environment.Exit(1);
  }
}

Console.WriteLine();
Console.WriteLine("Thankyou for attending OES Robot Wars, press any key");
Console.ReadKey(true);
Environment.Exit(0);
