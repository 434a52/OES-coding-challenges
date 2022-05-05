using OES.RobotWars.Enums;
using OES.RobotWars.Models;
using System.Collections.Generic;

namespace OES.RobotWars.Tests
{
  static internal class Specification
  {

    static internal class Input
    {
      static public string ArenaBoundaryString = "5 5";
      static public string Robot1InitialPositionString = "1 2 N";
      static public string Robot1InstructionsString = "LMLMLMLMM";
      static public string Robot2InitialPositionString = "3 3 E";
      static public string Robot2InstructionsString = "MMRMMRMRRM";

      static public Coordinate ArenaBoundary = new(5, 5);
      static public Coordinate Robot1InitialPosition = new(1, 2);
      static public Orientation Robot1InitialOrientation = Orientation.North;
      static public Coordinate Robot2InitialPosition = new(3, 3);
      static public Orientation Robot2InitialOrientation = Orientation.East;
      static public List<RobotInstruction> Robot1Instructions = new() {
        RobotInstruction.RotateLeft,
        RobotInstruction.Move,
        RobotInstruction.RotateLeft,
        RobotInstruction.Move,
        RobotInstruction.RotateLeft,
        RobotInstruction.Move,
        RobotInstruction.RotateLeft,
        RobotInstruction.Move,
        RobotInstruction.Move
      };
      static public List<RobotInstruction> Robot2Instructions = new() {
        RobotInstruction.Move,
        RobotInstruction.Move,
        RobotInstruction.RotateRight,
        RobotInstruction.Move,
        RobotInstruction.Move,
        RobotInstruction.RotateRight,
        RobotInstruction.Move,
        RobotInstruction.RotateRight,
        RobotInstruction.RotateRight,
        RobotInstruction.Move
      };
    }

    static internal class Output
    {
      static public string Robot1FinalPositionString = "1 3 N";
      static public string Robot2FinalPositionString = "5 1 E";
      static public Coordinate Robot1FinalPosition = new(1, 3);
      static public Orientation Robot1FinalOrientation = Orientation.North;
      static public Coordinate Robot2FinalPosition = new(5, 1);
      static public Orientation Robot2FinalOrientation = Orientation.East;

    }

  }
}
